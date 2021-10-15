using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordController : MonoBehaviour
{
    public float burnDamageAmount = 0.1f;
    public float coldDamageAmount = 0.1f;

    public GameObject hudBar;

    public Color damageTypeHot;
    public Color damageTypeCold;

    public static SwordController instance;
    private void Awake()
    {
        instance = this;
    }
    public void updateWeapon() 
    {
        switch (SwordDefinition.instance.currentElementState)
        {
            case (SwordDefinition.ElementalState.Hot):
                //set hud bar color to red
                hudBar.GetComponent<Image>().color = damageTypeHot;
                //set sword particle effect active

                break;
            case (SwordDefinition.ElementalState.Cold):
                //set hud bar color to blue
                hudBar.GetComponent<Image>().color = damageTypeCold;
                //set sword particle effect active

                break;

        }
    }

    private void Update()
    {
        playerEffect();
    }

    public void playerEffect() 
    {
        switch (SwordDefinition.instance.currentElementState) 
        {
            case (SwordDefinition.ElementalState.Hot):
                //burnPlayer();
                break;
            case (SwordDefinition.ElementalState.Cold):

                break;

        }
    }
    public void burnPlayer()
    {
        PlayerHealth.instance.healthBar.localScale = new Vector3(PlayerHealth.instance.healthBarAmount, 1f, 1f);

        PlayerHealth.instance.healthBarAmount = Mathf.Clamp(PlayerHealth.instance.healthBarAmount, 0.000f, 1.000f);

        if (hudBar.GetComponent<RectTransform>().localScale.x <= 0)
        {
            PlayerHealth.instance.healthBarAmount -= burnDamageAmount * Time.deltaTime;
            if (PlayerHealth.instance.healthBarAmount <= 0f)
            {
                PlayerHealth.instance.playerDeath();
            }
        }
    }
}
