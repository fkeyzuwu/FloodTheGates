using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreatureBattleUI : MonoBehaviour
{
    [SerializeField] private GameObject statsWindow;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI healthPerUnitText;
    [SerializeField] private TextMeshProUGUI attackPerUnitText;
    [SerializeField] private TextMeshProUGUI attackSpeedText;
    [SerializeField] private TextMeshProUGUI currentUnitHealthText;

    [SerializeField] private TextMeshProUGUI amountText;

    public void ToggleStatsWindow(bool toggle)
    {
        statsWindow.SetActive(toggle);
    }

    public void UpdateHealthText(int health)
    {
        healthText.text = "Health: " + health.ToString();
    }

    public void UpdateAttackText(int attack)
    {
        attackText.text = "Attack: " + attack.ToString();
    }

    public void UpdateHealthPerUnitText(int healthPerUnit)
    {
        healthPerUnitText.text = "Health (Per Unit): " + healthPerUnit.ToString();
    }

    public void UpdateAttackPerUnitText(int attackPerUnit)
    {
        attackPerUnitText.text = "Attack (Per Unit): " + attackPerUnit.ToString();
    }

    public void UpdateAttackSpeedText(float attackSpeed)
    {
        attackSpeedText.text = "Attack Speed: " + attackSpeed.ToString("0.00");
    }

    public void UpdateCurrentUnitHealthText(int currentUnitHealth, int healthPerUnit)
    {
        currentUnitHealthText.text = $"Current Unit Health: {currentUnitHealth} / {healthPerUnit}";
    }

    public void UpdateAmountText(int amount)
    {
        amountText.text = amount.ToString();
    }
}
