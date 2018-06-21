using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endboss_Anim : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("DestroyThis",3f);
	}

   private void DestroyThis()
    {
        this.gameObject.SetActive(false);
    }
}
