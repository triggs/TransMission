using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public LayerMask enemyMask;
    private Rigidbody2D enemyBody;
    private Transform enemyTransform;

    public float enemySpeed;
    private float enemyWidth, enemyHeight;


    void Start()
    {
        enemyTransform = this.transform;
        enemyBody = this.GetComponent<Rigidbody2D>();
        var spriteRenderer = this.GetComponent<SpriteRenderer>();
        enemyWidth = spriteRenderer.bounds.extents.x;
        enemyHeight = spriteRenderer.bounds.extents.y;
    }

    void FixedUpdate()
    {
        //grounded?
        Vector2 lineCastPosition = enemyTransform.position.toVector2() - enemyTransform.right.toVector2() * enemyWidth + Vector2.up * enemyHeight;
        Debug.DrawLine(lineCastPosition, lineCastPosition + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCastPosition, lineCastPosition + Vector2.down, enemyMask);

        //blocked?
        Debug.DrawLine(lineCastPosition, lineCastPosition - enemyTransform.right.toVector2() * 0.02f);
        bool isBlocked = Physics2D.Linecast(lineCastPosition, lineCastPosition - enemyTransform.right.toVector2() * 0.02f, enemyMask);

        if (!isGrounded || isBlocked)
        {
            //flip on y axis
            Vector3 currentRotation = enemyTransform.eulerAngles;
            currentRotation.y += 180;
            enemyTransform.eulerAngles = currentRotation;
            enemySpeed = enemySpeed * -1;
        }

        //move
        Vector2 enemyVelocity = enemyBody.velocity;
        enemyVelocity.x = enemySpeed;
        enemyBody.velocity = enemyVelocity;
    }
}