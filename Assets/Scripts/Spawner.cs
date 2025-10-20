using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject obstaclePrefab;
    public float queueTime = 2f;
    public float heightOffset = 1f;
    private float timer = 0f;   // added this
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() // modified this
    {
        timer += Time.deltaTime; 
        if (timer >= queueTime)
        {
            Vector3 position = transform.position + new Vector3(0, Random.Range(0, heightOffset), 0);
            GameObject go = Instantiate(obstaclePrefab, position, Quaternion.identity);
            timer = 0f; 
        }
    }
}
