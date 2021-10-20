using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public CardPacks cardPacks;
    public GamePanel gameOverPanel;
    public GameEndAd gameEndAd;
    public SinglePlayController singlePlayController;
    public MultiLocalController multiLocalController;
    public MultiOnlineController multiOnlineController;
    public GameObject card;
    public List<Sprite> cardpack;

    public List<Sprite> cardDeck;

    public List<GameObject> cardCompare;
    private int boardSize;

  

    // Start is called before the first frame update
    void Start()
    {

        
        boardSize = PlayerPrefs.GetInt("BoardSize", 16);
        cardDeck = new List<Sprite>(new Sprite[boardSize]);
        switch (PlayerPrefs.GetString("CardPack","numbers"))
        {
            case "numbers":
            cardpack = new List<Sprite>(cardPacks.numbers);
            break;

            case "letters":
            cardpack = new List<Sprite>(cardPacks.letters);
            break;

            case "shapes":
            cardpack = new List<Sprite>(cardPacks.shapes);
            break;

            case "animals":
            cardpack = new List<Sprite>(cardPacks.animals);
            break;

            case "dogs":
            cardpack = new List<Sprite>(cardPacks.dogs);
            break;

            case "cats":
            cardpack = new List<Sprite>(cardPacks.cats);
            break;

            case "kitchen":
            cardpack = new List<Sprite>(cardPacks.kitchen);
            break;

            case "office":
            cardpack = new List<Sprite>(cardPacks.office);
            break;

            case "furniture":
            cardpack = new List<Sprite>(cardPacks.furniture);
            break;

            case "sports":
            cardpack = new List<Sprite>(cardPacks.sports);
            break;

            case "camping":
            cardpack = new List<Sprite>(cardPacks.camping);
            break;

            case "playground":
            cardpack = new List<Sprite>(cardPacks.playground);
            break;

            case "transport":
            cardpack = new List<Sprite>(cardPacks.transport);
            break;

            case "boats":
            cardpack = new List<Sprite>(cardPacks.boats);
            break;

            case "cars":
            cardpack = new List<Sprite>(cardPacks.cars);
            break;
            
            default:
            cardpack = new List<Sprite>(cardPacks.numbers);
            break;
        }

        buildDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void buildDeck(){
        for(int i = 0; i < boardSize/2; i++){
            int randomNum = UnityEngine.Random.Range(0,cardpack.Count);
            insertCard(cardpack[randomNum]);
            insertCard(cardpack[randomNum]);
            cardpack.RemoveAt(randomNum);
        }

        dealCards();
    }

    private void insertCard(Sprite cardImage){

        int randomNum = UnityEngine.Random.Range(0, cardDeck.Count);

        if(cardDeck[randomNum] != null){
            insertCard(cardImage);
            return;
        }else{
            cardDeck[randomNum] = cardImage;
        }

    }

    private void dealCards(){
        int rows = (int)Mathf.Sqrt(boardSize);
        int cardnum = 0;
        for(int i = 1; i <= rows; i++){
            for(int j = 1; j <= rows; j++){
                GameObject cardPlaced = Instantiate(card,GameObject.Find("SafeArea").transform);
                cardPlaced.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = cardDeck[cardnum];
                float height = cardPlaced.GetComponent<RectTransform>().rect.height;
                float width = cardPlaced.GetComponent<RectTransform>().rect.width;
                float xMovement = 0f;
                float yMovement = 0f;

                if(j <= rows/2){
                    xMovement = (width/2) + ((width + 5f) * Mathf.Abs(j - (rows/2))) + 5f;
                }else{
                    xMovement = (width/2) + ((width + 5f) * (Mathf.Abs(j - (rows/2)) - 1)) +5f;
                }
            

                if(i <= rows/2){
                    yMovement = (height/2) + ((height + 5f) * Mathf.Abs(i - (rows/2))) + 5f;

                    if(j <= rows/2){
                        cardPlaced.transform.localPosition = new Vector3(-xMovement,yMovement,0f);
                    }else{
                        cardPlaced.transform.localPosition = new Vector3(xMovement,yMovement,0f);
                    }
                }else{
                    yMovement = (height/2) + ((height + 5f)* (Mathf.Abs(i - (rows/2)) - 1)) + 5f;

                    if(j <= rows/2){
                        cardPlaced.transform.localPosition = new Vector3(-xMovement,-yMovement,0f);
                    }else{
                        cardPlaced.transform.localPosition = new Vector3(xMovement,-yMovement,0f);
                    }
                }

                if(SceneManager.GetActiveScene().name == "SinglePlay" && PlayerPrefs.GetString("GameType", "Flips") == "Time"){
                    cardPlaced.GetComponent<Card>().flipped = true;
                    cardPlaced.GetComponent<Image>().sprite = cardPlaced.GetComponent<Card>().cardFrontSprite;
                    cardPlaced.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;

                }
                cardnum++;
            }
        }

        if(SceneManager.GetActiveScene().name == "SinglePlay" && PlayerPrefs.GetString("GameType", "Flips") == "Time"){
            StartCoroutine(singlePlayController.showTimedCards());
        }
    }

    public void compareCards(){

        if(SceneManager.GetActiveScene().name == "SinglePlay"){
            singlePlayController.compareCards();
        }else if(SceneManager.GetActiveScene().name == "MultiLocal"){
            multiLocalController.compareCards();
        }else{
            //multiOnlineController.compareCards();
        }

        if(cardDeck.Count == 0){
            gameOverPanel.openPanelCall("instant");
            gameEndAd.showGameEndAd();
        }
    }

}
