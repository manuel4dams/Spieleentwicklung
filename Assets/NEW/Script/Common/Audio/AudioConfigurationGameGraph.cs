using GameGraph;
using JetBrains.Annotations;

namespace Project
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AudioConfigurationGameGraph
    {
        public AudioConfiguration.Type type;
        public float loudnessFactor = 1f;
        public float volume => AudioConfiguration.GetVolumeForType(type, loudnessFactor);
    }
}
