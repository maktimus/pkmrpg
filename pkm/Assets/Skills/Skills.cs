using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills
{
    [CreateAssetMenu(menuName = "Skills/Create Skill")]
    public class Skills : ScriptableObject
    {
        public string description;
        public int levelNeeded;
        public int attackPower;
        public int critChance;

        [Header("Special or Physical")]
        public bool Special = false;
        public bool Physical = false;
    }
}