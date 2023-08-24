using UnityEngine;
using Photon.Pun;

public class ServerWait : MonoBehaviourPunCallbacks
{
    [SerializeField] private ServerList _serverList;

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        LoadingManager.Instance.SetLoadingEnable(true);
    }

    public void StartFight()
    {
        PhotonNetwork.LoadLevel(1);
        LoadingManager.Instance.SetLoadingEnable(true);
    }

    public override void OnLeftRoom()
    {
        _serverList.gameObject.SetActive(true);
        gameObject.SetActive(false);
        LoadingManager.Instance.SetLoadingEnable(false);
    }
}
