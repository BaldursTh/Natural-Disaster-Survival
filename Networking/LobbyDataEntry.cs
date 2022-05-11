using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Steamworks;
public class LobbyDataEntry : MonoBehaviour
{
    public CSteamID lobbyID;
    public string lobbyName;
    public string maxPlayers;
    public int currentPlayers;
    public string hasProximityChat;



    public TMP_Text lobbyNameText;
    public TMP_Text maxPlayerNumber;
    public Toggle proximityChat;


    public void SetLobbyData()
    {
        if (lobbyName == "")
        {
            lobbyNameText.text = ":D";
        }
        else
        {
            lobbyNameText.text = lobbyName;
        }
        maxPlayerNumber.text = currentPlayers + "/" + maxPlayers;
        proximityChat.isOn = hasProximityChat == "true";
        
    }

    public void JoinLobby()
    {
        SteamJoin.Instance.JoinLobby(lobbyID);
    }




}
