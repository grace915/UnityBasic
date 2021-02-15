using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle_basic : MonoBehaviour
{
    //초기화
   //오브젝트 새성때. 최초실행
   void Awake()
    {
        Debug.Log("플레이어 데이터가 준비되었습니다.");
    }
    //활성화
    void OnEnable()
    {
        Debug.Log("플레이어 로그인");
    }
    //업데이트 시작 직전, 최초실행
    void Start()
    {
        Debug.Log("사냥 장비를 챙겼습니다.");
    }
    //프레임
    // 물리 연산 업데이트. 
    // 1초에 50번. 여러번. cpu 많이 사용. 구정된 실행주기. 
    void FixedUpdate()
    {
        Debug.Log("이동~");
    }
    //게임 로직 업데이트
    //환경에 따라 실행주기 달라짐
    void Update()
    {
        Debug.Log("몬스터 사냥");
    }
    //모든 업데이트가 끝난 후 마지막에 호출
    //카메라, 후처리 등
    void LateUpdate()
    {
        Debug.Log("경험치 획득");
    }
    //비활성화
    void OnDisable()
    {
        Debug.Log("플레이어가 로그아웃했습니다.");
    }
    //해체
    private void OnDestroy()
    {
        Debug.Log("플레이어 데이터를 해제하였습니다.");
    }

    
}
