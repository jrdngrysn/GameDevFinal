using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{

    public GameObject[] locationSlots;
    public Location[] allLocations;

    public Dictionary<string, Location> locationDictionary = new Dictionary<string, Location>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < locationSlots.Length; i++)
        {
           int rand = Random.Range(0, allLocations.Length);

            if (!locationDictionary.ContainsKey(allLocations[rand].locationTitle))
            {
                locationDictionary.Add(allLocations[rand].locationTitle, allLocations[rand]);
                locationSlots[i].GetComponent<Slot>().linkedLocation = allLocations[rand];
            }
            else
            {

            }



        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
