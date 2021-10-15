using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStart : MonoBehaviour
{
    [Header("Sword Type")]
    public TextMeshProUGUI typeText;
    public string[] typeList = { "Hot", "Cold"};
    public int typeAmount = 0;

    [Header("Level")]
    public TextMeshProUGUI levelText;
    public string[] levelList = { "Level 1", "Level 2", "Level 3", "Level 4", "Level 5" };
    public int levelAmount = 0;

    public void Update()
    {
        typeText.SetText(typeList[typeAmount]);
        levelText.SetText(levelList[levelAmount]);
    }
    //----------------------------------------------------------------
    #region Button Methods for Sword Type
    public void TypeChangeRight() 
    {
        if (typeAmount == typeList.Length - 1) 
        {
            typeAmount = 0;
            return;
        }
        typeAmount++;
    }
    public void TypeChangeLeft()
    {
        if (typeAmount == 0)
        {
            typeAmount = typeList.Length - 1;
            return;
        }
        typeAmount--;
    }
    #endregion
    //----------------------------------------------------------------
    #region Button Methods for Level
    public void LevelChangeRight()
    {
        if (levelAmount == levelList.Length - 1)
        {
            levelAmount = 0;
            return;
        }
        levelAmount++;
    }
    public void LevelChangeLeft()
    {
        if (levelAmount == 0)
        {
            levelAmount = levelList.Length - 1;
            return;
        }
        levelAmount--;
    }
    #endregion
    //----------------------------------------------------------------
    #region Buttons for Stating Game
    public void StartGame() 
    {
        SwordDefinition.instance.currentWeaponState = SwordDefinition.WeaponState.Sheathed;

        switch (typeText.text)
        {
            case "Hot":
                SwordDefinition.instance.currentElementState = SwordDefinition.ElementalState.Hot;
                break;
            case "Cold":
                SwordDefinition.instance.currentElementState = SwordDefinition.ElementalState.Cold;
                break;
            default:
                Debug.LogError("Error on like 80 of UIStart.cs could not define current element state");
                break;
        }

        SwordController.instance.updateWeapon();

        UIControl.instance.CloseMainMenu();
        UIControl.instance.CloseStart();
    }


    #endregion
}
