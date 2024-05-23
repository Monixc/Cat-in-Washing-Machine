using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Diagnostics;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";

    public Text connectionInfoText;
    public Button joinButton;
    public Button controlButton;
    public GameObject controlPanel;

    public void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

       
        joinButton.interactable = false;

        connectionInfoText.text = "������ ������ ���� ��...";
    }

    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;
        connectionInfoText.text = "�¶��� : ������ ������ �����";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;
        connectionInfoText.text = "�������� : ������ ������ ������� ����\n���� ��õ� ��...";

        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {
        joinButton.interactable = false;

        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "�뿡 ����...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text = "�������� : ������ ������ ������� ����\n���� ��õ� ��...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "�� ���� ����. ���ο� �� ����...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "�� ���� ����";
        PhotonNetwork.LoadLevel("gamemain");
    }

    public void GoToControlScene()
    {
        if (controlPanel != null)
        {
            controlPanel.SetActive(true);
        }
    }

    public void CloseControlPanel()
    {
        if (controlPanel != null)
        {
            controlPanel.SetActive(false);
        }
    }
}
