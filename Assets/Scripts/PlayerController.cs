using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private float movementX;
    private float movementY;

    private Volume GlobalVolume;
    private VolumeProfile profile;

    private bool enemyCanDamage = true;
    public bool IsPaused = false;
    public bool CanPause = true;

    private float TimePassed = 0;

    public float moveSpeed = 6;
    public float baseSpeed = 6;
    public float hitSpeed = 9;

    public int gemAmount = 0;
    public int gemPickUpAmount = 16;
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
    private Vector3 movementDirection;

    public GameObject RestartMenu;
    public GameObject PlayMenu;
    public GameObject winTextObject;
    public GameObject unpauseBackgroundObject;

    private Settings settings;

    private float VignetteStandard = 0.2f;
    private float VignetteDamaged = 0.35f;

    private CameraController cameraController;

    public ParticleSystem PlayerParticles;

    public System.Random gemGainRnD = new System.Random();
    public System.Random SellGemRnD = new System.Random();

    public AudioClip damagedClip;
    public AudioSource PlayerSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        settings = FindAnyObjectByType<Settings>();
        cameraController = FindAnyObjectByType<CameraController>();
        GlobalVolume = FindAnyObjectByType<Volume>();
        profile = GlobalVolume.sharedProfile;
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
                cameraController.shake = false;

                if (!profile.TryGet<Vignette>(out var vignette))
                {
                    vignette = profile.Add<Vignette>(false);
                }

                vignette.active = settings.VignetteToggle.isOn;
                vignette.color.Override(Color.black);
                vignette.intensity.Override(VignetteStandard);

                TimePassed = 0f;
                moveSpeed = baseSpeed;
                enemyCanDamage = true;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY) + movementDirection;

        rb.MovePosition(transform.position += (movement * moveSpeed) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
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
        HealthSlider.maxValue = maxHealth;
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
                PlayerSource.clip = damagedClip;
                PlayerSource.Play();
                PlayerParticles.Play();
                moveSpeed = hitSpeed;

                cameraController.shake = true;

                if (!profile.TryGet<Vignette>(out var vignette))
                {
                    vignette = profile.Add<Vignette>(false);
                }

                vignette.active = true;
                vignette.color.Override(Color.red);
                vignette.intensity.Override(VignetteDamaged);
            }

            if (health <= 0)
            {
                cameraController.shake = false;

                if (!profile.TryGet<Vignette>(out var vignette))
                {
                    vignette = profile.Add<Vignette>(false);
                }

                vignette.active = settings.VignetteToggle.isOn;
                vignette.color.Override(Color.black);
                vignette.intensity.Override(VignetteStandard);

                health = 0;
                Destroy(gameObject);
                PlayMenu.SetActive(false);
                winTextObject.gameObject.SetActive(true);
                RestartMenu.SetActive(true);
            }

        }
    }

    public void PauseGame()
    {
        if(!IsPaused && CanPause)
        {//pause game
            Time.timeScale = 0;
            unpauseBackgroundObject.SetActive(true);
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
