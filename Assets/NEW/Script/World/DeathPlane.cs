using UnityEngine;

namespace ScriptGG
{
    public class DeathPlane : MonoBehaviour
    {
        public bool excludeTriggers = true;

        void OnTriggerEnter(Collider other)
        {
            if (excludeTriggers && other.isTrigger)
                return;

            Destroy(other.gameObject);
        }
    }
}
