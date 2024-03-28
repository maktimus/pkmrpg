using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyState
{
    private Vector3 targetPos;
    private Vector3 dir;

    public EnemyIdle(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        targetPos = GetRandomPointInCircle();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        //Debug.Log(targetPos);
        base.FrameUpdate();

        if(targetPos.z < enemy.bounds.min.z || targetPos.z > enemy.bounds.max.z || targetPos.x < enemy.bounds.min.x || targetPos.x > enemy.bounds.max.x)
        {
            targetPos = GetRandomPointInCircle();
        }

        dir = (targetPos - enemy.transform.position).normalized;

        enemy.MoveEnemy(dir * enemy.randMovementSpeed);

        if((enemy.transform.position - targetPos).sqrMagnitude < 0.01f)
        {
            targetPos = GetRandomPointInCircle();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private Vector3 GetRandomPointInCircle()
    {
        Bounds bounds = enemy.bounds;
        float offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
        float offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);

        //cant reach point of object in way, check if any obstacles in way of point, if so, get new point
        return enemy.transform.position + new Vector3 (offsetX, 0, offsetZ);
        //return enemy.transform.position + new Vector3(Random.insideUnitSphere.x * enemy.randMovementRange, 0f, Random.insideUnitSphere.z * enemy.randMovementRange);
    }
}
