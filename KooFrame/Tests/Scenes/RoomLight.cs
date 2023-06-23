using UnityEngine;

namespace KooFrame.Tests.Scenes
{
    public class Light
    {
        public void OpenLight()
        {
            Debug.Log("灯打开了");
        }

        public void ClossLight()
        {
            Debug.Log("灯关闭了");
        }
    }

    public class RoomLight : RefCounterSimple
    {
        private Light m_Light = new Light();


        public void EnterPeople()
        {
            if (RefCount == 0)
            {
                m_Light.OpenLight();
            }

            Retain();
            Debug.LogFormat("一个人进入房间,当前房间有{0}", RefCount);
        }

        public void LeavePeople()
        {
            Release();
            Debug.LogFormat("一个人离开房间,当前房间有{0}", RefCount);
        }

        protected override void OnZeroRef()
        {
            m_Light.ClossLight();
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("KooFramework/测试/RoomLight", false)]
        static void MenuItem()
        {
            var room = new RoomLight();
            room.EnterPeople();
            room.EnterPeople();
            room.EnterPeople();
            room.LeavePeople();
            room.LeavePeople();
            room.LeavePeople();
            room.LeavePeople();
            room.EnterPeople();

            
            
        }
#endif
    }
}