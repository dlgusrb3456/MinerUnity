using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 게임의 기본적인 설정 또는 여러 값들을 저장하는 정적 클래스 입니다.
public static class MinerEnvironment
{
    public static string environmentPath = Application.persistentDataPath;
    public static string settingJsonLocation = environmentPath + "/setting.json";
    public static string savedMapPath = environmentPath + "/saved/";

    /*
     맵 데이터 또는 설정 등을 저장할
     기본 데이터 저장 경로 (Application.persistentDataPath) 폴더의 구조는 다음과 같습니다 : 
        setting.json
        saved/
            map1.json
            map2.json
            ..
     initEnvironment() 는 saved 디렉터리와 setting.json이 존재하지 않을 경우 미리 생성하는 함수로,
     나중에 값을 불러올때 차질이 생기지 않게 하기 위하여 게임의 시작과 동시에 실행되어야 합니다.
    */
    public static void initEnvironment()
    {
        if (!File.Exists(settingJsonLocation))
        {
            File.Create(settingJsonLocation);
            // TODO: 설정 기능을 구현하게 된다면, setting.json의 기본값을 저장하는 부분이 필요할거에요.
        }

        if (!Directory.Exists(savedMapPath))
            Directory.CreateDirectory(savedMapPath);
    }
}
