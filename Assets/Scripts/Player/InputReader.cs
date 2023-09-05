using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEditor;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameControlsActions
{
	public static UnityAction<Vector2> moveEvent;
	public static UnityAction<Vector2> lookEvent;
	public static UnityAction<Vector2> mousePosEvent;
	public static UnityAction<float> zoomEvent;
	public static UnityAction<bool> sprintEvent;
	public static UnityAction<bool> panEvent;
	public static UnityAction dashEvent;
	public static UnityAction jumpEvent;
	public static UnityAction attackSlotEvent;
	public static UnityAction primarySlotEvent;
	public static UnityAction secondarySlotEvent;
	public static UnityAction cancelEvent;
	public static UnityAction pauseEvent;

	private GameInput gameInput;

	#region SETUP

	void OnEnable()
	{
		EditorApplication.playModeStateChanged += InitGameInput;
	}
	
	void OnDisable()
	{
		EditorApplication.playModeStateChanged -= InitGameInput;
		if (gameInput != null) DisableInput();
	}

	// strange workaround to fix manually refreshing input scriptable object instance
	void InitGameInput(PlayModeStateChange stateChange)
	{
		if (stateChange == PlayModeStateChange.EnteredPlayMode)
		{
			if (gameInput == null)
			{
				gameInput = new GameInput();
				gameInput.GameControls.SetCallbacks(this);
			}

	    	EnableInput();
		}
	}

	#endregion

	#region CALLBACKS

	// move player (wasd)
	public void OnMove(InputAction.CallbackContext context)
	{
		moveEvent?.Invoke(context.ReadValue<Vector2>());
	}

	// move camera (mouse delta)
	public void OnLook(InputAction.CallbackContext context)
	{
		lookEvent?.Invoke(context.ReadValue<Vector2>());
	}

	// mouse position on screen (mouse position)
	public void OnMousePos(InputAction.CallbackContext context)
	{
		mousePosEvent?.Invoke(context.ReadValue<Vector2>());
	}

	// move forward faster (lshift hold)
	public void OnSprint(InputAction.CallbackContext context)
	{
        if (context.phase == InputActionPhase.Performed)
		    sprintEvent?.Invoke(true);
		else
			sprintEvent?.Invoke(false);
	}

	// move fast shortly (lshift click)
	public void OnDash(InputAction.CallbackContext context)
	{
        if (context.phase == InputActionPhase.Performed)
		    dashEvent?.Invoke();
	}

	// jump player (space)
	public void OnJump(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			jumpEvent?.Invoke();
	}

	// start move camera (right mouse hold)
	public void OnPan(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			panEvent?.Invoke(true);
		else
			panEvent?.Invoke(false);
	}

	// start move camera (right mouse hold)
	public void OnZoom(InputAction.CallbackContext context)
	{
		zoomEvent?.Invoke(context.ReadValue<float>());
	}

	// attack (left mouse click)
	public void OnAttackSlot(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			attackSlotEvent?.Invoke();
	}

	// first ability (q)
	public void OnPrimarySlot(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			primarySlotEvent?.Invoke();
	}

    // second ability (e)
	public void OnSecondarySlot(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			primarySlotEvent?.Invoke();
	}

    // stop selected ability cast (right mouse click)
	public void OnCancel(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			cancelEvent?.Invoke();
	}

	// pause the game (escape)
	public void OnPause(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			pauseEvent?.Invoke();
	}

	#endregion

	#region SWITCH INPUT

	public void EnableInput()
	{
		gameInput.GameControls.Enable();
	}
    
	public void DisableInput()
	{
		gameInput.GameControls.Disable();
	}

	#endregion
}