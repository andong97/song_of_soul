using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AS_2D.FSM;

public class TestTrigger: FSM_BaseTrigger
{
    public override bool IsTriggerReach(FSM_Manager fsm_Manager)
    {
        Debug.Log("����Ϊִ������");
        if (true)
        {
            Debug.Log("���������Ļ�ִ��ֻ�ܲ��������ڴ�");
            return true;
        }
     }

    protected override void InitTrigger()
    {
        triggerID = FSM_TriggerID.Test;
    }

}
