using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Location linkedLocation;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(linkedLocation.locationInstance, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        GameManager.Instance.currentLocation = linkedLocation;
    }
}
