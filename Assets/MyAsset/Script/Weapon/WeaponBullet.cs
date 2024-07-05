using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class WeaponBullet : GameUnit
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int movingSpeed;
    [SerializeField] private int index;
    [SerializeField] private float range;
    private Character owner;

    public void OnInit()
    {
        this.transform.localScale = Vector3.one * 40;
        Invoke(nameof(OnDespawn), range * owner.GetSize());
    }
     
    public void OnDespawn()
    {

        SimplePool.Despawn(this);
    }
    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity * movingSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {   

        if ((other.CompareTag(Const.PLAYER_TAGNAME) || other.CompareTag(Const.ENEMY_TAGNAME)))
        {   
            Character characterGetHit = MapController.instance.GetCharacterByCollider(other);
            if (characterGetHit == null) return;
            if (characterGetHit == owner) return;
            ParticlePool.Play(ParticleType.Hit, characterGetHit.TF.position, Quaternion.Euler(0, 0, 0), characterGetHit.TF);
            OwnerHandle(owner,characterGetHit);                        
            if (owner.CompareTag(Const.PLAYER_TAGNAME)) OwnerPlayerHandle();
            OnDespawn();
            characterGetHit.ChangeState(new DespawnState());
            if (other.CompareTag(Const.PLAYER_TAGNAME))
            {
                HitPlayerHandle();
            }
            if (other.CompareTag(Const.ENEMY_TAGNAME))
            {
                HitEnemyHandle(characterGetHit);
            }
            
        }
        else if (other.CompareTag(Const.SHIELD_TAGNAME))
        {
            ProtectShield s = other.GetComponent<ProtectShield>();            
            if (owner != s.GetOwner())
            {
                OnDespawn();
                s.OnDespawn();
                ParticlePool.Play(ParticleType.Charge, owner.TF.position, Quaternion.Euler(0, 0, 0), owner.TF);
            }
        }

        
    }
    public void SetOwner(Character owner)
    {
        this.owner = owner;
    }
    public GameObject GetOwner()
    {
        return this.owner.gameObject;
    }
    public void HitPlayerHandle()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<Lose>();
        Lose.Instance.SetRankingText();
    }
    public void HitEnemyHandle(Character enemy)
    {
        MapController.instance.RemoveEnemyOnMap(enemy);
        if (MapController.instance.EnemyAlive() == 0)
        {
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<Win>();
        }
    }
    public void OwnerPlayerHandle()
    {
        CameraFollow.instance.ZoomOut();
        DataManager.ins.ObtainCoin();
        LevelManager.Ins.ObtainCoinCurrentLevel();
    }
    public void OwnerHandle(Character owner,Character characterGetHit)
    {
        owner.ChangeScale();
        owner.RemoveCharacterTarget(characterGetHit);
        owner.ResetTargetCharacter();
        owner.SetScore();
        if (owner.CheckAlive())
            ParticlePool.Play(ParticleType.Charge, owner.TF.position, Quaternion.Euler(0, 0, 0), owner.TF);
    }
 }
