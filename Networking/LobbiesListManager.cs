using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.UI;
public class LobbiesListManager : MonoBehaviour
{
    public static LobbiesListManager instance;


    
    public GameObject lobbyDataItemPrefab;
    public GameObject lobbyListContent;

    

    public List<GameObject> listOfLobbies = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void GetListOfLobbies()
    {
        SteamJoin.Instance.GetLobbiesList();
    }


    public void DisplayLobbies(List<CSteamID> lobbysIDs, LobbyDataUpdate_t result)
    {
        for(int i = 0; i < lobbysIDs.Count; i++)
        {
            if(lobbysIDs[i].m_SteamID == result.m_ulSteamIDLobby)
            {
                GameObject createdItem = Instantiate(lobbyDataItemPrefab);

                LobbyDataEntry lobbyDataEntry = createdItem.GetComponent<LobbyDataEntry>();

                lobbyDataEntry.lobbyID = (CSteamID)lobbysIDs[i].m_SteamID;

                lobbyDataEntry.lobbyName =
                    SteamMatchmaking.GetLobbyData((CSteamID)lobbysIDs[i].m_SteamID, "name");
                lobbyDataEntry.hasProximityChat = SteamMatchmaking.GetLobbyData((CSteamID)lobbysIDs[i].m_SteamID, "proximityChat");
                lobbyDataEntry.maxPlayers = SteamMatchmaking.GetLobbyData((CSteamID)lobbysIDs[i].m_SteamID, "maxPlayers");
                lobbyDataEntry.currentPlayers = SteamMatchmaking.GetNumLobbyMembers((CSteamID)lobbysIDs[i].m_SteamID);

                lobbyDataEntry.SetLobbyData();

                createdItem.transform.SetParent(lobbyListContent.transform);

                
                createdItem.transform.localScale = Vector3.one;
                

                listOfLobbies.Add(createdItem);
            }
        }
    }


    public void DestroyLobbies()
    {
        foreach(GameObject lobbyItem in listOfLobbies)
        {
            Destroy(lobbyItem);
        }
        listOfLobbies.Clear();
    }
}
