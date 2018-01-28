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
    public Transform restArea;

    void Start()
    {
        if (!activeHalf)
        {
            this.transform.position = restArea.transform.position;
        }
        controller = GetComponent<Controller2D>();
        charRenderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gravity = -jumpHeight / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if (activeHalf)
            {
                Vector2 tempPos = this.transform.position;
                this.transform.position = restArea.transform.position;
                otherHalf.transform.position = tempPos;
            }
            this.activeHalf = !this.activeHalf;
        }
        if (!activeHalf)
        { 
            return;
        }
        charRenderer.enabled = activeHalf;

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

		if (controller.collisions.below) {
			this.animator.SetBool ("jumping", false);

		}

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
  			this.animator.SetBool ("jumping", true);
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
			if (Input.GetKey (KeyCode.Space)) {
				//falling;
				if (gender == Gender.Male) { // male
					//ground pound
					gravityToUse = specialFallGravity;
					this.animator.SetBool ("groundPound", true);
				} else { // isfemale
					//float down
					gravityToUse = specialFallGravity;
					moveSpeedToUse = specialFallMoveSpeed;
					this.animator.SetBool ("floating", true);
				}
			} else {
				this.animator.SetBool ("floating", false);
				this.animator.SetBool ("groundPound", false);
			}
        }
		if (!Input.GetKey (KeyCode.Space)) {
			this.animator.SetBool ("floating", false);
			this.animator.SetBool ("groundPound", false);
		}
        float targetVelocityX = input.x * moveSpeedToUse;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravityToUse * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        this.animator.SetFloat("velocity", Mathf.Abs(velocity.x * Time.deltaTime));
        this.spriteRenderer.flipX = velocity.x < -0.01;
    }
}
