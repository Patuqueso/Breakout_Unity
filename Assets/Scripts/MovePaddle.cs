using UnityEngine;
using UnityEngine.InputSystem;

public class MovePaddle : MonoBehaviour
{

    private InputAction moveAction;
    private Rigidbody2D rb;
    public Vector2 MoveInput;
    public float speed = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move", true);
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = MoveInput.normalized * speed;
    }
}