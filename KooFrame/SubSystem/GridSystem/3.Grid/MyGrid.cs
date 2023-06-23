using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.VisualScripting;
using CodeMonkey.Utils;
using UnityEngine.Rendering;

namespace GridSystem
{
	public class MyGrid
	{
		public int X;
		public int Y;
		public int Value;
		public string GridName;
		public float cellsize;
		public bool IsCheckPlayer = false;			//玩家是否在上面
		public bool GridState;						//网格状态


		public Vector2 GridPos;						
		public MyGrid(int x,int y)
		{
			this.X = x;
			this.Y = y;
			this.GridPos = new Vector2((x+1)*10,(y+1)*10);
			this.GridName = x.ToString()+"_"+y.ToString();
		}

		// public void CheckPlayerisOnMe()
		// {
		// 	if(this.X != PlayerController.playerPos.x && this.Y != PlayerController.playerPos.y)
		// 	{
		// 		PlayerExitGird();
		// 	}
		// }
		public void ShowGirdcoordinate()
		{
			PlayerEnterGird();
		}
		
		private void PlayerEnterGird()
		{
			IsCheckPlayer = true;
		}
		private void PlayerExitGird()
		{
			IsCheckPlayer = false;
		}
	}
}
