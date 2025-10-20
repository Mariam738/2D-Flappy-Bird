using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < -1.5f)
        {
            Destroy(gameObject);
        }
    }
}
