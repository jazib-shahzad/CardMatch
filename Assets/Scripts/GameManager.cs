using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region VARIABLES
    public static GameManager Instance;

    // Refrence to card back
    public Sprite card_Back;
    // refrences to cards faces
    [Header("Include all card faces to be used")]
    public Sprite[] card_Faces;

    #endregion

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // sets the sprite for card face according to card value var
    public Sprite SetCardFace(int i)
    {
        return card_Faces[i];
    }
}
