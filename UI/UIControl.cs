using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [Header("Main Menu")]
    public CanvasGroup menuScreen;
    public GameObject menuScreenObj;
    public float menuTransitionTime;

    [Header("Options Menu")]
    public CanvasGroup optionScreen;
    public GameObject optionScreenObj;
    public float optionTransitionTime;

    [Header("Credits Menu")]
    public CanvasGroup creditsScreen;
    public GameObject creditsScreenObj;
    public float creditsTransitionTime;

    public static UIControl instance;
    private void Awake()
    {
        instance = this;
    }
    //----------------------------------------------------------------
    #region Start & Update
    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        if (menuScreenObj.activeSelf || optionScreenObj.activeSelf || creditsScreenObj.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            try
            {
                //Freeze.instance.FREEZE = true;
            }
            catch { }
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            try
            {
                //Freeze.instance.FREEZE = true;
            }
            catch { }
        }
    }
    #endregion
    //----------------------------------------------------------------
    #region Normal Menu Components
    //-------------------------------
    #region Start Game
    [ContextMenu("Open Main Menu screen")]
    public void StartGame()
    {
        CloseAll();
        //prompt the player if they want to do a tutorial
        //load other scene
    }
    #endregion
    //-------------------------------
    #region Main Menu
    [ContextMenu("Open Main Menu screen")]
    public void OpenMainMenu()
    {
        menuScreenObj.SetActive(true);
        DOTween.To(() => menuScreen.alpha, x => menuScreen.alpha = x, 1, menuTransitionTime);
    }
    [ContextMenu("Close Main Menu screen")]
    public void CloseMainMenu()
    {
        DOTween.To(() => menuScreen.alpha, x => menuScreen.alpha = x, 1, menuTransitionTime).OnComplete(() => { menuScreenObj.SetActive(false); });
    }
    #endregion
    //-------------------------------
    #region Options
    [ContextMenu("Open options screen")]
    public void PressOptions()
    {
        optionScreenObj.SetActive(true);
        DOTween.To(() => optionScreen.alpha, x => optionScreen.alpha = x, 1, optionTransitionTime);
    }
    [ContextMenu("Close options screen")]
    public void CloseOptions()
    {
        DOTween.To(() => optionScreen.alpha, x => optionScreen.alpha = x, 1, optionTransitionTime).OnComplete(() => { optionScreenObj.SetActive(false); });
    }
    #endregion
    //-------------------------------
    #region Credits
    [ContextMenu("Start Credits screen")]
    public void PressCredits()
    {
        creditsScreenObj.SetActive(true);
        DOTween.To(() => creditsScreen.alpha, x => creditsScreen.alpha = x, 1, creditsTransitionTime);
        UICredits.instance.RollCredits();
    }
    [ContextMenu("Stop Credits screen")]
    public void CloseCredits() 
    {
        UICredits.instance.ResetCredits();
        DOTween.To(() => creditsScreen.alpha, x => creditsScreen.alpha = x, 0, creditsTransitionTime).OnComplete(() => { creditsScreenObj.SetActive(false); OpenMainMenu(); });
    }
    #endregion
    //-------------------------------
    #region Universal Close
    [ContextMenu("Close all screens")]
    public void CloseAll()
    {
        try
        {
            CloseMainMenu();
            CloseOptions();
            CloseCredits();
        }
        catch { }
    }
    #endregion
    //-------------------------------
    #region Exit
    public void PressQuit() 
    {
        Application.Quit();
    }
    #endregion
    //-------------------------------
    #endregion
    //----------------------------------------------------------------
}
