﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "MerchantRun/Location")]
public class Location : ScriptableObject
{
    //scriptable object containing question text to be displayed and an array of answers

    public string locationTitle;
    public int locationSlot;

    public GameObject locationInstance;

    [Header("Products")]
    public bool nuts;
    public bool batteries;
    public bool circuits;

    [Header("Sell Values")]
    public int nutMultiplier;
    public int batteryMultiplier;
    public int circuitMultiplier;

    [Header("Buy Values")]
    public int nutMultiplierBuy;
    public int batteryMultiplierBuy;
    public int circuitMultiplierBuy;

    [Header("Money & Resources")]
    public int merchantMoney;

    public int nutCount;
    public int batteryCount;
    public int circuitCount;

    [Header("Things to do")]
    public Activity sellWares;
    public Activity buyWares;
    public Activity leave;


    [Header("Places to Go")]
    public Location[] locations;

}
