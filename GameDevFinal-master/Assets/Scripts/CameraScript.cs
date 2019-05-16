using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public bool atCity = false;


    float sizeValue = 8.5f;

    float movingSize = 7f;
    float zoomedSize = 5.5f;

    float sizeChangeValue = .04f;

    public float cameraSpeed = 9f;

    private Vector3 Origin;
    private Vector3 Diference;

    private bool Drag = false;


    Vector2 xBounds = new Vector2(-21,4);
    Vector2 yBounds = new Vector2(-22,-1);

    void LateUpdate()
    {
        if (!atCity)
        {
            if (GameManager.Instance.phaseOfLocation != "moving")
            {
                if (Input.GetMouseButton(0))
                {
                    Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;

                    if (Drag == false)
                    {
                        Drag = true;
                        Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    }
                }
                else
                {
                    Drag = false;
                }


                if (Drag == true)
                {
                    Camera.main.transform.position = Origin - Diference;
                }

                CheckBounds();

            }
            else
            {
                float step = cameraSpeed * Time.deltaTime;
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, -2.3f, -99.9f), step);

            }
             if (sizeValue < movingSize)
                {
                    sizeValue += sizeChangeValue;
                }
        
    }
        else
        {
            float step = cameraSpeed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, -2.3f, -99.9f), step);


            if (sizeValue > zoomedSize)
            {
                sizeValue -= sizeChangeValue;
            }
        }

        Camera.main.orthographicSize = sizeValue;
    }

    void CheckBounds ()
    {


        if (gameObject.transform.position.x > 27f)
        {
            gameObject.transform.position = new Vector3(27f, gameObject.transform.position.y, gameObject.transform.position.z);
        } else if (gameObject.transform.position.x < -26f)
        {
            gameObject.transform.position = new Vector3(-26f, gameObject.transform.position.y, gameObject.transform.position.z);
        }


        if (gameObject.transform.position.y > 30f)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 30f, gameObject.transform.position.z);
        } else if (transform.position.y < -10f)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, -10f, gameObject.transform.position.z);
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    float xAxis = Input.GetAxis("Horizontal");
    //    float yAxis = Input.GetAxis("Vertical");


    //if (!atCity) {
    //if (GameManager.Instance.phaseOfLocation != "moving")
    //{
    //            transform.localPosition = new Vector3(transform.localPosition.x + (xAxis / 3), transform.localPosition.y + (yAxis / 3), transform.localPosition.z);
    //        }
    //        else
    //        {
    //float step = cameraSpeed * Time.deltaTime;
    //transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, -2.3f, -99.9f), step);
    //        }
    //        if (sizeValue < movingSize)
    //        {
    //            sizeValue += sizeChangeValue;
    //        }
    //    }
    //else
    //{
    //float step = cameraSpeed * Time.deltaTime;
    //transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0,-2.3f, -99.9f), step);
    //if (sizeValue > zoomedSize)
    //{
    //    sizeValue -= sizeChangeValue;
    //}
    //    }


    //    Camera.main.orthographicSize = sizeValue;
    //}

}
