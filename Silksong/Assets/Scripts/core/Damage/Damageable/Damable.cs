using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ߣ����
/// ��򵥵�damable
/// </summary>
/// 
public class Damable : DamageableBase
{
    public override void takeDamage(DamagerBase damager)
    {
        hittedEffect();//������event.invoke()
    }

    protected virtual void hittedEffect()//�ܻ�Ч�� ���б�Ҫ���¼���ʽ����
    {
        Debug.Log(gameObject.name + " is hitted");
    }

}
