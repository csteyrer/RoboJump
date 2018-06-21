using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] switches;

    [SerializeField]
    GameObject exitDoor;

    int nrOfSwitches = 0;

    [SerializeField]
    Text switchCount;

    void Start()
    {
        GetNrOfSwitches();
    }

    public int GetNrOfSwitches()
    {
        int x = 0;

        for(int i = 0; i < switches.Length; i++)
        {
            if (switches[i].GetComponent<Switch>().isOn == false)
                x++;
            else if (switches[i].GetComponent<Switch>().isOn == true)
                nrOfSwitches--;
        }
        nrOfSwitches = x;

        return nrOfSwitches;
    }

    public void GetExitDoorState()
    {
        if(nrOfSwitches <= 0)
        {
            exitDoor.GetComponent<Door>().SetDoorState(3);
        }
    }

    void Update()
    {
        switchCount.text = GetNrOfSwitches().ToString();
        GetExitDoorState();
    }
}
