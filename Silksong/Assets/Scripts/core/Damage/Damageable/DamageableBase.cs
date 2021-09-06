using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ߣ����
/// damageable�ĳ������ 
/// </summary>
public abstract class DamageableBase : MonoBehaviour
{
    public bool invulnerable;//�Ƿ��޵�
    public abstract void takeDamage(DamagerBase damager);//�ܵ��˺�ʱ������Ч��

    protected virtual void Awake()//��awakeʱ������ ����趨
    {
        setRigidbody2D();
    }

    protected void setRigidbody2D()//ontriggerEnter�Ĵ�����Ҫ����һ���и���  ���ﱣ֤damable�и���
    {
        if(GetComponent<Rigidbody2D>()==null)//���û���ֶ��趨�ĸ��� �����һ��static����
        {
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;//Ĭ�����static���� ���ڻ��ء�ǽ��  Dynamic������Ҫ�ֶ����
        }
        gameObject.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.NeverSleep;//����������˯��״̬���޷���Ӧ��ײ
        
    }

}
