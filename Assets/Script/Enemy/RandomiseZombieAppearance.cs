using UnityEngine;

public class RandomiseZombieAppearance : MonoBehaviour
{
    public Material[] zombieMaterial;

    void Start()
    {
        var skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderer.material = zombieMaterial[Random.Range(0, zombieMaterial.Length)];
    }
}
