using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region VARIABLES
    public static GameManager Instance;

    // bool to track if card can be flipped(i.e if it's flipping don't let it get flipped in halfway)
    public static bool can_Flip = true;
    // add flipped cards to this list for match check
    public GameObject cardsHolder;
    public int sizeIndex;
    [HideInInspector]
    public List<Card> cardsMatches = new ();
    // Refrence to card back
    public Sprite card_Back;
    // refrences to cards faces
    [Header("Include all card faces to be used")]
    public Sprite[] card_Faces;

    public int totalMatches;
    private int turnsCount;
    private int matchesCount;
    private int combo;

    // UI refrences
    public GameObject loadingScreen;
    public GameObject gameOverPanel;
    public Text turnsText;
    public Text matchesText;
    public Text scoreText;
    public Text comboText;

    #endregion

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (sizeIndex == 1)
            cardsHolder.GetComponent<GridLayoutGroup>().cellSize = new Vector2(250, 250);
        else if (sizeIndex == 2)
            cardsHolder.GetComponent<GridLayoutGroup>().cellSize = new Vector2(200, 200);
        else if (sizeIndex == 3)
            cardsHolder.GetComponent<GridLayoutGroup>().cellSize = new Vector2(150, 150);
        else
            cardsHolder.GetComponent<GridLayoutGroup>().cellSize = new Vector2(250, 250);

        Invoke(nameof(DelayStart), 1f);

    }

    void DelayStart()
    {
        cardsHolder.GetComponent<GridLayoutGroup>().enabled = false;
        loadingScreen.SetActive(false);
    }
    public void MatchCheck(Card card)
    {
        turnsCount++;
        turnsText.text = turnsCount.ToString();
        cardsMatches.Add (card);

        if (cardsMatches.Count >= 2)
        {
            if (cardsMatches[0].card_Value == cardsMatches[1].card_Value)
            {
                matchesCount++;
                matchesText.text = matchesCount.ToString();
                combo++;
                comboText.text = combo.ToString();
                SoundsHandler.instance.PlayCardMatchSFX();
                totalMatches--;
                Invoke(nameof(CardsMatch), 0.3f);
            }
            else
            {
                if(combo >= 1)
                {
                    combo = 0;
                    comboText.text = combo.ToString();
                }
                Invoke(nameof(CardsMisMatch), 0.3f);
            }
        }
        else
        {
            can_Flip = true;
        }
    }

    void CardsMatch()
    {
        cardsMatches[0].gameObject.SetActive(false);
        cardsMatches[1].gameObject.SetActive(false);
        cardsMatches.Clear();
        if (totalMatches == 0)
        {
            SoundsHandler.instance.PlayGameOver();
            if (!PlayerPrefs.HasKey("Score"))
            {
                PlayerPrefs.SetInt("Score", 1);
                scoreText.text = "All Time Best: 1";
            }
            else
            {
                int score = PlayerPrefs.GetInt("Score");
                score += 1;
                PlayerPrefs.SetInt("Score", score);
                scoreText.text = "All Time Best: " + score.ToString();

            }
            gameOverPanel.SetActive(true);
        }
        can_Flip = true;
    }
    void CardsMisMatch()
    {
        SoundsHandler.instance.PlayCardMisMatchSFX();
        cardsMatches[0].CardMisMatch();
        cardsMatches[1].CardMisMatch();
        cardsMatches.Clear();
        can_Flip = true;
    }
    
    // sets the sprite for card face according to card value var
    public Sprite SetCardFace(int i)
    {
        return card_Faces[i];
    }
    public void Retry()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

}
