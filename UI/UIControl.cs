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

    [Header("Start Menu")]
    public CanvasGroup startScreen;
    public GameObject startScreenObj;
    public float startTransitionTime;

    [Header("Options Menu")]
    public CanvasGroup optionScreen;
    public GameObject optionScreenObj;
    public float optionTransitionTime;

    [Header("Credits Menu")]
    public CanvasGroup creditsScreen;
    public GameObject creditsScreenObj;
    public float creditsTransitionTime;

    [Header("Pause Menu")]
    public CanvasGroup pauseScreen;
    public GameObject pauseScreenObj;
    public float pauseTransitionTime;

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
        if (menuScreenObj.activeSelf || startScreenObj.activeSelf || optionScreenObj.activeSelf || creditsScreenObj.activeSelf || pauseScreenObj.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Freeze.instance.FREEZE = true;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Freeze.instance.FREEZE = false;
        }
    }
    private void Update()
    {
        if (creditsScreenObj.activeSelf && Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            StopCredits();
        }
    }
    #endregion
    //----------------------------------------------------------------
    #region Normal Menu Components
    //-------------------------------
    #region Main Menu
    [ContextMenu("Close Main Menu screen")]
    public void CloseMainMenu()
    {
        DOTween.To(() => menuScreen.alpha, x => menuScreen.alpha = x, 1, menuTransitionTime).OnComplete(() => { menuScreenObj.SetActive(false); });
    }
    #endregion
    //-------------------------------
    #region Start
    [ContextMenu("Open Start screen")]
    public void PressStart() 
    {
        startScreenObj.SetActive(true);
        DOTween.To(() => startScreen.alpha, x => startScreen.alpha = x, 1, startTransitionTime);
    }
    [ContextMenu("Close Start screen")]
    public void CloseStart()
    {
        DOTween.To(() => startScreen.alpha, x => startScreen.alpha = x, 1, startTransitionTime).OnComplete(() => { startScreenObj.SetActive(false); });
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
    public void StopCredits() 
    {
        UICredits.instance.ResetCredits();

        DOTween.To(() => creditsScreen.alpha, x => creditsScreen.alpha = x, 0, creditsTransitionTime).OnComplete(() => { creditsScreenObj.SetActive(false); });
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
    #region Pause Menu Components

    [ContextMenu("Pause Screen Transition In")]
    public void PauseScreenTransitionIn() 
    {
        pauseScreenObj.SetActive(true);
        DOTween.To(() => pauseScreen.alpha, x => pauseScreen.alpha = x, 1, pauseTransitionTime);
    }
    [ContextMenu("Pause Screen Transition Out")]
    public void PauseScreenTransitionOut()
    {
        DOTween.To(() => pauseScreen.alpha, x => pauseScreen.alpha = x, 0, pauseTransitionTime).OnComplete(() => { pauseScreenObj.SetActive(false); });
        
    }
    #endregion
    //----------------------------------------------------------------
}
