using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
#if UNITY_ANDROID
using GooglePlayGames;
#endif

public class Leaderboard : MonoBehaviour
{

    private static ILeaderboard time4;
    private static ILeaderboard time6;
    private static ILeaderboard flips4;
    private static ILeaderboard flips6;

    public GamePanel notLoggedInPanel;

   

    // Start is called before the first frame update
    void Start()
    {

        #if UNITY_ANDROID
        PlayGamesPlatform.Activate();
        #endif

        Social.localUser.Authenticate(success => {
            if (success)
            {
                Debug.Log("Authentication successful");
                string userInfo = "Username: " + Social.localUser.userName +
                    "\nUser ID: " + Social.localUser.id +
                    "\nIsUnderage: " + Social.localUser.underage;
                Debug.Log(userInfo);

                PlayerPrefs.SetInt("LoggedIn", 1);
            }
            else{
                Debug.Log("Authentication failed");

                PlayerPrefs.SetInt("LoggedIn", 0);
            }
        });

         time4 = Social.CreateLeaderboard();
         time6 = Social.CreateLeaderboard();
         flips4 = Social.CreateLeaderboard();
         flips6 = Social.CreateLeaderboard();

         #if UNITY_IOS

         GameObject.Find("Not-Logged-In-Main-Text").GetComponent<Text>().text = "YOU MUST BE SIGNED IN TO GAMECENTER TO ACCESS THE LEADERBOARDS";

        time4.id = "time4";
        time6.id = "time6";
        flips4.id = "flips4";
        flips6.id = "flips6";

        #elif UNITY_ANDROID

        GameObject.Find("Not-Logged-In-Main-Text").GetComponent<Text>().text = "YOU MUST BE SIGNED IN TO GOOGLE PLAY GAMES TO ACCESS THE LEADERBOARDS";

        time4.id = "CgkI-vrw5sgUEAIQAQ";
        time6.id = "CgkI-vrw5sgUEAIQAg";
        flips4.id = "CgkI-vrw5sgUEAIQAw";
        flips6.id = "CgkI-vrw5sgUEAIQBA";

        #endif


    }

    public void OpenLeaderboard()
    {
        if(PlayerPrefs.GetInt("LoggedIn", 0) == 1){
        Social.ShowLeaderboardUI();
        }else{
            notLoggedInPanel.openPanelCall("");
        }
    }


}
