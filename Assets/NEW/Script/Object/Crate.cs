using GameGraph;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Crate : Object, IHittable
    {
        [Header("Settings")] //
        private bool healthSliderVisible;

        [Header("References")] //
        public GameObject objectGameObject;
        public Slider healthSlider;

        public void Start()
        {
            healthSlider.maxValue = healthAmount;
        }

        public void OnHit(float damage)
        {
            if (hitSound)
                PlayHitSound();

            if (!destroyable) return;
            healthAmount--;
            healthSlider.value = healthAmount;

            if (!healthSliderVisible)
                ShowHealthSlider();

            if (healthAmount > 0f) return;
            Destroy(objectGameObject);
            SpawnContainedPickups();
        }

        private void ShowHealthSlider()
        {
            healthSlider.gameObject.SetActive(true);
            healthSliderVisible = true;
        }
    }
}
