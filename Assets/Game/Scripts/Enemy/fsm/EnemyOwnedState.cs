using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Noide
namespace NoidOwnedState
{
    public class Idle : State<Noide>
    {   
        static float s_curdelay;
        public override void Enter(Noide enemy)
        {
            s_curdelay = 0;
        }
        public override void Excute(Noide enemy)
        {
            if (enemy.IsDead()) enemy.ChangeState(enemyState.dead); 

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
        public override void Exit(Noide enemy)
        {
            enemy.moveSpeed = enemy.saveSpeed;
        }
    }
    public class Move : State<Noide>
    {
        static float s_curdelay;
        public override void Enter(Noide enemy)
        {
            s_curdelay = 0;
            if (enemy.wallCheck)
                enemy.wallCheck = false;
            
        }
        public override void Excute(Noide enemy)
        {

            if (enemy.IsDead()) enemy.ChangeState(enemyState.dead);

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
        public override void Exit(Noide enemy)
        {
        }
    }
    public class Die : State<Noide> //죽었을때
    {

        public override void Enter(Noide enemy)
        {
            GameObject.Destroy(enemy.gameObject);
        }
        public override void Excute(Noide enemy)
        {
            throw new System.NotImplementedException();
        }
        public override void Exit(Noide enemy)
        {
            throw new System.NotImplementedException();
        }
    }

}
#endregion

namespace WepSpiderOwnedState
{
    public class Idle : State<WebSpider>
    {
        public override void Enter(WebSpider enemy)
        {

            Debug.Log("IdleState");
        }
        public override void Excute(WebSpider enemy)
        {
            if (enemy.serch.isserch) 
                enemy.ChangeState(WebSpiderState.Attack);
        }
        public override void Exit(WebSpider enemy)
        {
          
        }
    }
    public class Attack : State<WebSpider>
    {
        public override void Enter(WebSpider enemy)
        {
            Debug.Log("attackState");
        }
        public override void Excute(WebSpider enemy)
        {
            
            if (!enemy.serch.isserch)
                enemy.ChangeState(WebSpiderState.Idle);
        }
        public override void Exit(WebSpider enemy)
        {
           
        }

    }
}
