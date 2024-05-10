using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider healthSlider;
    public Image fillImage;
    public float fullHealth;
    [SerializeField] private GameObject lowHealthWarning;
    [SerializeField] private Movement movement;
    [SerializeField] private Character character;
    [SerializeField] private GameObject deathCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] GameObject playerRoot;
    [SerializeField] GameObject uiCam;
    [Header("Variables")]
    public float health;
    public Color lowHealthColor = Color.red;
    public Color highHealthColor = Color.green;
    public bool dead;



    private void Start()
    {
        fillImage.color = highHealthColor;
        health = fullHealth;
        healthSlider.value = fullHealth;
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, fullHealth);
        Debug.Log("Damage Taken " + damage);
        SetHealthUI();
        Debug.Log(health);
        if (health <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerRoot.SetActive(false);
        uiCam.SetActive(true);
        movement.enabled = false;
        character.enabled = false;
        dead = true;
        pauseCanvas.SetActive(false);
        deathCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SetHealthUI()
    {
        healthSlider.value = health;
        fillImage.color = Color.Lerp(lowHealthColor, highHealthColor, health / fullHealth);
        if(health <= fullHealth * 0.25)
        {

        }
    }

    public void ReturnMenuButton()
    {
        SceneManager.LoadScene(0);
    }

}
