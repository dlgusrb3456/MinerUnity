using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    private static Stopwatch stopwatch = new Stopwatch();
    public Text timerText; // 유니티에서 추가

    // 로딩과 동시에 세기 시작. 
    void Start()
    {
        startTimer();
        StartCoroutine("timerTextUpdateLoop");
    }

    public static int elapsedTotalSeconds
    {
        get
		{
            return (int)(stopwatch.ElapsedMilliseconds / 1000);
		} 
    }
    public static int elapsedMinutes
    {
        get
		{
            return elapsedTotalSeconds / 60;
		} 
    }
    public static int elapsedSeconds
    {
        get
		{
            return elapsedTotalSeconds % 60;
		} 
    }

    // 절때 종료되지 않음.
    private IEnumerator timerTextUpdateLoop()
    {
        while (true)
        {
			string m = elapsedMinutes.ToString();
			if (m.Length < 2) m = '0' + m;

			string s = elapsedSeconds.ToString();
			if (s.Length < 2) s = '0' + s;

			timerText.text = m + ':' + s;
			yield return new WaitForSeconds(0.2f);
        }
    }

    /*
        1분 5초 경과했을때, 
        Debug.Log(Timer.elaspedTotalSeconds) => 65
        Debug.Log(Timer.elaspedSeconds) => 5 
        Debug.Log(Timer.elaspedMinutes) => 1 

        Timer.startTimer() 실행시 타이머 시작. (씬 로딩과 함께 자동으로 호출되는 함수)
        TImer.stopTimer() 실행시 타이머 일시정지.

        이 주석에서 명시된 모든 함수 및 프로퍼티들은 다른 클래스에서 똑같은 형태로 사용 가능
    */

    public static void startTimer() => stopwatch.Start();
    public static void pauseTimer() => stopwatch.Stop();

    void Update() { }
}
