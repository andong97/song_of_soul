using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ״̬���������Ļ���
/// </summary>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
public abstract class FSMManager<T1,T2> : MonoBehaviour
{

    public Animator animator;
    public AudioSource audios;
    public Rigidbody2D rigidbody2d;

   // public Collider2D triggerCollider;
    public Collision2D collision;

    public DamageableBase damageable;

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
        //Debug.Log(state);
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
        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
        if (GetComponent<AudioSource>() != null)
        {
            audios = GetComponent<AudioSource>();
        }
        if (GetComponent<Rigidbody2D>() != null)
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        InitWithScriptableObject();
        ////�����ȡ
    }

    protected void Awake()
    {
        statesDic.Clear();
        InitManager();
        damageable = GetComponent<DamageableBase>();
    }

    protected void Start()
    {
        if (statesDic.Count == 0)
            return;
        //Ĭ��״̬����
        currentStateID = defaultStateID;
        ChangeState(currentStateID);
        if (anyState != null)
            anyState.EnterState(this);

        //// Debug code
        //foreach (var state in statesDic.Values)
        //    foreach (var value in state.triggers)
        //    {
        //        Debug.LogWarning(this + "  " + state + "  " + value + "  " + value.GetHashCode());
        //    }

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

        if (anyState != null)
        {
            anyState.Act_State(this);
            anyState.TriggerState(this);
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

            anyState = (FSMBaseState<EnemyStates, EnemyTriggers>)ObjectClone.CloneObject(anyStateConfig.stateConfig);
            anyState.triggers = new List<FSMBaseTrigger<EnemyStates, EnemyTriggers>>();
            for (int k=0;k<anyStateConfig.triggerList.Count; k++)
            {
                anyState.triggers.Add(ObjectClone.CloneObject(anyStateConfig.triggerList[k]) as FSMBaseTrigger<EnemyStates, EnemyTriggers>);
                anyState.triggers[anyState.triggers.Count - 1].InitTrigger(this);
                //Debug.Log(this.gameObject.name+"  "+ anyState.triggers[anyState.triggers.Count - 1]+"  "+anyState.triggers[anyState.triggers.Count - 1].GetHashCode());
            }
            anyState.InitState(this);
        }
        for (int i = 0; i < stateConfigs.Count; i++)
        {
            FSMBaseState<EnemyStates, EnemyTriggers> tem = ObjectClone.CloneObject(stateConfigs[i].stateConfig) as FSMBaseState<EnemyStates, EnemyTriggers>;
            tem.triggers = new List<FSMBaseTrigger<EnemyStates, EnemyTriggers>>();
            for (int k=0;k< stateConfigs[i].triggerList.Count;k++)
            {
                tem.triggers.Add(ObjectClone.CloneObject(stateConfigs[i].triggerList[k]) as FSMBaseTrigger<EnemyStates, EnemyTriggers>);
                tem.triggers[tem.triggers.Count-1].InitTrigger(this);
                //Debug.Log(this.gameObject.name + "  " + tem.triggers[tem.triggers.Count - 1] + "  " + tem.triggers[tem.triggers.Count - 1].GetHashCode());
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
    public Enemy_State_SO_Config anyStateConfig;
    public override void InitWithScriptableObject()
    {
        if (anyStateConfig != null)
        {
            anyState = (FSMBaseState<NPCStates, NPCTriggers>)ObjectClone.CloneObject(anyStateConfig.stateConfig);
            anyState.triggers = new List<FSMBaseTrigger<NPCStates, NPCTriggers>>();
            for (int k = 0; k < anyStateConfig.triggerList.Count; k++)
            {
                anyState.triggers.Add(ObjectClone.CloneObject(anyStateConfig.triggerList[k]) as FSMBaseTrigger<NPCStates, NPCTriggers>);
                anyState.triggers[anyState.triggers.Count - 1].InitTrigger(this);
                //Debug.Log(this.gameObject.name+"  "+ anyState.triggers[anyState.triggers.Count - 1]+"  "+anyState.triggers[anyState.triggers.Count - 1].GetHashCode());
            }
            anyState.InitState(this);
        }
        for (int i = 0; i < stateConfigs.Count; i++)
        {
            FSMBaseState<NPCStates, NPCTriggers> tem = ObjectClone.CloneObject(stateConfigs[i].stateConfig) as FSMBaseState<NPCStates, NPCTriggers>;
            tem.triggers = new List<FSMBaseTrigger<NPCStates, NPCTriggers>>();
            for (int k = 0; k < stateConfigs[i].triggerList.Count; k++)
            {
                tem.triggers.Add(ObjectClone.CloneObject(stateConfigs[i].triggerList[k]) as FSMBaseTrigger<NPCStates, NPCTriggers>);
                tem.triggers[tem.triggers.Count - 1].InitTrigger(this);
                //Debug.Log(this.gameObject.name + "  " + tem.triggers[tem.triggers.Count - 1] + "  " + tem.triggers[tem.triggers.Count - 1].GetHashCode());
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
/// Ȼ���Player_State_SO_Config�ű���ȡ������Player_State_SO_Config���ע�ͼ��ɡ�
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
    //        anyState = (FSMBaseState<PlayerStates, PlayerTriggers>)ObjectClone.CloneObject(anyStateConfig.stateConfig);
    //        anyState.triggers = new List<FSMBaseTrigger<PlayerStates, PlayerTriggers>>();
    //        for (int k = 0; k < anyStateConfig.triggerList.Count; k++)
    //        {
    //            anyState.triggers.Add(ObjectClone.CloneObject(anyStateConfig.triggerList[k]) as FSMBaseTrigger<PlayerStates, PlayerTriggers>);
    //            anyState.triggers[anyState.triggers.Count - 1].InitTrigger(this);
    //            //Debug.Log(this.gameObject.name+"  "+ anyState.triggers[anyState.triggers.Count - 1]+"  "+anyState.triggers[anyState.triggers.Count - 1].GetHashCode());
    //        }
    //        anyState.InitState(this);
    //    }
    //    for (int i = 0; i < stateConfigs.Count; i++)
    //    {
    //        FSMBaseState<PlayerStates, PlayerTriggers> tem = ObjectClone.CloneObject(stateConfigs[i].stateConfig) as FSMBaseState<PlayerStates, PlayerTriggers>;
    //        tem.triggers = new List<FSMBaseTrigger<PlayerStates, PlayerTriggers>>();
    //        for (int k = 0; k < stateConfigs[i].triggerList.Count; k++)
    //        {
    //            tem.triggers.Add(ObjectClone.CloneObject(stateConfigs[i].triggerList[k]) as FSMBaseTrigger<PlayerStates, PlayerTriggers>);
    //            tem.triggers[tem.triggers.Count - 1].InitTrigger(this);
    //            //Debug.Log(this.gameObject.name + "  " + tem.triggers[tem.triggers.Count - 1] + "  " + tem.triggers[tem.triggers.Count - 1].GetHashCode());
    //        }
    //        statesDic.Add(stateConfigs[i].stateID, tem);
    //        tem.InitState(this);
    //    }
    //}
}

