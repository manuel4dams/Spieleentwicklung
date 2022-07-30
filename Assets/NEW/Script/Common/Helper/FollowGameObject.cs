using GameGraph.Common.Blocks;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class FollowGameObject : MonoBehaviour
{
    public Transform target;
    public bool followPosition;
    public Vector3 positionOffset;
    public float positionLerp = 1f;
    public bool followRotation;
    public Vector3 rotationEulerOffset;
    public float rotationLerp = 1f;

    public Updater.UpdaterType updaterType;

    void Update()
    {
        if (updaterType == Updater.UpdaterType.Update)
            Follow();
    }

    void LateUpdate()
    {
        if (updaterType == Updater.UpdaterType.LateUpdate)
            Follow();
    }

    private void Follow()
    {
        if (followPosition)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                target.position + positionOffset,
                positionLerp);
        }

        if (followRotation)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.Euler(target.eulerAngles + rotationEulerOffset), 
                rotationLerp);
        }
    }
}
