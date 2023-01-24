using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AC_Inventory_OpenClose : MonoBehaviour
{

    [SerializeField] public ActionList actionList;

    public void OpenClose_Inventory(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            actionList.Interact(0, true);
        }
    }

}
