using Project;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ScriptGG
{
    public class GammaPostProcessing : MonoBehaviour
    {
        public PostProcessVolume volume;

        public void Update()
        {
            volume.profile.TryGetSettings<ColorGrading>(out var colorGrading);
            // colorGrading.gamma.overrideState = true;
            colorGrading.gamma.value = new Vector4(1f, 1f, 1f, Prefs.Gamma.GetFloat());
        }
    }
}
