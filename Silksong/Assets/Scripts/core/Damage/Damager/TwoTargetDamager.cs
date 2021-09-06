using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ߣ����
/// ������Ŀ�겻ͬ�˺���damager�Ļ���   ������ˮ,��ҵĹ���
/// </summary>
public class TwoTargetDamager : Damager
{
    public LayerMask hittableLayers2;//��һĿ��
    public int damage2;//����һĿ����˺�


    public override int getDamage(DamageableBase target)//����Ŀ�귵���˺�
    {
        if (hittableLayers2.Contains(target.gameObject))
        {
            return damage2;
        }
        else
        {
            return damage;
        }
    }

}
