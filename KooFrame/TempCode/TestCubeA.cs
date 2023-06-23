using System;
using System.Collections;
using System.Collections.Generic;
using KooFrame;
using UnityEngine;

public class TestCubeA : MonoBehaviour
{
    private ResLoader _resLoader = new ResLoader();
    public Material test;
    private void Start()
    {
        

        _resLoader.LoadSync<Material>("resources://Select");

        _resLoader.LoadASync<Material>("Resources://Select", select =>
        {
            test = select;
            Debug.Log(test.name);
            Debug.Log(Time.time);
        });
        Fun();
    }

    void Fun()
    {
        _resLoader.LoadSync<Material>("resources://Select");
    }

    private void OnDestroy()
    {
        _resLoader.ReleaseAll();
        _resLoader = null;
    }
}
