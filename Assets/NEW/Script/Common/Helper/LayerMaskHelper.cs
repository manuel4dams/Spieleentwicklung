using UnityEngine;

namespace ScriptGG.Helper
{
    public class LayerMaskHelper
    {
        public static bool LayerIsInMask(int layer, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}
