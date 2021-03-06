﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{

    public GameObject[] locationSlots;
    public Location[] allLocations;

    public Dictionary<string, Location> locationDictionary = new Dictionary<string, Location>();

    public static Randomizer Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        for (int i = 0; i < locationSlots.Length; i++)
        {
            SetLocation(i);

        }
    }

    void SetLocation (int i)
    {
        int rand = Random.Range(0, allLocations.Length);

        if (!locationDictionary.ContainsKey(allLocations[rand].locationTitle))
        {
            locationDictionary.Add(allLocations[rand].locationTitle, allLocations[rand]);
            locationSlots[i].GetComponent<Slot>().linkedLocation = allLocations[rand];
        }
        else
        {
            SetLocation(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
