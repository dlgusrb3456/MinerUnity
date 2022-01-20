using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class dotProblem : MonoBehaviour
{
    public GameObject Panel_setFilePrefab1;
    public GameObject Panel_setFilePrefab2;
    public GameObject Button_mapPrefab;

    public Sprite imageBG;

    public Text Text_mironame;


    public void dotdotdotPanel()
    {
        
        string selectedFileName = Text_mironame.text;
        Debug.Log(selectedFileName);
        for (int i = 0; i < Map.localMaps.Count; i++)
        {
            if (Map.localMaps[i].name == selectedFileName)
            {
                Debug.Log(i);
                if (Map.localMaps[i].isShared)
                {
                    Debug.Log("isShared");
                    GameObject sharePanel = Instantiate(Panel_setFilePrefab1) as GameObject;
                    sharePanel.transform.parent = Button_mapPrefab.transform;
                    sharePanel.transform.GetComponent<Image>().sprite = imageBG;
                    sharePanel.transform.position = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0);
                    sharePanel.gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log("isNoneShared");
                    GameObject nonesharePanel = Instantiate(Panel_setFilePrefab2) as GameObject;
                    nonesharePanel.transform.parent = Button_mapPrefab.transform;
                    nonesharePanel.transform.position = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0);
                    nonesharePanel.gameObject.SetActive(true);
                }
                break;
            }

        }
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
