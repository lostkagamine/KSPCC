using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using KSPCrowdControl.Effects;
using UnityEngine;
using WebSocketSharp;

/**
 * -- Kerbal Space Program: Crowd Control --
 *
 * - Component: Core
 *
 * (C) Rin, 2020
 *
 * Kerbal Space Program is property of Squad and Take-Two Interactive, 2011-2020.
 * I am not affiliated with the developers of Kerbal Space Program.
 */

namespace KSPCrowdControl
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class KerbalCrowdControl : MonoBehaviour
    {

        private int power = 0;
        private WebSocket ws;

        private string effectToRun = "";

        private IEffectBase[] effects;

        public void Start()
        {
            Debug.Log("[KSP Crowd Control] Starting up!");

            effects = Assembly.GetAssembly(typeof(IEffectBase)).GetTypes().Where(x => x.IsClass && x.IsSubclassOf(typeof(IEffectBase))).Select(Activator.CreateInstance) as IEffectBase[];
            
#if DEBUG
            var url = "ws://127.0.0.1:4327";
#else
            var url = "wss://kspws.kagamine.systems";
#endif

            ws = new WebSocket(url);
            ws.OnMessage += OnWebSocketMessage;

            ws.Connect();
            Debug.Log("[KSP Crowd Control] Connection to server established, sending payload.");

            var i = new HelloMessage
            {
                type = "hello"
            };
            var hello = StringSerializationAPI.Serialize(i.GetType(), i);
            Debug.Log("[KSP Crowd Control] " + hello);
            ws.SendAsync(hello, null);

            var p = new PowerMessage
            {
                type = "power",
                power = 0
            };
            var message = StringSerializationAPI.Serialize(p.GetType(), p);
            ws.SendAsync(message, null);

            InvokeRepeating("PowerTick", 0f, 0.2f);
        }

        public void OnDisable()
        {
            ws.Close();
        }

        public void PowerTick()
        {
            power += 15;

            var p = new PowerMessage
            {
                type = "power",
                power = power
            };
            var message = StringSerializationAPI.Serialize(p.GetType(), p);
            ws.SendAsync(message, null);
        }

        public void OnWebSocketMessage(object sender, MessageEventArgs e)
        {
            EffectMessage msg;
            try
            {
                msg = (EffectMessage) StringSerializationAPI.Deserialize(new EffectMessage().GetType(), e.Data);
            }
            catch (Exception)
            {
                Debug.Log("[KSP Crowd Control] Something went wrong while deserialising message, ignoring.");
                return;
            }

            Debug.Log("Deserialisation done");

            if (msg.effect == null)
            {
                return;
            }

            effectToRun = msg.effect;

            Invoke("RunEffect",0f);
        }

        public void RunEffect()
        {
            try
            {
                foreach (var fx in effects)
                {
                    if (fx.Name == effectToRun)
                    {
#if !DEBUG
                        if (fx.Name > power)
                        {
                            ScreenMessages.PostScreenMessage("Not enough power for effect " + fx.ReadableName, 3f);
                            break;
                        }
#endif

                        fx.Execute();
                        power -= fx.Cost;
                        OnEffectActivate(fx);
                        break;
                    }
                }
            }
            catch (Exception err)
            {
                Debug.Log($"[KSP Crowd Control] oops: {err}");
            }
        }

        public void OnEffectActivate(IEffectBase effect)
        {
            try
            {
                ScreenMessages.PostScreenMessage("Effect triggered: " + effect.ReadableName, 3f);
            }
            catch (Exception err)
            {
                Debug.Log($"[KSP Crowd Control] shit: {err}");
            }
        }

    }
}
