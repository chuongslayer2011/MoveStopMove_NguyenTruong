using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static Player;

public class Character : GameUnit
{
    public class CharacterAttribute
    {
        public float movingSpeed;
        
    }
    [SerializeField] public Animator animator;
    [SerializeField] protected Weapon weaponGunner;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected WeaponOnHand weaponOnHand;
    [SerializeField] protected HairOnHead hairOnHead;
    [SerializeField] private GameObject attackRange;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] protected Renderer Pant;
    [SerializeField] protected Renderer bodySkin;
    [SerializeField] protected PantData pantData;
    [SerializeField] protected float movingSpeed;
    [SerializeField] protected Collider _collider;
    [SerializeField] protected TargetIndicator targetIndicator;
    [SerializeField] Transform pos;
    private int score;
    public Collider CLD
    {
        get
        {
            //tf = tf ?? gameObject.transform;
            
            return _collider;
        }
    }
    private string currentAnimName;
    public Character targetToAttack; 
    public List<Character> targetList = new List<Character>();
    private IState<Character> currentState;
    private bool canAttack = true;
    protected bool targetable = true;
    public bool isPlay;
    private CharacterAttribute characterAttribute = new CharacterAttribute();
    private void Start()
    {
        ChangeState(new IdleState());
        characterAttribute.movingSpeed = this.movingSpeed;

    }
    void Update()
    {   
        if (!isPlay)
        {
            return;
        }
        
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    } 
    public virtual void OnInit()
    {
        ChangeState(new IdleState());
        targetable = true;
        TF.localScale = Vector3.one;
        characterAttribute.movingSpeed = 5;
        targetList.Clear();
        targetToAttack = null;
        this.TF.rotation = Quaternion.Euler(0, 0, 0);
        InstantiateTargetIndicator();
        ResetBuff();
        score = 0;
        canAttack = true;
    }
    protected virtual void OnTriggerEnter(Collider other)
    {

        if ((other.CompareTag(Const.PLAYER_TAGNAME) || other.CompareTag(Const.ENEMY_TAGNAME)))
        {
            Character target = MapController.instance.GetCharacterByCollider(other);
            if (target != null)
            {   
                if(target.targetable)
                {
                    targetList.Add(target);
                    ResetTargetCharacter();
                }
                
            }
            
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {

        if (other.CompareTag(Const.PLAYER_TAGNAME) || other.CompareTag(Const.ENEMY_TAGNAME))
        {
            Character target = MapController.instance.GetCharacterByCollider(other);
            RemoveCharacterTarget(target);
        }
    }

    public void RemoveCharacterTarget(Character character)
    {
        targetList.Remove(character);
        ResetTargetCharacter();
    }
    
    public void ResetTargetCharacter()
    {
        if (targetList.Count == 0)
        {
            targetToAttack = null;
        }
        else
        {
            targetToAttack = null;

            if (targetList.Count > 0)
            {   
                targetList.RemoveAll(target => (target.gameObject.activeSelf == false || target.targetable == false));
                targetList.Sort((obj1, obj2) =>
                {
                    float distance1 = Vector3.Distance(obj1.transform.position, transform.position);
                    float distance2 = Vector3.Distance(obj2.transform.position, transform.position);
                    return distance1.CompareTo(distance2);
                });
                if (targetList.Count > 0)
                    targetToAttack = targetList[0];
            }
            
            
        }
        
        
    }
    public virtual IEnumerator Attack()
    {
        
        if (targetToAttack != null)
        {   
            Vector3 attackDirection = targetToAttack.transform.position - this.transform.position;
            attackDirection.Normalize();
            this.transform.forward = attackDirection;
            transform.LookAt(targetToAttack.transform.position);
            ChangeAnim(Const.ATTACK_ANIM);
            ResetBuff();
            yield return new WaitForSeconds(0.45f);
            if (this.rb.velocity.magnitude < 0.1f && targetable)
            this.weaponGunner.Attack(attackDirection);
            yield return new WaitForSeconds(0.15f);
            this.weaponOnHand.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            this.weaponOnHand.gameObject.SetActive(true);
            
        }
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            animator.ResetTrigger(animName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }
    

    public void ChangeState(IState<Character> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public void SetCanAttack(bool canAttack)
    {
        
        this.canAttack = canAttack;
        
    }
    public void SetTargetable(bool targetable)
    {
        this.targetable = targetable;
    }
    public bool CheckAlive()
    {
        return this.targetable;
    }
    public virtual void OnDespawn()
    {   
        this.targetList.Clear();
        this.targetToAttack = null;
        weaponOnHand.DestroyCurrentWeapon();
        SimplePool.Despawn(targetIndicator);
    }
    public virtual void Moving()
    {
        this.agent.enabled = true;
        Vector2 randomPosition = Random.insideUnitCircle * 50;
        Vector3 targetPoint = new Vector3(randomPosition.x, 1.5f, randomPosition.y);
        this.transform.LookAt(targetPoint);
        agent.SetDestination(targetPoint);
    }
    public bool CheckTargetInRange()
    {   
        if (targetList.Count > 0) {
            bool checkNullTarget = false;
            for (int i = 0; i < this.targetList.Count; i++)
            {
                if (this.targetList[i].gameObject.activeSelf == true)
                {
                    checkNullTarget = true;
                    break;
                }
            }
            return checkNullTarget;
        }
        return targetToAttack != null && targetList.Count > 0;
    }
    public void ChangeScale()
    {   
        if (this.transform.localScale.x < 1.5f)
        {
            Vector3 scaleChange = new Vector3(0.1f, 0.1f, 0.1f);
            this.transform.localScale += scaleChange;
        }
        

    }
    public float GetSize()
    {
        return this.transform.localScale.x;
    }
    public bool CheckAttack()
    {
        return this.rb.velocity.magnitude < 0.1f && targetToAttack != null && canAttack && targetable && targetToAttack.targetable;
    }
    public void StoppMoving()
    {   
        if(this.CompareTag(Const.ENEMY_TAGNAME))
        agent.enabled = false;
        this.rb.velocity = Vector3.zero;
    }
    public void SetIsPlay(bool isPlay)
    {
        this.isPlay = isPlay;
        targetIndicator.SetAlpha(isPlay == true? 1 : 0);
    }
    public void ObtainBuff()
    {
        
        movingSpeed *= 1.5f;
        if (this.CompareTag(Const.ENEMY_TAGNAME))
        {
            this.agent.speed = this.movingSpeed;
        }
    }
    public void ResetBuff()
    {
        movingSpeed = characterAttribute.movingSpeed;
        if (this.CompareTag(Const.ENEMY_TAGNAME))
        {
            this.agent.speed = this.movingSpeed;
        }
    }
    public Color GetCharacterColor()
    {
        return bodySkin.materials[0].color;
    }
    public Collider GetCollider()
    {
        return this._collider;
    }
    public bool CheckMoving()
    {
        return this.rb.velocity.magnitude < 0.1f;
    }
    public void InstantiateTargetIndicator()
    {
        targetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.targetIndicator);
        targetIndicator.SetTarget(pos);
    }
   
    public void SetScore()
    {
        score++;
        targetIndicator.SetScore(score);
    }
}
