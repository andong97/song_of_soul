using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ߣ����
/// damager�ĳ������ 
/// </summary>
public static class LayerMaskExtensions//layerMask����contains���� �ж�gameObject�Ƿ���layerMask��
{
    public static bool Contains(this LayerMask layers, GameObject gameObject)
    {
        return 0 != (layers.value & 1 << gameObject.layer);
    }
}


public abstract class DamagerBase : MonoBehaviour
{
    public bool ignoreInvincibility = false;//�����޵�
    public bool canDamage = true;
    public int damage;//�˺���ֵ


    public virtual int getDamage(DamageableBase target)//�����ɵľ����˺���ֵ
    {
        return damage;
    }
 
    protected abstract void makeDamage(DamageableBase target);//����˺����Ч��

    protected virtual void OnTriggerEnter2D(Collider2D collision)//����gameobjrect��layer��project setting ȷ����Щlayer������ײ
    {
        if(!canDamage)
        {
            return;
        }
        DamageableBase damageable = collision.GetComponent<DamageableBase>();//ֻ��ӵ��Damageable�����collider�ܹ���
        if (damageable )
        {
            //Debug.Log(damageable.gameObject.name + " ontrigger");
            if (!ignoreInvincibility && damageable.invulnerable)
            {
                return;
            }
            damageable.takeDamage(this);
            makeDamage(damageable);

        }
    }
}
