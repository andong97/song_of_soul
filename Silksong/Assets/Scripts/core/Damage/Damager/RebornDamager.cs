using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ߣ����
/// /���������˺��� ����������������damager ����ˮ  ������δʵ�� ������kit����
/// </summary>
public class RebornDamager:TwoTargetDamager
{
    //private SceneManger sceneManger;
    public LayerMask rebornLayer;
    void Start()
    {
        //sceneManger = GameObject.Find("Enviroment").GetComponent<SceneManger>();
    }

    protected  override void makeDamage(DamageableBase damageable)//
    {
        // base.makeDamge(Damageable);
      /*  if(rebornLayer.Contains(damageable.gameObject))
        {
            sceneManger.rebornPlayer();
        }*/

    }
}
