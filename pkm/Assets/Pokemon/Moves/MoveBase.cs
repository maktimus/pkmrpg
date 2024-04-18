using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//used to creeate new moves
[CreateAssetMenu(fileName = "Move", menuName = "Pokemon/Create new move")]
public class MoveBase : ScriptableObject
{
    [Header("Name")]
    [SerializeField] string name_;

    [Header("Info")]
    [TextArea]
    [SerializeField] string description;

    [SerializeField] PokemonType type;
    [SerializeField] int power;
    [SerializeField] int cdTime;


    public string Name
    {
        get { return name_; }
    }

    public string Description
    {
        get { return description; }
    }

    public PokemonType Type
    {
        get { return type; }
    }

    public int Power
    {
        get { return power; }
    }

    public int CdTime
    {
        get { return cdTime; }
    }

}
