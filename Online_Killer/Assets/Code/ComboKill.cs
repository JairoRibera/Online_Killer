using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

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
    private Shoot shoot_Ref;
    void Start()
    {
        shoot_Ref = GetComponent<Shoot>();
        ResetCombo();
    }

    void Update()
    {
        if (shoot_Ref.comboActivado == true)
        {
            comboTimer -= Time.deltaTime;

            if (comboTimer <= 0f)
            {
                ResetCombo();
            }
        }
        switch (shoot_Ref.EnemigosEliminados)
        {
            //Uso de patrones relacionales (case >= 6 and < 8) — esto es C# 9+, funciona en Unity 2021 o posterior.
            //Ya no necesitas hacer un if dentro de cada case.
            //El multiplicador sube automáticamente según los enemigos eliminados.
            case >= 4 and < 6:
                multiplier = 1;
                break;

            case >= 6 and < 8:
                multiplier = 2;
                break;

            case >= 8 and < 10:
                multiplier = 3;
                break;

            case >= 10:
                multiplier = 4;
                break;

            default:
                multiplier = 0;
                break;
        }
        UpdateUI();
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
        shoot_Ref.comboActivado = false;
        multiplier = baseMultiplier;
        comboTimer = 0;
        shoot_Ref.EnemigosEliminados = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (comboText != null)
            //comboText.text = isActive ? $"Combo: {comboTimer:F1}s" : "Combo OFF";

        if (multiplierText != null)
            //multiplierText.text = $"x{multiplier}";
            multiplierText.text = $"Combo x {multiplier}";

    }
    public void FailShot()
    {
        ResetCombo();
    }
}
