using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEditor.Overlays;
using UnityEngine;

public class RockMine : MonoBehaviour
{
    private bool playerCanMine = true;
    private float MineTime = 0;
    public int rockMineTime = 0;
    public int timeToMine = 3;

    public float shakeMagnitude = 0.1f;
    public float shakeDuration = 100f;
    public float Duration = 0f;

    public TextMeshProUGUI mineTimeText;
    private Camera Camera;
    public ParticleSystem RockParticleSystem;

    public Vector3 InitialPosition;
    public Vector3 ShakePosition;

    void Start()
    {
        Camera = FindAnyObjectByType<Camera>();
        InitialPosition = transform.localPosition;
    }

    void Update()
    {
        mineTimeText.transform.rotation = Quaternion.LookRotation(mineTimeText.transform.position - Camera.transform.position).normalized;


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

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (playerCanMine)
            {
                playerCanMine = false;
                RockParticleSystem.Play();
                rockMineTime += 1;

                while(Duration < shakeDuration)
                {

                    float shakeX = UnityEngine.Random.value;
                    float shakeY = UnityEngine.Random.value;
                    ShakePosition = new Vector3(shakeX, 0, shakeY);

                    transform.localPosition = ShakePosition;

                    Duration += Time.deltaTime;

                    if(Duration >= 2)
                    {
                        Duration = 0;
                        break;
                    }
                }

                transform.localPosition = InitialPosition;
            }


            mineTimeText.text = rockMineTime.ToString() + "s / 3s";

            if (rockMineTime == timeToMine)
            {
                gameObject.SetActive(false);
                rockMineTime = 0;
            }

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            rockMineTime = 0;
        }
    }
}
