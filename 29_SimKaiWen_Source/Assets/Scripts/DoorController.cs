using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    //Moving Door variables declaration (PUBLIC)
    public float speed = 2f;
    public bool isDown = true;  //If wall is down
    public bool isRandom = true;  //If you want the door to go down randomly

    //Moving Door variables declaration (PRIVATE)
    private float planeHeight;  //Height of the plane
    private float positionY;  //initial position of Y axis
    private bool isWaiting = false; //If wall is waiting up or down
    private bool ready = true; //True if wall is ready to move up or down

    public GameObject Player;

    void Start()
    {
        planeHeight = transform.localScale.y;
        if(isDown)
        {
            positionY = transform.position.y;
        }
        else
        {
            positionY = transform.position.y - planeHeight;
        }
    }

    // Update is called once per frame
    void Update()
    { 
        if (isDown == true)  //Checks if Door is down
        {
            if (transform.position.y < positionY + planeHeight)
            {
                transform.position += Vector3.up * Time.deltaTime * speed;
            }
            else if (isWaiting == false)
            {
                StartCoroutine(Waiting(0.25f));
            }
        }
        else if (isDown == false)  //Checks if Door is up
        {
            if (ready == false)
            {
                return;
            }

            if(transform.position.y > positionY)
            {
                transform.position -= Vector3.up * Time.deltaTime * speed;
            }
            else if (isWaiting == false)
            {
                StartCoroutine(Waiting(0.25f));
            }
        }
    }

    //Method for the Door to travel up and down
    IEnumerator Waiting(float time)
    {
        isWaiting = true;
        yield return new WaitForSeconds(time);

        //Going up
        isWaiting = false;
        isDown = !isDown;

        if (isRandom == true && !isDown) //When wall is up and is randomised
        {
            int number = Random.Range(0, 3);
            //Debug.Log(number);

            if (number != 1)
            {
                StartCoroutine(Retry(1.5f));
            }
        }
    }

    //Method that checks every 1.25 seconds if the wall is ready
    IEnumerator Retry(float time)
    {
        ready = false;
        yield return new WaitForSeconds(time);
        int number = Random.Range(0, 4);
        //Debug.Log("2-"+number);

        if (number != 1)
        {
            StartCoroutine(Retry(1.25f));
        }
        else
        {
            ready = true;
        }
    }
}
