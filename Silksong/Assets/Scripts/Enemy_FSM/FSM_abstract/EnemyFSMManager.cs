using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;



public abstract class EnemyFSMManager : MonoBehaviour
{

    //private Animator anim;
    //private AudioSource audio;
    public  Enemy_Parameters param;
    /// /// <summary>
    /// ��ǰ״̬
    /// </summary>
    public EnemyFSMBaseState currentState;
    public EnemyStates currentStateID;
    /// <summary>
    /// Ĭ��״̬
    /// </summary>
    public EnemyFSMBaseState defaultState;
    public EnemyStates defaultStateID;
    /// <summary>
    /// ��ǰ״̬������������״̬�б�
    /// </summary>
    public Dictionary<EnemyStates, EnemyFSMBaseState> statesDic = new Dictionary<EnemyStates, EnemyFSMBaseState>();
    /// <summary>
    /// ����״̬�б����Ӧ�����б��SO�ļ�
    /// </summary>
    public List<AS_2D.FSM.StateConfig_SO> stateConfig_SO;

    public void ChangeState(EnemyStates state)
    {
        if (currentState != null)
            currentState.ExitState(this);
        if (statesDic.ContainsKey(state))
        {
            currentState = statesDic[state];
            currentStateID = state;
        }
        else
        {
            Debug.LogError("����״̬������");
        }
        currentState.EnterState(this);
    }

    public EnemyFSMBaseState AddState(EnemyStates state)
    {
        //Debug.Log(triggerID);

        Type type = Type.GetType("Enemy"+state + "State");
        if (type == null)
        {
            Debug.LogError(state + "�޷���ӵ�" + "��states�б�");
            Debug.LogError("���stateIDö��ֵ����Ӧ��������Ӧö�������ϡ�_State������ö��ֵΪIdle��״̬����ΪIdle_State���������ü��أ�");
            return null;
        }
        else
        {
            EnemyFSMBaseState temp = Activator.CreateInstance(type) as EnemyFSMBaseState;
            statesDic.Add(state,temp);
            return temp;
        }
    }
    public EnemyFSMBaseState AddState(EnemyStates state,EnemyFSMBaseState stateClass)
    {
        statesDic.Add(state, stateClass);
        return stateClass;
    }
    public void RemoveState(EnemyStates state)
    {
        if (statesDic.ContainsKey(state))
            statesDic.Remove(state);
    }
    /// <summary>
    /// ���ڳ�ʼ��״̬���ķ������������״̬����������ӳ�����ȡ��������ȡ�Awakeʱִ�У��ɲ�ʹ�û��෽���ֶ��������
    /// </summary>
    public virtual void InitStates()
    {
        //Debug.Log("initStatesͨ��SO������ض�Ӧ״̬�߼�����");
        //Ϊ��ǰ״̬���������������״̬
        //for (int i = 0; i < stateConfig_SO.Count; i++)
        //{

        //    EnemyFSMBaseState temp = AddState(stateConfig_SO[i].stateID);
        //    //��Ӷ�Ӧ״̬�����������б�
        //    for (int j = 0; j < stateConfig_SO[i].trigger_IDs.Count; j++)
        //    {
        //        temp.AddTriggers(stateConfig_SO[i].trigger_IDs[j]);
        //    }
        //    //Ϊ��Ӧ״̬�������-״̬�Ĵ����ֵ�
        //    for (int k = 0; k < stateConfig_SO[i].map.Count; k++)
        //    {
        //        //Debug.Log(stateConfig_SO[i].map[k].triggerID+"," + stateConfig_SO[i].map[k].stateID);
        //        temp.AddTriggerStateID_map(stateConfig_SO[i].map[k].triggerID, stateConfig_SO[i].map[k].stateID);
        //    }
        //}

        ////�����ȡ
        //if (GetComponent<Animator>()!= null)
        //{
        //    anim = GetComponent<Animator>();
        //}
        //if (GetComponent<AudioSource>()!=null)
        //{
        //    audio = GetComponent<AudioSource>();
        //}

    }

    private void Awake()
    {
        if (!param.rigidbody)
            param.rigidbody = GetComponent<Rigidbody2D>();
        if (!param.animator)
            param.animator = GetComponent<Animator>();
        InitStates();
    }

    private void Start()
    {
        //Ĭ��״̬����
        currentStateID = defaultStateID;
        currentState = statesDic[currentStateID];

    }

    private void Update()
    {

        if (currentState != null)
        {
            //ִ��״̬����
            currentState.Act_State(this);
            //���״̬�����б�
            currentState.TriggerState(this);
        }
        else
        {
            Debug.LogError("currentStateΪ��");
        }


    }

}
