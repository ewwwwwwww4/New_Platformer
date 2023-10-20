using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform a;
    [SerializeField] Transform b;
    [SerializeField] bool isArrive;

    SpriteRenderer myRend;

    float horizontalFlip;
    private void Start()
    {
        myRend = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    private void Update()
    {
        horizontalFlip = Input.GetAxis("Horizontal");
      
        if (isArrive)
        {
            transform.position = Vector2.MoveTowards(transform.position, b.position, speed * Time.deltaTime);
            if (transform.position.x == b.position.x)
            {
                isArrive = !isArrive;
                myRend.flipX = true;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, a.position, speed * Time.deltaTime);
            if (transform.position.x == a.position.x)
            {
                isArrive = !isArrive;
                myRend.flipX = false;
            }
        }

    }
}
