using System.Data.SqlTypes;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public PlayerController playerController;

    public int GemMultPrice = 20;
    public void GemMultBuy()
    {
        if (playerController.money >= GemMultPrice)
        {
            playerController.money -= GemMultPrice;
            playerController.moneyMuliplier++;
            GemMultPrice += GemMultPrice;
        }
    }
}
