using UnityEngine;

public class RandomiseZombieAppearance : MonoBehaviour
{
    public Material[] zombieMaterial;

    // Start is called before the first frame update
    void Start()
    {
        var skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderer.material = zombieMaterial[Random.Range(0, zombieMaterial.Length)];
    }
}
