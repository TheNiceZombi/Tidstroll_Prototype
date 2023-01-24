using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TEST_InputSystem : MonoBehaviour
{
    PlayerInput playerInput;

    public void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInput.onActionTriggered += TestInput_onActionTriggered; //DebugMessage;
    }
    
    private void TestInput_onActionTriggered(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }

    public void DebugMessage(InputAction.CallbackContext context)
    {

        Debug.Log("The input worked. du ar BEST!!! "+ context.phase);
    }
}
