using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [Header("移动距离")]
    public float moveHeight = 3f;

    [Header("移动速度")]
    public float moveSpeed = 2f;


    [Header("停顿时间")]
    public float waitTime = 1.5f;


    Rigidbody2D rb;

    Vector2 startPosition;

    Vector2 topPosition;


    bool movingUp = true;

    bool waiting = false;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        startPosition = rb.position;

        topPosition =
        startPosition + Vector2.up * moveHeight;
    }



    void FixedUpdate()
    {

        if(waiting)
            return;



        Vector2 target;


        if(movingUp)
        {
            target = topPosition;
        }
        else
        {
            target = startPosition;
        }



        rb.MovePosition(
            Vector2.MoveTowards(
                rb.position,
                target,
                moveSpeed * Time.fixedDeltaTime
            )
        );



        //到达目标位置
        if(Vector2.Distance(rb.position,target)<0.01f)
        {
            StartCoroutine(WaitAtPoint());
        }

    }



    IEnumerator WaitAtPoint()
    {

        waiting=true;


        yield return new WaitForSeconds(waitTime);


        movingUp=!movingUp;


        waiting=false;

    }

}