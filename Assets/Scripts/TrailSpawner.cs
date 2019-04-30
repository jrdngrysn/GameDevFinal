using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSpawner : MonoBehaviour
{

    public GameObject currentLocationCoords;

    public GameObject dotToSpawn;

    public float journeySpeed = 4.0f;

    private float dotTimer = 0;

    public bool moving = true;
    // Start is called before the first frame update

    public static TrailSpawner Instance;

    void Start()
    {

            //setting the instance
            Instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        dotTimer += Time.deltaTime;

        currentLocationCoords = GameObject.Find(GameManager.Instance.currentLocation.locationTitle + "(Clone)");

        if (gameObject.transform.position.x != currentLocationCoords.transform.position.x && gameObject.transform.position.y != currentLocationCoords.transform.position.y)
        {
            moving = true;
           TrailMovement();

            if (dotTimer > .3f)
            {
                SpawnDots();
                dotTimer = 0;
            }
        }
        else
        {
            moving = false;
        }


    }


    void TrailMovement() {

        float step = journeySpeed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, currentLocationCoords.transform.position, step);



    }


    void SpawnDots()
    {
        Vector3 spawnPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 3);
        Instantiate(dotToSpawn, spawnPos, Quaternion.identity);
    }

}
