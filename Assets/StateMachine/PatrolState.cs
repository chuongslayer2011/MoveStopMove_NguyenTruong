using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Character>
{
    float timer;
    float randomtime;
    public void OnEnter(Character t)
    {
        t.ChangeAnim(Const.RUN_ANIM);
        if (t.CompareTag(Const.ENEMY_TAGNAME))
        t.Moving();
        randomtime = Random.Range(2.5f, 4f);
    }

    public void OnExecute(Character t)
    {
        timer += Time.deltaTime;
        if ((timer > randomtime || t.CheckTargetInRange()) && t.CompareTag(Const.ENEMY_TAGNAME))
        {
            t.ChangeState(new IdleState());
        }
    }

    public void OnExit(Character t)
    {

    }

}
