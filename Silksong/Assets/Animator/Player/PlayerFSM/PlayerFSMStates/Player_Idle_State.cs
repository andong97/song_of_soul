using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Idle_State : PlayerFSMBaseState
{
    public override void Act_State(FSMManager<PlayerStates, PlayerTriggers> fSM_Manager)
    {
        //Debug.Log("Player_Idle_State" + fSM_Manager.currentStateInfo.normalizedTime);
    }
}

