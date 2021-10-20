using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;

public class OnlineLobby : MonoBehaviour
{


    private ListenerRegistration listener;
    // Start is called before the first frame update
    void Start()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        
        listener = db.Collection("ActiveGames").Document(PlayerPrefs.GetString("CurrentGame")).Listen(snapshot => {

            Dictionary<string, object> gameStats = snapshot.ToDictionary();

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
