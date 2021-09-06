using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AS_2D.FSM
{


    /// <summary>
    /// State״̬ö�٣��Ͷ�Ӧ״̬��������һ��
    /// </summary>
    public enum FSM_StateID
    {
        NullStateID = 0,
        /// <summary>
        /// ԭ��״̬
        /// </summary>
        Idle,
        Walk,
        Run,
        Jump,
        JumpTwice,
        /// <summary>
        /// ��ǽ
        /// </summary>
        ClimbWall,
        /// <summary>
        /// ������
        /// </summary>
        ClimbLadder,
        /// <summary>
        /// ���
        /// </summary>
        Dash,
        /// <summary>
        /// ���γ��
        /// </summary>
        DashTwice,
        Test1,
        Test2,
    }
    /// <summary>
    /// ����Trigger����������ö�٣���Ӧ�����������ϡ�Trigger������ö��ֵΪPressBtn����������ΪPreesBtnTrigger���������ü��أ�
    /// </summary>
    public enum FSM_TriggerID
    {
        NullTriggerTransition = 0,
        /// <summary>
        /// ���������ƶ�����
        /// </summary>
        PressHorizontalBtn,
        /// <summary>
        /// ���������ƶ�����
        /// </summary>
        PressVerticalBtn,
        /// <summary>
        /// ������Ծ����
        /// </summary>
        PressJumpBtn,
        /// <summary>
        /// ���¶���������
        /// </summary>
        PressJumpTwiceBtn,
        /// <summary>
        /// ���³�̰���
        /// </summary>
        PressDashBtn,
        /// <summary>
        /// ���¶��γ�̰���
        /// </summary>
        PressDsahTwiceBtn,
        Test,
        Test2,
    }
}

