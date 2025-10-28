using Unity.VisualScripting;
using UnityEngine;

public class GemColourChanger : MonoBehaviour
{
    public Light GemLight;
    public System.Random ColourPickRnD = new System.Random();

    void Start()
    {
        switch (ColourPickRnD.Next(1, 5))
        {
            case 1:
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                GemLight.color = Color.red;
            break;

            case 2:
                gameObject.GetComponent<Renderer>().material.color = Color.green;
                GemLight.color = Color.green;
                break;

            case 3:
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                GemLight.color = Color.blue;
                break;

            case 4:
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                GemLight.color = Color.yellow;
                break;
        }
    }

}
