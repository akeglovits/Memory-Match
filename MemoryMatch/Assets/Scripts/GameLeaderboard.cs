using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
#if UNITY_ANDROID
using GooglePlayGames;
#endif

public class GameLeaderboard : MonoBehaviour
{
   private static ILeaderboard leaderboard;

   public GameObject leaderboardButton;

    // Start is called before the first frame update
    void Start()
    {

        #if UNITY_ANDROID
        PlayGamesPlatform.Activate();
        #endif

        if(PlayerPrefs.GetInt("LoggedIn", 0) == 0){
        Social.localUser.Authenticate(success => {
            if (success)
            {
                Debug.Log("Authentication successful");
                string userInfo = "Username: " + Social.localUser.userName +
                    "\nUser ID: " + Social.localUser.id +
                    "\nIsUnderage: " + Social.localUser.underage;
                Debug.Log(userInfo);

                PlayerPrefs.SetInt("LoggedIn", 1);

                leaderboardButton.SetActive(true);
            }
            else{
                Debug.Log("Authentication failed");

                PlayerPrefs.SetInt("LoggedIn", 0);

                leaderboardButton.SetActive(false);
            }
        });
        }else{
            leaderboardButton.SetActive(true);
        }

         leaderboard = Social.CreateLeaderboard();

         #if UNITY_IOS

        if(PlayerPrefs.GetString("GameType", "Flips") == "Time"){
            if(PlayerPrefs.GetInt("BoardSize", 16) == 16){
                leaderboard.id = "time4";
            }else{
                 leaderboard.id = "time6";
            }
        }else{
            if(PlayerPrefs.GetInt("BoardSize", 16) == 16){
                leaderboard.id = "flips4";
            }else{
                 leaderboard.id = "flips6";
            }
        }

        #elif UNITY_ANDROID


        if(PlayerPrefs.GetString("GameType", "Flips") == "Time"){
            if(PlayerPrefs.GetInt("BoardSize", 16) == 16){
                leaderboard.id = "CgkI-vrw5sgUEAIQAQ";
            }else{
                 leaderboard.id = "CgkI-vrw5sgUEAIQAg";
            }
        }else{
            if(PlayerPrefs.GetInt("BoardSize", 16) == 16){
                leaderboard.id = "CgkI-vrw5sgUEAIQAw";
            }else{
                 leaderboard.id = "CgkI-vrw5sgUEAIQBA";
            }
        }

        #endif


    }

    public static void ReportScore(long score)
    {

        Debug.Log("Reporting score " + score + " on leaderboard " + leaderboard.id);
        Social.ReportScore(score, leaderboard.id, success => {
            Debug.Log(success ? "Reported score successfully" : "Failed to report score");
        });
    }

    public void OpenLeaderboard()
    {
        if(PlayerPrefs.GetInt("LoggedIn", 0) == 1){
        Social.ShowLeaderboardUI();
        }
    }


}