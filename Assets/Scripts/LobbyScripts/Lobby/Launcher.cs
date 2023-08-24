using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        Debug.Log("Присоединяемся к мастер серверу");
        PhotonNetwork.ConnectUsingSettings();
        LoadingManager.Instance.SetLoadingEnable(true);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Присоединились к мастер серверу");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Присоединились к лобби");
        PhotonNetwork.NickName = $"Player{Random.Range(0, 1000)}";
        LoadingManager.Instance.SetLoadingEnable(false);
    }
}
