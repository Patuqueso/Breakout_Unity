using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocityBeforeCollision;
    // public static event System.Action<Player> OnGoalScored;

    private float speed;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBall();
    }

    private void ResetBall()
    {
        rb.position = Vector2.zero;
        rb.linearVelocity = Vector2.zero;

        Vector2 direction = Vector2.down;

        rb.linearVelocity = direction * speed;
    }

    private void FixedUpdate()
    {
        // Save the incoming velocity before Unity resolves a collision.
        velocityBeforeCollision = rb.linearVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            float offset = (rb.position.x - other.transform.position.x) / other.collider.bounds.size.x;

            Debug.Log("Offset: " + offset);

            Vector2 dir = new Vector2(
            offset,
            Mathf.Sign(velocityBeforeCollision.y)
            ).normalized;

            dir.y *= -1f;

            dir.x = Mathf.Sign(dir.x) *
                    Mathf.Max(Mathf.Abs(dir.x), 0.3f);

            rb.linearVelocity = dir.normalized * speed;
        }
        else if (other.gameObject.CompareTag("Brick") || other.gameObject.CompareTag("Wall"))
        {
            Vector2 normal = other.GetContact(0).normal;

            Vector2 reflectedDirection =
                Vector2.Reflect(velocityBeforeCollision.normalized, normal);

            rb.linearVelocity = reflectedDirection * speed;

            if (other.gameObject.CompareTag("Brick"))
            {
                other.gameObject.GetComponent<Brick>()?.DestroyBrick();
            }
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Goal_1"))
    //     {
    //         OnGoalScored?.Invoke(Player.Left);
    //         ResetBall();
    //     }
    //     else
    //     {
    //         OnGoalScored?.Invoke(Player.Right);
    //         ResetBall();
    //     }
    // }


}