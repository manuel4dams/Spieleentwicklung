using System;
using JetBrains.Annotations;
using UnityEngine.UI;

namespace GameGraph.Common.Blocks
{
    // TODO In the previous version UIElements was used, but this is the package UI, not UIElements
    [GameGraph("Common/UI")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class SliderSetValueNew
    {
        // Output
        public event Action valueSet;

        // Properties
        public Slider slider { private get; set; }
        public float value;

        public void SetValue()
        {
            slider.value = value;
            valueSet?.Invoke();
        }
    }
}
