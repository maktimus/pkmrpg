using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimController : MonoBehaviour
{
    public TextMeshPro statUpdate;
    //public Animator anim;
    public Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUpAnim(int hp, int atk, int spatk, int def, int spdef, int spd)
    {
        statUpdate.text = "HP +" + hp + "" +
            " ATK +" + atk + " SpA +" + spatk + " DEF +" + def + " SpDEF +" + spdef + " SPD +" + spd;
        anim.Play();
    }
}
