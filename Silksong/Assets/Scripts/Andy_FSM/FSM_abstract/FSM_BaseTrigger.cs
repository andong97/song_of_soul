using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS_2D.FSM {
    public abstract class FSM_BaseTrigger
    {
        public FSM_TriggerID triggerID;
        /// <summary>
        /// �������InitTrigger������ֵtriggerID��ʼֵ
        /// </summary>
        public FSM_BaseTrigger()
        {
            InitTrigger();
        }

        /// <summary>
        /// ��ʼ����������Ҫ����Ϊ��ֵtriggerID(���б��븳ֵ)��������������ʵ��
        /// </summary>
        protected abstract void InitTrigger();
        /// <summary>
        /// �Ƿ�ﵽ���������жϷ���
        /// </summary>
        /// <param name="fsm_Manager">������Ӧ״̬���fsm_manager</param>
        /// <returns></returns>
        public abstract bool IsTriggerReach(FSM_Manager fsm_Manager);
    }
}
