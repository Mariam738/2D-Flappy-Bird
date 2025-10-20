using UnityEngine;

public class ScrollingPlatform : MonoBehaviour
{
    public float scrollSpeed = 0.2f;
    public float restartPositionX = -1.25f;
    public float starX = 1.25f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        if (transform.position.x <= restartPositionX)
        {
            Vector3 newPosition = new Vector3(starX, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }
    }
}
