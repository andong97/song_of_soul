using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public  class EnemyFSMBaseState
{
    protected EnemyFSMManager fsmManager;
    [DisplayOnly]
    public  EnemyStates stateID;
    /// <summary>
    /// Ĭ�ϲ���ֵʱ����ȡ��ΪNullStateIDö��ֵ�����ڶ�Ӧ���ำ��Ӧö��IDֵ
    /// </summary>

    protected List<EnemyFSMBaseTrigger> triggers = new List<EnemyFSMBaseTrigger>();
    public void ClearTriggers()
    {
        triggers.Clear();
    }



    public EnemyFSMBaseState()
    {
        InitState();
    }

    /// <summary>
    /// ��ʼ����������Ҫ����Ϊ��ֵsateID(���б��븳ֵ)��������������ʵ��
    /// </summary>
    /// <param name="fsm_StateID">��ֵsateID</param>
    protected virtual void InitState() { triggers.Clear(); }

    /// <summary>
    /// Ϊ��״̬��������б���
    /// </summary>
    /// <param name="triggerID">Ҫ��ӵ�������triggerIDö�٣�ȷ����ö��ֵ�Ͷ�Ӧtrgger������Ӧ��ȷ</param>

    public void AddTriggers(EnemyTrigger triggerID,EnemyStates targetState) 
    {
        //Debug.Log(triggerID);

        Type type = Type.GetType(triggerID + "Trigger");
        if (type == null)
        {
            Debug.LogError(triggerID + "�޷���ӵ�" + stateID + "��triggers�б�");
            Debug.LogError("�������Trigger����������ö�٣���Ӧ�����������ϡ�Trigger������ö��ֵΪPressBtn����������ΪPreesBtnTrigger���������ü��أ�");
        }
        else 
        {
            triggers.Add(Activator.CreateInstance(type) as EnemyFSMBaseTrigger);
            triggers[triggers.Count - 1].targetState = targetState;
        }
    }
    public void AddTriggers(EnemyFSMBaseTrigger trigger)
    {
        triggers.Add(trigger);
    }


    /// <summary>
    /// ����״̬ʱ����
    /// </summary>
    public virtual void EnterState(EnemyFSMManager fSM_Manager) { }

    /// <summary>
    /// �˳�״̬ʱ����
    /// </summary>
    public virtual void ExitState(EnemyFSMManager fSM_Manager) { }

    /// <summary>
    /// ״̬������ˢ��
    /// </summary>
    public virtual void Act_State(EnemyFSMManager fSM_Manager) { }
    /// <summary>
    /// �ﵽ��������״̬������Ȼ��ִ�и�״̬
    /// </summary>
    public virtual void TriggerState(EnemyFSMManager fsm_Manager)
    {
        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReach(fsm_Manager))
            {
                fsm_Manager.ChangeState(triggers[i].targetState);
            }
        }
    }
}
