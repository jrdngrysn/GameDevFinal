using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "MerchantRun/Location")]
public class Location : ScriptableObject
{
    //scriptable object containing question text to be displayed and an array of answers

    public string locationTitle;
    public int locationSlot;

    public GameObject locationInstance;

    [Header("Good Values")]
    public int spiceMultiplier;
    public int saltMultiplier;
    public int artMultiplier;

    [Header("Things to do")]
    public Activity sellWares;
    public Activity buyWares;
    public Activity leave;


    [Header("Places to Go")]
    public Location[] locations;

}
