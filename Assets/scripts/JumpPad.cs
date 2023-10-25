using UnityEngine;

public class JumpPad : MonoBehaviour
{
    Animator myAnim;
    bool isPlayerOnPad = false;

    private void Start()
    {
        myAnim = GetComponent < Animator >();
    }

    public float jumpForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.transform.position.y > transform.position.y)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            myAnim.SetBool("falling", true);
            isPlayerOnPad = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isPlayerOnPad)
        {
            myAnim.SetBool("falling", false);
            isPlayerOnPad = false;
        }
    }
}

