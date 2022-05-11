using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HostLobbyData : MonoBehaviour
{
    public string lobbyName;
    public bool hasProximityChat;
    public float maxPlayers;
    public string lobbyType;

    public TMP_InputField nameInput;
    public Toggle proximityChatCheckbox;
    public Slider maxPlayersSlider;
    public Toggle publicL;
    public Toggle freindsL;
    public Toggle codeL;
    public void SetLobbyData()
    {
        lobbyName = nameInput.text;
        hasProximityChat = proximityChatCheckbox.isOn;
        maxPlayers = maxPlayersSlider.value;
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

    }
}
