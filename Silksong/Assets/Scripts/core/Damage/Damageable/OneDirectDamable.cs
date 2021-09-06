using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ߣ����
///ֻ������һ��������˺� �������� �����������
/// </summary>
public class OneDirectDamable : HpDamable
{
    public bool leftInvulnerable;//��ߵ��˺���Ч ��Ϊfalse���ұߵ��˺���Ч
    public override void takeDamage(DamagerBase damager)
    {
        if (currentHp <= 0)
        {
            return;
        }

        hittedEffect();
        damageDirection = damager.transform.position - transform.position;
        if ((leftInvulnerable && damageDirection.x < 0) || (!leftInvulnerable && damageDirection.x>0))
        {
            return;
        }
        
        addHp(-damager.getDamage(this));
        if (canHitBack)
            hitBack();
    }
}
