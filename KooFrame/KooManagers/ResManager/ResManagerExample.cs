using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace KooFrame.KooManagers.ResManager
{
    public class ResManagerExample : MonoBehaviour
    {
        public Material test;
        // private void Update()
        // {
        //     Debug.Log(test.name);
        // }
#if UNITY_EDITOR
        [MenuItem("KooFramework/测试/ResMgrExample", false)]
        static void MenuItem()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("ResManagerExample").AddComponent<ResManagerExample>();
            //new GameObject("Exmaple2").AddComponent<ResManagerExample>();
        }
#endif

        private ResLoader testLoader1 = new ResLoader();
        private ResLoader testLoader2 = new ResLoader();

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2.0f);
            testLoader1.LoadSync<Material>("resources://Sound/Powerup");
            //testLoader1.LoadSync<GameObject>("");
            //Debug.Log("资源加载了一个");
            yield return new WaitForSeconds(2.0f);
            testLoader1.LoadASync<Material>("resources://Select", select =>
            {
                test = select;
                Debug.Log(test.name);
                Debug.Log(Time.time);
            });
            yield return new WaitForSeconds(2.0f);
            test = testLoader2.LoadSync<Material>("resources://Select");
            yield return new WaitForSeconds(2.0f);
            testLoader1.ReleaseAll();
            yield return new WaitForSeconds(2.0f);
            testLoader2.ReleaseAll();
        }
    }
}