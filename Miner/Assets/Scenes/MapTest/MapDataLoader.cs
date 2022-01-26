using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class MapDataLoader
{
    public MapData Load(string fileName)
    {
        //filename에 ".json"문장이 없으면 입력해준다.
        if (fileName.Contains(".json") == false)
        {
            fileName += ".json";
        }
        // 파일의 경로, 파일명을 하나로 합칠 때 사용
        //Application.streamingAssetsPath : 현재 유니티 프로젝트 -- 폴더경로
        fileName = Path.Combine(Application.streamingAssetsPath, fileName);

        //"fileName" 파일에 있는 내용을 "dataAsJson" 변수에 문자열 형태로 저장
        string dataAsJson = File.ReadAllText(fileName);

        //역직렬화를 이용해 dataAsJson 변수에 있는 문자열 데이터를 MapData 클래스 인스턴스에 저장
        MapData mapData = new MapData();
        mapData = JsonConvert.DeserializeObject<MapData>(dataAsJson);

        return mapData; 
    }
}
