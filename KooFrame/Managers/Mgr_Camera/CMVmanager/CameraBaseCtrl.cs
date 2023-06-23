#if ENABLE_INPUT_SYSTEM


using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CameraBaseCtrl : MonoBehaviour
{
	public Transform CameraPlayer;
	public PlayerInput Input;
	[FormerlySerializedAs("cameraInputMono")] [FormerlySerializedAs("CameraInput")] public CameraInput cameraInput;
	//CameraCtrl CameraCtrl;
}
#endif