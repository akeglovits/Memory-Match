using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void moveCardPage(string movement){

        StartCoroutine(movePage(movement));
    }

    private IEnumerator movePage(string moveType){

        Vector2 destination = new Vector2(0f,0f);

        if(moveType == "close"){
            destination = new Vector2(1300f, 0f);
        }

        while(transform.localPosition.x != destination.x){

            transform.localPosition = Vector2.MoveTowards(transform.localPosition, destination, 80f);

            yield return new WaitForSeconds(.02f);
        }

        yield return new WaitForSeconds(.01f);

    }
}
