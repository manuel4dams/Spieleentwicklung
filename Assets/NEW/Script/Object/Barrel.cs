using GameGraph;
using JetBrains.Annotations;
using MyBox;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Barrel : Object, IHittable
    {
        [Header("Settings")] //
        public bool explosive;
        private bool healthSliderVisible;

        [Header("References")] //
        public GameObject objectGameObject;
        public Slider healthSlider;
        public GameObject explosionParticlePrefab;

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

            if(explosive)
                Instantiate(explosionParticlePrefab, objectTransform.position, objectTransform.rotation);

            Destroy(objectGameObject);
            SpawnContainedPickups();
        }

        private void ShowHealthSlider()
        {
            healthSlider.gameObject.SetActive(true);
            healthSliderVisible = true;
        }

        void OnDrawGizmosSelected()
        {
            if (explosive)
            {
                Gizmos.color = Color.red.WithAlphaSetTo(0.3f);
                Gizmos.DrawSphere(transform.position, explosionParticlePrefab.GetComponent<ExplosionState>().explosionRadius);
            }
        }
    }
}
