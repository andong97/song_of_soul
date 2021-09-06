using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EnemyFSMBaseTrigger
{
    public EnemyStates targetState;
    public EnemyTrigger triggerID;
    /// <summary>
    /// ���츳ֵtriggerTransitionID��ʼֵ
    /// </summary>
    /// <param name="trigger_TransitionID"></param>
    public EnemyFSMBaseTrigger(EnemyStates targetState)
    {
        this.targetState = targetState;
        InitTrigger();
    }

    /// <summary>
    /// ��ʼ����������Ҫ����Ϊ��ֵtriggerID(���б��븳ֵ)��������������ʵ��
    /// </summary>
    protected abstract void InitTrigger();
    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���
    /// </summary>
    /// <param name="fsm_Manager">������Ӧ״̬���fsm_manager</param>
    /// <returns></returns>
    public abstract bool IsTriggerReach(EnemyFSMManager fsm_Manager);
}