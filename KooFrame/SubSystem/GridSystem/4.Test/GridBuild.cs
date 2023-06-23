// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using GridSystem;
// using CodeMonkey.Utils;
//
//
// public class GridBuild : MonoBehaviour
// {
// 	public static GridMap<MyGrid> MyGridMap;
// 	public static bool PlayerIsInMap = true;
// 	public GameObject attack;
// 	[SerializeField]private GameObject attackani;
// 	[SerializeField] private GameObject damage;
//
// 	public void Awake()
// 	{
// 		JKEventSystem.AddEventListener<int,int>("PlayerInGrid", PlayerInGrid);
// 		PoolSystem.InitGameObjectPool("AttackAni", 10, attackani, 10);
// 		PoolSystem.InitGameObjectPool("Damage3x3", 10, damage, 10);
// 		PoolSystem.InitGameObjectPool("Attack3x3", 10, attack, 10);
// 	}
// 	private void Start()
// 	{
// 		MyGridMap = new GridMap<MyGrid>(500, 6, 10f, new Vector3(5, 5, 0));
// 		for (int x = 0; x < MyGridMap.GridTArray.GetLength(0); x++)
// 		{
// 			for (int y = 0; y < MyGridMap.GridTArray.GetLength(1); y++)
// 			{
// 				MyGridMap.GridTArray[x, y] = new MyGrid(x, y);
// 			}
// 		}
// 	}
//
// 	private void PlayerInGrid(int playerPosX, int playerPosY)
// 	{
// 		int x, y;
// 		x = playerPosX;
// 		y = playerPosY;
// 		try
// 		{
// 			MyGridMap.PlayerNowGrid.MyValue = (MyGrid)MyGridMap.GetGridObject(x, y);
// 		}
// 		catch
// 		{ 
// 			JKEventSystem.EventTrigger("玩家出界");
// 		}
//
// 	}
// }
