using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 作者：青瓜
/// damager的抽象基类 
/// damager和其trigger应作为独立的物体 有自己的layer 不再有子物体
/// </summary>
public static class LayerMaskExtensions//layerMask增加contains方法 判断gameObject是否在layerMask中
{
    public static bool Contains(this LayerMask layers, GameObject gameObject)
    {
        return 0 != (layers.value & 1 << gameObject.layer);
    }
}


public abstract class DamagerBase : MonoBehaviour
{
    public bool ignoreInvincibility = false;//无视无敌
    public bool canDamage = true;
    public int damage;//伤害数值


    public virtual int getDamage(DamageableBase target)//获得造成的具体伤害数值
    {
        return damage;
    }
 
    protected abstract void makeDamage(DamageableBase target);//造成伤害后的效果

    protected virtual void OnTriggerEnter2D(Collider2D collision)//根据gameobjrect的layer和project setting 确定哪些layer参与碰撞
    {
        if(!canDamage)
        {
            return;
        }
        DamageableBase damageable = collision.GetComponent<DamageableBase>();//只有拥有Damageable组件的collider受攻击
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
