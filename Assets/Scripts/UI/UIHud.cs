using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIHud : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    public Image healthBar;
    public Image energyBar;

    float maxHealth => playerController.playerHealthController.maxHealth;
    float currentHealth => playerController.playerHealthController.health;
    float armorCoefficient => playerController.playerHealthController.defenseK;

    

    void Start()
    {
        UpdateHealthBar();
        UpdateEnergyBar();
    }

    void Update()
    {
        UpdateHealthBar();
        UpdateEnergyBar();
    }

    void UpdateHealthBar()
    {
        if (armorCoefficient<1.2f)
        {
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color", new Color(0f, 255 / 256f, 69 / 256f));
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color_1", new Color(0f, 255 / 256f, 161 / 256f));
        }

        if ((armorCoefficient < 1.4f) && (armorCoefficient >= 1.2f))
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color_1", new Color(0f, 248 / 256f, 255 / 256f));

        if ((armorCoefficient < 1.6f) && (armorCoefficient >=  1.4f))
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color_1", new Color(0f, 121/256f, 255 / 256f));

        if ((armorCoefficient < 1.8f) && (armorCoefficient >= 1.6f))
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color_1", new Color(0f, 29 / 256f, 255 / 256f));

        if ((armorCoefficient < 2.0f) && (armorCoefficient >= 1.8f))
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color_1", new Color(164 / 256f, 0f, 255 / 256f));

        if  (armorCoefficient >= 2f)
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color_1", new Color(255 / 256f, 0f, 134 / 256f));

        /* if (armorCoefficient<1.2f)
        {
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color", new Color(0f, 255 / 256f, 69 / 256f));
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color_1", new Color(0f, 255 / 256f, 161 / 256f));
        }

        if ((armorCoefficient < 1.4f) && (armorCoefficient >= 1.2f))
        {
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color", new Color(0f, 255 / 256f, 161 / 256f));
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color_1", new Color(0f, 248 / 256f, 255 / 256f));
        }

        if ((armorCoefficient < 1.6f) && (armorCoefficient >=  1.4f))
        {
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color", new Color(0f, 248 / 256f, 255 / 256f));
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color_1", new Color(0f, 121 / 256f, 255 / 256f));
        }

        if ((armorCoefficient < 1.8f) && (armorCoefficient >= 1.6f))
        {
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color", new Color(0f, 121 / 256f, 255 / 256f));
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color_1", new Color(0f, 29 / 256f, 255 / 256f));
        }

        if ((armorCoefficient < 2.0f) && (armorCoefficient >= 1.8f))
        {
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color", new Color(0f, 29 / 256f, 255 / 256f));
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color_1", new Color(164 / 256f, 0f, 255 / 256f));
        }

        if  (armorCoefficient >= 2f)
        {
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color", new Color(164 / 256f, 0f, 255 / 256f));
            healthBar.canvasRenderer.GetMaterial().SetColor("_Color_1", new Color(255 / 256f, 0f, 134 / 256f));
        }



        float healthPercentage = currentHealth / maxHealth;*/
        

        float healthPercentage = currentHealth / maxHealth;
    }

    void UpdateEnergyBar()
    {
        if (playerController.playerHealthController.isHeartHas)
        {

            energyBar.canvasRenderer.GetMaterial().SetColor("_Color", new Color( 166/256f,236/256f,253/256f));
            energyBar.canvasRenderer.GetMaterial().SetColor("_Color_1", new Color(0f, 181 / 256f, 253 / 256f));

        }
        else
        {
            energyBar.canvasRenderer.GetMaterial().SetColor("_Color", new Color(253 / 256f, 170 / 256f, 166 / 256f));
            energyBar.canvasRenderer.GetMaterial().SetColor("_Color_1", new Color(253/256f, 0f, 0f));
        }
        energyBar.gameObject.SetActive(true);
    }
}