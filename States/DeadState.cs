using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    private Rigidbody2D RB;
    protected D_DeadState stateData;
    public DeadState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
        
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        entity.gameObject.layer = 20;

        RB = entity.aliveGO.GetComponent<Rigidbody2D>();
        RB.constraints = RigidbodyConstraints2D.FreezeAll; 

        startTime = Time.time;

     

       
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.deathTimer)
        {

            GameObject.Instantiate(stateData.deathBloodParticle, entity.aliveGO.transform.position, stateData.deathBloodParticle.transform.rotation);
            GameObject.Instantiate(stateData.deathChunkParticle, entity.aliveGO.transform.position, stateData.deathChunkParticle.transform.rotation);
           // Debug.Log("Timer in dead state done");

            entity.gameObject.SetActive(false);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
