using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject animals;

    private Transform playerTransform;
    private float range = 8;
    private float startTime = 1.0f;
    private float intervalTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        InvokeRepeating("SpawnRandomAnimals", startTime, intervalTime);
    }

    private void Update()
    {
        if (playerTransform.position.z > transform.position.z)
            transform.position += new Vector3(0,0,50);
    }
    void SpawnRandomAnimals()
    {

        Instantiate(animals, transform.position + new Vector3(Random.Range(-range,range),0.5f,0), animals.transform.rotation);
    }

   
}
