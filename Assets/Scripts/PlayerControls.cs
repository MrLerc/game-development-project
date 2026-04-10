using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControls : MonoBehaviour, ControlsMain.IPlayerActions
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Grab Settings")]
    [SerializeField] private float followSpeed = 15f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded = true;

    private Camera mainCamera;
    private Vector2 mouseWorldPos;

    private Rigidbody2D grabbedRb;
    private bool isHolding = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        // Player movement
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        // Move grabbed object
        if (isHolding && grabbedRb != null)
        {
            Vector2 targetPos = mouseWorldPos;

            Vector2 newPos = Vector2.Lerp(
                grabbedRb.position,
                targetPos,
                followSpeed * Time.fixedDeltaTime
            );

            grabbedRb.MovePosition(newPos);
        }
    }

    // ---------------- INPUT ----------------

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        mouseWorldPos = mainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            TryGrab();
        }

        if (context.canceled)
        {
            Release();
        }
    }

    // ---------------- GRAB ----------------

    private void TryGrab()
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Grabbable"))
        {
            grabbedRb = hit.collider.GetComponent<Rigidbody2D>();

            if (grabbedRb != null)
            {
                isHolding = true;
            }
        }
    }

    private void Release()
    {
        grabbedRb = null;
        isHolding = false;
    }

    // ---------------- COLLISION ----------------

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Grabbable"))
        {
            isGrounded = true;
        }
    }
}