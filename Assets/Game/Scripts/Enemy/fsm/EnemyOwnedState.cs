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
            enemy.animationState = AniState.idle;
        }
        public override void Excute(Noide enemy)
        {
            if (enemy.IsDead()) enemy.ChangeState(enemyState.DeadAni);
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
            if (enemy.transform.localScale != new Vector3(-2, 2))
                enemy.transform.localScale = new Vector3(-2, 2);
            else
                enemy.transform.localScale = new Vector3(2, 2);

            enemy.moveSpeed = enemy.saveSpeed;
        }
    }
    public class Move : State<Noide>
    {
        public override void Enter(Noide enemy)
        {

            enemy.animationState = AniState.Move;
            if (enemy.wallCheck)
                enemy.wallCheck = false;
            
        }
        public override void Excute(Noide enemy)
        {
            if (enemy.wallCheck)
                enemy.ChangeState(enemyState.Idle);
            if (enemy.IsDead()) enemy.ChangeState(enemyState.DeadAni);
            switch (enemy.enemyMoveDirection) {
                case movedirection.Left:
                    enemy.rig2D.velocity = new Vector2(enemy.moveSpeed*-1f, enemy.rig2D.velocity.y);
                    break;
                case movedirection.Right:
                    enemy.rig2D.velocity = new Vector2(enemy.moveSpeed, enemy.rig2D.velocity.y);
                    break;
            }
          
        }
        public override void Exit(Noide enemy)
        {
        }
    }
    public class DieAni : State<Noide> //죽었을때
    {

        static float s_dcurdelay;
        public override void Enter(Noide enemy)
        {
            enemy.moveSpeed = 0;
            s_dcurdelay = 0;
            enemy.GetComponent<BoxCollider2D>().enabled = false;
            enemy.animationState = AniState.Dead;
            enemy.sound.Death();
        }
        public override void Excute(Noide enemy)
        {
            s_dcurdelay += Time.deltaTime;
            if (s_dcurdelay >= 3)
            {
                Debug.Log("사망");
                enemy.ChangeState(enemyState.dead);
            }

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

namespace SpiderBossState
{

    public class Idle : State<BossSpieder>
    {
        static float s_curdelay;
        public override void Enter(BossSpieder enemy)
        {
            s_curdelay = 0;
            enemy.ChangeState(BossSpiderState.SlowMove);

        }
        public override void Excute(BossSpieder enemy)
        {
            if (enemy.camOut) enemy.ChangeState(BossSpiderState.CamOutMove);
        }
        public override void Exit(BossSpieder enemy)
        {

        }
    }
    public class FastMove : State<BossSpieder>
    {
        static float s_curdelay;
        public override void Enter(BossSpieder enemy)
        {

            s_curdelay = 0;

            enemy.BossMovementFast();
        }
        public override void Excute(BossSpieder enemy)
        {

            if (enemy.camOut) enemy.ChangeState(BossSpiderState.CamOutMove);
            s_curdelay += Time.deltaTime;
            if (s_curdelay >= 1.5)
            {
                enemy.ChangeState(BossSpiderState.SlowMove);
            }
        }
        public override void Exit(BossSpieder enemy)
        {

        }
    }
    public class SlowMove : State<BossSpieder>
    {
        static float s_curdelay;

        public override void Enter(BossSpieder enemy)
        {
            s_curdelay = 0;
            enemy.BossMovementSlow();
        }
        public override void Excute(BossSpieder enemy)
        {

            if (enemy.camOut) enemy.ChangeState(BossSpiderState.CamOutMove);
            s_curdelay += Time.deltaTime;
            if (s_curdelay >= 1.5)
            {
                enemy.ChangeState(BossSpiderState.FastMove);
            }
        }
        public override void Exit(BossSpieder enemy)
        {

        }
    }
    public class CamOutMove : State<BossSpieder>
    {
        static float s_curdelay;
        public override void Enter(BossSpieder enemy)
        {
            s_curdelay = 0;
            enemy.BossMovementCamOut();
        }
        public override void Excute(BossSpieder enemy)
        {
            s_curdelay += Time.deltaTime;
            if (s_curdelay >= 1)
            {
               if (enemy.camOut) enemy.ChangeState(BossSpiderState.CamOutMove);
               else  enemy.ChangeState(BossSpiderState.FastMove);

            }
        }
        public override void Exit(BossSpieder enemy)
        {

        }
    }

    public class Die : State<BossSpieder>
    {
        public override void Enter(BossSpieder enemy)
        {
        }
        public override void Excute(BossSpieder enemy)
        {
        }
        public override void Exit(BossSpieder enemy)
        {
        }
    }


}