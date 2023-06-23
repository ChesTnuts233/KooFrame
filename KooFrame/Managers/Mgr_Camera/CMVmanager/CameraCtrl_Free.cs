#if ENABLE_INPUT_SYSTEM


using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CameraCtrl_Free : MonoBehaviour
{
	public Transform CameraPlayer;
	public PlayerInput Input;
	[FormerlySerializedAs("cameraInputMono")] [FormerlySerializedAs("CameraInput")] public CameraInput cameraInput;
	//CameraCtrl CameraCtrl;

	public float mouseSensitivity = 100f;                   //鼠标灵敏度
	[SerializeField] private float yRotation = 0f;          //y轴旋转累加值
	[SerializeField] private float xRotation = 0f;          //x轴旋转累加值



	private void OnEnable()
	{
		Input.SwitchCurrentActionMap("CameraFreeCtrl");
		CameraInput.SetCursorState(true);
		Debug.Log(Input.currentActionMap.name);
		//UIMgr.Instance.OpenPanel<ControllerPanel_Free>("ControllerPanel_Free", UIMgr.E_UI_Layer.Top);

	}

	private void Update()
	{
		CameraControllerMove();
	}
	private void LateUpdate()
	{
		CameraControllerLook();
	}
	private void CameraControllerMove()
	{
		transform.Translate(cameraInput.MoveFree.x, 0, cameraInput.MoveFree.y);
	}

	private void CameraControllerLook()
	{
		float mouseX = cameraInput.LookFree.x * mouseSensitivity * Time.deltaTime;
		float mouseY = cameraInput.LookFree.y * mouseSensitivity * Time.deltaTime;


		yRotation -= mouseY; //对上下旋转值累加
		yRotation = Mathf.Clamp(yRotation, -80f, 80f);

		xRotation += mouseX; //对左右旋转值累加
							 //xRotation = Mathf.Clamp(xRotation, -180f, 180f);

		this.transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0f);
	}


}
#endif
