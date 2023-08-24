using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;

public class ServerList : ServerConnect
{
    [SerializeField] private Transform _serverContainer;
    [SerializeField] private ServerListItem _serverPrefub;

    private RoomInfo _roomInfo;

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        _roomInfo  = roomInfo;
    }

    public override void InteractionServer()
    {        
        PhotonNetwork.JoinRoom(_roomInfo.Name);
        LoadingManager.Instance.SetLoadingEnable(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        LoadingManager.Instance.SetLoadingEnable(true);

        for (int i = 0; i < _serverContainer.childCount;)
            Destroy(_serverContainer.GetChild(i).gameObject);

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;

            Instantiate(_serverPrefub, _serverContainer).Init(roomList[i], this);
        }

        LoadingManager.Instance.SetLoadingEnable(false);
    }
}
