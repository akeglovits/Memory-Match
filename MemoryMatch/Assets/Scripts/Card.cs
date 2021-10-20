using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Card : MonoBehaviour, IPointerClickHandler
{

    public Sprite cardFrontSprite;
    public Sprite cardBackSprite;

    public GameController gameController;
    

    public bool flipped;
    // Start is called before the first frame update
    void Start()
    {
            
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flipped && gameController.cardCompare.Count > 0 && !gameController.cardCompare.Contains(this.gameObject)){
            flipped = false;
            StartCoroutine(flipCard());
        }
    }

    public void OnPointerClick(PointerEventData eventData){
        
        if(!flipped && gameController.cardCompare.Count < 2){
            flipped = true;
            gameController.cardCompare.Add(this.gameObject);
            StartCoroutine(flipCard());        
        }
    }


    public IEnumerator flipCard(){

        yield return new WaitForSeconds(.01f);
        float rotation = 0f;

        if(PlayerPrefs.GetInt("Animation", 1) == 1){
            while(rotation < 90f){

                yield return new WaitForSeconds(.02f);

                transform.Rotate(0f, 15f, 0f, Space.Self);

                rotation += 15f;
            }
        }

        if(flipped){
            GetComponent<Image>().sprite = cardFrontSprite;
            transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
        }else{
            GetComponent<Image>().sprite = cardBackSprite;
            transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
        }

        if(PlayerPrefs.GetInt("Animation", 1) == 1){
            while(rotation > 0f){

                yield return new WaitForSeconds(.02f);

                transform.Rotate(0f, -15f, 0f, Space.Self);

                rotation -= 15f;
            }
        }

        if(flipped && gameController.cardCompare.Count == 2 && gameController.cardCompare[1] == this.gameObject){
            gameController.compareCards();
        }

    }

    public IEnumerator cardFade(){

        byte alpha = 240;
        Image cardBack = GetComponent<Image>();
        Image cardImage = transform.GetChild(0).gameObject.GetComponent<Image>();

        if(PlayerPrefs.GetInt("Animation", 1) == 1){
        while(alpha > 0){
                cardBack.color = new Color32(255, 255, 255, alpha);
                cardImage.color = new Color32(255, 255, 255, alpha);

                alpha -= 20;

                yield return new WaitForSeconds(.02f);
            }
        }

        yield return new WaitForSeconds(.01f);

        if(this != null){
            Destroy(this.gameObject);
        }

    }
}
