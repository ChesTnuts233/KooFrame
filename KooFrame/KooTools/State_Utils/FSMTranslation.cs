namespace KooFrame.KooTools.State_Utils
{
    //定义委托
    public delegate void FSMTranslationCallFunc();
    public class FSMTranslation
    {
        public FSMState FromState;
        public string name;
        public FSMState ToState;
        public FSMTranslationCallFunc callFunc; //回调函数

        public FSMTranslation(FSMState fromState, string name, FSMState toState, FSMTranslationCallFunc callFunc)
        {
            FromState = fromState;
            this.name = name;
            ToState = toState;
            this.callFunc = callFunc;
        }
    }
}