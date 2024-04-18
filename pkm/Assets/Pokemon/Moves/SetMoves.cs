using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetMoves : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> moveText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMove(List <Move> moves)
    {
        for (int i = 0; i < moveText.Count; i++)
        {
            if (i < moves.Count)
                moveText[i].text = moves[i].Base.Name;
            else
                moveText[i].text = "-";
        }
    }
}
