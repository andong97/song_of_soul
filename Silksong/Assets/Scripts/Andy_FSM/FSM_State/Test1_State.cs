using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AS_2D.FSM;

public class Test1_State: FSM_BaseState
{
    public override void Act_State(FSM_Manager fSM_Manager)
    {
        
        Debug.Log("����Ĭ��״̬Test1_State����Ϊ����");
    }

    protected override void InitState()
    {
        stateID = FSM_StateID.Test1;
    }
}
