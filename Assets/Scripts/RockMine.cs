using TMPro;
using UnityEngine;

public class RockMine : MonoBehaviour
{
    private bool playerCanMine = true;
    private float MineTime = 0;
    public int rockMineTime = 0;
    public int timeToMine = 3;

    public TextMeshProUGUI mineTimeText;

    void Start()
    {
        
    }

    void Update()
    {

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
                //rock shake and some particles
                rockMineTime += 1;
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
