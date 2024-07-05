using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform weaponHitRange;
    [SerializeField] private Transform tf;
    [SerializeField] private WeaponOnHand OnHand;
    [SerializeField] private Character owner;
    public void Attack(Vector3 attackDirection)
    {

       WeaponBullet bullet = SimplePool.Spawn<WeaponBullet>(OnHand.GetPoolType(), this.transform.position, tf.rotation);
       bullet.SetOwner(owner);
       bullet.OnInit();      
       bullet.transform.localScale *= owner.GetSize();
       bullet.transform.forward = attackDirection;
       bullet.SetVelocity(attackDirection);
       bullet.transform.Rotate(-90, tf.rotation.y ,tf.rotation.z);
    }
}
