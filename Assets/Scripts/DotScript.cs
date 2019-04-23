using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotScript : MonoBehaviour
{

    public float daysLeftWhenMade;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent = GameObject.Find("Dots").transform;
        daysLeftWhenMade = GameManager.Instance.daysLeft;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.daysLeft > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, GameManager.Instance.daysLeft / daysLeftWhenMade);
        }
    }
}
