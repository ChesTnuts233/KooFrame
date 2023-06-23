using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knapsack : Inventory
{
	private static Knapsack instance;
	public static Knapsack Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.Find("KnapsackPanel").GetComponent<Knapsack>();
			}
			return instance;
		}
	}
}
