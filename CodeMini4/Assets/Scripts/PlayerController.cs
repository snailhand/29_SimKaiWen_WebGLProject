using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Player variables declaration (PUBLIC)
    public float speed = 10f;
    public float airSpeed = 8f;
    public float gravityMod = 3f;
    public float maxSpeedChange = 10f;
    public float jumpHeight = 10f;
    public float fallSpeed = 20f;
    public float rotateSpeed = 25f;
    public int lifeCount = 3;

    //Player variables declaration (PRIVATE)
    private float aboveGround;
    private bool canMove = true;  //Player alive
    private bool isOnGround;
    private bool isStunned;  //Initial stun
    private bool stunnedState;  //enters stunned state if stunned
    private float pushForce;
    private Vector3 pushDirection;


    //Player Components referencing (PRIVATE)
    private Vector3 moveDirection;
    private Rigidbody playerRb;

    public GameObject cameraObject; //Camera Reference 
    public GameObject lifeText; //LivesCount Reference


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        //Displaying Lives Text
        lifeText.GetComponent<Text>().text = "Lives: " + lifeCount;

        //print(GameManager.gameManager.ballObject);

        if (canMove == true)
        {
            if (moveDirection.x != 0 || moveDirection.z != 0)
            {
                Vector3 playerDirection = moveDirection;

                playerDirection.y = 0;
                if (playerDirection == Vector3.zero)
                {
                    playerDirection = transform.forward;
                }
                Quaternion rotationPlayer = Quaternion.LookRotation(playerDirection);
                Quaternion playerRotation = Quaternion.Slerp(transform.rotation, rotationPlayer, Time.deltaTime * rotateSpeed);
                transform.rotation = playerRotation;
            }

            //Moving
            Moving();

            if (isOnGround == true)
            {

                //Jumping
                if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
                {
                    playerRb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                    isOnGround = false;
                }

            }

            //Player Drop down
            if (gameObject.transform.position.y < -11)
            {
                if (lifeCount > 0) //still have remaining lives
                {
                    transform.position = new Vector3(0, 1, 413);
                    lifeCount -= 1;
                }
            }
            if (lifeCount == 0)
            {
                SceneManager.LoadScene("LoseScene");
            }
           
        }

        Vector3 axisV = vAxis * cameraObject.transform.forward;
        Vector3 axisH = hAxis * cameraObject.transform.right;
        moveDirection = (axisH + axisV).normalized;
    }

    public void PlayerHit(Vector3 force, float time)
    {
        playerRb.velocity = force;
        pushForce = force.magnitude;
        pushDirection = Vector3.Normalize(force);
        StartCoroutine(Decrease(force.magnitude, time));

    }
    private IEnumerator Decrease(float value, float duration)
    {
        if (isStunned == true)
        {
            stunnedState = true;
        }
        isStunned = true;
        canMove = false;

        float forceTime = 0;
        forceTime = value / duration;

        //Reducing the force after stunned
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            yield return null;
            pushForce = pushForce - Time.deltaTime * forceTime;
            if (pushForce < 0)
            {
                pushForce = 0;
            }
            playerRb.AddForce(new Vector3(0, -gravityMod * GetComponent<Rigidbody>().mass, 0)); //Adding gravity
        }

        //Constant updating of Player's stunned state
        if (stunnedState == true)
        {
            stunnedState = false;
        }
        else
        {
            isStunned = false;
            canMove = true;
        }
    }
    
    
    private void Moving()
    {
        //lol this took me longer than I should've

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

}
    