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
    /// ����״̬
    /// </summary>
    public FSMBaseState<T1,T2> anyState;
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
    public virtual void InitManager()
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
        InitManager();
    }

    private void Start()
    {
        
        //Ĭ��״̬����
        currentStateID = defaultStateID;
        ChangeState(currentStateID);
        if (anyState != null)
            anyState.EnterState(this);
    }

    private void Update()
    {
        if (anyState != null)
        {
            anyState.Act_State(this);
            anyState.TriggerState(this);
        }
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

/// <summary>
///����Enemy״̬������������Ϊ�����SO���ù���
/// </summary>
public class EnemyFSMManager : FSMManager<EnemyStates, EnemyTriggers> 
{
    public List<Enemy_State_SO_Config> stateConfigs;
    public Enemy_State_SO_Config anyStateConfig;
    public override void InitWithScriptableObject()
    {
        if(anyStateConfig!=null)
        {
            anyState = ObjectClone.CloneObject(anyStateConfig.stateConfig) as FSMBaseState<EnemyStates, EnemyTriggers>;
        }
        for (int i = 0; i < stateConfigs.Count; i++)
        {
            FSMBaseState<EnemyStates, EnemyTriggers> tem = ObjectClone.CloneObject(stateConfigs[i].stateConfig) as FSMBaseState<EnemyStates, EnemyTriggers>;
            tem.ClearTriggers();
            foreach (var value in stateConfigs[i].triggerList)
            {
                tem.AddTriggers(value as FSMBaseTrigger<EnemyStates, EnemyTriggers>);
                tem.triggers[tem.triggers.Count-1].InitTrigger(this);
            }
            statesDic.Add(stateConfigs[i].stateID, tem);
            tem.InitState(this);
        }
    }
}
/// <summary>
///����NPC״̬������������Ϊ�����SO���ù���
/// </summary>
public class NPCFSMManager : FSMManager<NPCStates, NPCTriggers>
{
    public List<NPC_State_SO_Config> stateConfigs;
    public NPC_State_SO_Config anyStateConfig;
    public override void InitWithScriptableObject()
    {
        if (anyStateConfig != null)
        {
            anyState = ObjectClone.CloneObject(anyStateConfig.stateConfig) as FSMBaseState<NPCStates, NPCTriggers>;
        }
        for (int i = 0; i < stateConfigs.Count; i++)
        {
            FSMBaseState<NPCStates, NPCTriggers> tem = ObjectClone.CloneObject(stateConfigs[i].stateConfig) as FSMBaseState<NPCStates, NPCTriggers>;
            tem.ClearTriggers();
            foreach (var value in stateConfigs[i].triggerList)
            {
                tem.AddTriggers(value as FSMBaseTrigger<NPCStates, NPCTriggers>);
                tem.triggers[tem.triggers.Count - 1].InitTrigger(this);
            }
            statesDic.Add(stateConfigs[i].stateID, tem);
            tem.InitState(this);
        }
    }
}
/// <summary>
/// ����Player״̬����������Ĭ��û�����SO���ù��ܣ�
/// ����Ҫ��
/// ����ȡ���������ע��
/// Ȼ���State_SO_Config�ű���ȡ������Player_State_SO_Config���ע�ͼ��ɡ�
/// 
/// </summary>
public class PlayerFSMManager : FSMManager<PlayerStates, PlayerTriggers> 
{
    //public List<Player_State_SO_Config> stateConfigs;
    //public Player_State_SO_Config anyStateConfig;
    //public override void InitWithScriptableObject()
    //{
    //    if (anyStateConfig != null)
    //    {
    //        anyState = ObjectClone.CloneObject(anyStateConfig.stateConfig) as FSMBaseState<PlayerStates, PlayerTriggers>;
    //    }
    //    for (int i = 0; i < stateConfigs.Count; i++)
    //    {
    //        FSMBaseState<PlayerStates, PlayerTriggers> tem = ObjectClone.CloneObject(stateConfigs[i].stateConfig) as FSMBaseState<PlayerStates, PlayerTriggers>;
    //        tem.ClearTriggers();
    //        foreach (var value in stateConfigs[i].triggerList)
    //        {
    //            tem.AddTriggers(value as FSMBaseTrigger<PlayerStates, PlayerTriggers>);
    //            tem.triggers[tem.triggers.Count - 1].InitTrigger(this);
    //        }
    //        statesDic.Add(stateConfigs[i].stateID, tem);
    //        tem.InitState(this);
    //    }
    //}
}

