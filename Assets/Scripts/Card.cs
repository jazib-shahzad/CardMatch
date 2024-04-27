using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private bool isFlipped;
    private int card_Value;

    private Sprite card_Sprite;

    void Awake()
    {
        card_Sprite = GetComponent<Sprite>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Flip()
    {
        if(isFlipped)
        {
            isFlipped = false;
            card_Sprite = GameManager.Instance.card_Back;

        }else if(!isFlipped)
        {
            isFlipped = true;
            card_Sprite = GameManager.Instance.SetCardFace(card_Value);
        }
    }
}
