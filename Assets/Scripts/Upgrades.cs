using System.Data.SqlTypes;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public PlayerController playerController;

    public int GemMultPrice = 20;
    public int RockMinePrice = 50;
    public int LevelUpPrice = 25;

    public void LevelUp()
    {
        if(playerController.gemAmount >= LevelUpPrice)
        {
            playerController.gemAmount -= LevelUpPrice;
            playerController.level++;
            LevelUpPrice += LevelUpPrice;
        }
    }
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
