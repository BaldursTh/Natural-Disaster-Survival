using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class StartSettings : MonoBehaviour
{
    public bool isHost;
    public string ipAddress;

    public string lobbyName;
    public bool hasProximityChat;
    public int maxPlayers;
    public string lobbyType;
    public string code;

    public TMP_InputField nameInput;
    public Toggle proximityChatCheckbox;
    public Slider maxPlayersSlider;
    public Toggle publicL;
    public Toggle freindsL;
    public Toggle codeL;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
       
    }
    public void JoinGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    

    public void SetLobbyData()
    {
        lobbyName = nameInput.text;
        hasProximityChat = proximityChatCheckbox.isOn;
        maxPlayers = (int)maxPlayersSlider.value;
        if (publicL.isOn)
        {
            lobbyType = "public";
        }
        else if (freindsL.isOn)
        {
            lobbyType = "friends";
        }
        else
        {
            lobbyType = "code";
        }
        JoinGame();
    }
}
