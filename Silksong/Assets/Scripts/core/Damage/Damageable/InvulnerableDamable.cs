using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ߣ����
/// ��hpdamable�Ļ����� �����ܻ����޵л���
/// </summary>
public class InvulnerableDamable : HpDamable
{
    public bool invulnerableAfterDamage = true;//���˺��޵�
    public float invulnerabilityDuration = 3f;//�޵�ʱ��
    protected float inulnerabilityTimer;
    public override void takeDamage(DamagerBase damager)
    {
        base.takeDamage(damager);
        if(invulnerableAfterDamage)
        {
            enableInvulnerability();
        }
    }



    public void enableInvulnerability(bool ignoreTimer = false)//�����޵�
    {
        invulnerable = true;
        //technically don't ignore timer, just set it to an insanly big number. Allow to avoid to add more test & special case.
        inulnerabilityTimer = ignoreTimer ? float.MaxValue : invulnerabilityDuration;
    }

    void Update()
    {
        if (invulnerable)
        {
            inulnerabilityTimer -= Time.deltaTime;

            if (inulnerabilityTimer <= 0f)
            {
                invulnerable = false;
                GetComponent<BoxCollider2D>().enabled = false;//���¼���һ��collider �������޵�ʱ����trigger�� �����޵й���ontriggerEnter������
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }


}
