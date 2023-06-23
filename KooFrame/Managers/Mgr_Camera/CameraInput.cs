#if ENABLE_INPUT_SYSTEM


using KooFrame;
using MyUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInput : KooMonoBaseInput
{

	//CameraCtrl CameraCtrlMap; //ActionsMap产生的Map脚本

	#region 第三人称控制输入
	public Vector2 Look3rd;
	public Vector2 ScrollScale;
	#endregion

	#region 第一人称控制输入
	public Vector2 Look1st;
	#endregion

	#region 自由人称控制输入
	public Vector2 MoveFree;
	public Vector2 LookFree;
	#endregion

	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;

	/// <summary>
	/// 指针状态设置
	/// </summary>
	/// <param name="newState"></param>
	public static void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}

	#region 第三人称控制输入
	public void OnLook3rd(InputValue value)
	{
		if (cursorInputForLook)
		{
			LookInput3rd(value.Get<Vector2>());
		}
	}
	public void LookInput3rd(Vector2 newLookDirection)
	{
		Look3rd = newLookDirection;
	}
	public void OnScrollScale(InputValue value)
	{
		if (cursorInputForLook)
		{
			ScrollInput(value.Get<Vector2>());
		}
	}
	public void ScrollInput(Vector2 newScroll)
	{
		ScrollScale = newScroll;
	}
	public void Look3rdCtrl(InputAction.CallbackContext context)
	{
		Look3rd = context.ReadValue<Vector2>();
	}
	public void ScrollCtrl(InputAction.CallbackContext context)
	{
		ScrollScale = context.ReadValue<Vector2>();
	}
	#endregion

	#region 第一人称控制输入
	public void OnLook1st(InputValue value)
	{
		if (cursorInputForLook)
		{
			LookInput1st(value.Get<Vector2>());
		}
	}
	public void LookInput1st(Vector2 newLookDirection)
	{
		Look1st = newLookDirection;
	}
	public void Look1stCtrl(InputAction.CallbackContext context)
	{
		Look1st = context.ReadValue<Vector2>();
	}


	#endregion

	#region 自由人称控制输入
	public void OnMoveFree(InputValue value)
	{
		FreeMoveInput(value.Get<Vector2>());
	}
	public void FreeMoveInput(Vector2 newMoveDirection)
	{
		MoveFree = newMoveDirection;
	}
	public void OnLookFree(InputValue value)
	{
		LookFree = value.Get<Vector2>();
	}
	public void MoveFreeCtrl(InputAction.CallbackContext context)
	{
		var moveVector = context.ReadValue<Vector2>();
		MoveFree = moveVector;
	}
	public void LookFreeCtrl(InputAction.CallbackContext context)
	{
		var lookVector = context.ReadValue<Vector2>();
		LookFree = lookVector;
	}

	#endregion
}
#endif