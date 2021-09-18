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
    public FSMBaseTrigger()
    {
    }
    public FSMBaseTrigger(T1 targetState)
    {
        this.targetState = targetState;
    }


    public virtual void InitTrigger(FSMManager<T1,T2> fSMManager) { }
    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���
    /// </summary>
    /// <param name="fsm_Manager">������Ӧ״̬���fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReach(FSMManager<T1,T2> fsm_Manager) { return false; }

}
public class EnemyFSMBaseTrigger : FSMBaseTrigger<EnemyStates, EnemyTriggers> 
{
    public EnemyFSMBaseTrigger(EnemyStates targetState):base(targetState){ }
    public EnemyFSMBaseTrigger(){ }
}
public class NPCFSMBaseTrigger : FSMBaseTrigger<NPCStates, NPCTriggers>
{
    public NPCFSMBaseTrigger(NPCStates targetState) : base(targetState) { }
    public NPCFSMBaseTrigger() { }
}
public class PlayerFSMBaseTrigger : FSMBaseTrigger<PlayerStates, PlayerTriggers>
{
    public PlayerFSMBaseTrigger(PlayerStates targetState) : base(targetState) { }
    public PlayerFSMBaseTrigger() { }
}