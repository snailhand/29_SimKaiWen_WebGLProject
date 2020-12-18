using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Camera variables declaration (PUBLIC)
    public float followSpeed = 3f;  //Speed of the camera
    public float mouseSpeed = 3f; //Speed of camera's rotation
    public float cameraDistance = 3f; //Distance to which the camera is located

    public Transform player; //Object camera follows
    public Transform pivot; //Pivot on which the camera rotates
    public Transform cameraPos; //Camera Position

    private float turnSmoothing = 0.1f; //Smoothing of camera movement

    //Min and Max angle camera reaches (TESTING)
    public float minAngle = -35;
    public float maxAngle = 35;

    //Camera variables declaration (PRIVATE)
    float smoothX;
    float smoothY;
    float xSpeed;
    float ySpeed;

    //Camera variables declaration (PUBLIC)
    public float lookAngle;  //Angle on Y-Axis
    public float tiltAngle;  //Angle has up / down

    public void Init()
    {
        cameraPos = Camera.main.transform;
        pivot = cameraPos.parent;
    }

    private void FollowPlayer(float t)
    {
        //Method that makes camera follow Player
        float speed = t * followSpeed;
        Vector3 playerPosition = Vector3.Lerp(transform.position, player.position, speed * 1.5f); //Bring camera closer to player
        transform.position = playerPosition; //Updating camera position
    }

    private void HandleRotations(float time, float vAxis, float hAxis, float playerSpeed)
    {
        //Method that rotates the camera correctly
        if (turnSmoothing > 0)
        {
            //Change value over time
            smoothX = Mathf.SmoothDamp(smoothX, hAxis, ref xSpeed, turnSmoothing);
            smoothY = Mathf.SmoothDamp(smoothY, vAxis, ref ySpeed, turnSmoothing);
        }
        else
        {
            smoothX = hAxis;
            smoothY = vAxis;
        }

        tiltAngle -= smoothY * playerSpeed; //Update the angle that camera is moving
        tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
        pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0); //Modifies the up / down angle

        lookAngle += smoothX * playerSpeed; //Updates the rotation angle in Y
        transform.rotation = Quaternion.Euler(0, lookAngle, 0);

        //Stopping the camera from going underneath the plaen
        if (tiltAngle < -15)
        {
            tiltAngle = -15;
        }
    }


    // Update is called once per frame
    void Update()
    {
        //Referencing horizontal and vertical axis
        float hAxis = Input.GetAxis("Mouse X");
        float vAxis = Input.GetAxis("Mouse Y");
        float playerSpeed = mouseSpeed;

        //Controls the position
        FollowPlayer(Time.deltaTime);
        //Controls the rotation
        HandleRotations(Time.deltaTime, vAxis, hAxis, playerSpeed);
    }

    public static CameraController singleton;
    private void Awake()
    {
        singleton = this; //Self Assigns
        Init();
    }
}
