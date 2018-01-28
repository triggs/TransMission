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
    private float enemyWidth, enemyHeight;

    public float enemySpeed;
    public EnemyType EnemyType;


    //Chaser Varlables.
    public Transform raycastOrigin;
    private Vector2 direction;
    public float range;
    private Vector3 chaserInitialPosition;

    void Start()
    {
        enemyTransform = this.transform;
        enemyBody = this.GetComponent<Rigidbody2D>();
        var spriteRenderer = this.GetComponent<SpriteRenderer>();
        enemyWidth = spriteRenderer.bounds.extents.x;
        enemyHeight = spriteRenderer.bounds.extents.y;
        direction = new Vector2(enemySpeed, 0);
        chaserInitialPosition = enemyTransform.position;
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
        Debug.DrawLine(lineCastPosition, lineCastPosition + Vector2.down * 0.05f);
        bool isGrounded = Physics2D.Linecast(lineCastPosition, lineCastPosition + Vector2.down, enemyMask);

        //blocked?
        Debug.DrawLine(lineCastPosition, lineCastPosition - enemyTransform.right.toVector2() * 0.02f);
        bool isBlocked = Physics2D.Linecast(lineCastPosition, lineCastPosition - enemyTransform.right.toVector2() * 0.02f, enemyMask);

        if (!isGrounded || isBlocked)
        {
            //flip on y axis
            FlipYAxis();
        }
        Move();
    }

    private void FlipYAxis()
    {
        Vector3 currentRotation = enemyTransform.eulerAngles;
        currentRotation.y += 180;
        enemyTransform.eulerAngles = currentRotation;
        enemySpeed = enemySpeed * -1;
        direction = new Vector2(enemySpeed, 0);
    }

    public void Move()
    {
        Vector2 enemyVelocity = enemyBody.velocity;
        enemyVelocity.x = direction.x;
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
        //If you come to a ledge or a wall, stop. -- NTH
        //If you lose sight of the player, return to start position.
        Debug.DrawLine(raycastOrigin.position, raycastOrigin.position.toVector2() - enemyTransform.right.toVector2() * range);
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, direction, range);
        if (hit && hit.transform.tag == "Player")
        {
            //MoveTowardsPlayer();
            Move();
        }
        //else
        //{
        //    //MoveToInitPosition();
        //    Vector2 tempDirection = transform.position - chaserInitialPosition;
        //    if(tempDirection.x < chaserInitialPosition.x)
        //    {
        //        FlipYAxis();
        //    }
        //    else if (tempDirection.x > chaserInitialPosition.x)
        //    {
        //        FlipYAxis();
        //    }
        //    Move();

        //    //Vector2 enemyVelocity = enemyBody.velocity;
        //    //enemyVelocity.x = direction.x;
        //    //enemyBody.velocity = enemyVelocity;
        //}
    }
}