using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;



public abstract class EnemyFSMManager : MonoBehaviour
{

    public Animator animator;
    public AudioSource audio;
    public Rigidbody2D rigidbody;
    /// /// <summary>
    /// ��ǰ״̬
    /// </summary>
    public EnemyFSMBaseState currentState;
    [DisplayOnly]
    public EnemyStates currentStateID;
    /// <summary>
    /// Ĭ��״̬
    /// </summary>
    public EnemyFSMBaseState defaultState;
    [DisplayOnly]
    public EnemyStates defaultStateID;
    /// <summary>
    /// ��ǰ״̬������������״̬�б�
    /// </summary>
    public Dictionary<EnemyStates, EnemyFSMBaseState> statesDic = new Dictionary<EnemyStates, EnemyFSMBaseState>();
    /// <summary>
    /// ����״̬�б����Ӧ�����б��SO�ļ�
    /// </summary>
    public List<State_SO_Config> stateConfigs;

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
        for (int i = 0; i < stateConfigs.Count; i++)
        {
            EnemyFSMBaseState tem = stateConfigs[i].stateConfig ;
            tem.ClearTriggers();
            foreach(var value in stateConfigs[i].triggerList)
            {
                tem.AddTriggers(value as EnemyFSMBaseTrigger);
            }
            statesDic.Add(stateConfigs[i].stateID, tem);
        }

        ////�����ȡ
        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
        if (GetComponent<AudioSource>() != null)
        {
            audio = GetComponent<AudioSource>();
        }
        if(GetComponent<Rigidbody2D>()!=null)
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

    }

    private void Awake()
    {
        statesDic.Clear();
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
