using System.Collections.Generic;
using KSP.UI.Screens;

namespace KSPCrowdControl.Effects
{
    public interface IEffectBase
    {
        string GetName();
        string GetReadableName();
        int GetCost();
        void Execute();
    }

    public class ReverseGravityEffect : IEffectBase {
        public string GetName()
        {
            return "reverseGravity";
        }

        public string GetReadableName()
        {
            return "Reverse Gravity";
        }

        public int GetCost()
        {
            return 5000;
        }

        public void Execute()
        {
            CelestialBody b = FlightGlobals.currentMainBody;
            b.GeeASL *= -1; // there we go
        }
    }

    public class FuckTheRocketEffect : IEffectBase
    {
        public string GetName()
        {
            return "stage";
        }

        public string GetReadableName()
        {
            return "Stage";
        }

        public int GetCost()
        {
            return 3500;
        }

        public void Execute()
        {
            StageManager.ActivateNextStage();
        }
    }

    public class RandomExplosionEffect : IEffectBase
    {
        public string GetName()
        {
            return "randomExplosion";
        }

        public string GetReadableName()
        {
            return "Random Part Explosion";
        }

        public int GetCost()
        {
            return 3000;
        }

        public void Execute()
        {
            List<Part> parts = FlightGlobals.ActiveVessel.parts;
            int index = UnityEngine.Random.Range(1, parts.Count);
            parts[index].explode();
        }
    }

    public class TriggerLastStageEffect : IEffectBase
    {
        public string GetName()
        {
            return "triggerLast";
        }

        public string GetReadableName()
        {
            return "Trigger Last Stage In Staging Sequence";
        }

        public int GetCost()
        {
            return 25000;
        }

        public void Execute()
        {
            StageManager.ActivateStage(0);
        }
    }
}