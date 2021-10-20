using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HomePageButton : MonoBehaviour, IPointerClickHandler
{

    public string buttonGroup;
    public string buttonValueString;
    public int buttonValueInt;
    public SceneManage sceneManage;
    public float scaleDelay;

    public Sprite originalSprite;
    public Sprite disabledSprite;
    // Start is called before the first frame update
    void Start()
    {
        if(buttonGroup.Length > 0){

            if((buttonGroup == "GameType" && PlayerPrefs.GetString("GameType", "Flips") == buttonValueString) ||
            (buttonGroup == "BoardSize" && PlayerPrefs.GetInt("BoardSize", 16) == buttonValueInt) ||
            (buttonGroup == "LocalMultiPlayers" && PlayerPrefs.GetInt("LocalMultiPlayers", 2) == buttonValueInt)){
                toggleButtonStatus(false);
            }

        }else{
            StartCoroutine(buttonMovement());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleButtonStatus(bool enableStatus){
        GetComponent<Button>().enabled = enableStatus;

        if(enableStatus){
            GetComponent<Image>().sprite = originalSprite;
        }else{
            GetComponent<Image>().sprite = disabledSprite;

            if(buttonGroup == "GameType"){
                if(buttonValueString == "Time"){
                    GameObject.Find("Single-Play-Description-Text").GetComponent<Text>().text = "CARDS SHOW FOR 5 SECONDS AT THE BEGINNING OF THE GAME. THEN FIND ALL PAIRS AS QUICK AS YOU CAN!";
                }else{
                    GameObject.Find("Single-Play-Description-Text").GetComponent<Text>().text = "CLEAR THE BOARD BY FINDING PAIRS WITH THE LEAST AMOUNT OF FLIPS!";
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData){

        if(buttonGroup.Length > 0){
            
            switch (buttonGroup)
            {
                case "GameType":
                sceneManage.changeGameType(buttonValueString);
                break;

                case "BoardSize":
                sceneManage.changeBoardSize(buttonValueInt);
                break;

                case "LocalMultiPlayers":
                sceneManage.changeLocalMultiPlayers(buttonValueInt);
                break;
                
                default:
                sceneManage.changeGameType(buttonValueString);
                break;
            }

            foreach(HomePageButton currentButton in FindObjectsOfType(typeof(HomePageButton))){
                if(currentButton.buttonGroup == buttonGroup){
                    if(buttonValueInt == currentButton.buttonValueInt && buttonValueString == currentButton.buttonValueString){
                        currentButton.toggleButtonStatus(false);
                    }else{
                        currentButton.toggleButtonStatus(true);
                    }
                }
            }
        }
    }

    private IEnumerator buttonMovement(){

        Vector3 scaleChange = new Vector3(.02f,.02f,.02f);

        yield return new WaitForSeconds(scaleDelay);

        while(true){
            while(transform.localScale.y >= .95f){
                transform.localScale -= scaleChange;
                yield return new WaitForSeconds(.02f);
            }

            while(transform.localScale.y <= 1.05f){
                transform.localScale += scaleChange;
                yield return new WaitForSeconds(.02f);
            }

            while(transform.localScale.y > 1f){
                transform.localScale -= scaleChange;
                yield return new WaitForSeconds(.02f);
            }

            yield return new WaitForSeconds(3f);
        }
    }
}
