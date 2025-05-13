using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 2f;
    [SerializeField]private float dashSpeed = 10f;
    [SerializeField]private TrailRenderer myTrailRenderer;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;

    private bool isDashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (!isDashing)
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            if (!isDashing)
            {
                StartCoroutine(DashRoutine());
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);

        if(context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

    private void OnDash(InputAction.CallbackContext context) {
        if (context.performed && !isDashing)
        {
            StartCoroutine(DashRoutine());
        }
    }

    private IEnumerator DashRoutine() {
        isDashing = true;
        rb.linearVelocity = moveInput.normalized * dashSpeed;
        myTrailRenderer.emitting = true;
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(0.25f);
    }
}
