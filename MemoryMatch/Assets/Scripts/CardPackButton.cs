using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPackButton : MonoBehaviour
{

    public List<Sprite> packContents;
    public string packType;
    public CardPackList cardPackList;
    public CardPackUnlock cardPackUnlock;
    public GameObject cardPackRow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("allpacks", 0) == 1 || PlayerPrefs.GetInt(cardPackUnlock.currentSet, 0) == 1 || PlayerPrefs.GetInt(packType, 0) == 1){
            transform.GetChild(1).gameObject.GetComponent<Image>().enabled = false;
            transform.GetChild(2).gameObject.GetComponent<Image>().enabled = false;
        }else{
            transform.GetChild(1).gameObject.GetComponent<Image>().enabled = true;
            transform.GetChild(2).gameObject.GetComponent<Image>().enabled = true;
        }
    }

    public void showPack(){
        cardPackList.currentPack = packType;
        cardPackList.currentPosition = transform.position;

        cardPackUnlock.currentPack = packType;

        int rows = (int)Mathf.Floor(packContents.Count/3);
        for(int i = 0; i <= rows; i++){
            int cardsInserted = 0;
            GameObject packRow = Instantiate(cardPackRow, cardPackList.gameObject.transform.GetChild(0).GetChild(0));
            for(int j = i * 3; j < (i+1) * 3; j++){
                if(j < packContents.Count){
                    packRow.transform.GetChild(cardsInserted).GetChild(0).gameObject.GetComponent<Image>().sprite = packContents[j];
                    cardsInserted++;
                }
            }

            if(cardsInserted <= 2){
                for(int k = cardsInserted; k <= 2; k++){
                    Destroy(packRow.transform.GetChild(k).gameObject);
                }
            }

            packRow.transform.localScale = new Vector3(0f,0f,0f);
        }

        StartCoroutine(cardPackList.openCardPack());
    }
}
