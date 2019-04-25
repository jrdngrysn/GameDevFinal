using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        transform.localPosition = new Vector3(transform.localPosition.x + (xAxis / 3), transform.localPosition.y + (yAxis / 3), transform.localPosition.z);

    }
}
