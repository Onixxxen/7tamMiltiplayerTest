using Photon.Realtime;
using TMPro;
using UnityEngine;

public class ServerListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _roomName;

    private RoomInfo _roomInfo;
    private ServerList _serverList;

    public void Init(RoomInfo info, ServerList serverList)
    {
        _roomInfo = info;
        _roomName.text = _roomInfo.Name;
        _serverList = serverList;
    }

    public void OnClick()
    {
        _serverList.SetRoomInfo(_roomInfo);
        _serverList.InteractionServer();
    }
}
