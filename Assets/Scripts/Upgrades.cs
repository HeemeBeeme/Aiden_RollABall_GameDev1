using System.Data.SqlTypes;
using TMPro;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public PlayerController playerController;

    public TextMeshProUGUI SellMultPriceTxt;
    public TextMeshProUGUI RockMineTimePriceTxt;
    public TextMeshProUGUI LevelUpPriceTxt;

    public int SellMultPrice = 20;
    public int RockMineTimePrice = 50;
    public int LevelUpPrice = 25;

    private void Start()
    {
        SellMultPriceTxt.text = $"$ {SellMultPrice}";
        RockMineTimePriceTxt.text = $"$ {RockMineTimePrice}";
        LevelUpPriceTxt.text = $"$ {LevelUpPrice}";
    }

    public void SellMultTextUpdate()
    {
        SellMultPriceTxt.text = $"$ {SellMultPrice}";
    }

    public void RockMineTimeTextUpdate()
    {
        RockMineTimePriceTxt.text = $"$ {RockMineTimePrice}";
    }

    public void LevelUpTextUpdate()
    {
        LevelUpPriceTxt.text = $"Gems: {LevelUpPrice}";
    }

    public void LevelUp()
    {
        if(playerController.gemAmount >= LevelUpPrice)
        {
            playerController.gemAmount -= LevelUpPrice;
            playerController.level++;
            LevelUpPrice += LevelUpPrice;
            LevelUpTextUpdate();
        }
    }
    public void SellMultBuy()
    {
        if (playerController.money >= SellMultPrice)
        {
            playerController.money -= SellMultPrice;
            playerController.moneyMuliplier++;
            SellMultPrice += SellMultPrice;
            SellMultTextUpdate();
        }
    }

    public void RockMineTimeBuy()
    {
        if(playerController.money >= RockMineTimePrice)
        {
            playerController.money -= RockMineTimePrice;
            playerController.RockMiningSpeed += 0.5f;
            RockMineTimePrice += RockMineTimePrice;
            RockMineTimeTextUpdate();
        }
    }
}
