#if ENABLE_INPUT_SYSTEM


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CameraCtrl_1st : MonoBehaviour
{


	public float mouseSensitivity = 100f;                   //鼠标灵敏度
	[SerializeField] private float yRotation = 0f;          //y轴旋转累加值
	[SerializeField] private float xRotation = 0f;          //x轴旋转累加值

	public bool is1stCtrl = false;                          //是否开启第一人称
	public Transform CameraPlayer;
	public PlayerInput Input;
	[FormerlySerializedAs("cameraInputMono")] [FormerlySerializedAs("CameraInput")] public CameraInput cameraInput;							//CameraCtrl CameraCtrl;
	
	
	private void OnEnable()
	{
		
		CameraInput.SetCursorState(true);
		Input.SwitchCurrentActionMap("Camera1stCtrl");
		Debug.Log(Input.currentActionMap.name);
	}
	private void Update()
	{
		Camera1stController();
	}

	private void Camera1stController()
	{
		float mouseX = cameraInput.Look1st.x * mouseSensitivity * Time.deltaTime;
		float mouseY = cameraInput.Look1st.y * mouseSensitivity * Time.deltaTime;
		
		yRotation -= mouseY; //对上下旋转值累加
		yRotation = Mathf.Clamp(yRotation, -80f, 80f);
		xRotation += mouseX; //对左右旋转值累加
							 //xRotation = Mathf.Clamp(xRotation, -180f, 180f);

		this.transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0f);
	}
}
#endif
