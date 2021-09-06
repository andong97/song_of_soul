using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ߣ����
/// ӵ������ֵ����ط�����damable 
/// </summary>
public class HpDamable :Damable
{
    public int maxHp = 5;//�������ֵ

    public int currentHp;//��ǰhp

    public bool resetHealthOnSceneReload;

    protected Vector2 damageDirection;//�˺���Դ�ķ���

    public bool canHitBack;//�ܷ񱻻��� Ŀǰ���˻�����ʱ������ 
    public float hitBackDistance;


    public override void takeDamage(DamagerBase damager)
    {
        if ( currentHp <= 0)
        {
            return;
        }

        base.takeDamage(damager);

        addHp(-damager.getDamage(this));
        damageDirection = damager.transform.position - transform.position;

        if(canHitBack)
        hitBack();
    }

    protected void hitBack()
    {

        if(damageDirection.x>0)
        {
            transform.Translate(Vector2.left *hitBackDistance,Space.World);
        }
        else
        {
            transform.Translate(Vector2.right * hitBackDistance, Space.World);
        }
    }
    public void setHp(int hp)
    {
        currentHp = hp;
        checkHp();
    }

    protected virtual void checkHp()
    {
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        if (currentHp <= 0)
        {
            die();
        }
    }
    protected void addHp(int number)//���ܵ��˺� number<0
    {
        currentHp += number;
        checkHp();

    }

    protected virtual void die()
    {
        Destroy(gameObject);//δ����
        Debug.Log(gameObject.name+" die");
    }



}
