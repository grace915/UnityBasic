using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle_btn : MonoBehaviour
{
    void Update()
    {
        // Input : 게임내 입력을 관리. 입력방식 down stay up
        if (Input.anyKeyDown) //아무입력을 최초로 받을 때 true
            Debug.Log("플레이어가 아무 키를 눌렀습니다.");

        if (Input.anyKey)
            Debug.Log("플레이어가 아무 키를 누르고 있습니다.");

        //getKey 키보드 버튼을 입력 받으면 true
        if (Input.GetKeyDown(KeyCode.Return)) //return = enter
            Debug.Log("아이템을 구입하였습니다.");

        if (Input.GetKey(KeyCode.LeftArrow))  
            Debug.Log("왼쪽으로 이동중");

        if (Input.GetKeyUp(KeyCode.RightArrow))
            Debug.Log("오늘쪽 이동을 멈추었습니다.");

        
        //GetMouse 마우스 버튼을 입력 받으면 true

        //0 왼쪽버튼, 1 오른쪽 버튼
        if (Input.GetMouseButtonDown(0)){
            Debug.Log("미사일 발사!");
        }
        if (Input.GetMouseButton(0))
        {
            Debug.Log("미사일 모으는 중...");
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("슈퍼 미사일 발사!!");
        }

        //GetButton input 버튼을 누르면 true
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("점프!");
        }
        if (Input.GetButton("Fire1"))
        {
            Debug.Log("점프 모으는 중...");
        }
        if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("슈퍼 점프!!");
        }

        //행이동
        if (Input.GetButton("Horizontal"))
        {
            //Debug.Log("행 이동 중..." + Input.GetAxis("Horizontal")); //가중치. 중간값
            Debug.Log("행 이동중..." + Input.GetAxisRaw("Horizontal")); //0 1 -1
        }
        //종이동
        if (Input.GetButton("Vertical"))
        {
            //Debug.Log("종 이동 중..." + Input.GetAxis("Vertical")); //가중치. 중간값
            Debug.Log("종 이동중..." + Input.GetAxisRaw("Vertical")); //0 1 -1
        }




    }
}
