using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerList : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _playerContainer;
    [SerializeField] private PlayerListItem _playerPrefub;
    [SerializeField] private GameObject _startFightButton;

    public Transform PlayerContainer => _playerContainer;
    public PlayerListItem PlayerPrefub => _playerPrefub;

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(_playerPrefub, _playerContainer).Init(newPlayer);        
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _startFightButton.SetActive(PhotonNetwork.IsMasterClient);
    }
}
