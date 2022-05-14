using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoidOwnedState
{
    public class Idle : State<Enemy>
    {
        static float s_curdelay;

        public override void Enter(Enemy enemy)
        {
            s_curdelay = 0;
            Debug.Log("idle Enter");
        }
        public override void Excute(Enemy enemy)
        {
            s_curdelay += Time.deltaTime;

            if (s_curdelay >= 3.0f)
            enemy.ChangeState(enemyState.Move);
        }
        public override void Exit(Enemy enemy)
        {
           // Debug.Log("idle Exit");
        }
    }
    public class Move : State<Enemy>
    {
        static float s_curdelay;
        public override void Enter(Enemy enemy)
        {
            s_curdelay = 0;
            Debug.Log("Move Enter");
        }
        public override void Excute(Enemy enemy)
        {
            s_curdelay += Time.deltaTime;

            if (s_curdelay >= 2.0f)
             enemy.ChangeState(enemyState.Idle);
        }
        public override void Exit(Enemy enemy)
        {
           // Debug.Log("Move Exit");
        }
    }
}