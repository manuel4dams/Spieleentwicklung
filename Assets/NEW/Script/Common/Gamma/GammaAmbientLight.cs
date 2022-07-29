using Project;
using UnityEngine;

namespace ScriptGG
{
    public class GammaAmbientLight : MonoBehaviour
    {
        void Update()
        {
            var gamma = Prefs.Gamma.GetFloat();
            RenderSettings.ambientLight = new Color(gamma, gamma, gamma, 1.0f);
        }
    }
}
