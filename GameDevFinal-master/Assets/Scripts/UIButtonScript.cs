using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonScript : MonoBehaviour
{

     Animator animator;
     public string buttonType;
     public string good;
     public Location emptyLocation;
     public Text changeableTextPrice;
     public Text changeableTextCount;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (buttonType == "stat")
        {
            animator.SetBool("PressedBool", true);
        }
      
    }

    private void Update()
    {
        if (GameManager.Instance.phaseOfLocation != "moving" && GameManager.Instance.phaseOfLocation != "leaving") {
            if (buttonType == "sell")
            {
                if (good == "nut")
                {
                    changeableTextPrice.text = "$ " + GameManager.Instance.currentLocation.nutMultiplier.ToString();
                    changeableTextCount.text = "x" + GameManager.Instance.spices.ToString();
                }
                else if (good == "battery")
                {
                    changeableTextPrice.text = "$ " + GameManager.Instance.currentLocation.batteryMultiplier.ToString();
                    changeableTextCount.text = "x" + GameManager.Instance.salts.ToString();
                }
                else if (good == "circuit")
                {
                    changeableTextPrice.text = "$ " + GameManager.Instance.currentLocation.circuitMultiplier.ToString();
                    changeableTextCount.text = "x" + GameManager.Instance.arts.ToString();
                }
            }
            else if (buttonType == "buy")
            {
                if (good == "nut")
                {
                    changeableTextPrice.text = "$ " + GameManager.Instance.currentLocation.nutMultiplierBuy.ToString();
                    changeableTextCount.text = "x" + GameManager.Instance.currentLocation.nutCount.ToString();

                }
                else if (good == "battery")
                {
                    changeableTextPrice.text = "$ " + GameManager.Instance.currentLocation.batteryMultiplierBuy.ToString();
                    changeableTextCount.text = "x" + GameManager.Instance.currentLocation.batteryCount.ToString();
                }
                else if (good == "circuit")
                {
                    changeableTextPrice.text = "$ " + GameManager.Instance.currentLocation.circuitMultiplierBuy.ToString();
                    changeableTextCount.text = "x" + GameManager.Instance.currentLocation.circuitCount.ToString();
                }
            }
            }


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

    public void LeaveClick ()
    {
        animator.SetBool("Shown", false);
        GameManager.Instance.emptyLocation.locationSlot = GameManager.Instance.currentLocation.locationSlot;
        GameManager.Instance.prevLocationName = GameManager.Instance.currentLocation.locationTitle;
        GameManager.Instance.phaseOfLocation = "leaving";

        GameManager.Instance.currentLocation = emptyLocation;
        //Debug.Log("click");
       
        Camera.main.GetComponent<CameraScript>().atCity = false;
       

    }

    public void BuyNuts()
    {
        if (GameManager.Instance.currentLocation.nutCount > 0 && GameManager.Instance.coins >= GameManager.Instance.currentLocation.nutMultiplierBuy)
        {
            GameManager.Instance.spices++;
            GameManager.Instance.coins -= GameManager.Instance.currentLocation.nutMultiplierBuy;
            GameManager.Instance.currentLocation.merchantMoney += GameManager.Instance.currentLocation.nutMultiplierBuy;
            GameManager.Instance.currentLocation.nutCount--;
            GameManager.Instance.PlayMoneySound();
        }
        else
        {
            GameManager.Instance.PlayWrongSound();
        }
    }

    public void BuyBatteries()
    {
        if (GameManager.Instance.currentLocation.batteryCount > 0 && GameManager.Instance.coins >= GameManager.Instance.currentLocation.batteryMultiplierBuy)
        {
            GameManager.Instance.salts++;
            GameManager.Instance.coins -= GameManager.Instance.currentLocation.batteryMultiplierBuy;
            GameManager.Instance.currentLocation.merchantMoney += GameManager.Instance.currentLocation.nutMultiplierBuy;
            GameManager.Instance.currentLocation.batteryCount--;
            GameManager.Instance.PlayMoneySound();
        }
        else
        {
            GameManager.Instance.PlayWrongSound();
        }
    }
    public void BuyCirbuits()
    {
        if (GameManager.Instance.currentLocation.circuitCount > 0 && GameManager.Instance.coins >= GameManager.Instance.currentLocation.circuitMultiplierBuy)
        {
            GameManager.Instance.arts++;
            GameManager.Instance.coins -= GameManager.Instance.currentLocation.circuitMultiplierBuy;
            GameManager.Instance.currentLocation.merchantMoney += GameManager.Instance.currentLocation.nutMultiplierBuy;
            GameManager.Instance.currentLocation.circuitCount--;
            GameManager.Instance.PlayMoneySound();
        }
        else
        {
            GameManager.Instance.PlayWrongSound();
        }
    }


    public void SellNuts()
    {
        if (GameManager.Instance.spices > 0 && GameManager.Instance.currentLocation.merchantMoney >= GameManager.Instance.currentLocation.nutMultiplier)
        {
            GameManager.Instance.spices--;
            GameManager.Instance.coins += GameManager.Instance.currentLocation.nutMultiplier;
            GameManager.Instance.currentLocation.merchantMoney -= GameManager.Instance.currentLocation.nutMultiplier;
            GameManager.Instance.currentLocation.nutCount++;
            GameManager.Instance.PlayMoneySound();
        }
        else
        {
            GameManager.Instance.PlayWrongSound();
        }
    }

    public void SellBatteries()
    {
        if (GameManager.Instance.salts > 0 && GameManager.Instance.currentLocation.merchantMoney >= GameManager.Instance.currentLocation.batteryMultiplier)
        {
            GameManager.Instance.salts--;
            GameManager.Instance.coins += GameManager.Instance.currentLocation.batteryMultiplier;
            GameManager.Instance.currentLocation.merchantMoney -= GameManager.Instance.currentLocation.nutMultiplier;
            GameManager.Instance.currentLocation.batteryCount++;
            GameManager.Instance.PlayMoneySound();
        }
        else
        {
            GameManager.Instance.PlayWrongSound();
        }
    }
    public void SellCirbuits()
    {
        if (GameManager.Instance.arts > 0 && GameManager.Instance.currentLocation.merchantMoney >= GameManager.Instance.currentLocation.circuitMultiplier)
        {
            GameManager.Instance.arts--;
            GameManager.Instance.coins += GameManager.Instance.currentLocation.circuitMultiplier;
            GameManager.Instance.currentLocation.merchantMoney -= GameManager.Instance.currentLocation.nutMultiplier;
            GameManager.Instance.currentLocation.circuitCount++;
            GameManager.Instance.PlayMoneySound();
        }
        else
        {
            GameManager.Instance.PlayWrongSound();
        }
    }
    public void PlayClickSound() 
    {
        GameManager.Instance.PlayClickSound();
    }



    //    public GameObject movableUI;
    //    Vector3 startingPos;
    //    public GameObject lerpTarget;
    //    Vector3 endingPos;
    //    public float lerpTime = .1f;
    //    bool isLerping = false;
    //    string textPosition = "lower";
    //    public bool isMoving = false;


    //    // Start is called before the first frame update
    //    void Start()
    //    {
    //        startingPos = movableUI.transform.localPosition;
    //        endingPos = lerpTarget.transform.localPosition;
    //    }

    //    // Update is called once per frame
    //    void Update()
    //    {
    //        if(isMoving)
    //        {
    //        RectTransform rtr = this.GetComponent<RectTransform>();
    //        Vector2 endPos = new Vector2(rtr.anchoredPosition.x, rtr.anchoredPosition.y + 50f);
    //        //rtr.anchoredPosition = Vector2.MoveTowards(rtr.anchoredPosition, endPos, 3f * Time.deltaTime);
    //        rtr.anchoredPosition = new Vector2(rtr.anchoredPosition.x - 100f, rtr.anchoredPosition.y - 1000f);

    //        }
    //    //if (isLerping)
    //    //{
    //    //    if (transform.localPosition.y <= endingPos.y)
    //    //    {
    //    //        movableUI.transform.localPosition = Vector3.Lerp(startingPos, endingPos, Time.time / lerpTime);
    //    //        //Debug.Log("lerp up");

    //    //    } else if (transform.localPosition.y >= startingPos.y)
    //    //    {
    //    //        movableUI.transform.localPosition = Vector3.Lerp(endingPos, startingPos, Time.time / lerpTime);
    //    //        //Debug.Log("lerp down");
    //    //    }
    //    //}
    //}

    //public void WhenClicked()
    //{
    //RectTransform rtr = this.GetComponent<RectTransform>();

    //Debug.Log(rtr.anchoredPosition);
    //isMoving = !isMoving;

    //    //if (textPosition == "lower")
    //    //{
    //    //    isLerping = true;
    //    //    textPosition = "upper";

    //    //} else if (textPosition == "upper")
    //    //{
    //    //    isLerping = true;
    //    //    textPosition = "lower";
    //    //}

    //}
}
