using System.Collections.Generic;
using KSP.UI.Screens;

namespace KSPCrowdControl.Effects
{
    public interface IEffectBase
    {
        string Name { get; }
        string ReadableName { get; }
        int Cost { get; }
        
        void Execute();
    }

    public class ReverseGravityEffect : IEffectBase {
        public string Name => "reverseGravity";
        public string ReadableName => "Reverse Gravity";
        public int Cost => 5000;

        public void Execute()
        {
            CelestialBody b = FlightGlobals.currentMainBody;
            b.GeeASL *= -1; // there we go
        }
    }

    public class FuckTheRocketEffect : IEffectBase
    {
        public string Name => "stage";
        public string ReadableName => "Stage";
        public int Cost => 3500;
        
        public void Execute()
        {
            StageManager.ActivateNextStage();
        }
    }

    public class RandomExplosionEffect : IEffectBase
    {
        public string Name => "randomExplosion";
        public string ReadableName => "Random Part Explosion";
        public int Cost => 3000;

        public void Execute()
        {
            List<Part> parts = FlightGlobals.ActiveVessel.parts;
            int index = UnityEngine.Random.Range(1, parts.Count);
            parts[index].explode();
        }
    }

    public class TriggerLastStageEffect : IEffectBase {
        public string Name => "triggerLast";
        public string ReadableName => "Trigger Last Stage In Staging Sequence";
        public int Cost => 25000;

        public void Execute()
        {
            StageManager.ActivateStage(0);
        }
    }
}