using Unity.VisualScripting;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public Light GemLight;
    public ParticleSystem ParticleSystem;

    public PlayerController playerController;
    private AudioSource PlayerSource;
    public AudioClip PickUpClip;
    public int PickUpAmount;

    public System.Random ColourPickRnD = new System.Random();

    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        PlayerSource = playerController.GetComponent<AudioSource>();
        var MainSystem = ParticleSystem.main;

        switch (ColourPickRnD.Next(1, 5))
        {
            case 1:
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                GemLight.color = Color.red;
                MainSystem.startColor = Color.red;
            break;

            case 2:
                gameObject.GetComponent<Renderer>().material.color = Color.green;
                GemLight.color = Color.green;
                MainSystem.startColor = Color.green;
                break;

            case 3:
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                GemLight.color = Color.blue;
                MainSystem.startColor = Color.blue;
                break;

            case 4:
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                GemLight.color = Color.yellow;
                MainSystem.startColor = Color.yellow;
                break;
        }

    }

    private void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ParticleSystem.Play();

            PlayerSource.clip = PickUpClip;
            PlayerSource.Play();

            playerController.gemPickUpAmount = PickUpAmount;
            playerController.gemAmount += playerController.gemGainRnD.Next(1, playerController.gemPickUpAmount);
            if (playerController.health < playerController.maxHealth)
            {
                playerController.health += 10;

                if (playerController.health > playerController.maxHealth)
                {
                    playerController.health = playerController.maxHealth;
                }
            }

            gameObject.SetActive(false);

        }
    }
}
