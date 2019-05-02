using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //ints to track good answers and bad ones
     public int coins = 3;
     public int spices = 0;
     public int salts = 0;
     public int arts = 0;
     public int daysLeft = 15;
     public TextMesh playerMoney;
     public TextMesh merchantMoney;

    
    //bool tracks if the player has reached the end
    [HideInInspector] public bool endingReached = false;
    //string to track which option the player is "hovering over"
    [HideInInspector] public string buttonHover = "left";
    //int to count what question the player is on
    [HideInInspector] public int questionCounter = 0;

    //references to the UI text
    public Text locationTextField;
    public Text statTextField;

    public string phaseOfLocation;

    string movementText;
    string welcomeText;
    string activityText;

    //tracking the current question
    public Location currentLocation;

    //setting up singleton of game manager
    public static GameManager Instance;

    public Dictionary<string, Location> locationDictionary = new Dictionary<string, Location>();

    public string[] startingText = new string[5];

    public string[] winText = new string[5];

    public string[] loseText = new string[5];

    public int outOfGameTextTrack = 0;

    public Location firstLocation;

    public AudioSource audioSource;

    public AudioClip clickSound;

    public AudioClip moneySound;

    public AudioClip alarmSound;

    bool playedAlarmSound = false;

    public AudioSource peopleSource;

    public AudioClip peopleSound;

    bool playingPeopleSound = false;


    [Header("Animation Stuff")]

    public Animator playerTableAnimator;
    public Animator merchantTableAnimator;

    public Animator exitButtonAnimator;

    public GameObject hamburgerButton;
    Animator hamburgerAnimator;


    // Start is called before the first frame update
    void Start()
    {
        //setting the instance
        Instance = this;
        peopleSource.clip = peopleSound;

        currentLocation = Randomizer.Instance.locationSlots[Random.Range(0, Randomizer.Instance.locationSlots.Length)].GetComponent<Slot>().linkedLocation;

        hamburgerAnimator = hamburgerButton.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        ManageStatText();

        ManageLocationText();

        }

    void ManageStatText ()
    {
        statTextField.text ="Coins: " + coins.ToString() + "\t\t\t: " + spices.ToString() + "\t\t\t: " + salts.ToString() + "\t\t\t: " + arts.ToString() + "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tDays Left: " + daysLeft.ToString();
    }

    void ManageLocationText()
    {
        if (currentLocation.locationTitle == "The Old World")
        {
            TheOldWorldText();

        } else if (daysLeft < 1){

            if (playedAlarmSound == false)
            {
                audioSource.PlayOneShot(alarmSound);
                playedAlarmSound = true;
            }

            if (coins >= 50)
            {
                EndGameWin();
            }
            else
            {
                EndGameLose();
            }
        }
        else
        {


            welcomeText = "Welcome to " + currentLocation.locationTitle.ToString() + "\n1. Continue";
            movementText = "En route to " + currentLocation.locationTitle.ToString() + "...";
            activityText = "What would you like to do in " + currentLocation.locationTitle.ToString() + "?" + "\n1. Sell Wares" + "\n2. Buy Wares" + "\n3. Leave";


            if (TrailSpawner.Instance.moving == true)
            {
                phaseOfLocation = "moving";
                playerTableAnimator.SetBool("Shown", false);
                merchantTableAnimator.SetBool("Shown", false);
                exitButtonAnimator.SetBool("Shown", false);
                hamburgerAnimator.SetBool("Active", true);
                Camera.main.GetComponent<CameraScript>().atCity = false;
                peopleSource.Stop();
                locationTextField.text = movementText;

            }
            else if (TrailSpawner.Instance.moving == false && (phaseOfLocation == "moving" || phaseOfLocation == "welcome"))
            {
                phaseOfLocation = "welcome";
                locationTextField.text = welcomeText;
                playerTableAnimator.SetBool("Shown", true);
                merchantTableAnimator.SetBool("Shown", true);
                exitButtonAnimator.SetBool("Shown", true);
                hamburgerAnimator.SetBool("Active", false);
                playerMoney.text = "$" + coins.ToString();
                merchantMoney.text = "$" + currentLocation.merchantMoney.ToString();
                Camera.main.GetComponent<CameraScript>().atCity = true;
                if (playingPeopleSound == false)
                {
                    peopleSource.Play();
                    playingPeopleSound = true;
                }

                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    phaseOfLocation = "activity";
                    audioSource.PlayOneShot(clickSound);
                    playingPeopleSound = false;
                }
            } else if (phaseOfLocation == "leaving")
            {
                playerTableAnimator.SetBool("Shown", false);
                merchantTableAnimator.SetBool("Shown", false);
                hamburgerAnimator.SetBool("Active", true);
                peopleSource.Stop();
            }
            else if (phaseOfLocation == "activity")
            {
                locationTextField.text = activityText;

                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    phaseOfLocation = "sell";
                    audioSource.PlayOneShot(clickSound);

                }
                else if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    phaseOfLocation = "buy";
                    audioSource.PlayOneShot(clickSound);
                }
                else if (Input.GetKeyUp(KeyCode.Alpha3))
                {
                    phaseOfLocation = "leave";
                    audioSource.PlayOneShot(clickSound);
                }
            }
            else if (phaseOfLocation == "sell")
            {
                locationTextField.text = currentLocation.sellWares.activityDescription + "\n1. Sell Spices" + "\n2. Sell Salts" + "\n3. Sell Art" + "\n4. Back";
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    audioSource.PlayOneShot(clickSound);
                    if (spices >= Mathf.Abs(currentLocation.sellWares.spiceChange))
                    {
                        coins += currentLocation.sellWares.coinChange * currentLocation.nutMultiplier;
                        spices += currentLocation.sellWares.spiceChange;
                        audioSource.PlayOneShot(moneySound);
                        phaseOfLocation = "sold";
                    }
                    else
                    {
                        phaseOfLocation = "not enough";
                    }

                }
                else if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    audioSource.PlayOneShot(clickSound);
                    if (salts >= Mathf.Abs(currentLocation.sellWares.saltChange))
                    {
                        coins += currentLocation.sellWares.coinChange * currentLocation.batteryMultiplier;
                        salts += currentLocation.sellWares.saltChange;
                        audioSource.PlayOneShot(moneySound);
                        phaseOfLocation = "sold";
                    }
                    else
                    {
                        phaseOfLocation = "not enough";
                    }
                }
                else if (Input.GetKeyUp(KeyCode.Alpha3))
                {
                    audioSource.PlayOneShot(clickSound);
                    if (arts >= Mathf.Abs(currentLocation.sellWares.artChange))
                    {
                        coins += currentLocation.sellWares.coinChange * currentLocation.circuitMultiplier;
                        arts += currentLocation.sellWares.artChange;
                        audioSource.PlayOneShot(moneySound);
                        phaseOfLocation = "sold";
                    }
                    else
                    {
                        phaseOfLocation = "not enough";
                    }
                }
                else if (Input.GetKeyUp(KeyCode.Alpha4))
                {
                    audioSource.PlayOneShot(clickSound);
                    phaseOfLocation = "activity";
                }
            }
            else if (phaseOfLocation == "buy")
            {
                locationTextField.text = currentLocation.buyWares.activityDescription + "\n1. Buy Goods" + "\n2. Back";
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    audioSource.PlayOneShot(clickSound);
                    if (coins >= Mathf.Abs(currentLocation.buyWares.coinChange))
                    {
                        coins += currentLocation.buyWares.coinChange;
                        spices += currentLocation.buyWares.spiceChange;
                        salts += currentLocation.buyWares.saltChange;
                        arts += currentLocation.buyWares.artChange;
                        audioSource.PlayOneShot(moneySound);
                        phaseOfLocation = "bought";
                    }
                    else
                    {
                        phaseOfLocation = "not enough";
                    }

                }
                else if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    audioSource.PlayOneShot(clickSound);
                    phaseOfLocation = "activity";
                }
            }
            else if (phaseOfLocation == "sold")
            {
                locationTextField.text = currentLocation.sellWares.activityResult + "\n 1. Back";


                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    audioSource.PlayOneShot(clickSound);
                    phaseOfLocation = "activity";

                }

            }
            else if (phaseOfLocation == "bought")
            {
                locationTextField.text = currentLocation.buyWares.activityResult + "\n 1. Back";

                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    audioSource.PlayOneShot(clickSound);
                    phaseOfLocation = "activity";

                }

            }
            else if (phaseOfLocation == "not enough")
            {
                locationTextField.text = "You dont have enough for that. \n1. Back";
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    audioSource.PlayOneShot(clickSound);
                    phaseOfLocation = "activity";
                }
            }
            else if (phaseOfLocation == "leave")
            {
                if (currentLocation.locations.Length == 1)
                {
                    locationTextField.text = currentLocation.leave.activityDescription + " From here you can go to:" + "\n1. " + currentLocation.locations[0].locationTitle + "\n2. Back";
                    if (Input.GetKeyUp(KeyCode.Alpha1))
                    {
                        audioSource.PlayOneShot(clickSound);
                        phaseOfLocation = "moving";
                        daysLeft--;
                        currentLocation = currentLocation.locations[0];
                    }
                    else if (Input.GetKeyUp(KeyCode.Alpha2))
                    {
                        audioSource.PlayOneShot(clickSound);
                        phaseOfLocation = "activity";
                    }
                }
                else if (currentLocation.locations.Length == 2)
                {
                    locationTextField.text = currentLocation.leave.activityDescription + " From here you can go to:" + "\n1. " + currentLocation.locations[0].locationTitle + "\n2. " + currentLocation.locations[1].locationTitle + "\n3. Back";
                    if (Input.GetKeyUp(KeyCode.Alpha1))
                    {
                        audioSource.PlayOneShot(clickSound);
                        phaseOfLocation = "moving";
                        daysLeft--;
                        currentLocation = currentLocation.locations[0];
                    }
                    else if (Input.GetKeyUp(KeyCode.Alpha2))
                    {
                        audioSource.PlayOneShot(clickSound);
                        phaseOfLocation = "moving";
                        daysLeft--;
                        currentLocation = currentLocation.locations[1];
                    }
                    else if (Input.GetKeyUp(KeyCode.Alpha3))
                    {
                        audioSource.PlayOneShot(clickSound);
                        phaseOfLocation = "activity";
                    }
                }
                else if (currentLocation.locations.Length == 3)
                {
                    locationTextField.text = currentLocation.leave.activityDescription + " From here you can go to:" + "\n1. " + currentLocation.locations[0].locationTitle + "\n2. " + currentLocation.locations[1].locationTitle + "\n3. " + currentLocation.locations[2].locationTitle + "\n4. Back";
                    if (Input.GetKeyUp(KeyCode.Alpha1))
                    {
                        audioSource.PlayOneShot(clickSound);
                        phaseOfLocation = "moving";
                        daysLeft--;
                        currentLocation = currentLocation.locations[0];
                    }
                    else if (Input.GetKeyUp(KeyCode.Alpha2))
                    {
                        audioSource.PlayOneShot(clickSound);
                        phaseOfLocation = "moving";
                        daysLeft--;
                        currentLocation = currentLocation.locations[1];
                    }
                    else if (Input.GetKeyUp(KeyCode.Alpha3))
                    {
                        audioSource.PlayOneShot(clickSound);
                        phaseOfLocation = "moving";
                        daysLeft--;
                        currentLocation = currentLocation.locations[2];
                    }
                    else if (Input.GetKeyUp(KeyCode.Alpha4))
                    {
                        audioSource.PlayOneShot(clickSound);
                        phaseOfLocation = "activity";
                    }
                }
                else if (currentLocation.locations.Length == 4)
                {
                    locationTextField.text = currentLocation.leave.activityDescription + " From here you can go to:" + "\n1. " + currentLocation.locations[0].locationTitle + "\n2. " + currentLocation.locations[1].locationTitle + "\n3. " + currentLocation.locations[2].locationTitle + "\n4. " + currentLocation.locations[3].locationTitle + "\n5. Back";
                    if (Input.GetKeyUp(KeyCode.Alpha1))
                    {
                        audioSource.PlayOneShot(clickSound);
                        phaseOfLocation = "moving";
                        daysLeft--;
                        currentLocation = currentLocation.locations[0];
                    }
                    else if (Input.GetKeyUp(KeyCode.Alpha2))
                    {
                        audioSource.PlayOneShot(clickSound);
                        phaseOfLocation = "moving";
                        daysLeft--;
                        currentLocation = currentLocation.locations[1];
                    }
                    else if (Input.GetKeyUp(KeyCode.Alpha3))
                    {
                        audioSource.PlayOneShot(clickSound);
                        phaseOfLocation = "moving";
                        daysLeft--;
                        currentLocation = currentLocation.locations[2];
                    }
                    else if (Input.GetKeyUp(KeyCode.Alpha4))
                    {
                        audioSource.PlayOneShot(clickSound);
                        phaseOfLocation = "moving";
                        daysLeft--;
                        currentLocation = currentLocation.locations[3];
                    }
                    else if (Input.GetKeyUp(KeyCode.Alpha5))
                    {
                        audioSource.PlayOneShot(clickSound);
                        phaseOfLocation = "activity";
                    }
                }

            }
        }
    }

    void TheOldWorldText ()
    {

        locationTextField.text = startingText[outOfGameTextTrack] + "\n1. Continue";

        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            audioSource.PlayOneShot(clickSound);
            outOfGameTextTrack++;
        }

        if (outOfGameTextTrack > 4)
        {
            currentLocation = firstLocation;
            outOfGameTextTrack = 0;
        }

    }

    void EndGameWin ()
    {
        locationTextField.text = winText[outOfGameTextTrack] + "\n1. Continue";

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            audioSource.PlayOneShot(clickSound);
            outOfGameTextTrack++;
        }

        if (outOfGameTextTrack > 4)
        {
            outOfGameTextTrack = 0;
            SceneManager.LoadScene("StartScene");
        }
    }

    void EndGameLose () 
    {
        locationTextField.text = loseText[outOfGameTextTrack] + "\n1. Continue";

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            audioSource.PlayOneShot(clickSound);
            outOfGameTextTrack++;
        }

        if (outOfGameTextTrack > 4)
        {
            outOfGameTextTrack = 0;
            SceneManager.LoadScene("StartScene");
        }
    }

}
