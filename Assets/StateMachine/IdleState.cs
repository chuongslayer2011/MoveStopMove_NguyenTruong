using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Character>
{
    float timer;
    float randomtime;
    public void OnEnter(Character t)
    {   
        if(t.CompareTag(Const.ENEMY_TAGNAME))
        t.ChangeAnim(Const.ILDE_ANIM);
        t.StoppMoving();
        t.SetCanAttack(true);
        t.ResetTargetCharacter();
        randomtime = 1f;
    }

    public void OnExecute(Character t)
    {
        if (t.CheckAttack())
        {
            t.ChangeState(new AttackState());
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > randomtime && t.CompareTag(Const.ENEMY_TAGNAME))
            {
                t.ChangeState(new PatrolState());
            }
        }     
    }

    public void OnExit(Character t)
    {
        t.ResetTargetCharacter();
    }

}
