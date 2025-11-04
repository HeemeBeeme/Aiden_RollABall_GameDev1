using Unity.VisualScripting;
using UnityEngine;

public class GemColourChanger : MonoBehaviour
{
    public Light GemLight;
    public ParticleSystem ParticleSystem;

    public System.Random ColourPickRnD = new System.Random();

    void Start()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ParticleSystem.Play();
            gameObject.SetActive(false);
        }
    }

}
