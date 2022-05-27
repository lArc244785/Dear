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
            if (!enemy.model.GetComponent<SpriteRenderer>().flipX)
                enemy.model.GetComponent<SpriteRenderer>().flipX = true;
            else
                enemy.model.GetComponent<SpriteRenderer>().flipX = false;

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
            if (enemy.health.hp <= 0)
                enemy.ChangeState(WebSpiderState.Die);

            if (enemy.serch.isserch) 
                enemy.ChangeState(WebSpiderState.Attack);
        }
        public override void Exit(WebSpider enemy)
        {
          
        }

       

    }
    public class Attack : State<WebSpider>
    {
        private float nextTime = 0.0f;


        public override void Enter(WebSpider enemy)
        {
            Debug.Log("attackState");
            
        }
        public override void Excute(WebSpider enemy)
        {

            if (enemy.health.hp <= 0)
                enemy.ChangeState(WebSpiderState.Die);

            if (Time.time > nextTime)
            {
                nextTime = Time.time + enemy.fireCycle;
                enemy.Attack();
            }
            if (!enemy.serch.isserch)
            {
                enemy.ChangeState(WebSpiderState.Idle);
            }
        }
        public override void Exit(WebSpider enemy)
        {
           
        }
    }
    public class Die : State<WebSpider>
    {
        private float nextTime = 0.0f;


        public override void Enter(WebSpider enemy)
        {
            GameObject.Destroy(enemy.gameObject);
        }
        public override void Excute(WebSpider enemy)
        {
           
        }
        public override void Exit(WebSpider enemy)
        {

        }
    }

}


namespace JumpSpiderOwnedState
{
    public class Idle : State<JumpSpider>
    {
        public override void Enter(JumpSpider enemy)
        {
            Debug.Log("IdleState");
        }
        public override void Excute(JumpSpider enemy)
        {

            if (enemy.health.hp <= 0)
                enemy.ChangeState(jumpSpiderState.Dead);

            if (enemy.areaCollider.isserch)
                enemy.ChangeState(jumpSpiderState.Atack);

        }
        public override void Exit(JumpSpider enemy)
        {

        }



    }
    public class Attack : State<JumpSpider>
    {
        private float nextTime = 0.0f;

        public override void Enter(JumpSpider enemy)
        {
            Debug.Log("attackState");
        }
        public override void Excute(JumpSpider enemy)
        {
            if (Time.time > nextTime)
            {
                nextTime = Time.time + enemy.jumpDelay;
                enemy.Jump();
            }

            if (enemy.health.hp <= 0)
                enemy.ChangeState(jumpSpiderState.Dead);

            if (!enemy.areaCollider.isserch)
                enemy.ChangeState(jumpSpiderState.Idle);

        }
        public override void Exit(JumpSpider enemy)
        {

        }
    }
    public class Die : State<JumpSpider>
    {
        public override void Enter(JumpSpider enemy)
        {
            GameObject.Destroy(enemy.gameObject);
        }
        public override void Excute(JumpSpider enemy)
        {

        }
        public override void Exit(JumpSpider enemy)
        {

        }
    }

}

