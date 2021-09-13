using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public  class FSMBaseState<T1,T2>
{
    protected FSMManager<T1,T2> fsmManager;
    [DisplayOnly]
    public  T1 stateID;
    /// <summary>
    /// Ĭ�ϲ���ֵʱ����ȡ��ΪNullStateIDö��ֵ�����ڶ�Ӧ���ำ��Ӧö��IDֵ
    /// </summary>

    protected List<FSMBaseTrigger<T1,T2>> triggers = new List<FSMBaseTrigger<T1,T2>>();
    public void ClearTriggers()
    {
        triggers.Clear();
    }



    public FSMBaseState()
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

    public void AddTriggers(T2 triggerID,T1 targetState) 
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
            triggers.Add(Activator.CreateInstance(type) as FSMBaseTrigger<T1,T2>);
            triggers[triggers.Count - 1].targetState = targetState;
        }
    }
    public void AddTriggers(FSMBaseTrigger<T1,T2> trigger)
    {
        triggers.Add(trigger);
    }


    /// <summary>
    /// ����״̬ʱ����
    /// </summary>
    public virtual void EnterState(FSMManager<T1,T2> fSM_Manager) { }

    /// <summary>
    /// �˳�״̬ʱ����
    /// </summary>
    public virtual void ExitState(FSMManager<T1,T2> fSM_Manager) { }

    /// <summary>
    /// ״̬������ˢ��
    /// </summary>
    public virtual void Act_State(FSMManager<T1,T2> fSM_Manager) { }
    /// <summary>
    /// �ﵽ��������״̬������Ȼ��ִ�и�״̬
    /// </summary>
    public virtual void TriggerState(FSMManager<T1,T2> fsm_Manager)
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
public class EnemyFSMBaseState : FSMBaseState<EnemyStates,EnemyTrigger> 
{

}
