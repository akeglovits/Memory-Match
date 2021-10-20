using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class OnlineConnection : MonoBehaviour
{

public LoadingCircle circle;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(circle.circleSpin());
                Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
  var dependencyStatus = task.Result;
  if (dependencyStatus == Firebase.DependencyStatus.Available) {
    // Create and hold a reference to your FirebaseApp,
    // where app is a Firebase.FirebaseApp property of your application class.
       Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

    StartCoroutine(loadOnline());
    

  } else {
    UnityEngine.Debug.LogError(System.String.Format(
      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
    // Firebase Unity SDK is not safe to use here.
  }
});

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator loadOnline(){


         AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("OnlineHome");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
