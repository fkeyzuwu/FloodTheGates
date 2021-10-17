using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class CreatureBattleUI : MonoBehaviour
{
    private Creature creature;

    [Header("Stats Window")]
    [SerializeField] private GameObject statsWindow;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI healthPerUnitText;
    [SerializeField] private TextMeshProUGUI attackPerUnitText;
    [SerializeField] private TextMeshProUGUI attackSpeedText;
    [SerializeField] private TextMeshProUGUI currentUnitHealthText;

    [Header("Amount")]
    [SerializeField] private TextMeshProUGUI amountText;

    [Header("Key")]
    [SerializeField] private GameObject keyTextObj;
    [SerializeField] private TextMeshProUGUI keyText;

    void Start()
    {
        creature = GetComponentInParent<Creature>();
        if(creature != null)
        {
            keyTextObj.SetActive(creature.hasAuthority);
        }
    }

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

    public void UpdateKeyCodeText(KeyCode keycode)
    {
        keyText.text = keycode.ToString();
    }
}
