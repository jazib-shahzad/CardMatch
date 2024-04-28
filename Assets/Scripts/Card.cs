using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    #region VARIABLES
    // bool to track if card is flipped or not
    private bool isFlipped;
    // refrence to image component
    private Image image;
    // card value according to which card faces will be added (later private set from gamemanager)
    public int card_Value;
    // flip card animation duration
    [Header("Flip Card Duration")]
    public float flipDuration = 0.5f;

    #endregion

    void Awake()
    {
        // cache refrences
        image = GetComponent<Image>();
    }

    public void Flip()
    {
        // return if card is in process of being flipped
        if (!GameManager.can_Flip) return;
        if(!isFlipped) // card is not flipped, flip it
        {
            GameManager.can_Flip = false;
            isFlipped = true;
            SoundsHandler.instance.PlayCardFlipSFX();
            // flip animation, also sets card face when animation completes
            StartCoroutine(FlipCoroutine(new Vector3(-1f, 1f, 1f), Vector3.one, GameManager.Instance.SetCardFace(card_Value)));
        }
    }

    public void CardMisMatch()
    {
        isFlipped = false;
        GetComponent<Image>().sprite = GameManager.Instance.card_Back;
        //StartCoroutine(FlipCoroutine(Vector3.one, new Vector3(-1f, 1f, 1f)));
    }

    IEnumerator FlipCoroutine(Vector3 fromScale, Vector3 toScale, Sprite sprite = null)
    {
        float elapsedTime = 0f;
        while (elapsedTime < flipDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / flipDuration);
            image.rectTransform.localScale = Vector3.Lerp(fromScale, toScale, t);
            yield return null;
        }
        // set sprite if it's not null (i.e only for card face scenario)
        if(sprite != null)
            GetComponent<Image>().sprite = sprite;
        GameManager.Instance.MatchCheck(GetComponent<Card>());
    }
}
