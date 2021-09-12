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
    /// 构造赋值triggerTransitionID初始值
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
    /// 初始化方法，必要操作为赋值triggerID(自行编码赋值)，可做其他操作实现
    /// </summary>
    protected virtual void InitTrigger() { }
    /// <summary>
    /// 是否达到该条件的判断方法
    /// </summary>
    /// <param name="fsm_Manager">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReach(EnemyFSMManager fsm_Manager) { return false; }

}