using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//to create footstep sound gallery
[CreateAssetMenu(fileName = "New Footstep Collection", menuName = "Create New Footstep Collection")]
public class FootstepCollection : ScriptableObject
{
    public List<AudioClip> footstepSounds = new List<AudioClip>();
}
