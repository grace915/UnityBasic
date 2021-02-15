using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaTime : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        //Time.deltatime 이전 프레임의 완료까지 걸린시간. 프레임이 적으면 크고, 프레임이 많으면 작음
        //10프레임은 1초에 10번 돌았다.
        //translate : 벡터에 곱사기
        //vector 함수 : 시간 매개변수에 곱하기
        Vector3 vec = new Vector3(
            Input.GetAxisRaw("Horizontal")*Time.deltaTime, 
            Input.GetAxis("Vertical")*Time.deltaTime, 0);//벡터
        transform.Translate(vec); //벡터값을 현재 위치에 더함

    }
}
