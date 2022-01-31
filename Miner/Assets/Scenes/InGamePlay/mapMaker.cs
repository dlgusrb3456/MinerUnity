using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapMaker : MonoBehaviour
{
    public GameObject grassPrefab;
    public GameObject startPrefab;
    public GameObject endPrefab;
    public GameObject rockPrefab;

    private GameObject grass_obj;
    private GameObject start_obj;
    private GameObject end_obj;
    private GameObject rock_obj;


    //arr만 받아오면 그냥 쓰면 됨;

    int[,] testarr = new int[7, 7]
    {
        {4,4,4,4,4,4,4 },
        {4,0,1,1,1,1,4 },
        {4,0,1,1,1,1,4 },
        {4,0,1,1,1,1,4 },
        {4,0,0,2,1,1,4 },
        {4,1,1,1,3,1,4 },
        {4,4,4,4,4,4,4 }
    };

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < testarr.GetLength(0); i++)
        {
            for (int j = 0; j < testarr.GetLength(1); j++)
            {
                Debug.Log("i :" + i + "j :" + j);

                Vector3 position = new Vector3(-1.0f + (float)0.5 * i, -1.0f + (float)0.5 * j, 0);
                Debug.Log(position.x);
                Debug.Log(position.y);
                if (testarr[i, j] == 1)
                {
                    grass_obj = Instantiate(grassPrefab);
                    grass_obj.transform.position = position;

                }
                else if (testarr[i, j] == 2)
                {

                    end_obj = Instantiate(endPrefab);
                    end_obj.transform.position = position;
                }
                else if (testarr[i, j] == 3)
                {
                    start_obj = Instantiate(startPrefab);
                    start_obj.transform.position = position;
                }
                else if (testarr[i, j] == 4)
                {
                    rock_obj = Instantiate(rockPrefab);
                    rock_obj.transform.position = position;
                }
                
            }
        }

    }
}
