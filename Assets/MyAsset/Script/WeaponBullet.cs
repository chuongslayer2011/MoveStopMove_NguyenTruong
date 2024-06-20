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

        if ((other.CompareTag(Const.PLAYER_TAGNAME) || other.CompareTag(Const.ENEMY_TAGNAME)) && other.gameObject != owner.gameObject)
        {   
            Character _character = MapController.instance.GetCharacterByCollider(other);
            if (_character == null)
            {
                return;
            }
            ParticlePool.Play(ParticleType.Hit, _character.TF.position , Quaternion.Euler(0,0,0),_character.TF);
            
            owner.ChangeScale();
            if (owner.CompareTag(Const.PLAYER_TAGNAME))
            {
                CameraFollow.instance.ZoomOut();
                DataManager.ins.ObtainCoin();
                LevelManager.Ins.ObtainCoinCurrentLevel();
            }
            
            owner.RemoveCharacterTarget(_character);
            owner.ResetTargetCharacter();
            owner.SetScore();
            OnDespawn();
            _character.ChangeState(new DespawnState());
            if (other.CompareTag(Const.PLAYER_TAGNAME))
            {
                UIManager.Ins.CloseAll();
                UIManager.Ins.OpenUI<Lose>();
                Lose.Instance.SetRankingText();
            }
            if (other.CompareTag(Const.ENEMY_TAGNAME))
            {
               MapController.instance.RemoveEnemyOnMap(_character);
               if(MapController.instance.EnemyAlive() == 0)
                {
                    UIManager.Ins.CloseAll();
                    UIManager.Ins.OpenUI<Win>();
                }
            }
            if (owner.CheckAlive())
            ParticlePool.Play(ParticleType.Charge, owner.TF.position, Quaternion.Euler(0, 0, 0), owner.TF);
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
 }
