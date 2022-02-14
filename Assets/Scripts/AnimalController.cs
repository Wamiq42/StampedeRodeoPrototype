using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    private float speed = 5.0f;
    private float horizontalInput;

    public bool isTamed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTamed)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Animal"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }


    public void setTamed(bool value)
    {
        isTamed = value;
    }
}
