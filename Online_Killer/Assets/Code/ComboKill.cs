using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboKill : MonoBehaviour
{
    [Header("Config")]
    public float comboDuration = 5f;   // Tiempo que dura el combo
    public int baseMultiplier = 1;     // Multiplicador inicial

    [Header("UI")]
    public Text comboText;
    public Text multiplierText;

    private float comboTimer;
    private int multiplier;
    private bool isActive;

    void Start()
    {
        ResetCombo();
    }

    void Update()
    {
        if (isActive)
        {
            comboTimer -= Time.deltaTime;

            if (comboTimer <= 0f)
            {
                ResetCombo();
            }
        }
    }

    public int AddKill()
    {
        if (!isActive)
        {
            isActive = true;
            multiplier = baseMultiplier;
        }

        multiplier++;                  // Subimos el multiplicador
        comboTimer = comboDuration;    // Reiniciamos el tiempo

        //UpdateUI();

        return multiplier;             // Devuelve el multiplicador actual
    }

    private void ResetCombo()
    {
        isActive = false;
        multiplier = baseMultiplier;
        comboTimer = 0;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (comboText != null)
            comboText.text = isActive ? $"Combo: {comboTimer:F1}s" : "Combo OFF";

        if (multiplierText != null)
            multiplierText.text = $"x{multiplier}";
    }
}
