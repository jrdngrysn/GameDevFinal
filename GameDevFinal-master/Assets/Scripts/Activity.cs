using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Activity
{
    //serializable class for answers
    //keeps track of the answer description to be displayed
    //keeps track of if the answer is good or bad
    //keeps track of the next sprite to display if the answer if chosen

    [TextArea]
    public string activityDescription;
    [TextArea]
    public string activityResult;

    public int coinChange;
    public int spiceChange;
    public int saltChange;
    public int artChange;



}
