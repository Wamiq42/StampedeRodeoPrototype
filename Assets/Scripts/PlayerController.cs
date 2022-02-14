using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    public Rigidbody playerRb;
    public Transform cameraTransform;
    public Transform landingArea;
    public GameObject landingAreaGameObject;
    public bool onAnimal = false;


    private GameObject animal;
    private Vector3 cameraOffset = new Vector3(0, 6.4f, -7.9f);
    private float speed = 5.0f;
    private float horizontalInput;
 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollowsPlayer();
        PlayerMovement();
       
    }

    /*For Player Jumping
     * used the velocity method i made here.
     */
    void Jumping()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            landingAreaGameObject.SetActive(true);
            if (onAnimal)
            {
                animal.transform.parent = null;
                Destroy(animal);
                onAnimal = false;
            }

            playerRb.velocity = VelocityCalculator(landingArea.position, transform.position, 1.0f);
        }
    }

    /*PlayerMovement
     * normal basic player movement;
     * 
     */
    void PlayerMovement()
    {
        Jumping();

        if (!onAnimal)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            playerAnimator.SetFloat("Speed_f", 1.0f);
        }

        else if (onAnimal)
        {
           
            horizontalInput = Input.GetAxis("Horizontal");
            playerAnimator.SetFloat("Speed_f", 0);
            transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
            transform.Translate(Vector3.forward * speed * Time.deltaTime * 2);
        }
    }



    /*This method return me a value of vector3 which is actually the velocity that i need to apply
     * along the approximate axis. I used the 9th or 10th class physics distance formulae that were some like
     * S1= Vit
     * S2= Vit + 1/2 at^2
     * in this case we dont need acceleration since we need a parabolic arc so i used gravity.
     * S2= Vit + 1/2 gt^2
     * and derived a formulae for velocity over Y-axis.
     */
    Vector3 VelocityCalculator(Vector3 target, Vector3 Origin, float time)
    {
        Vector3 distance = target - Origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0.5f;

        float distanceAtPositionY = distance.y;
        float distanceAtPositionXandZ = distanceXZ.magnitude;

        float velocityAtPositionX = distanceAtPositionXandZ / time;
        float velocityAtPositionY = distanceAtPositionY / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= velocityAtPositionX;
        result.y = velocityAtPositionY * 1.5f;

        return result;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LandingZone"))
        {
            playerRb.velocity = new Vector3(0, 0, 0);
            landingArea.position += new Vector3(0, 0, 7.5f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Animal"))
        {
            onAnimal = true;
            animal = collision.gameObject;
            animal.GetComponent<AnimalController>().setTamed(true);
            animal.transform.parent = transform;
        }
        if(collision.gameObject.CompareTag("Ground"))
        {
           landingAreaGameObject.SetActive(false);
        }
    }






    /*This method is used to make camera follow player
     * Using Transform.
     */
    void CameraFollowsPlayer()
    {
        cameraTransform.position = transform.position + cameraOffset;
    }
}

