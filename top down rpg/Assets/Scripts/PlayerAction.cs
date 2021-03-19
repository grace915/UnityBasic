using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float Speed;
    public GameManager manager;

    float h;
    float v;
    bool isHorizonMove;
    Animator anim;
    Vector3 direcVec;
    GameObject scanObject;


    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // move value
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 :Input.GetAxisRaw("Vertical");

        //check button down & up
        bool hDown = manager.isAction ? false : Input.GetButton("Horizontal");
        bool vDown = manager.isAction ? false :Input.GetButton("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

        //check horizontal move
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;

        //animation
        if(anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetInteger("hAxisRaw", (int)h);
            anim.SetTrigger("isChange");
        }
        
        else if(anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetInteger("vAxisRaw", (int)v);
            anim.SetTrigger("isChange");
        }

        //Direction
        if (vDown && v == 1)
            direcVec = Vector3.up;
        else if (vDown && v == -1)
            direcVec = Vector3.down;
        else if (hDown && h == -1)
            direcVec = Vector3.left;
        else if (hDown && h == 1)
            direcVec = Vector3.right;

        //scan object
        if(Input.GetButtonDown("Jump") && scanObject != null)
        {
            manager.Action(scanObject);
        }


    }

    private void FixedUpdate()
    {
        //move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;

        //Ray
        Debug.DrawRay(rigid.position, direcVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, direcVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
    }
}
