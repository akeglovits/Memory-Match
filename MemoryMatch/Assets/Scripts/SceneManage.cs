using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{

    public string gameType;
    public int boardSize;

    public int localMultiPlayers;
    // Start is called before the first frame update
    void Start()
    {
        gameType = PlayerPrefs.GetString("GameType", "Flips");
        boardSize = PlayerPrefs.GetInt("BoardSize", 16);
        localMultiPlayers = PlayerPrefs.GetInt("LocalMultiPlayers", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeGameType(string type){
        gameType = type;
    }

    public void changeBoardSize(int size){
        boardSize = size;
    }

    public void changeLocalMultiPlayers(int players){
        localMultiPlayers = players;
    }

    public void loadScene(string scene){
        PlayerPrefs.SetString("GameType", gameType);
        PlayerPrefs.SetInt("BoardSize", boardSize);
        PlayerPrefs.SetInt("LocalMultiPlayers", localMultiPlayers);

        SceneManager.LoadScene(scene);
    }
}
