using System.Collections;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    private RoomManager _roomManager;
    private PlayerView _playerView;
    private PlayerPresenter _playerPresenter;
    private PlayerModel _playerModel;

    private void OnEnable()
    {
        if (_playerPresenter != null)
            _playerPresenter.Enable();
    }

    private void OnDisable()
    {
        _playerPresenter.Disable();
    }

    private void Awake()
    {
        _roomManager = FindObjectOfType<RoomManager>();

        _playerModel = new PlayerModel();

        StartCoroutine(SearchPlayer());
    }

    private IEnumerator SearchPlayer()
    {
        yield return new WaitUntil(HasPlayer);

        for (int i = 0; i < _roomManager.PlayerSpawner.Count; i++)
            if (_roomManager.PlayerSpawner[i].PhotonView.IsMine)
                _playerView = _roomManager.PlayerSpawner[i].PlayerView;

        _playerPresenter = new PlayerPresenter(_playerView, _playerModel);
        _playerPresenter.Enable();
        _playerView.Init();
    }

    private bool HasPlayer()
    {
        return _roomManager.PlayerSpawner.Count > 0;
    }
}
