using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AS_2D.FSM
{
    [CreateAssetMenu(fileName = "StateConfig_SO", menuName = "SO��������/״̬����SO", order = 1)]
    public class StateConfig_SO : ScriptableObject
    {
        [Tooltip("��ǰ״̬��stateID��ö��ֵ")]
        public FSM_StateID stateID;
        [Tooltip("��ǰ״̬ת��������ö��ֵ�б�trigger_IDs")]
        public List<FSM_TriggerID> trigger_IDs;
        [Tooltip("��ǰ״̬ת������������ת��״̬�б�triggerID_stateID")]
        public List<triggerID_stateID> map;
        
        [Serializable]
        public struct triggerID_stateID
        {
            public FSM_TriggerID triggerID;
            public FSM_StateID stateID;
        }
    }

    
}
