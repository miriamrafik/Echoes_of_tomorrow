using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float cameraSpeed = 5f;

    [Header("Camera Bounds")]
    public float minX, maxX;
    public float minY, maxY;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );

        Vector3 smoothPosition = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * cameraSpeed
        );

        float clampX = Mathf.Clamp(smoothPosition.x, minX, maxX);
        float clampY = Mathf.Clamp(smoothPosition.y, minY, maxY);

        transform.position = new Vector3(clampX, clampY, transform.position.z);
    }
}
