using TMPro;
using UnityEditor.Overlays;
using UnityEngine;

public class RockMine : MonoBehaviour
{
    private bool playerCanMine = true;
    private float MineTime = 0;
    public int rockMineTime = 0;
    public int timeToMine = 3;

    public float shakeSpeed = 2f;
    public float shakeAmount = 2f;

    public TextMeshProUGUI mineTimeText;
    private Camera Camera;
    public ParticleSystem RockParticleSystem;

    Vector3 RockPosition;

    void Start()
    {
        Camera = FindAnyObjectByType<Camera>();
        RockPosition = gameObject.transform.position;
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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (playerCanMine)
            {
                playerCanMine = false;
                RockParticleSystem.Play();
            /*gameObject.transform.position = new Vector3((Mathf.Sin(Time.time * shakeSpeed) * shakeAmount), 0, 0);*/
                rockMineTime += 1;
            }


            mineTimeText.text = rockMineTime.ToString() + "s / 3s";

            if (rockMineTime == timeToMine)
            {
                gameObject.SetActive(false);
                rockMineTime = 0;
            }

            gameObject.transform.position = RockPosition;
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
