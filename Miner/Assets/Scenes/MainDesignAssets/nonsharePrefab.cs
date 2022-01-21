using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nonsharePrefab : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Panel_setFilePrefab2;
    public GameObject Button_mapPrefab;


    public void share()
    {

    }

    public void delete()
    {

    }

    public void close()
    {
        Panel_setFilePrefab2.gameObject.SetActive(true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
