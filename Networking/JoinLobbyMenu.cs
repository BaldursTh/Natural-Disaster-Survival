using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private StartSettings pack = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;
    [SerializeField] private TMP_InputField ipAddressInputField = null;
    [SerializeField] private Button joinButton = null;

    private void OnEnable()
    {
        NetworkManagerTest.OnClientConnected += HandleClientConnected;
        NetworkManagerTest.OnClientDisconnected += HandleClientDisconnected;
    }
    private void OnDisable()
    {
        NetworkManagerTest.OnClientConnected -= HandleClientConnected;
        NetworkManagerTest.OnClientDisconnected -= HandleClientDisconnected;
    }

    public void JoinLobby()
    {
        string _ipAddress = ipAddressInputField.text;

        pack.ipAddress = _ipAddress;
        pack.isHost = false;
        print($"{_ipAddress}");
        pack.JoinGame();

        joinButton.interactable = false;
    }
    private void HandleClientConnected()
    {
        joinButton.interactable = true;

        gameObject.SetActive(false);
        landingPagePanel.SetActive(false);
    }
    private void HandleClientDisconnected()
    {
        joinButton.interactable = true;
    }
}
