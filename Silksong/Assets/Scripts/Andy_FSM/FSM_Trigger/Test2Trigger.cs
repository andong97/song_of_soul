using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AS_2D.FSM;

public class Test2Trigger: FSM_BaseTrigger
{
    public override bool IsTriggerReach(FSM_Manager fsm_Manager)
    {
        Debug.Log("����Ϊtest2��ִ�м������");
        if (true)
        {
            Debug.Log("���������Ļ�����false����ִ��test2״̬����ʱ�����������test2״̬������");
            return false;
        }
     }

    protected override void InitTrigger()
    {
        triggerID = FSM_TriggerID.Test2;
    }

}
