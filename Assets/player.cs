using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public float xinput;
    public float speed;
    public float jumphight;
    private bool moving;
    private int facingdir = 1;
    private bool facingright = true;
    public float groundcheck;
    private bool isgrounded;
    public LayerMask whatisground;
    public float dashspeed;
    public float dashduration;
    public float dashtime;
    public bool isattacking;
    public int combocount;
    public float jiasutime;
    public float jiasu;
    public float jiasufudu;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        xinput = Input.GetAxisRaw("Horizontal");
        
        if(xinput != 0)
        {
            jiasutime -= Time.deltaTime;
        }
        else
        {
            jiasutime = jiasu;
        }


        if (isattacking == true)
        {
            rb.velocity = new Vector2(xinput * 1f , rb.velocity.y);
        }
        movement();
        dash();
        flipcontroller();

        isgrounded = Physics2D.Raycast(transform.position, Vector2.down, groundcheck, whatisground);
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isattacking = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        donghuacontrol();

    }
    private void dash()
    {
        dashtime -=Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            dashtime = dashduration;
        }
               
    }
    private void movement()
    {
        if (isattacking == true)
        {
            return;
        }
        if (dashtime > 0)
        {
            rb.velocity = new Vector2(facingdir * dashspeed, 0);
        }
        else
        {
            if(jiasutime > 0&&jiasutime!=jiasu)
            {
                if(xinput > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x + jiasufudu, rb.velocity.y);
                }
                else if(xinput < 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x - jiasufudu, rb.velocity.y);
                }
                
            }
            else if(jiasutime==jiasu)
            {
                if (rb.velocity.x>0)
                {
                    rb.velocity = new Vector2(rb.velocity.x - jiasufudu, rb.velocity.y);
                    if (rb.velocity.x < 0)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                    }
                }
                else if (rb.velocity.x < 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x + jiasufudu, rb.velocity.y);
                    if (rb.velocity.x > 0)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                    }
                }
            }
        }

    }
    private void Jump()
    {
        if(isgrounded)
            rb.velocity = new Vector2(0, jumphight);
    }

    private void donghuacontrol()
    {
        moving = !(xinput == 0);

        anim.SetFloat("yvelocity",rb.velocity.y);

        anim.SetBool("moving", moving);
        anim.SetBool("isgrounded", isgrounded);
        anim.SetBool("isdashing", dashtime > 0);
        anim.SetBool("isattacking", isattacking);
        anim.SetInteger("combocount", combocount);
    }
    private void flip()
    {
        facingdir = facingdir * -1;
        facingright = !facingright;
        transform.Rotate(0, 180, 0);
    }
    private void flipcontroller()
    {   
        if(isattacking == true)
        {
            return;
        }
        if (rb.velocity.x > 0&& !facingright)
        {
            flip();
        }
        else if (rb.velocity.x < 0&&facingright)
        {
            flip();
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x,transform.position.y-groundcheck));
    }
    public void atkend()
    {
        isattacking = false;
    }
}
