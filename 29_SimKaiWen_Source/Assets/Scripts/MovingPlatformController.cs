using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public float speed = 0f;
    public GameObject Player;
    bool forward =true;
    public float zlimit = 0f;
    public float startPos = 20f;

    bool movingP;  //Condition is true when Player is riding the Moving Platform
    
    void Start()    
    {
        
    }

    // Update is called once per frame
    void Update()
    {     // y the fuck does it not go backward
        if (transform.position.z < zlimit && forward)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else if (transform.position.z > startPos && !forward)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * -speed);
        }

        if (transform.position.z <= zlimit && transform.position.z <= startPos) //40
        {
            forward = true; // go 20 to 40
        }   
        if (transform.position.z >= startPos && transform.position.z >= zlimit) //20
        {
            forward = false; //40 to 20
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //collision.collider is player Reference

        if (collision.gameObject.CompareTag("Player"))
        {
            movingP = true;  //riding
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        movingP = false; //Not riding
        collision.collider.transform.SetParent(null);
    }
}
