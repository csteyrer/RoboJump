using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    Animator anim;

    //is entry or exit door
    [SerializeField]
    GameObject DoorType;

    //track the state of the door
    int stateOfDoor = 1;

    void Start()
    {
        //initialize the animator
        anim = GetComponent<Animator>();

        //set entry door to open
        if (DoorType.name == "EntryDoor")
            anim.SetFloat("DoorState", 3);

        //set exit door to locked
        if (DoorType.name == "ExitDoor")
            LockDoor();
    }

    //function to lock the door and set it's state
    void LockDoor()
    {
        if(DoorType.name == "ExitDoor")
        {
            anim.SetFloat("DoorState", 1);
            stateOfDoor = 1;
        }
    }

    //function to unlock the door and set it's state
    void UnlockDoor()
    {
        if (DoorType.name == "ExitDoor")
        {
            anim.SetFloat("DoorState", 2);
            stateOfDoor = 2;
        }
    }

    //function to open the door and set it's state
    void OpenDoor()
    {
        if (DoorType.name == "ExitDoor")
        {
            anim.SetFloat("DoorState", 3);
            stateOfDoor = 3;
        }
    }

    //function set the state of the door
    public void SetDoorState(int state)
    {
        if (GetDoorState() == 3)
            return;
         
        if (state == 1 && DoorType.name == "ExitDoor")
            LockDoor();
        if (state == 2 && DoorType.name == "ExitDoor")
            UnlockDoor();
        if (state == 3 && DoorType.name == "ExitDoor")
        {
            AudioManager.instance.PlaySound("DoorOpen");
            OpenDoor();
        }
    }

    //function to get the current door state
    public int GetDoorState()
    {
        return stateOfDoor;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (stateOfDoor == 3 && col.gameObject.tag == "Player")
        {
            Menu.DeactivateTimeScale();
            Menu.Finished();
        }
    }
}
