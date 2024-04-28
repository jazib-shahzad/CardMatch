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
    // bool to track if card can be flipped(i.e if it's flipping don't let it get flipped in halfway)
    private bool can_Flip = true;
    // card value according to which card faces will be added (later private set from gamemanager)
    public int card_Value;
    // flip sfx
    public AudioClip flipFX;
    // flip card animation duration
    [Header("Flip Card Duration")]
    public float flipDuration = 0.5f;
    // refrence to audio source
    AudioSource audioSource;

    #endregion

    void Awake()
    {
        // cache refrences
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        image = GetComponent<Image>();
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
        // return if card is in process of being flipped
        if (!can_Flip) return;

        if(isFlipped)   // card is flipped, flip it back
        {
            can_Flip = false;
            isFlipped = false;
            audioSource.clip = flipFX;
            audioSource.Play();
            GetComponent<Image>().sprite = GameManager.Instance.card_Back;
            StartCoroutine(FlipCoroutine(Vector3.one, new Vector3(-1f, 1f, 1f)));

        }
        else if(!isFlipped) // card is not flipped, flip it
        {
            can_Flip = false;
            isFlipped = true;
            audioSource.clip = flipFX;
            audioSource.Play();
            //GetComponent<Image>().sprite = GameManager.Instance.SetCardFace(card_Value);
            // flip animation, also sets card face when animation completes
            StartCoroutine(FlipCoroutine(new Vector3(-1f, 1f, 1f), Vector3.one, GameManager.Instance.SetCardFace(card_Value)));
        }
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
        can_Flip = true;
    }
}
