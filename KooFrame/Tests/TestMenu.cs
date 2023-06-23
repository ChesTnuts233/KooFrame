#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


namespace KooFrame.Test
{
    public class TestMenu
    {
        [MenuItem("KooFramework/测试/消息系统测试1")]
        private static void Test()
        {
            KooUtils.RegisterMgs("test1",data=>{Debug.LogFormat("消息："+data);});
            KooUtils.UnRegisterMgs("test1");
            KooUtils.SendMgs("test1", "你好");
        }

        [UnityEditor.MenuItem("KooFramework/测试/消息系统测试2", false, 1)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("MsgReceiverObj")
                .AddComponent<TestMsgSystem>();
        }
        
        [MenuItem("KooFramework/测试/单例测试")]
        private static void TestSingleton()
        {
            
        }

    }
}
#endif