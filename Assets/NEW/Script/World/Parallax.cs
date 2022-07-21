using System;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

namespace ScriptGG
{
    public class Parallax : MonoBehaviour
    {
        public Transform parallaxReferencePoint;
        [NonReorderable] // Because unity sucks in drawing their UI, disabling reorder fixes a failure where the first entry overlaps some content
        public List<ParallaxLayer> layers;

        void Start()
        {
            foreach (var layer in layers)
            {
                var goL = Instantiate(layer.renderer.gameObject, transform);
                // goL.transform.position = goL.transform.position.OffsetX(-layer.renderer.bounds.size.x);
                layer.left = goL.transform;

                var goR = Instantiate(layer.renderer.gameObject, transform);
                // goR.transform.position = goR.transform.position.OffsetX(layer.renderer.bounds.size.x);
                layer.right = goR.transform;
            }
        }

        void Update()
        {
            foreach (var layer in layers)
            {
                var boundsX = layer.renderer.bounds.size.x;
                var refX = parallaxReferencePoint.position.x + layer.offsetX;
                var xInBounds = refX * layer.speed % boundsX;
                var posX = xInBounds
                           + Mathf.Floor((refX - xInBounds) / boundsX) * boundsX
                           + boundsX / 2f;
                layer.renderer.transform.position = layer.renderer.transform.position.SetX(posX);
                layer.left.position = layer.left.position.SetX(posX - boundsX);
                layer.right.position = layer.right.position.SetX(posX + boundsX);
            }
        }
    }

    [Serializable]
    public class ParallaxLayer
    {
        public Renderer renderer;
        public float speed;
        public float offsetX;
        internal Transform left;
        internal Transform right;
    }
}
