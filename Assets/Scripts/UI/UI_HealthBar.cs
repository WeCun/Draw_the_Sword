using System;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class UI_HealthBar : MonoBehaviour
{
    [SerializeField] private CharacterStats stats;
    private Slider slider;

    private void Start()
    {
        slider = GetComponentInChildren<Slider>();

        stats.onHealthChanged += UpdateHealthBar;
        
        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        slider.maxValue = stats.GetMaxHealth();
        slider.value = stats.currentHealth;
    }
}
