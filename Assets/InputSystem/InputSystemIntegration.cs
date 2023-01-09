using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace AC
{

	public class InputSystemIntegration : MonoBehaviour
	{

		#region Variables

		[SerializeField] InputAction movementAction = null;
		[SerializeField] InputAction holdToRunAction = null;
		[SerializeField] InputAction toggleRunAction = null;
		[SerializeField] InputAction interactionAAction = null;
		[SerializeField] InputAction interactionBAction = null;
		[SerializeField] InputAction toggleCursorAction = null;
		[SerializeField] InputAction menuAction = null;
		[SerializeField] InputAction cursorMovementAction = null;
		[SerializeField] InputAction inventoryAction = null;

		#endregion


		#region UnityStandards

		private void Start()
		{
			// Mouse delegates
			KickStarter.playerInput.InputMousePositionDelegate = Custom_MousePosition;
			KickStarter.playerInput.InputGetMouseButtonDelegate = Custom_GetMouseButton;
			KickStarter.playerInput.InputGetMouseButtonDownDelegate = Custom_GetMouseButtonDown;

			// Keyboard / controller delegates
			KickStarter.playerInput.InputGetAxisDelegate = Custom_GetAxis;
			KickStarter.playerInput.InputGetButtonDelegate = Custom_GetButton;
			KickStarter.playerInput.InputGetButtonDownDelegate = Custom_GetButtonDown;
			KickStarter.playerInput.InputGetButtonUpDelegate = Custom_GetButtonUp;
			KickStarter.playerInput.InputGetFreeAimDelegate = Custom_GetFreeAim;

			// Touch delegates
			KickStarter.playerInput.InputTouchCountDelegate = Custom_TouchCount;
			KickStarter.playerInput.InputTouchPositionDelegate = Custom_TouchPosition;
			KickStarter.playerInput.InputTouchDeltaPositionDelegate = Custom_TouchDeltaPosition;
			KickStarter.playerInput.InputGetTouchPhaseDelegate = Custom_TouchPhase;

#if !UNITY_EDITOR
			if (KickStarter.settingsManager.inputMethod == InputMethod.TouchScreen)
			{
				UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Enable ();
			}
#endif
		}


		private void OnEnable()
		{
			movementAction.Enable();
			holdToRunAction.Enable();
			interactionAAction.Enable();
			interactionBAction.Enable();
			toggleCursorAction.Enable();
			cursorMovementAction.Enable();
			menuAction.Enable();
			inventoryAction.Enable();
		}


		private void OnDisable()
		{
			movementAction.Disable();
			holdToRunAction.Disable();
			interactionAAction.Disable();
			interactionBAction.Disable();
			toggleCursorAction.Disable();
			cursorMovementAction.Disable();
			menuAction.Disable();
			inventoryAction.Disable();
		}

		#endregion


		#region MouseInput

		private Vector2 Custom_MousePosition(bool cursorIsLocked)
		{
			if (cursorIsLocked)
				return new Vector2(Screen.width / 2f, Screen.height / 2f);

			return Mouse.current.position.ReadValue();
		}


		private bool Custom_GetMouseButton(int button)
		{
			switch (button)
			{
				case 0:
					return Mouse.current.leftButton.isPressed;

				case 1:
					return Mouse.current.rightButton.isPressed;

				default:
					return false;
			}
		}


		private bool Custom_GetMouseButtonDown(int button)
		{
			switch (button)
			{
				case 0:
					return Mouse.current.leftButton.wasPressedThisFrame;

				case 1:
					return Mouse.current.rightButton.wasPressedThisFrame;

				default:
					return false;
			}
		}

		#endregion


		#region KeyboardControllerInput

		private float Custom_GetAxis(string axisName)
		{
			switch (axisName)
			{
				case "Horizontal":
					return movementAction.ReadValue<Vector2>().x;

				case "Vertical":
					return movementAction.ReadValue<Vector2>().y;

				case "Run":
					return holdToRunAction.ReadValue<float>();

				case "CursorHorizontal":
					return cursorMovementAction.ReadValue<Vector2>().x;

				case "CursorVertical":
					return cursorMovementAction.ReadValue<Vector2>().y;
			}
			return 0f;
		}


		private bool Custom_GetButton(string axisName)
		{
			ButtonControl[] buttonControls = GetButtonControls(axisName);
			foreach (ButtonControl buttonControl in buttonControls)
			{
				if (buttonControl.isPressed) return true;
			}
			return false;
		}


		private bool Custom_GetButtonDown(string axisName)
		{
			ButtonControl[] buttonControls = GetButtonControls(axisName);
			foreach (ButtonControl buttonControl in buttonControls)
			{
				if (buttonControl.wasPressedThisFrame) return true;
			}
			return false;
		}


		private bool Custom_GetButtonUp(string axisName)
		{
			ButtonControl[] buttonControls = GetButtonControls(axisName);
			foreach (ButtonControl buttonControl in buttonControls)
			{
				if (buttonControl.wasReleasedThisFrame) return true;
			}
			return false;
		}


		private Vector2 Custom_GetFreeAim(bool cursorIsLocked)
		{
			if (cursorIsLocked)
			{
				return new Vector2(Custom_GetAxis("CursorHorizontal"), Custom_GetAxis("CursorVertical"));
			}
			return Vector2.zero;
		}


		private ButtonControl[] GetButtonControls(string axisName)
		{
			InputAction inputAction = GetInputAction(axisName);
			if (inputAction != null)
			{
				ButtonControl[] controls = new ButtonControl[inputAction.controls.Count];
				for (int i = 0; i < controls.Length; i++)
				{
					controls[i] = (ButtonControl)inputAction.controls[i];
				}
				return controls;
			}
			return new ButtonControl[0];
		}


		private InputAction GetInputAction(string axisName)
		{
			switch (axisName)
			{
				case "ToggleRun":
					return toggleRunAction;

				case "InteractionA":
					return interactionAAction;

				case "InteractionB":
					return interactionBAction;

				case "ToggleCursor":
					return toggleCursorAction;

				case "Menu":
					return menuAction;

				case "inventory":
					return inventoryAction;
				default:
					return null;
			}
		}

		#endregion


		#region TouchInput

		private int Custom_TouchCount()
		{
			return UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count;
		}


		private Vector2 Custom_TouchPosition(int index)
		{
			return UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[index].screenPosition;
		}


		private Vector2 Custom_TouchDeltaPosition(int index)
		{
			return UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[index].delta;
		}


		private UnityEngine.TouchPhase Custom_TouchPhase(int index)
		{
			UnityEngine.InputSystem.TouchPhase touchPhase = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[index].phase;
			switch (touchPhase)
			{
				case UnityEngine.InputSystem.TouchPhase.Began:
					return UnityEngine.TouchPhase.Began;

				case UnityEngine.InputSystem.TouchPhase.Canceled:
					return UnityEngine.TouchPhase.Canceled;

				case UnityEngine.InputSystem.TouchPhase.Ended:
					return UnityEngine.TouchPhase.Ended;

				case UnityEngine.InputSystem.TouchPhase.Moved:
					return UnityEngine.TouchPhase.Moved;

				case UnityEngine.InputSystem.TouchPhase.Stationary:
					return UnityEngine.TouchPhase.Stationary;

				default:
					return UnityEngine.TouchPhase.Canceled;
			}
		}

		#endregion

	}

}