using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController animalTamed;

    private int score = 0;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        animalTamed = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        

        time += Time.deltaTime;
        score = Mathf.RoundToInt(time);

        if(animalTamed.onAnimal)
        {
            time += 25;
        }


        Debug.Log("Score : " + score);
    }
}
