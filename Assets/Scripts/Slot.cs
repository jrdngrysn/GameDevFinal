using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Location linkedLocation;
    public int slotNumber;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(linkedLocation.locationInstance, transform);

    }
  

    private void OnMouseDown()
    {
        GameManager.Instance.currentLocation = linkedLocation;
    }
}
