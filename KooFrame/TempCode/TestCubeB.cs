using System;
using System.Collections;
using System.Collections.Generic;
using KooFrame;
using UnityEngine;

public class TestCubeB : MonoBehaviour
{
    private ResLoader _resLoader = new ResLoader();
    public Material test;
    private IEnumerator Start()
    {
        _resLoader.LoadSync<Material>("Resources://Select");
        Fun();
        yield return new WaitForSeconds(3f);
        _resLoader.LoadASync<Material>("resources://Select", select =>
        {
            test = select;
            Debug.Log(test.name);
            Debug.Log(Time.time);
        });
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
