using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    //list of all questions
    public List<Location> allLocations = new List<Location>(7);
   
    //the ending questions for winning and losing
    public Location endWin;
    public Location endLose;

    //setting up the question manager as a singleton
    public static QuestionManager Instance;



    // Start is called before the first frame update
    private void Start()
    {
        //instancing this script
        Instance = this;
    }

    void Update()
    {
        //navigating the buttons and questions

        ButtonNavigation();

        ////when the player presses enter, navigate the questions
        //if (!GameManager.Instance.endingReached && Input.GetKeyDown(KeyCode.Return))
        //{
        //    QuestionNavigation();
        //}
    }


    public void ButtonNavigation()
    {
        //this function keeps track of player input on the buttons

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (GameManager.Instance.buttonHover == "right")
            {
                GameManager.Instance.buttonHover = "left";
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (GameManager.Instance.buttonHover == "left")
            {
                GameManager.Instance.buttonHover = "right";
            }
        }
    }

    //public void QuestionNavigation () { 
    
        ////this function cycles to the next question when the player selects an answer, logs whether that answer was good and or bad, and cycles to a new sprite according to the selected answer
        ////once the ending is reached it sets the current question to the ending the player received

        //    if (GameManager.Instance.buttonHover == "left")
        //    {
        //        GameManager.Instance.currentCatSprite = GameManager.Instance.currentLocation.answers[0].nextSprite;

        //        if (GameManager.Instance.currentLocation.answers[0].goodOrBad == "good")
        //        {
        //            GameManager.Instance.goodAnswers++;
        //        }
        //        else
        //        {
        //            GameManager.Instance.badAnswers++;
        //        }
        //    }
        //    else
        //    {
        //        GameManager.Instance.currentCatSprite = GameManager.Instance.currentLocation.answers[1].nextSprite;

        //        if (GameManager.Instance.currentLocation.answers[1].goodOrBad == "good")
        //        {
        //            GameManager.Instance.goodAnswers++;
        //        }
        //        else
        //        {
        //            GameManager.Instance.badAnswers++;
        //        }
        //    }



        //    if (GameManager.Instance.questionCounter < 6)
        //    {
        //        GameManager.Instance.questionCounter++;

        //    }
        //    else
        //    {
        //        GameManager.Instance.endingReached = true;

        //        if (GameManager.Instance.goodAnswers > GameManager.Instance.badAnswers)
        //        {
        //            GameManager.Instance.currentLocation = endWin;
        //        }
        //        else
        //        {
        //            GameManager.Instance.currentLocation = endLose;
        //        }
        //    }
        //}
    }


