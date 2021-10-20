using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSetButton : MonoBehaviour
{

    public List<string> setPacks;
    public List<Transform> setRows;
    public string setTitle;

    public string setType;

    public CardPackUnlock cardPackUnlock;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showSet(){

        cardPackUnlock.currentSet = setType;
        
        int unlockedPacks = 0;
        foreach(string pack in setPacks){
            if(PlayerPrefs.GetInt(pack, 0) == 1){
                unlockedPacks++;
            }
        }

        if(unlockedPacks < setPacks.Count){
            GameObject.Find("Unlock-Set-Row").transform.localScale = new Vector3(1f,1f,1f);
        }

        GameObject.Find("Card-Packs-Title-Row").transform.localScale = new Vector3(1f,1f,1f);

        GameObject.Find("Card-Packs-Title").GetComponent<Text>().text = setTitle;
        foreach(Transform set in setRows){
            set.localScale = new Vector3(1f,1f,1f);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(GameObject.Find("Card-Packs-List").GetComponent<RectTransform>());

        StartCoroutine(moveSetPanel("open"));
    }

    public void closeSet(){
        StartCoroutine(moveSetPanel("close"));
    }

    public IEnumerator moveSetPanel(string movement){

        Vector2 destination = new Vector2(0f,0f);

        if(movement == "close"){
            destination = new Vector2(1300f, 0f);
        }

        while(GameObject.Find("Card-Packs").transform.localPosition.x != destination.x){

            GameObject.Find("Card-Packs").transform.localPosition = Vector2.MoveTowards(GameObject.Find("Card-Packs").transform.localPosition, destination, 80f);

            yield return new WaitForSeconds(.02f);
        }

        yield return new WaitForSeconds(.01f);

        if(movement == "close"){
            foreach(Transform child in GameObject.Find("Card-Packs-List").transform){
                child.localScale = new Vector3(0f,0f,0f);
            }
        }

    }
}
