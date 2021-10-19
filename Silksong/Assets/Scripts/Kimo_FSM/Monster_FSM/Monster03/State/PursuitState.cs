using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitState : EnemyFSMBaseState
{
    public Transform target;
    public float avoidForce = 2;//�����Ĵ�С
    public float MAX_SEE_AHEAD = 2.0f;//��Ұ��Χ
    public LayerMask layer;
    public float maxSpeed = 2;
    public float maxForce = 2;

    public override void InitState(FSMManager<EnemyStates, EnemyTriggers> fSMManager)
    {
        base.InitState(fSMManager);
        fsmManager = fSMManager;
        stateID = EnemyStates.PursuitState;
    }
    public override void Act_State(FSMManager<EnemyStates, EnemyTriggers> fSM_Manager)
    {
        base.Act_State(fSM_Manager);
        fsmManager.rigidbody2d.AddForce(Project4(fsmManager));
        Force(fsmManager);
    }
    public void Force(FSMManager<EnemyStates, EnemyTriggers> fsmManager)
    {
        if (!target)
        {
            GameObject[] a = GameObject.FindGameObjectsWithTag("Player");
            target = a[0].transform;
        }
        Vector2 desiredVelocity = (target.position - fsmManager.transform.position).normalized * maxSpeed;
        Vector2 steeringForce = (desiredVelocity - fsmManager.rigidbody2d.velocity);
        if (steeringForce.magnitude > maxForce) steeringForce = steeringForce.normalized * maxForce;
        Debug.DrawLine(fsmManager.transform.position, (Vector2)fsmManager.transform.position + steeringForce, Color.green);
        fsmManager.rigidbody2d.AddForce(steeringForce);
    }
    public Vector2 Project4(FSMManager<EnemyStates, EnemyTriggers> fsmManager)
    {
        Vector2 steeringForce = Vector2.zero;
        Vector2 toward = fsmManager.rigidbody2d.velocity.normalized;
        Vector2 vetical = new Vector2(-toward.y, toward.x);
        Vector2 pos = fsmManager.transform.position;
        Vector2 pointA = pos - (toward + vetical) * fsmManager.GetComponent<Collider2D>().bounds.extents.magnitude;
        Vector2 pointB = pos + toward * MAX_SEE_AHEAD + vetical * fsmManager.GetComponent<Collider2D>().bounds.extents.magnitude;
        Collider2D wall = Physics2D.OverlapArea(pointA, pointB, layer);
        Debug.DrawLine(pointA, pointB, Color.red);
        if (wall)
        {
            Vector2 ahead = pos + fsmManager.rigidbody2d.velocity.normalized * MAX_SEE_AHEAD;
            steeringForce = (ahead - (Vector2)wall.transform.position).normalized;
            steeringForce *= avoidForce;
        }
        Debug.DrawLine(pos, pos + steeringForce);
        return steeringForce;
    }
}
