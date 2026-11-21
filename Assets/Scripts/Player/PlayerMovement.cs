using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInputs playerInputs;

    public Rigidbody rb;

    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float rotationSpeed = 10f;

    public Transform playerTransform;

    public bool canMove = true;

    public void FlipCanMove(bool input)
    {
        canMove = input;
    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;
        
        Vector2 moveInput = playerInputs.moveInput;

        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        rb.linearVelocity = moveDirection * moveSpeed;

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            playerTransform.rotation = Quaternion.Slerp(
                playerTransform.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );
        }
    }
}