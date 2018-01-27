using UnityEngine;
using System.Collections;

public enum EnemyType
{
    LedgePatrol, // dumbly partols along a ledge until its blocked or at the edge, then turns around.
    Static,
    Chaser
}

public class EnemyController : MonoBehaviour
{
    public LayerMask enemyMask;
    private Rigidbody2D enemyBody;
    private Transform enemyTransform;

    public float enemySpeed;
    private float enemyWidth, enemyHeight;

    public EnemyType EnemyType;

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
        switch (EnemyType)
        {
            case EnemyType.LedgePatrol:
                LedgePatrolMoveBehaviour();
                break;
            case EnemyType.Static:
                StaticMoveBehaviour();
                break;
            case EnemyType.Chaser:
                ChaserMoveBehaviour();
                break;
            default:
                break;
        }
    }

    private void LedgePatrolMoveBehaviour()
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

    private void StaticMoveBehaviour()
    {
        //Dont move. just stand still.
    }

    private void ChaserMoveBehaviour()
    {
        //Stand Still
        //If you spot the player in front of you, chase him.
        //If you come to a ledge or a wall, stop.
        //If you lose sight of the player, return to start position.
    }
}