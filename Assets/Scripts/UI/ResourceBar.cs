using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] resourceTextsArray;
    [SerializeField] public Dictionary<string, TextMeshProUGUI> resourceTexts = new Dictionary<string, TextMeshProUGUI>();

    private void Awake()
    {
        foreach(TextMeshProUGUI text in resourceTextsArray)
        {
            resourceTexts.Add(text.transform.parent.name, text);
        }
    }

    public void UpdateResourceUI(string name, int amount)
    {
        resourceTexts[name].text = amount.ToString();
    }
}
