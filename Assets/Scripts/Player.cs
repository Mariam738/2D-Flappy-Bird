using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce = 2.0f;
    // added these two lines
    public bool isDead = false; 
    GameManager GameManager; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // added these two lines
        rb.simulated = false; // Disable physics at start
        GameManager = Object.FindFirstObjectByType<GameManager>(); 
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rb.linearVelocity = Vector2.up * jumpForce;
        }
    }

    // Added these 2 method
    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Up Pipe" ||
            collision.gameObject.name == "Down Pipe" ||
            collision.gameObject.name == "Platform")
        {
            isDead = true;
            GameManager.GameOver();
        }
    }
    public void EnableBirdPhysics()
    {
        rb.simulated = true;
    }
}
