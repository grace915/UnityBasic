using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{

    public float rotateSpeed;
    public float jumpPower;
    public int itemCount;
    public GameManagerLogic manager;
    bool isJump;
    Rigidbody rigid;
    AudioSource audio;

    // Start is called before the first frame update
    void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.Self);

        if(Input.GetButtonDown("Jump") && !isJump){
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }

        if(transform.position.y < -10)
            SceneManager.LoadScene("Example1_" + manager.stage);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
            isJump = false;

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item" ){
            
            itemCount++;
            audio.Play();
            other.gameObject.SetActive(false);
            manager.GetItem(itemCount);
        } 
        else if(other.tag == "Finish" ){
            if(itemCount == manager.totalItemCount){
                //Game Clear
                if(manager.stage == 2)
                    SceneManager.LoadScene(0);
                SceneManager.LoadScene(manager.stage+1);

            }else{
                //Restart
                SceneManager.LoadScene(manager.stage);
            }
            
        }
    }
}
