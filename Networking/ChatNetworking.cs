using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class ChatNetworking : NetworkBehaviour
{
    [SerializeField] private GameObject chatUI = null;
    [SerializeField] TMPro.TMP_Text chatText = null;
    [SerializeField] TMPro.TMP_InputField inputField = null;

    private static event Action<string> OnMessage;

    public override void OnStartAuthority()
    {
        chatUI.SetActive(true);
        OnMessage += HandleNewMessage;
    }

    [ClientCallback]

    private void OnDestroy()
    {
        if (!hasAuthority) { return; }

        OnMessage -= HandleNewMessage;
    }

    private void HandleNewMessage(string _message)
    {
        chatText.text += _message;
    }

    [Client]

    public void Send(string _message)
    {
        
        if (!Input.GetKeyDown(KeyCode.Return)) { return; }
        
        if (!string.IsNullOrWhiteSpace(_message)) { return; }
        

        CmdSendMessage(inputField.text);

        inputField.text = string.Empty;
    }
    [Command]
    private void CmdSendMessage(string _message)
    {
        RpcHandleMessage($"[{connectionToClient.connectionId}]: {_message}");
    }
    [ClientRpc]
    private void RpcHandleMessage(string _message)
    {
        print(_message);
        OnMessage?.Invoke($"\n{_message}");
        
    }

}
