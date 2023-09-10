using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameControlsActions, GameInput.IUIInputsActions
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
	public static UnityAction upgradeMenuEvent;

	private GameInput gameInput;

    public void InitGameInput()
	{
		if (gameInput == null)
		{
			gameInput = new GameInput();
			gameInput.GameControls.SetCallbacks(this);
			gameInput.UIInputs.SetCallbacks(this);
		}

		EnableUIInputs();
		EnablePlayerInputs();

    }

	#region SUBSCRIBERS

	public void ClearAllSubscribers()
	{
		Debug.Log("here");
		ClearSubscribers(moveEvent);
		ClearSubscribers(lookEvent);
		ClearSubscribers(mousePosEvent);
		ClearSubscribers(zoomEvent);
		ClearSubscribers(sprintEvent);
		ClearSubscribers(panEvent);
		ClearSubscribers(dashEvent);
		ClearSubscribers(jumpEvent);
		ClearSubscribers(attackSlotEvent);
		ClearSubscribers(primarySlotEvent);
		ClearSubscribers(secondarySlotEvent);
		ClearSubscribers(cancelEvent);
	}

	void ClearSubscribers<T>(UnityAction<T> a)
	{
		if (a == null) return;
		foreach (var e in a.GetInvocationList()) a -= (e as UnityAction<T>);
	}

	void ClearSubscribers(UnityAction a)
	{
		if (a == null) return;
		foreach (var e in a.GetInvocationList()) a -= (e as UnityAction);
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
		if (context.phase == InputActionPhase.Performed)
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
		Debug.Log("Input E");
		if (context.phase == InputActionPhase.Performed)
			secondarySlotEvent?.Invoke();
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

    public void OnUpgradeMenu(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed)
            upgradeMenuEvent?.Invoke();
    }

    #endregion

    #region SWITCH INPUT

	public void EnableUIInputs() {
		gameInput.UIInputs.Enable();
	}

    public void DisableUIInputs() {
		gameInput.UIInputs.Disable();
    }

    public void EnablePlayerInputs()
	{
		gameInput.GameControls.Enable();
	}
    
	public void DisablePlayerInputs()
	{
		gameInput.GameControls.Disable();
	}

	#endregion
}