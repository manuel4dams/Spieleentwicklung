using UnityEditor;
using UnityEngine;

namespace ScriptGG.Editor
{
    public class SnapTopGround : MonoBehaviour
    {
        [MenuItem("My/Snap Tp Ground %g")]
        public static void Do()
        {
            foreach (var t in Selection.transforms)
            {
                var hits = Physics.RaycastAll(t.position + Vector3.up, Vector3.down, 10f);
                foreach (var hit in hits)
                {
                    if(hit.collider.gameObject == t.gameObject)
                        continue;

                    var newPosition = hit.point;
                    
                    var boxCollider = t.GetComponent<BoxCollider>();
                    if (boxCollider != null)
                        newPosition.y +=boxCollider.bounds.extents.y;
                    
                    t.position = newPosition;
                    break;
                }
            }
        }
    }
}
