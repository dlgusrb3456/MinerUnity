using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPlayer : MonoBehaviour
{
    
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject player5;
    public GameObject player6;
    public GameObject player7;
    public GameObject player8;


    void Start()
    {
        player1.SetActive(false);
        player2.SetActive(false);
        player3.SetActive(false);
        player4.SetActive(false);
        player5.SetActive(false);
        player6.SetActive(false);
        player7.SetActive(false);
        player8.SetActive(false);




        StartCoroutine(BlinkImg());
    }


    public IEnumerator BlinkImg()
    {
        while (true)
        {
            player1.SetActive(true);
            yield return new WaitForSeconds(.5f);
            player1.SetActive(false);
            
            //player2.SetActive(true);
            //yield return new WaitForSeconds(.2f);
            //player2.SetActive(false);

            player3.SetActive(true);
            yield return new WaitForSeconds(.5f);
            player3.SetActive(false);

            player4.SetActive(true);
            yield return new WaitForSeconds(.5f);
            player4.SetActive(false);

            player5.SetActive(true);
            yield return new WaitForSeconds(.5f);
            player5.SetActive(false);

            player6.SetActive(true);
            yield return new WaitForSeconds(.5f);
            player6.SetActive(false);

            //player7.SetActive(true);
            //yield return new WaitForSeconds(.2f);
            //player7.SetActive(false);

            player8.SetActive(true);
            yield return new WaitForSeconds(.5f);
            player8.SetActive(false);
        }
    }



}
