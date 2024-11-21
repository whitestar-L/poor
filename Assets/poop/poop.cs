using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poop : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public Transform groundcube;
    private int facingdir = 1;
    private bool facingright = true;
    public float groundcheck;
    private bool isgrounded;
    public LayerMask whatisground;
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        isgrounded = Physics2D.Raycast(groundcube.transform.position, Vector2.down, groundcheck, whatisground);
        rb.velocity = new Vector2(facingdir * 1,0);
        flipcontroller();
    }
    private void flip()
    {
        facingdir = facingdir * -1;
        facingright = !facingright;
        transform.Rotate(0, 180, 0);
    }
    private void flipcontroller()
    {
        if (isgrounded==false)
        {
            flip();
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundcube.transform.position, new Vector3(groundcube.transform.position.x, groundcube.transform.position.y - groundcheck));
    }
}
