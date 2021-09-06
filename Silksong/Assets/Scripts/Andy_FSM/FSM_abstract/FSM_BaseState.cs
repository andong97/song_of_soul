using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS_2D.FSM
{
    public abstract class FSM_BaseState
    {
        public FSM_Manager fsmManager;
        public FSM_StateID stateID;
        /// <summary>
        /// Ĭ�ϲ���ֵʱ����ȡ��ΪNullStateIDö��ֵ�����ڶ�Ӧ���ำ��Ӧö��IDֵ
        /// </summary>
        public FSM_StateID StateID
        {
            get
            {
                if (stateID != FSM_StateID.NullStateID)
                {
                    return stateID;
                }
                Debug.Log("���ڶ�Ӧ�����stateID����Ӧö��IDֵ����ǰδ��ֵΪFSM_StateID.NullStateID");
                return FSM_StateID.NullStateID;
            }
        }

        protected List<FSM_BaseTrigger> triggers = new List<FSM_BaseTrigger>();
        /// <summary>
        /// ����������״̬ID��Ӧ���ֵ�ӳ���
        /// </summary>
        protected Dictionary<FSM_TriggerID, FSM_StateID> TriggerStateID_map = new Dictionary<FSM_TriggerID, FSM_StateID>();


       
        public FSM_BaseState()
        {
            InitState();
         }

        /// <summary>
        /// ��ʼ����������Ҫ����Ϊ��ֵsateID(���б��븳ֵ)��������������ʵ��
        /// </summary>
        /// <param name="fsm_StateID">��ֵsateID</param>
        protected abstract void InitState();
       
        /// <summary>
        /// Ϊ��״̬��������б���
        /// </summary>
        /// <param name="triggerID">Ҫ��ӵ�������triggerIDö�٣�ȷ����ö��ֵ�Ͷ�Ӧtrgger������Ӧ��ȷ</param>

        public void AddTriggers(FSM_TriggerID triggerID) {
            //Debug.Log(triggerID);
            
            Type type = Type.GetType(triggerID + "Trigger");
            if (type == null)
            {
                Debug.LogError(triggerID + "�޷���ӵ�" + stateID + "��triggers�б�");
                Debug.LogError("�������Trigger����������ö�٣���Ӧ�����������ϡ�Trigger������ö��ֵΪPressBtn����������ΪPreesBtnTrigger���������ü��أ�");
            }
            else {
                triggers.Add(Activator.CreateInstance(type) as FSM_BaseTrigger);
            }
        }

        /// <summary>
        /// ��Ӵ�����������Ӧ״̬
        /// </summary>
        /// <param name="_triggerID">��������ö��ֵ</param>
        /// <param name="ID">״̬ö��ֵ</param>
        public void AddTriggerStateID_map(FSM_TriggerID _triggerID,FSM_StateID ID) {
            if (_triggerID == FSM_TriggerID.NullTriggerTransition)
            {
                Debug.LogError("���ܽ���NullTriggerTransition�����");
                return;
            }
            if (ID == FSM_StateID.NullStateID)
            {
                Debug.LogError("���ܽ���NullStateID�����");
                return;
            }
            if (TriggerStateID_map.ContainsKey(_triggerID))
            {
                Debug.LogError(_triggerID+"���������Ѵ��ڣ��޷��ٴ����");
                return;
            }
            //AddTriggers(_triggerID);
            TriggerStateID_map.Add(_triggerID, ID);
        }


        /// <summary>
        /// ɾ�����������Լ���Ӧ��״̬
        /// </summary>
        /// <param name="trigger">��������ö��ֵ</param>
        public void DeleteTriggerStateID_map(FSM_TriggerID trigger)
        {
            if (trigger == FSM_TriggerID.NullTriggerTransition)
            {
                Debug.LogError("���ܽ���NullTriggerTransition��ɾ��");
                return;
            }
            
            if (!TriggerStateID_map.ContainsKey(trigger))
            {
                Debug.LogError(trigger + "�������������ڣ��޷�ɾ��");
                return;
            }
            TriggerStateID_map.Remove(trigger);
        }

        /// <summary>
        /// ���ݴ���������ȡ����ǰ����״̬�ֵ��ڵ����Ӧ״̬ID
        /// </summary>
        /// <param name="trigger">��������ö��ֵ</param>
        /// <returns></returns>
        public FSM_StateID GetTriggerID(FSM_TriggerID trigger) {
            if (TriggerStateID_map.ContainsKey(trigger))
            {
                return TriggerStateID_map[trigger];
            }
            Debug.LogError(trigger + "�������������ڣ��޷�����");
            return FSM_StateID.NullStateID;
        }


        /// <summary>
        /// ����״̬ʱ���ã�Ĭ�ϻ��෽��ֻ��ֵ״̬���е�fsmManger
        /// </summary>
        public virtual void EnterState(FSM_Manager fSM_Manager) 
        {
            fsmManager = fSM_Manager;
        }
        
        /// <summary>
        /// �˳�״̬ʱ����
        /// </summary>
        public virtual void ExitState(FSM_Manager fSM_Manager) { }

        /// <summary>
        /// ״̬������ˢ��
        /// </summary>
        public abstract void Act_State(FSM_Manager fSM_Manager);
        /// <summary>
        /// �ﵽ��������״̬������Ȼ��ִ�и�״̬
        /// </summary>
        public virtual void TriggerState(FSM_Manager fsm_Manager)
        {
            for (int i = 0; i < triggers.Count; i++)
            {
                if(triggers[i].IsTriggerReach(fsm_Manager))
                {
                    fsm_Manager.ChangeState(triggers[i].triggerID);
                }
             }
        }

        
    }
}

