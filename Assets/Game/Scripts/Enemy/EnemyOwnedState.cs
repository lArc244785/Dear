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

            if (s_curdelay >= enemy.patrolTime)
            {
                switch (enemy.enemyMoveDirection)
                {
                    case movedirection.Left:
                        enemy.enemyMoveDirection = movedirection.Right;
                        break;
                    case movedirection.Right:
                        enemy.enemyMoveDirection = movedirection.Left;
                        break;
                }

                enemy.ChangeState(enemyState.Move);
            }
        }
        public override void Exit(Enemy enemy)
        {
            enemy.moveSpeed = enemy.saveSpeed;
        }
    }
    public class Move : State<Enemy>
    {
        static float s_curdelay;
        public override void Enter(Enemy enemy)
        {
            s_curdelay = 0;
            if (enemy.wallCheck)
                enemy.wallCheck = false;

            Debug.Log("Move Enter");
        }
        public override void Excute(Enemy enemy)
        {

            switch (enemy.enemyMoveDirection) {
                case movedirection.Left:
                    enemy.rig2D.velocity = new Vector2(enemy.moveSpeed*-1f, enemy.rig2D.velocity.y);
                    break;
                case movedirection.Right:
                    enemy.rig2D.velocity = new Vector2(enemy.moveSpeed, enemy.rig2D.velocity.y);
                    break;
            }
            
            if(enemy.wallCheck)
            enemy.ChangeState(enemyState.Idle);
        }
        public override void Exit(Enemy enemy)
        {
        }



    }
}