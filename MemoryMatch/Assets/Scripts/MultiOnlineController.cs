using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiOnlineController : MonoBehaviour
{
    public GameController gameController;
    public GameObject playerTurnText;

    public AudioClip success;
    private List<int> scores = new List<int>();

    private int currentPlayer;
    // Start is called before the first frame update
    async void Start()
    {
        for(int i = 1; i <= PlayerPrefs.GetInt("LocalMultiPlayers", 2); i++){
            scores.Add(0);
            GameObject.Find("P" + i + "-Score").GetComponent<Text>().enabled = true;
            GameObject.Find("Place-" + i + "-Score").GetComponent<Text>().enabled = true;
        }

        currentPlayer = Random.Range(1, PlayerPrefs.GetInt("LocalMultiPlayers", 2) + 1);

        StartCoroutine(gameStartDelay());
    }

    // Update is called once per frame
    void Update()
    {

        
        for(int i = 1; i <= PlayerPrefs.GetInt("LocalMultiPlayers", 2); i++){
            GameObject.Find("P" + i + "-Score").GetComponent<Text>().text = "P" + i + ": " + scores[i-1];

            if(i == currentPlayer){
                GameObject.Find("P" + i + "-Score").GetComponent<Text>().color = Color.green;
            }else{
                GameObject.Find("P" + i + "-Score").GetComponent<Text>().color = Color.white;
            }
        }

        playerTurnText.transform.SetAsLastSibling();
    }

    public void compareCards(){

        if(gameController.cardCompare[0].transform.GetChild(0).gameObject.GetComponent<Image>().sprite == gameController.cardCompare[1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite){
            StartCoroutine(gameController.cardCompare[0].GetComponent<Card>().cardFade());
            StartCoroutine(gameController.cardCompare[1].GetComponent<Card>().cardFade());
            gameController.cardCompare.Clear();
            gameController.cardDeck.RemoveRange(0,2);

            GameObject.Find("Game-Sounds").GetComponent<AudioSource>().PlayOneShot(success);

            scores[currentPlayer - 1]++;
        }else{
            gameController.cardCompare.Clear();
            if(currentPlayer == PlayerPrefs.GetInt("LocalMultiPlayers", 2)){
                currentPlayer = 1;
            }else{
                currentPlayer++;
            }

            StartCoroutine(showPlayerTurn());
        }

        if(gameController.cardDeck.Count == 0){
            List<int> sortedScores = new List<int>();
            List<int> sortedIndex = new List<int>();

            for(int i = 0; i < PlayerPrefs.GetInt("LocalMultiPlayers", 2); i++){

                if(sortedIndex.Count == 0){
                    sortedScores.Add(scores[i]);
                    sortedIndex.Add(i);
                }else{
                    for(int j = 0; j < sortedScores.Count; j++){
                        if(j == 0 && scores[i] > sortedScores[j]){
                            sortedScores.Insert(0,scores[i]);
                            sortedIndex.Insert(0, i);
                            break;
                        }else if(j == sortedScores.Count - 1){
                            sortedScores.Add(scores[i]);
                            sortedIndex.Add(i);
                            break;
                        }else{
                            if(scores[i] <= sortedScores[j] && scores[i] > sortedScores[j+1]){
                                sortedScores.Insert(j+1,scores[i]);
                                sortedIndex.Insert(j+1, scores[i]);
                                break;
                            }
                        }
                    }
                }

            }

            for(int i = 0; i < sortedIndex.Count; i++){
                string player = "P1";
                int playerScore = sortedScores[i];

                switch (sortedIndex[i])
                {
                    case 0:
                    player = "P1";
                    break;

                    case 1:
                    player = "P2";
                    break;

                    case 2:
                    player = "P3";
                    break;

                    case 3:
                    player = "P4";
                    break;

                    default:
                    player = "P1";
                    break;
                }

                GameObject.Find("Place-" + (i+1) + "-Score").GetComponent<Text>().text = player + ": " + playerScore;

                if(i == 0){
                    if(sortedScores[i] == sortedScores[i+1]){
                        GameObject.Find("Game-Over-Text").GetComponent<Text>().text = "IT'S A TIE!";
                    }else{
                        GameObject.Find("Game-Over-Text").GetComponent<Text>().text = player + " WINS!";
                    }

                    GameObject.Find("Place-" + (i+1) + "-Crown").GetComponent<Image>().enabled = true;
                    
                }else{
                    if(sortedScores[i] == sortedScores[0]){
                        GameObject.Find("Place-" + (i+1) + "-Crown").GetComponent<Image>().enabled = true;
                    }
                }
            }

        }

    }

    private IEnumerator gameStartDelay(){
        yield return new WaitForSeconds(.4f);

        StartCoroutine(showPlayerTurn());
    }

    private IEnumerator showPlayerTurn(){

        playerTurnText.GetComponent<Text>().text = "P" + currentPlayer + " TURN!";

        Vector3 growScale = new Vector3(.1f,.1f,.1f);

        while(playerTurnText.transform.localScale.y < 1f){
            playerTurnText.transform.localScale += growScale;
            yield return new WaitForSeconds(.02f);
        }

        yield return new WaitForSeconds(1f);

        while(playerTurnText.transform.localScale.y > 0f){
            playerTurnText.transform.localScale -= growScale;
            yield return new WaitForSeconds(.02f);
        }

        playerTurnText.transform.localScale = new Vector3(0f,0f,0f);
    }
}
