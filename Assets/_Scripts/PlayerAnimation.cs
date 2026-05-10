using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        float speed = Mathf.Abs(x) + Mathf.Abs(z);
        bool isMoving = speed > 0.1f;
        bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);

        animator.SetFloat("Speed", speed);
        animator.SetBool("IsRunning", isRunning);

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");
        }
    }
}