using System.Collections;
using System.Collections.Generic;
using KooFrame.Managers;
using KooFrame.KooManagers;
using KooFrame.KooManagers.UIManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGame
{
    public class GameModel : KooPortalManager
    {
        public static void MyGameLoad()
        {
            SceneManager.LoadScene("Game");
        }
        protected override void LaunchInDevelopingMode()
        {
            KooUIManager.Instance.LoadPanel("Panel");
            Debug.Log("Game:"+Mode.ToString());
            Debug.Log("Developing");
        }

        protected override void LaunchInTestingMode()
        {
            Debug.Log("Game:"+Mode.ToString());
            Debug.Log("Testing");

        }

        protected override void LaunchInProductionMode()
        {
            Debug.Log("Game:"+Mode.ToString());
            Debug.Log("Production");

        }
    }
}

