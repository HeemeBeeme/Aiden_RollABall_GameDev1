using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private bool enemyCanDamage = true;
    private bool playerCanMine = true;
    public bool IsPaused = false;
    private int score = 0;
    private float TimePassed = 0;
    private float MineTime = 0;
    public float speed = 5;
    public float baseSpeed = 5;
    public int health = 100;
    private int PickUpNum = 0;
    public int rockMineTime = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI collectionText;
    public Vector3 PlayerSpawnPoint;
    public GameObject RestartMenu;
    public GameObject winTextObject;
    public GameObject pauseTextObject;
    public GameObject unpauseTextObject;
    public GameObject unpauseBackgroundObject;
    public ParticleSystem PlayerParticles;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerSpawnPoint = gameObject.transform.position;
        PickUpNum = GameObject.FindGameObjectsWithTag("PickUp").Length;
        SetCountText();
        SetHealthText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (enemyCanDamage == false)
        {
            TimePassed += Time.deltaTime;

            if (TimePassed >= 1f)
            {
                TimePassed = 0f;
                speed = baseSpeed;
                enemyCanDamage = true;
            }
        }

        if (playerCanMine == false)
        {
            MineTime += Time.deltaTime;

            if (MineTime >= 1f)
            {
                MineTime = 0f;
                playerCanMine = true;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            score++;
            SetCountText();
        }

        if (other.gameObject.CompareTag("KillZone"))
        {
            gameObject.transform.position = PlayerSpawnPoint;
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = $"Score: {score}";
        collectionText.text = $"Gems: {score}/{PickUpNum}";
        if (score >= PickUpNum)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            winTextObject.SetActive(true);
            RestartMenu.SetActive(true);

        }
    }

    void SetHealthText()
    {
        healthText.text = $"Health: {health}";
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (enemyCanDamage)
            {
                enemyCanDamage = false;
                health -= 25;
                PlayerParticles.Play();
                SetHealthText();
                speed *= 1.5f;
            }

            if (health <= 0)
            {
                health = 0;
                SetHealthText();
                Destroy(gameObject);
                winTextObject.gameObject.SetActive(true);
                winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
                RestartMenu.SetActive(true);
            }

        }

        if (collision.gameObject.CompareTag("Rock"))
        {
            if (playerCanMine)
            {
                playerCanMine = false;
                //rock shake and some particles
                rockMineTime += 1;
            }

            if(rockMineTime == 5)
            {
                collision.gameObject.SetActive(false);
                rockMineTime = 0;
            }
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Rock"))
        {
            rockMineTime = 0;
        }
    }

    public void PauseGame()
    {
        if(IsPaused == false)
        {
            Time.timeScale = 0;
            unpauseBackgroundObject.SetActive(true);
            unpauseTextObject.SetActive(true);
            pauseTextObject.SetActive(false);
            IsPaused = true;
        }
        else if(IsPaused == true)
        {
            Time.timeScale = 1;
            unpauseBackgroundObject.SetActive(false);
            unpauseTextObject.SetActive(false);
            pauseTextObject.SetActive(true);
            IsPaused = false;
        }
    }

}
