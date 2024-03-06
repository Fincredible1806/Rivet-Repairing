using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    public int health;
    public bool dead;
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthSlider.value = health;
        if(health <= 0)
        {
            dead = true;
        }
    }

}
