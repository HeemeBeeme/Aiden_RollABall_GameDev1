using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEditor.Overlays;
using UnityEngine;

public class RockMine : MonoBehaviour
{
    private bool playerCanMine = true;
    private bool shake = false;

    private float MineTime = 0;
    private float shakeMagnitude = 0.1f;

    public int rockMineTime = 0;
    public int timeToMine = 3;

    public TextMeshProUGUI mineTimeText;
    private Camera Camera;
    public ParticleSystem RockParticleSystem;
    public GameObject RockMesh;
    public GameObject Gem;

    public Vector3 InitialPosition;
    public Vector3 ShakePosition;

    void Start()
    {
        Camera = FindAnyObjectByType<Camera>();
        InitialPosition = RockMesh.transform.localPosition;
    }

    void Update()
    {
        mineTimeText.transform.rotation = Quaternion.LookRotation(mineTimeText.transform.position - Camera.transform.position).normalized;

        if (shake == true)
        {
            ShakePosition = UnityEngine.Random.insideUnitSphere * shakeMagnitude;
           RockMesh.transform.localPosition = ShakePosition;
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

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (playerCanMine)
            {
                playerCanMine = false;
                RockParticleSystem.Play();
                rockMineTime += 1;

                shake = true;
            }


            mineTimeText.text = rockMineTime.ToString() + "s / 3s";

            if (rockMineTime == timeToMine)
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
