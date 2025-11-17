using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class RockMine : MonoBehaviour
{
    private bool shake = false;

    private float shakeMagnitude = 0.1f;

    public float rockMineTime = 0;
    public int timeToMine = 3;
    public float rockMiningSpeed = 1;

    public TextMeshProUGUI mineTimeText;
    private Camera Camera;
    public ParticleSystem RockParticleSystem;
    public GameObject RockMesh;
    public GameObject Gem;
    public PlayerController playerController;

    public Vector3 InitialPosition;
    public Vector3 ShakePosition;

    void Start()
    {
        Camera = FindAnyObjectByType<Camera>();
        InitialPosition = RockMesh.transform.localPosition;
        playerController = FindAnyObjectByType<PlayerController>();
    }

    void Update()
    {
        rockMiningSpeed = playerController.RockMiningSpeed;

        mineTimeText.transform.rotation = Quaternion.LookRotation(mineTimeText.transform.position - Camera.transform.position).normalized;

        if (shake == true)
        {
            ShakePosition = UnityEngine.Random.insideUnitSphere * shakeMagnitude;
           RockMesh.transform.localPosition = ShakePosition;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RockParticleSystem.Play();
            rockMineTime += (rockMiningSpeed * Time.deltaTime);
            shake = true;

            double rockMineTimeRound = Math.Round(rockMineTime, 2);

            mineTimeText.text = rockMineTimeRound.ToString() + "s / 3s";

            if (rockMineTime >= timeToMine)
            {
                gameObject.SetActive(false);
                RockMesh.SetActive(false);
                Gem.SetActive(true);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shake = false;
        }
    }
}
