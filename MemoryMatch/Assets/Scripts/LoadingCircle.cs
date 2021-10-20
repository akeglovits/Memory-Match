using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCircle : MonoBehaviour
{
    public List<Sprite> loadingCircles;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(circleSpin());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator circleSpin(){

        while(true){
            for(int i = 0; i < loadingCircles.Count; i++){
                GetComponent<Image>().sprite = loadingCircles[i];
                yield return new WaitForSeconds(.1f);
            }
            yield return new WaitForSeconds(.01f);
        }
    }
}
