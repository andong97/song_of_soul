using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AS_2D.FSM;

public class Test2_State : FSM_BaseState
{
    public override void Act_State(FSM_Manager fSM_Manager)
    {
        Debug.Log("������test1״̬����test�������л�����test2��Ϊ״̬");
    }

    protected override void InitState()
    {
        stateID = FSM_StateID.Test2;
    }
}
