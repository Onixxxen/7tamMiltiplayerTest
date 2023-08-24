using Photon.Pun;
using System.IO;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    private PhotonView _photonView;
    private PlayerView _playerView;

    private const string _folder = "PhotonPrefabs";
    private const string _file = "Player";

    public PhotonView PhotonView => _photonView;
    public PlayerView PlayerView => _playerView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();

        if (_photonView.IsMine)
            SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));

        var player = PhotonNetwork.Instantiate(Path.Combine(_folder, _file), randomPositionOnScreen, Quaternion.identity);
        _playerView = player.GetComponent<PlayerView>();       
    }
}
