using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Bullet bullet;
    public GameManager gameManager;
    public float thrustForce = 1.0f;
    public float turnForce = 1.0f;
    private Rigidbody2D rb;
    private bool thrusting;
    private float turnDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        thrusting = Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            turnDirection = 1.0f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            turnDirection = -1.0f;
        }
        else
        {
            turnDirection = 0.0f;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (thrusting)
        {
            rb.AddForce(transform.up * thrustForce);
        }
        if (turnDirection != 0.0f)
        {
            rb.AddTorque(turnDirection * turnForce);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bullet, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            this.gameManager.PlayerDied();
        }
    }
}
