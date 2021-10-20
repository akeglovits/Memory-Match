using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleCard : MonoBehaviour
{

    public Sprite cardFrontSprite;
    public Sprite cardBackSprite;

    public float initialDelay;
    
    public bool flipped;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startFlip());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator startFlip(){
        yield return new WaitForSeconds(initialDelay);
        StartCoroutine(flipCard());
    }


    public IEnumerator flipCard(){

        yield return new WaitForSeconds(.01f);
        float rotation = 0f;

       
            while(rotation < 90f){

                yield return new WaitForSeconds(.02f);

                transform.Rotate(0f, 15f, 0f, Space.Self);

                rotation += 15f;
            }
        

        if(flipped){
            GetComponent<Image>().sprite = cardFrontSprite;
            transform.GetChild(0).gameObject.GetComponent<Text>().enabled = true;

            flipped = false;
        }else{
            GetComponent<Image>().sprite = cardBackSprite;
            transform.GetChild(0).gameObject.GetComponent<Text>().enabled = false;

            flipped = true;
        }

            while(rotation > 0f){

                yield return new WaitForSeconds(.02f);

                transform.Rotate(0f, -15f, 0f, Space.Self);

                rotation -= 15f;
            }
        
        if(flipped){
            StartCoroutine(flipCard());
        }
        
        

    }

}
