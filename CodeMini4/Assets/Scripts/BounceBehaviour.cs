using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBehaviour : MonoBehaviour
{
    //Variables declaration (PUBLIC)
    public float force = 100f;
    public float hitTime = 0.5f;

    //Direction it is going when collided with the Player.
    private Vector3 hitDirection; 

    private void OnCollisionEnter(Collision collision)
    {
        //Checking for collision position
        foreach (ContactPoint contact in collision.contacts)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                hitDirection = contact.normal; 
                //contact = contact point the collision hits
                //normal = perpenticular angle of the surface

                collision.gameObject.GetComponent<PlayerController>().PlayerHit(-hitDirection * force, hitTime); //Calling hitted method in Player Script
                return;
            }
        }
    }

    void Update()
    {
        
    }
}
