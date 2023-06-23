using System;
using System.Collections;
using System.Collections.Generic;
using KooFrame;
using UnityEngine;

public class TestCubeC : MonoBehaviour
{
    public Material test;
    private void Start()
    {
        test = Resources.Load<Material>("Select");
    }

    private void Update()
    {
        Debug.Log(test.name);
    }
}
