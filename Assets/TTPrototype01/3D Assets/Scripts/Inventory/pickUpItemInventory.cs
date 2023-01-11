using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class pickUpItemInventory : MonoBehaviour
{
    [SerializeField] public int itemNumber;

    public void AddItem()
    {
        //Debug.Log(gameObject.name + " :: Gave you :: ");
        KickStarter.runtimeInventory.Add(itemID: itemNumber);

        Destroy(gameObject);
    }



}
