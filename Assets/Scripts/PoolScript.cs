using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolScript : MonoBehaviour
{

    private static PoolScript instance;
    public static PoolScript Instance { get { return instance; } }

    public static List<GameObject> PooledWallObjects;
    static GameObject Temp;

    public GameObject Wallcubes;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {




        //PooledWallObjects = new List<GameObject>();

        //for (int i = 0; i <= 100; i++)
        //{

        //    GameObject InstantiatedObj = Instantiate(Wallcubes);
        //    InstantiatedObj.name = i.ToString();
        //    InstantiatedObj.SetActive(false);
        //    PooledWallObjects.Add(InstantiatedObj); 
        //}
    }

    public GameObject getFromPool(int i)
    {
        foreach (GameObject g in PooledWallObjects)
        {
            if (g.name == i.ToString() && g.activeInHierarchy == false)
            {
                g.SetActive(true);
                return g;
            }
        }
        GameObject newObjInstance = Instantiate(Wallcubes);
        newObjInstance.name = i.ToString();

        PooledWallObjects.Add(newObjInstance);
        return newObjInstance;

    }





}