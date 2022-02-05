using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private VirtualJoystick virtualJoystick;
    public float moveSpeed = 5;
    SpriteRenderer spriteRenderer;


    //종료 이벤트;
    public Text TimerText;
    public GameObject Panel_preventEnds;

    private void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        Panel_preventEnds.SetActive(false);
    }

    private void Update()
    {
        float x = virtualJoystick.Horizontal();
        float y = virtualJoystick.Vertical();

        if(x != 0 || y != 0)
        {
            transform.position += new Vector3(x, y, 0) * moveSpeed * Time.deltaTime;
        }

        //방향전환
        if (x < 0)
            spriteRenderer.flipX = false;
        if (x > 0)
            spriteRenderer.flipX = true;



    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish") // 도착지점에 충돌하면
        {
            // 게임 정지, 종료 > 게임 종료(플레이 시간 등 정보 들어간)판넬 뜨게
            Panel_preventEnds.SetActive(true);
            Timer.pauseTimer();
            Debug.Log("colision");
        }
    }

}
