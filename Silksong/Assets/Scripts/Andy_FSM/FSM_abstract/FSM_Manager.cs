using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

        
namespace AS_2D.FSM {
    public abstract class FSM_Manager:MonoBehaviour
    {

        //private Animator anim;
        //private AudioSource audio;
        /// /// <summary>
        /// ��ǰ״̬
        /// </summary>
        public FSM_BaseState currentState;
        /// <summary>
        /// ��ǰ״̬ID
        /// </summary>
        public FSM_StateID currentStateID;
        /// <summary>
        /// Ĭ��״̬
        /// </summary>
        public FSM_BaseState defaultState;
        /// <summary>
        /// Ĭ��״̬��ID
        /// </summary>
        public FSM_StateID defaultStateID;
        /// <summary>
        /// ��ǰ״̬������������״̬�б�
        /// </summary>
        public List<FSM_BaseState> states = new List<FSM_BaseState>();
        /// <summary>
        /// ����״̬�б����Ӧ�����б��SO�ļ�
        /// </summary>
        public List<StateConfig_SO> stateConfig_SO;

        public void ChangeState(FSM_TriggerID tiggerID) {
            FSM_StateID targetStateID = currentState.GetTriggerID(tiggerID);

            currentState.ExitState(this);
            if (targetStateID == FSM_StateID.NullStateID)
            {
                currentState = defaultState;

                return;
            }

            if (targetStateID == defaultStateID)
            {
                currentState = defaultState;
                return;
            }
            else {
                currentState = states.Find(p => p.stateID == targetStateID);
                currentStateID = targetStateID;
            }

            currentState.EnterState(this);
        }

        FSM_BaseState AddState(FSM_StateID stateID)
        {
            //Debug.Log(triggerID);

            Type type = Type.GetType(stateID + "_State");
            if (type == null)
            {
                Debug.LogError(stateID + "�޷���ӵ�"  + "��states�б�");
                Debug.LogError("���stateIDö��ֵ����Ӧ��������Ӧö�������ϡ�_State������ö��ֵΪIdle��״̬����ΪIdle_State���������ü��أ�");
                return null;
            }
            else
            {
                FSM_BaseState temp = Activator.CreateInstance(type) as FSM_BaseState;
                states.Add(temp);
                return temp;
            }
        }
        /// <summary>
        /// ���ڳ�ʼ��״̬���ķ������������״̬����������ӳ�����ȡ��������ȡ�Awakeʱִ�У��ɲ�ʹ�û��෽���ֶ��������
        /// </summary>
        public virtual void InitStates() {
            //Debug.Log("initStatesͨ��SO������ض�Ӧ״̬�߼�����");
            //Ϊ��ǰ״̬���������������״̬
            for (int i = 0; i < stateConfig_SO.Count; i++)
            {
                
                FSM_BaseState temp =  AddState(stateConfig_SO[i].stateID);
                //��Ӷ�Ӧ״̬�����������б�
                for (int j = 0; j < stateConfig_SO[i].trigger_IDs.Count; j++)
                {
                    temp.AddTriggers (stateConfig_SO[i].trigger_IDs[j]);
                }
                //Ϊ��Ӧ״̬�������-״̬�Ĵ����ֵ�
                for (int k = 0; k < stateConfig_SO[i].map.Count; k++)
                {
                    //Debug.Log(stateConfig_SO[i].map[k].triggerID+"," + stateConfig_SO[i].map[k].stateID);
                    temp.AddTriggerStateID_map(stateConfig_SO[i].map[k].triggerID, stateConfig_SO[i].map[k].stateID);
                }
            }

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
            InitStates();
        }

        private void Start()
        {
            //Ĭ��״̬����
            defaultState = states.Find(p => p.StateID == defaultStateID);
            currentStateID = defaultStateID;
            currentState = defaultState;
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
            else {
                Debug.LogError("currentStateΪ��");
            }
            
            
        }

    }
}
