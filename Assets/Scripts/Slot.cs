using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Location linkedLocation;
    public int slotNumber;
    public int distanceCounter = 0;
    bool subtracting = false;
    int distance = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(linkedLocation.locationInstance, transform);
        linkedLocation.locationSlot = slotNumber;

    }
    private void Update()
    {
        if (subtracting)
        {
            if (distanceCounter <= distance)
            {
                subtracting = false;
                Invoke("SubtractDistance", .5f);
            }
            else
            {
                CancelInvoke();
                subtracting = false;
            }
        }
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.phaseOfLocation != "moving")
        {
            distanceCounter = 0;distance = Mathf.Abs(GameManager.Instance.currentLocation.locationSlot - slotNumber);
            GameManager.Instance.currentLocation = linkedLocation;
            subtracting = true;
        }
    }

    void SubtractDistance()
    {
        GameManager.Instance.daysLeft--;
        distanceCounter++;
        subtracting = true;
    }
}
