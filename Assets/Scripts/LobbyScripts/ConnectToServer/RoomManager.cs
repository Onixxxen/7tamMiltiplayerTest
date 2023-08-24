using Photon.Pun;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private const string _folder = "PhotonPrefabs";
    private const string _file = "PlayerManager";

    public List<PlayerSpawner> PlayerSpawner = new List<PlayerSpawner>();

    public static RoomManager Instance;

    private void Start()
    {
        if (Instance)
        {
            Destroy(gameObject);
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        GameObject playerManager;

        if (scene.buildIndex == 1)
        {
            playerManager = PhotonNetwork.Instantiate(Path.Combine(_folder, _file), Vector2.zero, Quaternion.identity);
            PlayerSpawner.Add(playerManager.GetComponent<PlayerSpawner>());
        }

        LoadingManager.Instance.SetLoadingEnable(false);
    }
}
