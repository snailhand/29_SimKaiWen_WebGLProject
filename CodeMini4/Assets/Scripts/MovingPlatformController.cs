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
    bool movingP ;
    // Start is called before the first frame update
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
            forward = true; // go 20 >40
        }   
        if (transform.position.z >= startPos && transform.position.z >= zlimit) //20
        {
            forward = false; //40 to 20
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            movingP = true;
            
        }
    }
}
