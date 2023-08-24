using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text _name;

    private Player _player;

    public void Init(Player player)
    {
        _player = player;
        _name.text = _player.NickName;
    }

    public override void OnPlayerLeftRoom(Player player)
    {
        if (_player == player)
            Destroy(gameObject);
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
