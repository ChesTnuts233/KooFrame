using System.Collections;
using System.Collections.Generic;
using KooFrame;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CountPanel : BasePanel
{
	//view
	private Button btnAdd;
	private Button btnReduce;
	private Text textNum;

	//model
	private int num = 0;
	protected override void Awake()
	{
		base.Awake();
	}
	// Start is called before the first frame update
	void Start()
	{
		//control
		btnAdd = GetControl<Button>("Add");
		btnReduce = GetControl<Button>("Reduce");
		textNum = GetControl<Text>("Num");
		btnAdd.onClick.AddListener(() =>
		{
			num++;
			textNum.text = num.ToString();
		});
		btnReduce.onClick.AddListener(() =>
		{
			num--;
			textNum.text = num.ToString();
		});
	}
	private void Update()
	{

	}
	/// <summary>
	/// 初始化数据
	/// </summary>
	public void InitInfo()
	{
		//Debug.Log("初始化数据");
	}


	protected override void OnClick(string btnName)
	{

	}


	public override void OpenMe()   //显示面板后要做的事情
	{
		//Debug.Log("显示面板");
	}
	public override void CloseMe()
	{

	}

}
