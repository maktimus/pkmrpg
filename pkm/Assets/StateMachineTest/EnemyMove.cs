using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyMove
{

    Rigidbody rb { get; set; }
    void MoveEnemy(Vector3 velocity);
}
