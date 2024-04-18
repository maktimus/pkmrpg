using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //references the information of the base attack
    public MoveBase Base { get; set; }

    //...the cost of energy to use it
    public int cdTime { get; set; }

    //sets the move 
    public Move(MoveBase pBase)
    {
        Base = pBase;
        cdTime = pBase.CdTime;
    }
}
