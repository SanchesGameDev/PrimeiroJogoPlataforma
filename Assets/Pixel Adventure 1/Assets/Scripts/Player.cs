using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public bool isJumping;
    public bool doubleJump;
    private Rigidbody2D rig;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       Move(); 
       Jump();
    }

    void Move() 
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f);
        transform.position += movement * Time.deltaTime * speed;
        if(Input.GetAxis("Horizontal") > 0) //Andando pra Direita
        {
            animator.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
         if(Input.GetAxis("Horizontal") < 0) //Andando pra Esquerda
        {
            animator.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
         if(Input.GetAxis("Horizontal") == 0) //Parado
        {
            animator.SetBool("walk", false);
        }
        
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!isJumping)
            {
                rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                animator.SetBool("jump", true);
            }else if(doubleJump)
            {
                rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                doubleJump = false;
            }
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
            animator.SetBool("jump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isJumping = true;
    }

}
