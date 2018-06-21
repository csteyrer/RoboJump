using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortLayer : MonoBehaviour
{
    //name of the sorting layer
    public string sortLayerName; 

    void Start()
    {
        //get each of the sprites that are a child of the game object that the scene is attached to
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            //set those sprites sorting layer name to the one we specified
            sr.GetComponent<Renderer>().sortingLayerName = sortLayerName;
        }
    }
}
