using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    float speed = 5f;
    public GameObject Player;
    bool forward =true;
    public float xlimit = 0f;
    public float zlimit = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < xlimit && forward == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else if (transform.position.z > zlimit && forward == false)
        {
            transform.Translate(Vector3.forward *  Time.deltaTime * -speed);
        }
    }
}
