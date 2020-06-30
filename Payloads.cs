using FullSerializer;
using UnityEngine;

namespace KSPCrowdControl
{

    public struct PowerMessage
    {
        public string type;

        public int power;
    }

    public struct EffectMessage
    {
        public string type;

        public string effect;
    }

    public struct HelloMessage
    {
        public string type;
    }

}