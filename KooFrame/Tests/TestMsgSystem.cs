using System.Collections;
using KooFrame.BaseSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace KooFrame.Test
{
    public class TestMsgSystem : KooMonoBehaviour
    {
#if UNITY_EDITOR
        
#endif
        private void Awake()
        {
            RegisterMsg("Do", DoSomething);
            RegisterMsg("Do", DoSomething1);
            RegisterMsg("Do1", DoSomething1);
        }

        private IEnumerator Start()
        {
            SendMsg("Do", "hello");
            
            UnRegisterAllMsg("Do");
            yield return new WaitForSeconds(1.5f);
            
            SendMsg("Do", "hello1");
            SendMsg("Do1","hello2");

            yield return new WaitForSeconds(2f);

            //Destroy(this.gameObject);
            //yield return new WaitForSeconds(2.0f);
        }

        void DoSomething(object data)
        {
            // do something
            Debug.LogFormat("0Received Do msg:{0}", data);
        }
        
        void DoSomething1(object data)
        {
            // do something
            Debug.LogFormat("1Received Do msg:{0}", data);
        }


        protected override void BeforeOnDestroy()
        {
        }
    }
}