using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{

    public string settingType;
    public Sprite settingOn;
    public Sprite settingOff;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt(settingType, 1) == 1){
            GetComponent<Image>().sprite = settingOn;

             if(settingType == "Music"){
                GameObject.Find("Background-Music").GetComponent<AudioSource>().mute = false;
            }else if(settingType == "Sounds"){
                GameObject.Find("Game-Sounds").GetComponent<AudioSource>().mute = false;
            }

        }else{
            GetComponent<Image>().sprite = settingOff;

             if(settingType == "Music"){
                GameObject.Find("Background-Music").GetComponent<AudioSource>().mute = true;
            }else if(settingType == "Sounds"){
                GameObject.Find("Game-Sounds").GetComponent<AudioSource>().mute = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleSetting(){

        if(PlayerPrefs.GetInt(settingType, 1) == 1){
            PlayerPrefs.SetInt(settingType, 0);
            GetComponent<Image>().sprite = settingOff;

            if(settingType == "Music"){
                GameObject.Find("Background-Music").GetComponent<AudioSource>().mute = true;
            }else if(settingType == "Sounds"){
                GameObject.Find("Game-Sounds").GetComponent<AudioSource>().mute = true;
            }
        }else{
            PlayerPrefs.SetInt(settingType, 1);
            GetComponent<Image>().sprite = settingOn;

            if(settingType == "Music"){
                GameObject.Find("Background-Music").GetComponent<AudioSource>().mute = false;
            }else if(settingType == "Sounds"){
                GameObject.Find("Game-Sounds").GetComponent<AudioSource>().mute = false;
            }
        }
    }
}
