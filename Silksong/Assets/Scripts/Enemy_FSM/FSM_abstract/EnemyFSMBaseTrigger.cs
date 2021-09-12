using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;

[Serializable]
public  class EnemyFSMBaseTrigger
{
    [DisplayOnly]
    public EnemyTrigger triggerID;
    public EnemyStates targetState;
    /// <summary>
    /// ���츳ֵtriggerTransitionID��ʼֵ
    /// </summary>
    /// <param name="trigger_TransitionID"></param>
    /// 
    public EnemyFSMBaseTrigger()
    {
        this.targetState = EnemyStates.Enemy_Idle_State;
        InitTrigger();
    }
    public EnemyFSMBaseTrigger(EnemyStates targetState)
    {
        this.targetState = targetState;
        InitTrigger();
    }


    /// <summary>
    /// ��ʼ����������Ҫ����Ϊ��ֵtriggerID(���б��븳ֵ)��������������ʵ��
    /// </summary>
    protected virtual void InitTrigger() { }
    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���
    /// </summary>
    /// <param name="fsm_Manager">������Ӧ״̬���fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReach(EnemyFSMManager fsm_Manager) { return false; }

}