﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Location linkedLocation;
    public int distFromStart;
    public int distanceCounter = 0;
    bool subtracting = false;
    int distance = 0;

    public GameObject nutIconMap;
    public GameObject batteryIconMap;
    public GameObject circuitIconMap;

    Vector3 nutStart;
    Vector3 batteryStart;
    Vector3 circuitStart;

    public bool nutsLocation;
    public bool batteriesLocation;
    public bool circuitsLocation;

    public TextMesh dayText;
    public LabelFloat floatScript;

    public int prevDaysLeft;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(linkedLocation.locationInstance, transform);
        linkedLocation.locationSlot = distFromStart;

        SetResources();

        nutIconMap = GameObject.Find(linkedLocation.locationTitle + "(Clone)/Box/Nut");
        batteryIconMap = GameObject.Find(linkedLocation.locationTitle + "(Clone)/Box/Battery");
        circuitIconMap = GameObject.Find(linkedLocation.locationTitle + "(Clone)/Box/Circuit");

        nutStart = nutIconMap.transform.localPosition;
        batteryStart = batteryIconMap.transform.localPosition;
        circuitStart = circuitIconMap.transform.localPosition;


        floatScript = GameObject.Find(linkedLocation.locationTitle + "(Clone)/Box").GetComponent<LabelFloat>();
        floatScript.dayText = dayText;

        prevDaysLeft = 50;

        SetIcons();
    }
    private void Update()
    {
       

        CheckResources();
        SetIcons();
        SetDayText();
        ChangeResources();
    }

    void ChangeResources()
    {
        if (GameManager.Instance.daysLeft < prevDaysLeft)
        {
            int rand;
            if (linkedLocation.nuts)
            {
                rand = Random.Range(0, 1);

                if (rand == 0 && linkedLocation.nutMultiplier > 10)
                {
                    linkedLocation.nutMultiplier -= 2;
                    linkedLocation.nutMultiplierBuy++;
                }
                else if (linkedLocation.nutMultiplierBuy > 10 && linkedLocation.nutMultiplier < linkedLocation.nutMultiplierBuy)
                {
                    linkedLocation.nutMultiplier++;
                    linkedLocation.nutMultiplierBuy--;
                }
                if (linkedLocation.nutMultiplier > linkedLocation.nutMultiplierBuy)
                {
                    linkedLocation.nutMultiplier = linkedLocation.nutMultiplierBuy;
                }
            }
            if (linkedLocation.batteries)
            {
                rand = Random.Range(0, 1);

                if (rand == 0 && linkedLocation.batteryMultiplier > 10)
                {
                    linkedLocation.batteryMultiplier -= 2;
                    linkedLocation.batteryMultiplierBuy++;
                }
                else if(linkedLocation.batteryMultiplierBuy > 10 && linkedLocation.batteryMultiplier < linkedLocation.batteryMultiplierBuy)
                {
                    linkedLocation.batteryMultiplier++;
                    linkedLocation.batteryMultiplierBuy--;
                }
                if (linkedLocation.batteryMultiplier > linkedLocation.batteryMultiplierBuy)
                {
                    linkedLocation.batteryMultiplier = linkedLocation.batteryMultiplierBuy;
                }
            }

            if (linkedLocation.circuits)
            {
                rand = Random.Range(0, 1);

                if (rand == 0 && linkedLocation.circuitMultiplier > 10)
                {
                    linkedLocation.circuitMultiplier -= 2;
                    linkedLocation.circuitMultiplierBuy++;
                }
                else if (linkedLocation.circuitMultiplierBuy > 10 && linkedLocation.circuitMultiplier < linkedLocation.circuitMultiplierBuy)
                {
                    linkedLocation.circuitMultiplier++;
                    linkedLocation.circuitMultiplierBuy--;
                }

                if (linkedLocation.circuitMultiplier > linkedLocation.circuitMultiplierBuy)
                {
                    linkedLocation.circuitMultiplier = linkedLocation.circuitMultiplierBuy;
                }
            }
            prevDaysLeft--;
        }
    }

    void SetResources()
    {
        linkedLocation.merchantMoney = Random.Range(27, 482);

        nutsLocation = linkedLocation.nuts;
        batteriesLocation = linkedLocation.batteries;
        circuitsLocation = linkedLocation.circuits;

        if (linkedLocation.nuts)
        {
            linkedLocation.nutCount = Random.Range(1, 10);
        }

        if (linkedLocation.batteries)
        {
            linkedLocation.batteryCount = Random.Range(1, 10);
        }

        if (linkedLocation.circuits)
        {
            linkedLocation.circuitCount = Random.Range(1, 10);
        }
    }

    void SetDayText()
    {
        int dist;

        if (GameManager.Instance.prevLocationName != linkedLocation.locationTitle)
        {
            if (distFromStart != GameManager.Instance.currentLocation.locationSlot)
            {
                dist = Mathf.Abs(GameManager.Instance.currentLocation.locationSlot - distFromStart) + 1;
            }
            else
            {
                dist = 2;
            }
        }
        else
        {
            dist = 0;
        }

        dayText.text = dist.ToString() + " Days Away";
    }

    void CheckResources ()
    {
        if (linkedLocation.nutCount < 1)
        {
            nutsLocation = false;
        }

        if (linkedLocation.batteryCount < 1)
        {
            batteriesLocation = false;
        }

        if (linkedLocation.circuitCount < 1)
        {
            circuitsLocation = false;
        }
    }

    void SetIcons()
    {
        nutIconMap.transform.localPosition = nutStart;
        batteryIconMap.transform.localPosition = batteryStart;
        circuitIconMap.transform.localPosition = circuitStart;

        if (batteriesLocation)
        {
            batteryIconMap.SetActive(true);
        }
        else
        {
            batteryIconMap.SetActive(false);
            nutIconMap.transform.localPosition = circuitStart;
            circuitIconMap.transform.localPosition = batteryStart;
        }

        if (circuitsLocation)
        {
            circuitIconMap.SetActive(true);
        }
        else
        {
            circuitIconMap.SetActive(false);
            nutIconMap.transform.localPosition = circuitStart;
        }

        if (nutsLocation)
        {
            nutIconMap.SetActive(true);
        }
        else
        {
            nutIconMap.SetActive(false);


        }

      
    }

    private void OnMouseDown()
    {

        if (GameManager.Instance.phaseOfLocation == "leaving")
        {
            GameManager.Instance.PlayBlipSound();
            distanceCounter = 0;
            if (GameManager.Instance.prevLocationName != linkedLocation.locationTitle)
            {
                if (distFromStart != GameManager.Instance.currentLocation.locationSlot)
                {
                    distance = Mathf.Abs(GameManager.Instance.currentLocation.locationSlot - distFromStart) + 1;
                }
                else
                {
                    distance = 2;
                }
            }
            else
            {
                distance = 0;
            }
            GameManager.Instance.currentLocation = linkedLocation;
            subtracting = true;
            GameManager.Instance.phaseOfLocation = "moving";
            SubtractDistance();
        }
    }

    private void OnMouseEnter()
    {
        GetComponentInChildren<LabelFloat>().selected = true;
    }

    private void OnMouseExit()
    {
        Invoke("FlipBool", .7f);
    }

    void FlipBool()
    {
        GetComponentInChildren<LabelFloat>().selected = false;
    }

    void SubtractDistance()
    {
        if (distanceCounter < distance)
        {
            GameManager.Instance.daysLeft--;
            distanceCounter++;
            Invoke("SubtractDistance", .7f);
        }
        else
        {
            CancelInvoke();
        }
    }
}
