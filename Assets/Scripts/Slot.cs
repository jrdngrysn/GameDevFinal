using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Location linkedLocation;
    public int distFromStart;
    public int distanceCounter = 0;
    bool subtracting = false;
    int distance = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(linkedLocation.locationInstance, transform);
        linkedLocation.locationSlot = distFromStart;

        linkedLocation.merchantMoney = Random.Range(17, 138);

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
            distanceCounter = 0;

            if (distFromStart != GameManager.Instance.currentLocation.locationSlot)
            {
                distance = Mathf.Abs(GameManager.Instance.currentLocation.locationSlot - distFromStart) + 1;
            }
            else
            {
                distance = 2;
            }
            GameManager.Instance.currentLocation = linkedLocation;
            subtracting = true;
            GameManager.Instance.phaseOfLocation = "moving";
        }
    }

    void SubtractDistance()
    {
        GameManager.Instance.daysLeft--;
        distanceCounter++;
        subtracting = true;
    }
}
