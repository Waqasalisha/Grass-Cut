using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

using System;



public class LevelManager : MonoBehaviour
{
    public int coloumns, rows;

    public FlowerSpawner FlowerEffectSpawner;
    float FlowerEffectDuration = 3f;



    public GameObject WallPiece;
    public GameObject PathPiece;
    private int _requiredTriggerCount;



    #region Levels Lists



    public List<Level> AllLevels;
    public List<int> CutterPositions= new List<int>();
    #endregion


    public GameObject CutterPrefab;
    public static bool destroyFlowers;
    public LevelHolder CurrentLevel;
    public LevelHolder NextLevel;
    public LevelHolder PreviousLevel;
    public static bool resetScale = false;
    int CurrentLevelNumber = 0;
    public int _triggerCount;
    public UIManager _UIManager;



    void Start()
    {
        _triggerCount = 0;
        _requiredTriggerCount = AllLevels[CurrentLevelNumber].LevelPath.Count;
        GenerateNextLevel(AllLevels[CurrentLevelNumber].LevelPath,CutterPositions[CurrentLevelNumber]);
        CurrentLevelNumber++;
        CurrentLevel.Cutter.SetActive(true);

    }





    public void IncreamentTriggerCount()
    {
        _triggerCount++;


        if (_triggerCount == _requiredTriggerCount)
        {
            LevelCompleted();
        }
    }

    private void LevelCompleted()
    {
        _UIManager.HideRestartButton();
        _UIManager.HideSkipButton();
        CurrentLevel.Cutter.SetActive(false);

        bool FlowerEffectCompleted = FlowerEffectSpawner.RunLevelCompleteFlowerEffect(FlowerEffectDuration, CurrentLevel.transform);

        if (FlowerEffectCompleted)
        {
            StartCoroutine(StartNextLevelAfterFlowerEffectCo(4f));
        }
    }

    public IEnumerator StartNextLevelAfterFlowerEffectCo(float dur)
    {

        yield return new WaitForSeconds(dur);

        if (CurrentLevelNumber < AllLevels.Count)
        {
            _UIManager.SetLevelText(CurrentLevelNumber);
           
            GenerateNextLevel(AllLevels[CurrentLevelNumber].LevelPath,CutterPositions[CurrentLevelNumber]);

            CurrentLevelNumber++;
            if (CurrentLevelNumber >= AllLevels.Count)
            {

                CurrentLevelNumber = 0;
            }
        }
       // _UIManager.ShowRestartButton();
      //  _UIManager.ShowSkipButton();
        
    }

    void GenerateNextLevel(List<int> LevelPathToGenerate, int indexPosition )
    {
        if (PreviousLevel != null)
        {
            PreviousLevel.ResetLevel();

            PreviousLevel = CurrentLevel;
            CurrentLevel = NextLevel;
            NextLevel = PreviousLevel;
            CurrentLevel.GenerateLevel(10, 10, LevelPathToGenerate, WallPiece, PathPiece,indexPosition);
            CurrentLevel.Cutter.SetActive(true);
            CurrentLevel.transform.position = new Vector3(40f, 0f, 0f);

            MoveLevel(1f);
        }
        else
        {
            CurrentLevel.GenerateLevel(10, 10, LevelPathToGenerate, WallPiece, PathPiece,indexPosition);
            CurrentLevel.Cutter.SetActive(true);
            PreviousLevel = CurrentLevel;
        }


        _triggerCount = 0;
        _requiredTriggerCount = LevelPathToGenerate.Count;


        








    }


    void MoveLevel(float moveDur)
    {

        // TODO - Animate these
        CurrentLevel.transform.DOMove(new Vector3(0f, 0f, 0f), moveDur)       // position = (new Vector3(0, 0, 0))  ;
            .OnComplete(() =>
            {
                
                _UIManager.ShowRestartButton();
                _UIManager.ShowSkipButton();

            });

        PreviousLevel.transform.DOMove(new Vector3(-40f, 0f, 0f), moveDur)    // position = (new Vector3(-30, 0, 0));
            .OnComplete(() =>
            {
                FlowerEffectSpawner.ResetEffect();
            });
    }

    public void RestartLevel()
    {

        _triggerCount = 0;
        _requiredTriggerCount = AllLevels[CurrentLevelNumber - 1].LevelPath.Count;
       
        CurrentLevel.Cutter.SetActive(false);
        GenerateNextLevel(AllLevels[CurrentLevelNumber - 1].LevelPath,CutterPositions[CurrentLevelNumber-1]);
        
        

    }

    public void SkipLevel()
    {
        //StartNextLevelAfterFlowerEffectCo(0);
       CurrentLevel.HideCubes();
        LevelCompleted();

       /* if (CurrentLevelNumber >= AllLevels.Count)
        {
            CurrentLevelNumber = 0;
        }
        int skipLevel = CurrentLevelNumber;
        _triggerCount = 0;
        _requiredTriggerCount = AllLevels[skipLevel].LevelPath.Count;

        CurrentLevel.Cutter.SetActive(false);
        GenerateNextLevel(AllLevels[skipLevel].LevelPath, CutterPositions[skipLevel]);
        _UIManager.SetLevelText(skipLevel);

        CurrentLevelNumber = CurrentLevelNumber + 1;
        FlowerEffectSpawner.ConfettiSapawner();*/

    }

    


    


}










