using UnityEngine;

namespace ScriptGG
{
    public class DeathPlane : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
        }
    }
}
