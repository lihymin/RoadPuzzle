using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float h;
    [SerializeField] private float speed;
    [SerializeField] private float LadderDownSpeed;
    [SerializeField] private float JumpPower;
    [SerializeField] private bool isWalkAnim;
    [SerializeField] private bool isRunAnim;
    [SerializeField] private bool isJumpAnim;
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(h * speed, rigid.velocity.y);

        //Search
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

        //Player Flip
        if (rigid.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        else if (rigid.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        
        //Player Animation
        if (rigid.velocity.x != 0 && !isWalkAnim)
        {
            anim.SetTrigger("isWalk");
            isWalkAnim = true;
        }

        if ((Input.GetKey(KeyCode.LeftShift) && rigid.velocity.x != 0 && !isRunAnim)
         || (isWalkAnim && Input.GetKeyDown(KeyCode.LeftShift) && rigid.velocity.x != 0))
        {
            anim.SetTrigger("isRun");
            speed = 3f;
            isRunAnim = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetTrigger("isStop");
            speed = 1.5f;
            isWalkAnim = false;
            isRunAnim = false;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && rigid.velocity.x != 0)
        {
            anim.SetTrigger("isWalk");
            speed = 1.5f;
            isRunAnim = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumpAnim)
        {
            anim.SetTrigger("isJump");
            rigid.AddForce(Vector2.up * speed * JumpPower, ForceMode2D.Impulse);
            isJumpAnim = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {   
            isJumpAnim = false;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetTrigger("isRun");
            }

            else
            {
                anim.SetTrigger("isWalk");
                if (rigid.velocity.x == 0)
                {
                    anim.SetTrigger("isStop");
                }
            }
            
            anim.SetTrigger("isJumpStop");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Ladder"))
        { 
            if (Input.GetKeyDown(KeyCode.Space) && !isJumpAnim)
            {
                anim.SetTrigger("isJump");
                rigid.AddForce(Vector2.up * JumpPower * 2.5f, ForceMode2D.Impulse);
                isJumpAnim = true;
            }

            if (rigid.velocity.y <= 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, LadderDownSpeed);
                //rigid.velocity = new Vector2(rigid.velocity.x, 0);
                anim.SetTrigger("isJumpStop");
                isJumpAnim = false;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    anim.SetTrigger("isRun");
                }

                else
                {
                    if (rigid.velocity.x != 0)
                    {
                        anim.SetTrigger("isWalk");
                    }
                    
                    else if (rigid.velocity.x == 0)
                    {
                        anim.SetTrigger("isStop");
                    }
                }
            }
        }
    }
}
