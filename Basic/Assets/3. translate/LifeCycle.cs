using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //Vector3 3차원
        int number = 4; //스칼라
        Vector3 vec = new Vector3(0.3f, 1.5f, 0.3f);//벡터
        transform.Translate(vec); //벡터값을 현재 위치에 더함
        

      
    }

    void Update()
    {
        Vector3 vec = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);//벡터
         transform.Translate(vec); //벡터값을 현재 위치에 더함


        //GetAxisRaw - 01-1
        //GetAxis - 중간값
    }
   
}
