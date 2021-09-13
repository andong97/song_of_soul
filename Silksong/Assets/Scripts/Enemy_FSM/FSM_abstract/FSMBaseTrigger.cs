using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;

[Serializable]
public  class FSMBaseTrigger<T1,T2>
{
    [DisplayOnly]
    public T2 triggerID;
    public T1 targetState;
    /// <summary>
    /// ���츳ֵtriggerTransitionID��ʼֵ
    /// </summary>
    /// <param name="trigger_TransitionID"></param>
    /// 
    public FSMBaseTrigger()
    {
        InitTrigger();
    }
    public FSMBaseTrigger(T1 targetState)
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
    public virtual bool IsTriggerReach(FSMManager<T1,T2> fsm_Manager) { return false; }

}
public class EnemyFSMBaseTrigger : FSMBaseTrigger<EnemyStates, EnemyTrigger> 
{
    public EnemyFSMBaseTrigger(EnemyStates targetState):base(targetState)
    {

    }
    public EnemyFSMBaseTrigger()
    { }
}