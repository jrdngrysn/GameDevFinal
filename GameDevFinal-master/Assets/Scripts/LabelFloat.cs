using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelFloat : MonoBehaviour
{
    public Vector3 startPos;

    public bool selected = false;
    public TextMesh dayText;

    bool down = true;

    Color alphaChange;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        alphaChange = new Color(1, 1, 1, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.phaseOfLocation == "leaving")
        {
            if (selected)
            {
                Vector3 tempPos = startPos;
                tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * 1f) * .08f;
                transform.position = tempPos;
                down = false;

            }
            else
            {
                Vector3 tempPos = startPos;
                tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * 1f) * .0001f;
                transform.position = tempPos;
                down = true;
            }


        }
        else
        {
            down = true;
        }


        if (down)
        {
            if (alphaChange.a > 0)
            {
                alphaChange.a -= .35f;
            }
        }
        else
        {
            if (alphaChange.a < 1)
            {
                alphaChange.a += .15f;
            }

        }
        dayText.color = alphaChange;
    }
}
