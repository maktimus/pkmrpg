using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MonHeader : MonoBehaviour
{
    public TextMeshPro petLabel;
    // Start is called before the first frame update
    void Start()
    {
        petLabel = GetComponent<TextMeshPro>();
    }

    public void UpdateLabel(int level)
    {
        petLabel.text = "level: " +  level.ToString() + " Moanne";
    }
}
