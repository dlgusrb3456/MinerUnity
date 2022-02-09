using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPlayer : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;


    public void Player()
    {
        Invoke("changeImg", 2f);

    }

    void changeImg()
    {
        //player1.color = Color.clear;
       
    }
    
}
