using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPosition : MonoBehaviour
{
    public GameObject A;  //A라는 GameObject변수 선언
    Transform AT;
    void Start()
    {
        AT = A.transform;
    }
    void LateUpdate()
    {
        transform.position = new Vector3(AT.position.x, AT.position.y, transform.position.z);
    }

    //void Update()
    //{
    //    transform.position = Vector3.Lerp(transform.position, AT.position, 2f * Time.deltaTime);
    //    transform.Translate(0, 0, -10); //카메라를 원래 z축으로 이동
    //}
}
