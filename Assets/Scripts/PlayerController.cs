using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private bool enemyCanDamage = true;
    private int score = 0;
    private float TimePassed = 0;
    public float speed = 5;
    public float baseSpeed = 5;
    public int health = 100;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI collectionText;
    public GameObject RestartButton;
    public GameObject winTextObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetCountText();
        SetHealthText();
    }

    private void Update()
    {
        if(enemyCanDamage == false)
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
            other.gameObject.SetActive(false);
            score++;
            SetCountText();
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
        countText.text = "Score: " + score.ToString();
        collectionText.text = $"Cubes: {score}/10";
        if (score >= 10)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            winTextObject.SetActive(true);
            RestartButton.SetActive(true);

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
            if (enemyCanDamage == true)
            {
                enemyCanDamage = false;
                health -= 25;
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
                RestartButton.SetActive(true);
            }

        }
    }

}
