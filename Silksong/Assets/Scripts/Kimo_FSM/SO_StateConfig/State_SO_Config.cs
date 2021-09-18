using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.Reflection;


public class State_SO_Config<T1,T2,T3> : ScriptableObject
{
    [HideInInspector]
    [SerializeReference]
    public T1 lastStateID;
    [HideInInspector]
    [SerializeReference]
    public T2 lastTriggerID;
    [Header("----------------------State Config Area------------------------")]
    public T1 stateID;
    [SerializeReference]
    public T3 stateConfig;
    [Header("----------------------Trigger Config Area----------------------")]
    [Space(20)]
    public T2 triggerID;
    [SerializeReference]
    public System.Object triggerConfig;
    [Header("-----------------------Triggers List Area----------------------")]
    [Space(20)]
    [SerializeReference]
    public List<System.Object> triggerList;
    [NonSerialized]
    public Type triggerType;
    [NonSerialized]
    public Type stateType;

    private void Awake()
    {
        lastStateID = stateID;
        stateType = Type.GetType(stateID.ToString());
        if (stateType != null)
            stateConfig = (T3)Activator.CreateInstance(stateType) ;
        else
            Debug.LogError("�Ҳ�������Ӧ��State������ö�������Ƿ�������һ�¡�");


        lastTriggerID = triggerID;
        triggerType = Type.GetType(triggerID.ToString());
        if (triggerType != null)
            triggerConfig = Activator.CreateInstance(triggerType);
        else
            Debug.LogError("�Ҳ�������Ӧ��Trigger������ö�������Ƿ�������һ�¡�");
    }

    
}


[CreateAssetMenu(fileName = "Enemy_State_SO_Config", menuName = "SO_Enemy����", order = 2)]
public class Enemy_State_SO_Config : State_SO_Config<EnemyStates, EnemyTriggers,EnemyFSMBaseState> { }


[CreateAssetMenu(fileName = "NPC_State_SO_Config", menuName = "SO_NPC����", order = 2)]
public class NPC_State_SO_Config : State_SO_Config<NPCStates, NPCTriggers, NPCFSMBaseState> { }

//[CreateAssetMenu(fileName = "Player_State_SO_Config", menuName = "SO_Player����", order = 2)]
//public class Player_State_SO_Config : State_SO_Config<PlayerStates, PlayerTriggers, NPCFSMBaseState> { }





/// <summary>
/// �������ߣ�����Inspirte����ˢ��
/// </summary>
[CustomEditor(typeof(Enemy_State_SO_Config))]
public class State_SO_Config_Editor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Enemy_State_SO_Config config = target as Enemy_State_SO_Config;
        if (config.lastStateID != config.stateID)
        {
            config.lastStateID = config.stateID;
            config.stateType = Type.GetType(config.stateID.ToString());
            if (config.stateType != null)
                config.stateConfig = Activator.CreateInstance(config.stateType) as EnemyFSMBaseState;
            else
                Debug.LogError("�Ҳ�������Ӧ��State������ö�������Ƿ�������һ�¡�");

        }
        if (config.lastTriggerID != config.triggerID)
        {
            config.lastTriggerID = config.triggerID;
            config.triggerType = Type.GetType(config.triggerID.ToString());
            if (config.triggerType != null)
                config.triggerConfig = Activator.CreateInstance(config.triggerType);
            else
                Debug.LogError("�Ҳ�������Ӧ��Trigger������ö�������Ƿ�������һ�¡�");

        }
        if (GUILayout.Button("Add to List"))
        {
            config.triggerList.Add(ObjectClone.CloneObject(config.triggerConfig));
        }
    }



}



/// <summary>
/// ������Inspirteֻ����ʾ�������޸ĵ�����
/// </summary>
public class DisplayOnly:PropertyAttribute{ }
[CustomPropertyDrawer(typeof(DisplayOnly))]
public class DisplayOnlyDraw:PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, true);
        GUI.enabled = true;
    }
}

