using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using Unity.Mathematics;
using System;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private bool enemyCanDamage = true;
    public bool IsPaused = false;

    private float TimePassed = 0;
    public float speed = 5;
    public float baseSpeed = 5;

    public int gemAmount = 0;
    public int maxHealth = 100;
    public int health = 100;
    public int level = 1;
    public int money = 0;

    public int moneyMuliplier = 1;
    public float RockMiningSpeed = 1;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI gemText;
    public TextMeshProUGUI levelText;

    public UnityEngine.UI.Slider HealthSlider;

    public Vector3 PlayerSpawnPoint;

    public GameObject RestartMenu;
    public GameObject PlayMenu;
    public GameObject winTextObject;
    public GameObject pauseTextObject;
    public GameObject unpauseBackgroundObject;

    public ParticleSystem PlayerParticles;

    public System.Random gemGainRnD = new System.Random();
    public System.Random SellGemRnD = new System.Random();


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerSpawnPoint = gameObject.transform.position;
    }

    private void Update()
    {
        SetHealthText();
        SetGemText();
        SetMoneyText();
        SetLevelText();

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
            //other.gameObject.SetActive(false);
            gemAmount += gemGainRnD.Next(1, 16);

            if(health < maxHealth)
            {
                health += 10;

                if(health > maxHealth)
                {
                    health = maxHealth;
                }
            }
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

    void SetGemText()
    {
        gemText.text = $"Gems: {gemAmount}";
    }

    void SetLevelText()
    {
        levelText.text = $"Level: {level}";
    }

    void SetMoneyText()
    {
        moneyText.text = $"$ {money}";
    }

    void SetHealthText()
    {
        healthText.text = $"Health: {health}/{maxHealth}";
        HealthSlider.value = health;
    }

    public void SellGems()
    {
        money += SellGemRnD.Next(gemAmount, gemAmount * moneyMuliplier);
        gemAmount = 0;
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
                speed *= 1.5f;
            }

            if (health <= 0)
            {
                health = 0;
                Destroy(gameObject);
                PlayMenu.SetActive(false);
                winTextObject.gameObject.SetActive(true);
                winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
                RestartMenu.SetActive(true);
            }

        }
    }

    public void PauseGame()
    {
        if(IsPaused == false)
        {//pause game
            Time.timeScale = 0;
            unpauseBackgroundObject.SetActive(true);
            pauseTextObject.SetActive(false);
            PlayMenu.SetActive(false);
            IsPaused = true;
        }
        else if(IsPaused == true)
        {//unpause game
            Time.timeScale = 1;
            unpauseBackgroundObject.SetActive(false);
            PlayMenu.SetActive(true);
            IsPaused = false;
        }
    }

}
