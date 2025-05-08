using TMPro;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour
{
    public StatType statType;
    public string statName;
    public TextMeshProUGUI description;
    public TextMeshProUGUI statValueText;
    public TextMeshProUGUI statNameText;

    private void OnValidate()
    {
        gameObject.name = "Stat - " + statType.ToString();
        if(statNameText != null)
            statNameText.text = statName;
    }

    private void Start()
    {
        UpdateStat();
        statNameText.text = statName;
    }

    public void UpdateStat()
    {
        //将属性中的值显示出来
        statValueText.text = PlayerManager.instance.player.GetComponent<PlayerStats>().GetStatValue(statType).ToString();
    }
}
