using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueSounds : MonoBehaviour
{

    private static ContinueSounds instance = null;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null){
              instance = this;
              DontDestroyOnLoad(this.gameObject);
         }else if(instance != this){
              Destroy(this.gameObject);
              return;
         }
    }
}
