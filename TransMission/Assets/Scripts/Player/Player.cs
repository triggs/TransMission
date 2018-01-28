using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gender
{
    Male, Female
}

public class Player : MonoBehaviour
{

    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    public float moveSpeed = 6;

    float gravity;
    public float specialFallGravity;
    public float specialFallMoveSpeed;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    public GameObject otherHalf;
    public bool activeHalf;
    Renderer charRenderer;
    SpriteRenderer spriteRenderer;
    Controller2D controller;
    Animator animator;

    public bool attacking = false;
    public float attackTime = 0.333f;//333 millis
    public float attackTimer = 0;

    public Gender gender;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        charRenderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }

    void Update()
    {
        //		if (!activeHalf) {
        //			return;
        //		}
        charRenderer.enabled = activeHalf;

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        if (attacking)
        {
            if (this.attackTimer > 0)
            {
                this.attackTimer -= Time.deltaTime;
            }
            else
            {
                this.attacking = false;
                this.animator.SetBool("attacking", this.attacking);
            }
        }
        else
        {

            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                this.activeHalf = !this.activeHalf;
            }

            if (Input.GetKeyDown(KeyCode.E) && !this.attacking)
            {
                this.attacking = true;
                this.animator.SetBool("attacking", this.attacking);
                this.attackTimer = attackTime;
            }

        }
        
        float gravityToUse = gravity;
        float moveSpeedToUse = moveSpeed;
        if (velocity.y < 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //falling;
                if (gender == Gender.Male) // male
                {
                    //ground pound
                    gravityToUse = specialFallGravity;
                }
                else // isfemale
                {
                    //float down
                    gravityToUse = specialFallGravity;
                    moveSpeedToUse = specialFallMoveSpeed;
                }
            }
        }
        float targetVelocityX = input.x * moveSpeedToUse;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravityToUse * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        this.animator.SetFloat("velocity", Mathf.Abs(velocity.x * Time.deltaTime));
        this.spriteRenderer.flipX = velocity.x < -0.01;
    }
}
