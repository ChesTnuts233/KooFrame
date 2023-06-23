using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoCtrl : MonoBehaviour
{
	public static VideoPlayer videoPlayer;
	public static bool isStop = false;
	// Start is called before the first frame update
	void Start()
	{
		videoPlayer = GetComponent<VideoPlayer>();
	}

	// Update is called once per frame
	void Update()
	{

	}
}