using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Character>
{
    float timer;
    public void OnEnter(Character t)
    {   t.StartCoroutine(t.Attack());
        t.SetCanAttack(false);
    }

    public void OnExecute(Character t)
    {
        timer += Time.deltaTime;
        if (timer > 1.5)
        {
           
            t.ChangeState(new IdleState());
        }
    }

    public void OnExit(Character t)
    {

    }

}
