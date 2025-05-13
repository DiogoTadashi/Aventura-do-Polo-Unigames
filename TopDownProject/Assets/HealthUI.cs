using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public int maxLives = 5;
    public int currentLives;

    // Referência à imagem da barra de vida no Canvas
    public Image healthBarImage;
    public Sprite[] healthBarSprites;

    public static event Action OnPlayerDied;

    void Start()
    {
        ResetHealth();
        GameController.OnReset += ResetHealth;
    }

    void OnDestroy()
    {
        GameController.OnReset -= ResetHealth;
    }

// testando a barra de vida
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(1);
        }
    }

    void ResetHealth()
    {
        currentLives = maxLives;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentLives -= damage;
        currentLives = Mathf.Clamp(currentLives, 0, maxLives);
        UpdateHealthBar();
        if (currentLives <= 0)
        {
            if (OnPlayerDied != null)
            {
                OnPlayerDied.Invoke();
            }
        }
    }

    void UpdateHealthBar()
    {
        int spriteIndex = maxLives - currentLives;

        if (spriteIndex >= 0 && spriteIndex < healthBarSprites.Length)
        {
            healthBarImage.sprite = healthBarSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Index fora dos limites do array healthBarSprites: " + spriteIndex);
        }
    }

    public void Heal(int amount)
    {
        currentLives += amount;
        currentLives = Mathf.Clamp(currentLives, 0, maxLives);
        UpdateHealthBar();
    }
}
