using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    GameObject switchOn;
    [SerializeField]
    GameObject switchOff;

    public bool isOn = false;

    void Start()
    {
        //sets the switch to off sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = switchOff.GetComponent<SpriteRenderer>().sprite;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            //set the switch to on sprite
            gameObject.GetComponent<SpriteRenderer>().sprite = switchOn.GetComponent<SpriteRenderer>().sprite;

            if (isOn == false)
                AudioManager.instance.PlaySound("Switch");
            //set the isOn to true when triggered
            isOn = true;
        }
    }
}
