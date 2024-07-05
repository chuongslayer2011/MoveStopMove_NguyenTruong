using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnState : IState<Character>
{
    float timer;
    public void OnEnter(Character t)
    {
        t.ChangeAnim(Const.DIE_ANIM);
        t.OnDespawn();
        t.SetTargetable(false);
        if (t.CompareTag(Const.ENEMY_TAGNAME))
            t.agent.enabled = false;
        
    }

    public void OnExecute(Character t)
    {
        timer += Time.deltaTime;
        if (timer > 1.5)
        {
            if (t.CompareTag(Const.ENEMY_TAGNAME))
                SimplePool.Despawn(t);
        }
    }

    public void OnExit(Character t)
    {

    }

}

