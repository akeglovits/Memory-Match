using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPackList : MonoBehaviour
{

    public Vector3 currentPosition;

    public string currentPack;

    public GameObject selectRow;
    public GameObject unlockRow;

    public GameObject selectButton;
    public GameObject selectedButton;

    public CardPackUnlock cardPackUnlock;

    public float scaleScale;
    public float moveScale;

    // Start is called before the first frame update
    void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GameObject.Find("Card-Page-List").GetComponent<RectTransform>());
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = currentPack.ToUpper();
        
        if(PlayerPrefs.GetInt("allpacks", 0) == 1 || PlayerPrefs.GetInt(currentPack, 0) == 1){

            selectRow.transform.localScale = new Vector3(1f,1f,1f);
            unlockRow.transform.localScale= new Vector3(0f,0f,0f);

        }else{

            unlockRow.transform.localScale = new Vector3(1f,1f,1f);
            selectRow.transform.localScale = new Vector3(0f,0f,0f);

            
        }

        if(PlayerPrefs.GetString("CardPack","numbers") == currentPack){
            selectedButton.transform.SetAsLastSibling();
        }else{
            selectButton.transform.SetAsLastSibling();
        }

    }

    public void closeCardPackCall(){

        cardPackUnlock.currentPack = "allpacks";
        StartCoroutine(closeCardPack());
    }

    public IEnumerator openCardPack(){

        transform.position = currentPosition;
        transform.SetAsLastSibling();

        bool scaleDone = false;
        bool movementDone = false;

        while(!movementDone || !scaleDone){

            if(transform.localScale.y < 1f){
                transform.localScale += new Vector3(scaleScale, scaleScale, scaleScale);
            }else{
                scaleDone = true;
            }

            if(transform.localPosition.y != 0f){
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(0f,0f), moveScale);
            }else{
                movementDone = true;
            }

            yield return new WaitForSeconds(.02f);
        }

        for(int i = 3; i < transform.GetChild(0).GetChild(0).childCount; i++){
            transform.GetChild(0).GetChild(0).GetChild(i).localScale = new Vector3(1f,1f,1f);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetChild(0).GetChild(0).gameObject.GetComponent<RectTransform>());

    }


    public IEnumerator closeCardPack(){

        bool scaleDone = false;
        bool movementDone = false;

        for(int i = 2; i < transform.GetChild(0).GetChild(0).childCount; i++){
            transform.GetChild(0).GetChild(0).GetChild(i).localScale = new Vector3(0f,0f,0f);
        }

        while(!movementDone || !scaleDone){

            if(transform.localScale.y > 0f){
                transform.localScale -= new Vector3(scaleScale,scaleScale,scaleScale);
            }else{
                scaleDone = true;
            }

            if(transform.position.x != currentPosition.x){
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(currentPosition.x, currentPosition.y), moveScale);
            }else{
                movementDone = true;
            }

            yield return new WaitForSeconds(.02f);
        }

        transform.SetAsFirstSibling();

        for(int i = 3; i < transform.GetChild(0).GetChild(0).childCount; i++){
            Destroy(transform.GetChild(0).GetChild(0).GetChild(i).gameObject);
        }
    }

    

    public void selectCardPack(){

        PlayerPrefs.SetString("CardPack",currentPack);
    }
}
