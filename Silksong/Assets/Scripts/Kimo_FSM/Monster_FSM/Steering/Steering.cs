using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CreateAssetMenu(fileName ="New Steering",menuName ="Steering")]
public class Steering : ScriptableObject
{
    //steering��ֻ���湲����Ϣ������˵����ֵ���͵����ݣ���������ϢӦ����fsmmanage�ṩ
    public float weight = 1;
    public virtual void Force(FSMManager<EnemyStates, EnemyTriggers> fsmManager)
    {
    }
}
