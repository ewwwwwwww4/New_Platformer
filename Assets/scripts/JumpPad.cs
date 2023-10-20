using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    //Rigidbody2D myBody;
    Animator myAnim;
    //SpriteRenderer myRend;
    bool falling = false;
    private void Start()
    {
       // myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
       // myRend = GetComponent<SpriteRenderer>();
    }

    public float jumpForce = 10f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.transform.position.y > transform.position.y)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            myAnim.SetBool("falling", true);
        }
        else { myAnim.SetBool("falling", false); }
}
    }
   
