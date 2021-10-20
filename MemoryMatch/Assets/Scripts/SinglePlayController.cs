using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SinglePlayController : MonoBehaviour
{

    public GameController gameController;
      private float score;

    public AudioClip success;
      private int coins;

    public bool timerGo;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;

        if(PlayerPrefs.GetInt("BoardSize", 16) == 16){
            coins = 5;
        }else{
            coins = 10;
        }

        GameObject.Find("Coins-Game-End").GetComponent<Text>().text = "+" + coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        double timeAdd = Time.deltaTime;
        if(timerGo){
            score += (float)Math.Round(timeAdd, 2);
        }
        if(PlayerPrefs.GetString("GameType", "Flips") == "Flips"){

            GameObject.Find("Score-Game-End-Label").GetComponent<Text>().text = "FLIPS";

            GameObject.Find("Score").GetComponent<Text>().text = PlayerPrefs.GetString("GameType", "Flips").ToUpper() + ": " + score.ToString();

             GameObject.Find("Score-Game-End").GetComponent<Text>().text = score.ToString();
        }else{

            GameObject.Find("Score-Game-End-Label").GetComponent<Text>().text = "TIME";

            GameObject.Find("Score").GetComponent<Text>().text = PlayerPrefs.GetString("GameType", "Flips").ToUpper() + ": " + score.ToString("#.00");

            GameObject.Find("Score-Game-End").GetComponent<Text>().text = score.ToString("#.00");
        }
    }

    public void compareCards(){
        if(PlayerPrefs.GetString("GameType", "Flips") == "Flips"){
            score++;
        }

        if(gameController.cardCompare[0].transform.GetChild(0).gameObject.GetComponent<Image>().sprite == gameController.cardCompare[1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite){
            StartCoroutine(gameController.cardCompare[0].GetComponent<Card>().cardFade());
            StartCoroutine(gameController.cardCompare[1].GetComponent<Card>().cardFade());
            gameController.cardCompare.Clear();
            gameController.cardDeck.RemoveRange(0,2);

            GameObject.Find("Game-Sounds").GetComponent<AudioSource>().PlayOneShot(success);
        }else{
            gameController.cardCompare.Clear();
        }

        if(gameController.cardDeck.Count == 0){


            if(PlayerPrefs.GetString("GameType", "Flips") == "Time"){

                timerGo = false;

                if(score < PlayerPrefs.GetFloat("BestTime"+PlayerPrefs.GetInt("BoardSize", 16), 9999)){
                    PlayerPrefs.SetFloat("BestTime"+PlayerPrefs.GetInt("BoardSize", 16), score);
                }
                GameObject.Find("Best-Game-End").GetComponent<Text>().text = PlayerPrefs.GetFloat("BestTime"+PlayerPrefs.GetInt("BoardSize", 16), 9999).ToString("#.00");
            }else{
                if((int)score < PlayerPrefs.GetInt("BestFlips"+PlayerPrefs.GetInt("BoardSize", 16), 9999)){
                    PlayerPrefs.SetInt("BestFlips"+PlayerPrefs.GetInt("BoardSize", 16), (int)score);
                }
                GameObject.Find("Best-Game-End").GetComponent<Text>().text = PlayerPrefs.GetInt("BestFlips"+PlayerPrefs.GetInt("BoardSize", 16), 9999).ToString();
            }
            

            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + coins);
        }
    }


    public void startTimer(){
        if(PlayerPrefs.GetString("GameType", "Flips") == "Time"){
            timerGo = true;
        }
    }

    public void stopTimer(){
        if(PlayerPrefs.GetString("GameType", "Flips") == "Time"){
            timerGo = false;
        }
    }

    public IEnumerator showTimedCards(){

        yield return new WaitForSeconds(5f);

        foreach(Card currentCard in FindObjectsOfType(typeof(Card))){
            currentCard.flipped = false;
            StartCoroutine(currentCard.flipCard());
        }

        yield return new WaitForSeconds(1f);

        startTimer();
    }
}
