using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Mirror;
using Steamworks.NET;
using UnityEngine.SceneManagement;

public class SteamLobby : MonoBehaviour
{
    private StartSettings settings;

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;

    private const string HostAddressKey = "wowzers";

    public NetworkManagerTest networkManager;

    public WaitingScreen waitingScreen;
    

    private void Start()
    {
        networkManager = GetComponent<NetworkManagerTest>();
        
        settings = GameObject.FindGameObjectWithTag("ServerSettings").GetComponent<StartSettings>();
        
        if (!SteamManager.Initialized) { return; }

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
        networkManager.maxConnections = settings.maxPlayers;

        if (settings.isHost)
        {
            HostLobby();
        }
        else if (!settings.isHost)
        {
            
            networkManager.networkAddress = settings.ipAddress;
            networkManager.StartClient();
        }
    }
    public void HostLobby()
    {
        if (settings.lobbyType == "public")
        {
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, networkManager.maxConnections);
        }
        else if (settings.lobbyType == "friends")
        {
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, networkManager.maxConnections);
        }
        else if (settings.lobbyType == "code")
        {
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, networkManager.maxConnections);
            isCodeOnly = true;
        }

    }
    bool isCodeOnly;

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if(callback.m_eResult != EResult.k_EResultOK)
        {
            
            return;
        }
        
        networkManager.StartHost();
        string _code = CreatRandomCode(6);
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "code", _code);
        settings.code = _code;
        if (isCodeOnly)
        {
            SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "isCodeOnly", "true");
            isCodeOnly = false;
        }
        else
        {
            SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "isCodeOnly", "false");
            isCodeOnly = false;
        }

        if (settings.hasProximityChat)
        {
            SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "proximityChat", "true");
        }
        else
        {
            SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "proximityChat", "false");
        }
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name", settings.lobbyName);
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "maxPlayers", settings.maxPlayers.ToString());
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "currentPlayers", "dunno");
        waitingScreen.code = _code;
        waitingScreen.UpdateWaitingScreen();
    }

    const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";
    public string CreatRandomCode(int length)
    {
        string code = "";
        for (int i = 0; i < length; i++)
        {
            
            code += glyphs[Random.Range(0, glyphs.Length)];
            
        }
        return code;
    }
    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }
    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        if (NetworkServer.active) { return; }

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);
        settings.code = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "code");
        networkManager.networkAddress = hostAddress;
        networkManager.StartClient();
        _callback = callback;
       
    }
    LobbyEnter_t _callback;
    public void LeaveLobby()
    {
        SteamMatchmaking.LeaveLobby(new CSteamID(_callback.m_ulSteamIDLobby));
        networkManager.StopClient();
        networkManager.StopHost();
        Destroy(networkManager.gameObject);
        Destroy(settings.gameObject);
        SceneManager.LoadScene("NetworkTesting");
        
    }

}
