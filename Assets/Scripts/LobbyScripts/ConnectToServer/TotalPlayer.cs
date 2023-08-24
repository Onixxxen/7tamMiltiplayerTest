using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TotalPlayer : MonoBehaviourPunCallbacks
{
    [SerializeField] private PhotonTeamsManager _teamsManager;

    public void JoinToTeam()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
            p.JoinTeam("Fight");
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1)
            JoinToTeam();
    }
}
