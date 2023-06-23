using System.Collections;
using System.Collections.Generic;
using KooFrame.BaseSystem;
using KooFrame.Managers;
using UnityEngine;

namespace MyGame
{
    public class TestModel : KooPortalManager
    {
        public GameObject test;
        protected override void LaunchInDevelopingMode()
        {
            Debug.Log("Test:"+Mode.ToString());
            Debug.Log("testDeveloping");
        }

        protected override void LaunchInTestingMode()
        {
            Debug.Log("Test:"+Mode.ToString());
            Debug.Log("testTesting");
            //GameModel.MyGameLoad();
        }

        protected override void LaunchInProductionMode()
        {
            Debug.Log("Test:"+Mode.ToString());
            Debug.Log("testProduction");
        }
    }
}

