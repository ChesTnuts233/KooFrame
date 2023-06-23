using System;
using GridSystem;
using UnityEngine;

namespace GamePlay
{
    public class GridXZTesting : MonoBehaviour
    {
        private GridMapXZ<int> gridtest;
        private void Start()
        {
            gridtest = new GridMapXZ<int>(10, 10, 1f, new Vector3(0, 0, 0));
        }
    }
}