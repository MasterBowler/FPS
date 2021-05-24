using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float dodgeSpeed = 50f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool canDoubleJump = false;

    public float dodgeCooldown = 2f;
    public float dodgeDuration = 0.3f;
    float currentSpeed;
    bool canDodge = true;

    private void Start()
    {
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if(Input.GetKey(KeyCode.LeftShift) && canDodge)
        {
            currentSpeed = dodgeSpeed;
            StartCoroutine(ResetSpeed());
            StartCoroutine(ResetCooldown());
        }

        controller.Move(move * currentSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && (isGrounded || canDoubleJump))
        {
            canDoubleJump = !canDoubleJump;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        
        controller.Move(velocity * Time.deltaTime);
    }

    IEnumerator ResetCooldown()
    {
        canDodge = false;
        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true;
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(dodgeDuration);
        currentSpeed = speed;
    }
}
