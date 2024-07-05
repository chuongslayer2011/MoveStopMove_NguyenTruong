using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BoosterType{
    SpeedBoost = 0,
    ProtectShield =1
}
public class Booster : GameUnit
{   
    private BoosterType type;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Const.PLAYER_TAGNAME) || other.CompareTag(Const.ENEMY_TAGNAME))
        {
            Character character = MapController.instance.GetCharacterByCollider(other);
            BoosterTriggerHandle(character);
            OnDespawn();          
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
    private void BoosterTriggerHandle(Character character)
    {
        ParticlePool.Play(ParticleType.Buff, character.TF.position, Quaternion.Euler(0, 0, 0), character.TF);
        switch (this.type)
        {
            case BoosterType.SpeedBoost:
                {
                    character.ObtainBuff();
                    break;
                }
            case BoosterType.ProtectShield:
                {
                    ProtectShield _shield = SimplePool.Spawn<ProtectShield>(PoolType.Shield, character.TF);
                    _shield.OnInit(character);
                    break;
                }
        }
    }
}
