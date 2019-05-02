using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public bool atCity = false;
    float sizeValue = 8.5f;


    float movingSize = 8.5f;
    float zoomedSize = 6f;

    float sizeChangeValue = .04f;

    public float cameraSpeed = 9f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");


        if (!atCity) {
            transform.localPosition = new Vector3(transform.localPosition.x + (xAxis / 3), transform.localPosition.y + (yAxis / 3), transform.localPosition.z);

            if (sizeValue < movingSize)
            {
                sizeValue += sizeChangeValue;
            }
        }
        else
        {
            float step = cameraSpeed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0,-2.3f, -99.9f), step);
            if (sizeValue > zoomedSize)
            {
                sizeValue -= sizeChangeValue;
            }
        }


        Camera.main.orthographicSize = sizeValue;
    }
}
