# Flappy Bird 🐦

## Demo Video 🎬

## Steps 🔢 

* 2D URP Project
* Folders: Scripts, Art
* 2D Image Sprite: 
   * Mesh Type: Full Rect
   * Override for Android Max Size: 2048
   * Edit Sprites in Scene Editor
      * Background, Green Obstacles, Floor, Birds, 0, 1
* ➕Screen with aspect ratio 9 x 16
---
* Camera position -1 z
* ➕Background sprite
* Camera Size = Sprite Height / (2 * Sprite Pixel Per Unit ) = 255 / (2 * 100) = 1.275
* ➕ Player / Bird sprite
* ➕ Sorting Layers **in this order**: Background, Gameloop, UI 
   * Set Order Layer 
      * Background ➡️ Background 
      * Player ➡️ Gameloop
---
* Create animation clip called **Bird**
   * Enable Loop Time
   * Add it on Player GO
   * Open animation to add birds sprites with wing moving on different key frames(0, 5, 10 seconds)
* ➕ **Capsule Collider 2D** on Player
   * Set Direction ➡️ Horizontal 
   * Set Offset ➡️ 0 
   * Edit Collider Points
* ➕ Rigid Body **2D** on Player
   * Body Type ➡️ Dynamic
* ➕ Platform
   * Adjust Y Position
   * Set Sorting Layer ➡️ Gameloop
   * Set Draw Mode ➡️ Tiles
      * Set Width ➡️ 3
      * Adjust Position so that Platform is aligned with the left side of background
   * ➕ **2D Box Collider**
       * Edit
      
* ▶️ Play and check:
   * Bird Falls on Platform
   * Bird Has Flying Animation
---
* Create animation clip called **Tile Platform**
   * Enable Loop Time
   * Add it on Platform GO
   * Open animation to change transform position on different key frames 
     * Make duration from 0 - 180 (seconds)
     * Make first frame x = - of last frame x
     * Add some key frames in the middle (optional)
---
* Set Project Settings > Player 
   * Active Input Manager ➡️ Both
   * Scripting Backend ➡️IL2CPP 
   * IL2CPP information stack ➡️ Method Name, File Name, Line Number
* Create Player.cs 
   * Add it on Player GO
  ```c#
  using UnityEngine;

  public class Player : MonoBehaviour
  {
    public RigidBody2D rb;
    public float jumpForce = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rb.linearVelocity = Vector2.up * jumpForce;
        }
    }
  }
  ```
* ▶️ Play and check:
   * Bird Move Up on Mouse Click
* Add **Up Pipe** and **Down Pipe**
   * Add Box Collider 2D
* In Player Rigid Body
   * Set Collision Detection ➡️ Continuous
   * Interpolate ➡️ Interpolate
--- 
* Create Empty Parent **Obstacle Spawner** for Up Pipe and Down Pipe
   * Reset Transforms of 3 GO
   * Adjust X in Parent
   * Set Up Pipe Y Position ➡️ 1
   * Set Down Pipe Y Position ➡️ -1
   * Add Box Collider 2D on Up Pipe and Down Pipe
   * ➕ Component Obstacle.cs on Parent
  ```c# 
  using UnityEngine;

  public class Obstacle : MonoBehaviour
  {
    public float speed = 2.0f;

    void Start()
    {
        
    }


    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
  }
  ```
  * Create Prefab From Obstacle Spawner 
* ▶️ Play and check:
   * Check Pipes Move Left 
* Add these 2 lines in the Obstacle.cs
  ```c# 
  void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        // add these two lines
        if (transform.position.x < -1.5f)
        {
            Destroy(gameObject);
        }
    }
  ```
* ▶️ Play and check:
  * Pipes Gets Destroyed
* Remove **Obstacle Spawner**  from the scene
---
* Create Empty GO **Spawner**
  * Reset Position
  * Set X Position ➡️ 1
  * ➕ Component Script **Spawner.cs** 
    ```c# 
    using UnityEngine;

    public class Spawner : MonoBehaviour
    {
      [Header("Spawn Settings")]
      public GameObject obstaclePrefab;
      public float queueTime = 2f;
      public float heightOffset = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
      void Start()
      {
        
      }

    // Update is called once per frame
      void Update()
      {
          Vector3 position = transform.position + new Vector3(0, Random.Range(0, heightOffset), 0);
          GameObject go = Instantiate(obstaclePrefab, position, Quaternion.identity);
      }
    }
    ```
  * Set Obstacle Prefab ➡️Obstacle Spawner
    * In Obstacle Spawner Set Speed ➡️ 2
  * Set Queue Time ➡️ 5 
* ▶️ Play and check:
  * Many Pipes get Generated
* Modify **Spawner.cs** 
  ```c#
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
  ```
* ▶️ Play and check:
  * One Pipe Generated at a Time In Scene View
  * No Pipes in Game View
* In Obstacle Spawner Prefab 
  * Set Speed ➡️ 2 from inspector
  * Set Upper Pipe and Lower Pipe Storing Layer ➡️ Gameloop
* ▶️ Play and check:
  * Pipes Appear in Game View
---
* In Platform: 
  * Disable Animator
  * EditTile Width ➡️ 4
    * Edit Box Collider 2D
  * ➕ Component Script **ScrollingPlatform.cs**
  ```c#
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
  ```
* ▶️ Play and check:
   * Platform is moving without lag
   * Edit Scroll Speed ➡️ 0.5 on Scrolling Platform component
* In Player GO > RigidBody 2D > Constraints > 
  * Enable Freeze Rotation Z
---
* Create Empty Game Manager
  * ➕Component "GameManager.cs" 
   ```c#
  using UnityEngine;
  using TMPro;
  using UnityEngine.SceneManagement;

  public class GameManager : MonoBehaviour
  {
    public GameObject player;
    public GameObject startButton;
    public TextMeshProUGUI gameOverCounter;
    public float counterTimer = 5f;
    private bool isGameOver = false;
    void Start()
    {
        gameOverCounter.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (isGameOver)
        {
            counterTimer -= Time.unscaledDeltaTime;
            gameOverCounter.text = "Restarting in: " + Mathf.Ceil(counterTimer).ToString();
            if (counterTimer <= 0)
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name());
                SceneManager.LoadScene("GamePlay"); // Name of My Scene
                Time.timeScale = 1f;
            }
        }
    }

    public void StartGame()
    {
        startButton.SetActive(false);
        player.GetComponent<Player>().EnableBirdPhysics();
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverCounter.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("GamePlay"); // Name of My Scene
        Time.timeScale = 1f;
    }
  }
  ```

* ➕ UI > Canvas
  * Set Render Mode ➡️ Screen Space - Overlay
  * Set UI Scale Mode ➡️ Scale With Screen Size
  * ➕ UI > Button **"Start"**
    * Import TMP Essentials
    * Set Source Image to Play sprite ▶️
    * Set Width and Height ➡️ 250
    * Clear Text 
  * ➕ UI > Text **"Game Over Text"**
    * Set Width and Height ➡️ 400 x 250
    * Set Y Position ➡️ 200
    * Set Font Size ➡️ 50
    * Set Alignment ➡️ Center and Middle
    * Clear Text
* In Game Manager GO Assign:
  * Player ➡️ Player 
  * Start Button ➡️ Start
  * Game Over Counter ➡️ Game Over Text
* Edit Player.cs
   ```c#
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
        if (collision.gameObject.name == "Up Pipe"   ||
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
  ```

* ➕ Trigger 
  * Start > On Click > ➕
    * Set Runtime ➡️ Game Manager
    * Set Function ➡️ GameManager > StartGame ()
* ▶️ Playtest and check:
  * On Clicking Play Game Start
  * On Colliding Game Over Appear With a Restart Timer 
  * Adjust any values as needed like:
    * Speed in Obstacle Spwaner
    * Jump Force in Player
    * Pipes Y Position
    * Height Offest in Spawner
    * etc...

## We Are Done🎉🥳