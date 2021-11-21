using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Enemy_Idle_State : EnemyFSMBaseState
{
    public override void Act_State(FSMManager<EnemyStates, EnemyTriggers> fSM_Manager)
    {
        fsmManager = fSM_Manager;
    }
    public override void EnterState(FSMManager<EnemyStates, EnemyTriggers> fSM_Manager)
    {
        fSM_Manager.GetComponent<Animator>().Play("Enemy_Idle");
        fSM_Manager.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
    public override void InitState(FSMManager<EnemyStates, EnemyTriggers> fSM_Manager)
    {
        base.InitState(fSM_Manager);
    }
}
