using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {

        //talk data
        //npc a : 1000, b : 2000, desk 3000, box 4000, coin 5000

        talkData.Add(1000, new string[] { "안녕?:0", "반가워~:1", "쯔꾸르는 처음이지?:2" });
        talkData.Add(2000, new string[] {"누구냐 넌:3", "아 새입자구나:2", "이곳은 나의 땅이야:2", "구경할래?:1"});
        talkData.Add(3000, new string[] { "누군가가 사용했던 흔적이 있는 책상이다." });
        talkData.Add(4000, new string[] { "신비한 나무상자다", "무엇이 들어가 있을까? " });

        portraitData.Add(1000 + 0, portraitArr[0] );
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);

        //quest talk
        talkData.Add(10 + 1000, new string[] {"어서와.:0", "이 마을에 놀라운 전설이 있어!:1", "왼쪽 호수 쪽에 루도가 알려줄거야.:2"});
        talkData.Add(11 + 1000, new string[] { "아직 못 만났어?:1", "루도는 왼쪽 호수에 있어.:1" });
        talkData.Add(11 + 2000, new string[] { "누구냐 넌:3", "아, 호수의 전설을 들으러 온거야?:1", "그럼 일 좀 하나 해주면 좋을텐데...:0", "내 집 근처에 떨어진 동전 좀 주워줬으면 해.:1" });

        talkData.Add(20 + 1000, new string[] { "루도의 동전?:1", "돈을 또 흘렸단말이야??:3", "루도 이자식 혼쭐이 나봐야해.:3" });
        talkData.Add(20 + 2000, new string[] { "동전.. 내 동전... 어딨니...:1" });
        talkData.Add(20 + 5000, new string[] { "루도의 동전을 발견했다." });
        talkData.Add(21 + 2000, new string[] { "찾아줘서 고마워!:2" });


        
        
    }

    public string GetTalk(int id, int talkIndex)
    {

        if (!talkData.ContainsKey(id))
        {
            
            Debug.Log("없어" + talkIndex + ","+id);

            //퀘스트 맨 처음 대사마저 없을 때
            //기본 대사를 가지고 온다. 
            if (!talkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 100, talkIndex);
            //해당 퀘스트 진행 중 대사가 없을 때
            //퀘스트 맨처음 대사를 가지고 온다. 
            else
                return GetTalk(id - id % 10, talkIndex);
       
        }

        //끝나면 null 리턴
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
            return talkData[id][talkIndex];

       


    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
