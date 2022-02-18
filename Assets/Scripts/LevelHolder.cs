using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHolder : MonoBehaviour
{
    public LevelManager LM;
    public Transform WallPieceParent;
    public Transform PathPieceParent;
    public GameObject Cutter;
    private List<GameObject> wallPiecesList = new List<GameObject>();
    public List<GameObject> PathPieceList = new List<GameObject>();
    PathScript _pathScript;

    public void GenerateLevel(int col, int rows, List<int> PathIndexes, GameObject wallPiece, GameObject PathPiece,int pos)
    {
        WallCubeGenerator(col, rows, wallPiece);
        PathGenerator(PathIndexes, PathPiece);
         for(int i=0; i<PathPieceList.Count; i++)
        {
            if(PathPieceList[i].name== pos.ToString())
            {   
                Cutter.transform.localPosition = PathPieceList[i].transform.localPosition;
                return;
            }
        }
       
        FlowerSpawner.paths = PathPieceList;
    }

    void WallCubeGenerator(int Coloumns, int Rows, GameObject wallPiece)
    {
        int index = 1;
        wallPiecesList.Clear();

        for (int rows = 0; rows < Rows; rows++)
        {
            for (int coloumns = 0; coloumns < Coloumns; coloumns++)
            {
                GameObject InstantiatedObj = Instantiate(wallPiece);    //TODO - Move to pool
                InstantiatedObj.transform.SetParent(WallPieceParent);
                InstantiatedObj.transform.localPosition = new Vector3(coloumns, 0.5f, rows);

                InstantiatedObj.name = index.ToString();

                wallPiecesList.Add(InstantiatedObj);

                index++;
            }
        }
    }

    void PathGenerator(List<int> PathList, GameObject PathPiece)
    {
        PathPieceList.Clear();

        for (int i = 0; i < wallPiecesList.Count; i++)
        {
            if (!PathList.Contains(i)) continue;

            Vector3 pos = wallPiecesList[i].transform.localPosition;

            wallPiecesList[i].SetActive(false);

            GameObject InstantiatedPathPrefab = Instantiate(PathPiece); //TODO - Move to pool
            InstantiatedPathPrefab.transform.SetParent(PathPieceParent);
            InstantiatedPathPrefab.transform.localPosition = pos;

            PathScript pathPiece = InstantiatedPathPrefab.GetComponent<PathScript>();
            InstantiatedPathPrefab.name = i.ToString();

            PathPieceList.Add(InstantiatedPathPrefab);

            pathPiece.SetLevelManager(LM);
        }
    }




    public void ResetLevel()
    {
        
        if (WallPieceParent.childCount >= 1)
        {
            for (int i = 0; i < WallPieceParent.childCount; i++)
            {
                //print("Destroying Wallpiece" + WallPieceParent.GetChild(i).gameObject.name);
                Destroy(WallPieceParent.GetChild(i).gameObject);
            }
        }

        if (PathPieceParent.childCount >= 1)
        {
            for (int i = 0; i < PathPieceParent.childCount; i++)
            {
                //print("Destroying Wallpiece" + PathPieceParent.GetChild(i).gameObject.name);
                Destroy(PathPieceParent.GetChild(i).gameObject);
            }
        }

        Cutter.SetActive(false);
    }
    public void HideCubes()
    {
            for (int i = 0; i < PathPieceParent.childCount; i++)
            {
                _pathScript = PathPieceParent.GetChild(i).gameObject.GetComponent<PathScript>();
                _pathScript.grass.SetActive(false);

            }

        }











}
