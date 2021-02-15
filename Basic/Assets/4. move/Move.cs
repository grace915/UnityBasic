using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Vector3 target = new Vector3(8, 1.5f, 0);
    Vector3 my = new Vector3(2.22f, 1.71f, 0.6f);
    

    // Update is called once per frame
    void Update()
    {
        //1. moveTowards. 등속운동(현재위치, 목표위치, 속도) 매개변수에 비례해서 속도증가(최대2)
        
        // transform.position = Vector3.MoveTowards(transform.position, target, 2f);

        //2. SmoothDamp 부드러운 감속이동(현재위치, 목표위치, 참조속도, 속도) 매개변수에 반비례해서 속도 증가
        //ref 참조접근, 실시간으로 바뀌는 값 적용 가능
        
        Vector3 velo = Vector3.zero;//속도 0
        Vector3 velo2 = Vector3.up * 50; //목표지점 의미 사라짐 그래서 잘 안써!

        //transform.position = Vector3.SmoothDamp(transform.position, target, ref velo, 0.1f);

        //3. Lerp 선형보간(현재위치, 목표위치, 속도), SmoothDamp보다 감속시간이 김, 매개변수에 비례(최대1)
        
        //transform.position = Vector3.Lerp(transform.position, target, 0.05f);

        //4. SLerp 구면선형보간. 호를 그리며 이동
       transform.position = Vector3.Slerp(transform.position, target, 0.05f);
        
    }
}
