using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider healthSlider;
    public Image fillImage;
    public float fullHealth;
    [SerializeField] private GameObject lowHealthWarning;
    [SerializeField] private Movement movement;
    [SerializeField] private Character character;
    [SerializeField] private GameObject weaponUI;
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
        movement.enabled = false;
        character.enabled = false;
        dead = true;
        Time.timeScale = 0f;
        weaponUI.SetActive(false); 
    }

    public void SetHealthUI()
    {
        healthSlider.value = health;
        fillImage.color = Color.Lerp(lowHealthColor, highHealthColor, health / fullHealth);
        if(health <= fullHealth * 0.25)
        {

        }
    }

}
