using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public GameManager gameManager;
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;
    public float maxSpeed;
    public float jumpPower;


    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    int jumpCount;
    CapsuleCollider2D capsuleCollider;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator.SetBool("isJumping", false);
        jumpCount = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                break;

        }

        audioSource.Play();

    }

    void Update()
    {
        /*
        //jump 1번만 가능
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }
        */
        //jump 2번할래
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            jumpCount++;
            PlaySound("JUMP");
        }
        

        //stop speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.1f, rigid.velocity.y);
        }

        //direction sprite
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            
        //animation
        if (Mathf.Abs(rigid.velocity.x) <0.1f   )
            animator.SetBool("isWalking", false);
        else
            animator.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move speed
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h*2, ForceMode2D.Impulse);

        //max speed
        if (rigid.velocity.x > maxSpeed)//right max speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        if (rigid.velocity.x < maxSpeed*(-1))//left max speed
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);

        if(rigid.velocity.y < 0)
        {
            //Landing platform
            Debug.DrawRay(rigid.position, Vector3.down, new Color(1, 0, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.7f)
                {
                    animator.SetBool("isJumping", false);
                    jumpCount = 0;

                }

            }
        }

   

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Attack
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
                PlaySound("ATTACK");
            }
            else //Damaged
            {
                OnDamaged(collision.transform.position);
                PlaySound("DAMAGED");
            }
            
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

         
        if (collision.gameObject.tag == "Item")
        {
           
            //Point
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");

            if (isBronze)
                gameManager.stagePoint += 50;
            if (isSilver)
                gameManager.stagePoint += 100;
            if (isGold)
                gameManager.stagePoint += 300;

            //Deactive Item
            collision.gameObject.SetActive(false);

            //sound
            PlaySound("ITEM");
        }
        else if(collision.gameObject.tag == "Finish")
        {
            //Next stage
            gameManager.NextStage();
            // sound
            PlaySound("FINISH");

        }
    }

    void OnAttack(Transform enemy)
    {
        //Point
        gameManager.stagePoint += 100;

        //Reaction Force
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        //Enemy Die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }

    //무적상태
    void OnDamaged(Vector2 targetPos)
    {

        //Health down
        gameManager.HealthDown();

        //change layer
        gameObject.layer = 11;

        //view alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //reaction force

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 10, ForceMode2D.Impulse);

        //animation
        animator.SetTrigger("doDamaged");

        Invoke("OffDamaged", 3);

    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie()
    {
        //spirte alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //sprite flip y
        spriteRenderer.flipY = true;
        //collider disable
        capsuleCollider.enabled = false;
        //die effect jump
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        PlaySound("DIE");



    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }
}
