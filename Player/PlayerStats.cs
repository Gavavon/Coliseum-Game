using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int playerHealth = 100;

    /*
     * Limb 1.2X
     * Chest 1.5X
     * Head 2.5X
     * 
     */

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damagePlayer(int damageAmount) 
    {
        playerHealth -= damageAmount;
    }
}
