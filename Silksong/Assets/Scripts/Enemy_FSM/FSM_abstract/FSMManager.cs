using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;



public abstract class FSMManager<T1,T2> : MonoBehaviour
{

    public Animator animator;
    public AudioSource audio;
    public Rigidbody2D rigidbody;
    /// /// <summary>
    /// ��ǰ״̬
    /// </summary>
    public FSMBaseState<T1,T2> currentState;
    [DisplayOnly]
    public T1 currentStateID;
    /// <summary>
    /// Ĭ��״̬
    /// </summary>
    public FSMBaseState<T1,T2> defaultState;
    [DisplayOnly]
    public T1 defaultStateID;
    /// <summary>
    /// ��ǰ״̬������������״̬�б�
    /// </summary>
    public Dictionary<T1, FSMBaseState<T1,T2>> statesDic = new Dictionary<T1, FSMBaseState<T1,T2>>();
    /// <summary>
    /// ����״̬�б����Ӧ�����б��SO�ļ�
    /// </summary>


    public void ChangeState(T1 state)
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

    public FSMBaseState<T1,T2> AddState(T1 state)
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
            FSMBaseState<T1,T2> temp = Activator.CreateInstance(type) as FSMBaseState<T1,T2>;
            statesDic.Add(state,temp);
            return temp;
        }
    }
    public FSMBaseState<T1,T2> AddState(T1 state,FSMBaseState<T1,T2> stateClass)
    {
        statesDic.Add(state, stateClass);
        return stateClass;
    }
    public void RemoveState(T1 state)
    {
        if (statesDic.ContainsKey(state))
            statesDic.Remove(state);
    }
    /// <summary>
    /// ���ڳ�ʼ��״̬���ķ������������״̬����������ӳ�����ȡ��������ȡ�Awakeʱִ�У��ɲ�ʹ�û��෽���ֶ��������
    /// </summary>
    /// 

    public virtual void InitWithScriptableObject()
    {
    }
    public virtual void InitStates()
    {


        InitWithScriptableObject();
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
        //currentState = statesDic[currentStateID];
        ChangeState(currentStateID);
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


public class EnemyFSMManager : FSMManager<EnemyStates, EnemyTrigger> 
{
    public List<Enemy_State_SO_Config> stateConfigs;
    public override void InitWithScriptableObject()
    {
        for (int i = 0; i < stateConfigs.Count; i++)
        {
            FSMBaseState<EnemyStates, EnemyTrigger> tem = stateConfigs[i].stateConfig;
            tem.ClearTriggers();
            foreach (var value in stateConfigs[i].triggerList)
            {
                tem.AddTriggers(value as FSMBaseTrigger<EnemyStates, EnemyTrigger>);
            }
            statesDic.Add(stateConfigs[i].stateID, tem);
        }
    }
}
