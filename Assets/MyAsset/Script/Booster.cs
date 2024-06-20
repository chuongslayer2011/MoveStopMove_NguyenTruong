using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BoosterType{
    speedBoost = 0,
    protectShield =1
}
public class Booster : GameUnit
{   
    private BoosterType type;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Const.PLAYER_TAGNAME) || other.CompareTag(Const.ENEMY_TAGNAME))
        {
            Character character = MapController.instance.GetCharacterByCollider(other);
            if (type == BoosterType.speedBoost)
            {
                
                character.ObtainBuff();
                
               
            }
            else if (type == BoosterType.protectShield)
            {
                ProtectShield _shield = SimplePool.Spawn<ProtectShield>(PoolType.shield, character.transform);
                _shield.transform.localScale = Vector3.one * 4;
                _shield.SetOwner(character);
            }
            OnDespawn();
            ParticlePool.Play(ParticleType.Buff, other.gameObject.transform.position, Quaternion.Euler(0, 0, 0), character.transform);
        }
    }
    public void OnInit()
    {
        int random = Random.Range(0, 2);
        this.type = (BoosterType)random;
    }
    public void OnDespawn()
    {
        SimplePool.Despawn(this);
        MapController.instance.SpawnBooster();
    }
}
