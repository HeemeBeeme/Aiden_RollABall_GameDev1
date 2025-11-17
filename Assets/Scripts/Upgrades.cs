using System.Data.SqlTypes;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public PlayerController playerController;
    public RockMine rockMine;

    public int GemMultPrice = 20;
    public int RockMinePrice = 50;
    public void GemMultBuy()
    {
        if (playerController.money >= GemMultPrice)
        {
            playerController.money -= GemMultPrice;
            playerController.moneyMuliplier++;
            GemMultPrice += GemMultPrice;
        }
    }

    public void RockMineBuy()
    {
        if(playerController.money >= RockMinePrice)
        {
            playerController.money -= RockMinePrice;
            playerController.RockMiningSpeed += 0.5f;
            RockMinePrice += RockMinePrice;
        }
    }
}
