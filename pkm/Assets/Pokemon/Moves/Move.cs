using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public MoveBase Base { get; set; }

    public int cost { get; set; }

    public Move(MoveBase pBase)
    {
        Base = pBase;
        cost = pBase.Cost;
    }
}
