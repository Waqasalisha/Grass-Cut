using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevel : MonoBehaviour
{

    public GameObject Level1;
    public GameObject level2;
    public GameObject Level3;

    public GameObject Current;
    public Transform WallPieceParent;
    public GameObject Prefab;
    // Start is called before the first frame update
    void Start()
    {
   
    int row = 10;
    int col = 10;
    
    // some function that builds the grid
    int index=1;
    //Vector3 pos = new Vector3(0,0.5f,0);
    for (int r = 0; r <row; r++)
    { 
        
        for (int c = 0; c < col; c++)
        {       
            GameObject InstantiatedObj =Instantiate (Prefab);
           InstantiatedObj.transform.position = new Vector3(c,0.5f,r);
           
            

            InstantiatedObj.name = index.ToString();
            InstantiatedObj.transform.SetParent(WallPieceParent, true);
            index++;
            //print( "of " + InstantiatedObj.p);
            // pos= new Vector3(r,0.5f,c);
        }
        
        //pos = new Vector3(pos.x-1,0.5f,r);
      
    
        
     }



        // Current=Level1;
        // Next=level2;
       //Next.transform.position=level2.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeLevel(){
        // Next.transform.position= new Vector3(10,0,0);
        // Current.gameObject.transform.position= new Vector3(-10,0,0);

        // Current=Next;
        
        // Current.gameObject.transform.position=new Vector3(1,1,1);
        // Next=Level3;
        // Current=Next;

        
        
    }


}
