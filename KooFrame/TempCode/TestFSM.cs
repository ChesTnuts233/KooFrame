using System;
using System.Collections;
using UnityEngine;
using KooFrame.BaseSystem;
using KooFrame.KooTools.State_Utils;
using Unity.VisualScripting.FullSerializer;


public class TestFSM : KooMonoBehaviour
{
    private FSM fsm;
    private FSMState firstState;
    private FSMState secondState;
    private FSMState thirdState;
    private FSMTranslation firstToSecond;
    private FSMTranslation secondToThird;
    private FSMTranslation thirdToFirst;

    private void Awake()
    {
        fsm = new FSM();
        //创建状态
        firstState = new FSMState("first");
        secondState = new FSMState("second");
        thirdState = new FSMState("third"); //创建跳转
        firstToSecond = new FSMTranslation(firstState, "ftos", secondState, FTS);
        secondToThird = new FSMTranslation(secondState, "stot", thirdState, STT);
        thirdToFirst = new FSMTranslation(thirdState, "ttof", firstState, TTF);
    }

    private IEnumerator Start()
    {
        fsm.AddState(firstState);
        fsm.AddState(secondState);
        fsm.AddState(thirdState);
        fsm.AddTranslation(firstToSecond);
        fsm.AddTranslation(secondToThird);
        fsm.AddTranslation(thirdToFirst);
        fsm.StartFSM(firstState);
        yield return new WaitForSeconds(1f);
        fsm.HandleEvent("ftos");
        yield return new WaitForSeconds(1f);
        fsm.HandleEvent("stot");
        yield return new WaitForSeconds(1f);
        fsm.HandleEvent("ttof");
    }

    public void FTS()
    {
        Debug.Log("触发了一状态到二状态");
    }

    public void STT()
    {
        Debug.Log("触发了二状态到三状态");
    }

    public void TTF()
    {
        Debug.Log("触发了三状态到一状态");
    }

    protected override void BeforeOnDestroy()
    {
        
    }
}