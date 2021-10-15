using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    [HideInInspector]
    public float healthBarAmount = 5f;
    [HideInInspector]

    public RectTransform healthBar;

    public static PlayerHealth instance;
    private void Awake()
    {
        instance = this;
    }

    public void TakeDamage(float amount)
    {
        healthBarAmount -= amount / 100;
        if (healthBarAmount <= 0f)
        {
            playerDeath();
        }
    }

    public void playerDeath()
    {
        Debug.Log("Player Died");
        //healthBarAmount = 1;
        //Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}
