using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform a;
    [SerializeField] Transform b;
    [SerializeField] bool isArrive;
    [SerializeField] bool isArrive2;
    // Update is called once per frame
    private  void Update()
    {

        if (isArrive)
        {
            transform.position = Vector2.MoveTowards(transform.position, b.position, speed * Time.deltaTime);
            if (transform.position.x == b.position.x)
            {
                isArrive = !isArrive;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, a.position, speed * Time.deltaTime);
            if (transform.position.x == a.position.x)
            {
                isArrive = !isArrive;
            }
        }


        if (isArrive2)
        {
            transform.position = Vector2.MoveTowards(transform.position, b.position, speed * Time.deltaTime);
            if (transform.position.y == b.position.y)
            {
                isArrive2 = !isArrive2;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, a.position, speed * Time.deltaTime);
            if (transform.position.y == a.position.y)
            {
                isArrive2 = !isArrive2;
            }
        }
    }
}
