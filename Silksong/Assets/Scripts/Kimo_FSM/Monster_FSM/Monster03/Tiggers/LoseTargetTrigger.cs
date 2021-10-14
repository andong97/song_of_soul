using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTargetTrigger : EnemyFSMBaseTrigger
{
    GameObject target;
    public float outRange = 5;
    public override void InitTrigger(FSMManager<EnemyStates, EnemyTriggers> fSMManager)
    {
        base.InitTrigger(fSMManager);
        triggerID = EnemyTriggers.LoseTargetTrigger;
        targetState = EnemyStates.WanderState;
    }
    public override bool IsTriggerReach(FSMManager<EnemyStates, EnemyTriggers> fsm_Manager)
    {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        if ((target.transform.position - fsm_Manager.transform.position).magnitude > outRange)
            return true;
        return false;
    }
}
