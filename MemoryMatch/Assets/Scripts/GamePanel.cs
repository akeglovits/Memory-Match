using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{

    public GameObject currentPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openPanelCall(string type){
        if(type == "instant"){
            transform.localScale = new Vector3(1f,1f,1f);
            GameObject.Find("Block-Background").transform.SetAsLastSibling();
            transform.SetAsLastSibling();
        }else{
            StartCoroutine(openPanel());
        }
    }

    public void closePanelCall(string type){

        if(type == "instant"){
            transform.SetAsFirstSibling();
            GameObject.Find("Block-Background").transform.SetAsFirstSibling();
            transform.localScale = new Vector3(0f,0f,0f);
        }else{
            StartCoroutine(closePanel());
        }
    }

    public void setCurrentPanel(string panel){
        currentPanel = GameObject.Find(panel);
    }

    public void goBackToCurrentPanel(){
        GameObject.Find("Block-Background").transform.SetAsLastSibling();
        currentPanel.transform.SetAsLastSibling();
    }

    public IEnumerator openPanel(){
        float scale = .1f;
        float scale2 = .03f;

        GameObject.Find("Block-Background").transform.SetAsLastSibling();
        transform.SetAsLastSibling();

        while(transform.localScale.y < 1.1f){
            transform.localScale += new Vector3(scale,scale,scale);
            yield return new WaitForSeconds(.02f);
        }

        yield return new WaitForSeconds(.05f);

        while(transform.localScale.y > 1f){
            transform.localScale -= new Vector3(scale2,scale2,scale2);
            yield return new WaitForSeconds(.02f);
        }


        transform.localScale = new Vector3(1f,1f,1f);
    }


    public IEnumerator closePanel(){
        float scale = .1f;
        float scale2 = .03f;
        while(transform.localScale.y < 1.1f){
            transform.localScale += new Vector3(scale2,scale2,scale2);
            yield return new WaitForSeconds(.02f);
        }

        yield return new WaitForSeconds(.1f);

        while(transform.localScale.y > 0){
            transform.localScale -= new Vector3(scale,scale,scale);
            yield return new WaitForSeconds(.02f);
        }

        transform.localScale = new Vector3(0f,0f,0f);

        transform.SetAsFirstSibling();
        GameObject.Find("Block-Background").transform.SetAsFirstSibling();

    }
}
