using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenStars : MonoBehaviour {

    public Animator anim1;
    public Animator anim2;
    public Animator anim3;
    public Menu end;
    [Range(0,3)]
    public int stars;
    private bool first = true;
    
    

    void Update () {
        if (end.InEndScreen() && first)
        {
            anim1.SetInteger("Stars", stars);
            anim2.SetInteger("Stars", stars);
            anim3.SetInteger("Stars", stars);
            first = false;
        }
	}
}
