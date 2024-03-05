using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public bool dead;
    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            dead = true;
        }
    }

}
