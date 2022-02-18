using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIManager : MonoBehaviour
{
    public int CurrentLevel;
    public TextMeshProUGUI LevelText;
    public GameObject ResetButton;
    public GameObject SkipButton;
    public LevelManager _LevelManager;

  


    public void RestartLevel()
    {
        HideRestartButton();
        _LevelManager.RestartLevel();
       
 
    }


    public void SetLevelText(int levelNo)
    {
        CurrentLevel = levelNo;
        LevelText.SetText("Level   " + (CurrentLevel + 1).ToString());
    }

    public void HideRestartButton()
    {
        ResetButton.SetActive(false);
    }
    public void ShowRestartButton()
    {
        ResetButton.SetActive(true);
    }

    public void SkipLevel()
    {
        HideSkipButton();   
        _LevelManager.SkipLevel();
       
    }
    public void HideSkipButton()
    {
        SkipButton.SetActive(false);
    }
    public void ShowSkipButton()
    {
        SkipButton.SetActive(true);
    }

}
