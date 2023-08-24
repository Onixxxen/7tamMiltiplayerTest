using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        Debug.Log("�������������� � ������ �������");
        PhotonNetwork.ConnectUsingSettings();
        LoadingManager.Instance.SetLoadingEnable(true);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("�������������� � ������ �������");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("�������������� � �����");
        PhotonNetwork.NickName = $"Player{Random.Range(0, 1000)}";
        LoadingManager.Instance.SetLoadingEnable(false);
    }
}
