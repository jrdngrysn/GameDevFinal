using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonScript : MonoBehaviour
{

     Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("PressedBool", false);
    }

   

    public void WhenCLicked()
    {
        if (animator.GetBool("PressedBool") == false)
        {
            animator.SetBool("PressedBool", true);
        }
        else
        {
            animator.SetBool("PressedBool", false);
        }

    }










    //    public GameObject movableUI;
    //    Vector3 startingPos;
    //    public GameObject lerpTarget;
    //    Vector3 endingPos;
    //    public float lerpTime = .1f;
    //    bool isLerping = false;
    //    string textPosition = "lower";



    //    // Start is called before the first frame update
    //    void Start()
    //    {
    //        startingPos = movableUI.transform.localPosition;
    //        endingPos = lerpTarget.transform.localPosition;
    //    }

    //    // Update is called once per frame
    //    void Update()
    //    {

    //        if (isLerping)
    //        {
    //            if (transform.localPosition.y <= endingPos.y)
    //            {
    //                movableUI.transform.localPosition = Vector3.Lerp(startingPos, endingPos, Time.time / lerpTime);
    //                //Debug.Log("lerp up");

    //            } else if (transform.localPosition.y >= startingPos.y)
    //            {
    //                movableUI.transform.localPosition = Vector3.Lerp(endingPos, startingPos, Time.time / lerpTime);
    //                //Debug.Log("lerp down");
    //            }
    //        }
    //    }

    //    public void WhenClicked()
    //    {
    //        if (textPosition == "lower")
    //        {
    //            isLerping = true;
    //            textPosition = "upper";

    //        } else if (textPosition == "upper")
    //        {
    //            isLerping = true;
    //            textPosition = "lower";
    //        }

    //    }
}
