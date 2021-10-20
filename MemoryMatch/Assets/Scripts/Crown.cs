using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crown : MonoBehaviour
{
    public List<Sprite> crownSprites;

    public int currentSprite;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(animationDelay());
    }

    // Update is called once per frame
    private IEnumerator shineAnimation()
    {

        for(int i = 0; i < crownSprites.Count; i++){
            GetComponent<Image>().sprite = crownSprites[i];

            yield return new WaitForSeconds(.1f);
        }

        StartCoroutine(animationDelay());
        
    }

    private IEnumerator animationDelay(){

        yield return new WaitForSeconds(2f);
        
        StartCoroutine(shineAnimation());    
    }
}
