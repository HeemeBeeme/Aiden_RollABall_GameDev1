using System;
using System.Collections;
using System.Threading;
using TMPro;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class RockMine : MonoBehaviour
{
    private bool shake = false;

    private float shakeMagnitude = 0.1f;

    public float rockMineTime = 0;
    public int timeToMine = 3;
    public float rockMiningSpeed = 1;

    public float audioTime = 0;

    public TextMeshProUGUI mineTimeText;
    private Camera Camera;
    public ParticleSystem RockParticleSystem;
    public GameObject RockMesh;
    public GameObject Gem;
    public PlayerController playerController;
    private AudioSource PlayerSource;
    public AudioClip MiningClip;

    public Vector3 InitialPosition;
    public Vector3 ShakePosition;

    void Start()
    {
        Camera = FindAnyObjectByType<Camera>();
        InitialPosition = RockMesh.transform.localPosition;
        playerController = FindAnyObjectByType<PlayerController>();
        PlayerSource = playerController.GetComponent<AudioSource>();
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

            mineTimeText.text = rockMineTimeRound.ToString() + $"s / {timeToMine}s";

            PlayMiningAudio();
            audioTime += Time.deltaTime;

            if (audioTime >= MiningClip.length)
            {
                audioTime = 0;
            }

            if (rockMineTime >= timeToMine)
            {
                PlayerSource.Stop();
                gameObject.SetActive(false);
                RockMesh.SetActive(false);
                Gem.SetActive(true);
            }
        }
    }

    private void PlayMiningAudio()
    {
        if(audioTime == 0)
        {
            PlayerSource.clip = MiningClip;
            PlayerSource.Play();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shake = false;
            PlayerSource.Stop();
            PlayerSource.clip = null;
            audioTime = 0;
        }
    }
}
