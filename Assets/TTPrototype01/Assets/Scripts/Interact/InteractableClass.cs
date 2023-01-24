using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableClass : MonoBehaviour
{
    //[SerializeField] public string interactScript;

    public bool executeScriptBool = false;

    //
    [SerializeField] public string interactType;
    //

    
    private void FixedUpdate()
    {
        if(executeScriptBool)
        {
            ExecuteScript();
        }
    }
    

    public void ExecuteScript()
    {
        switch (interactType)
        {
            case ("Inventory Item"):

                Debug.Log("Picking Up Inventory Item");
                GetComponent<AC_pickUpItemInventory>().AddItem();

                break;

            case ("Unlock Door"):

                Debug.Log("Try Opening door");
                GetComponent<AC_DoorBehaviour>().UnlockDoor();

                break;

            case ("Open Door"):

                Debug.Log("Try Opening door");
                GetComponent<AC_DoorBehaviour>().GoThroughDoor();

                break;
        }
    }
}
