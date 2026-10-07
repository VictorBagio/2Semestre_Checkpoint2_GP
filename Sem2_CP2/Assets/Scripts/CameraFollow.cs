using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Smooth")]
    public float smoothSpeed = 5f;

    [Header("Offset")]
    public Vector3 offset = new Vector3(0f, 1f, -10f);

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}