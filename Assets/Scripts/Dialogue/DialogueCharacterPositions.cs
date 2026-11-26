using UnityEngine;

public class DialogueCharacterPositions : MonoBehaviour
{
    public Camera mainCamera;

    public Transform leftPosition;
    public Transform rightPosition;

    public float distanceFromCamera = 3f;

    private void LateUpdate()
    {
        PositionObject(leftPosition, 0.25f);
        PositionObject(rightPosition, 0.75f);
    }

    private void PositionObject(Transform target, float viewportX)
    {
        if (target == null) return;

        Vector3 position = mainCamera.ViewportToWorldPoint(
            new Vector3(viewportX, 0.5f, distanceFromCamera)
        );

        target.position = position;
        target.rotation = mainCamera.transform.rotation;
    }
}