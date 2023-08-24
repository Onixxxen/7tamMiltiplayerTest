using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;
using UnityEngine;

public abstract class ServerConnect : MonoBehaviourPunCallbacks, IConnectsToServer
{
    [SerializeField] protected GameObject ServerWait;
    [SerializeField] protected TMP_Text ErrorCreationText;
    [SerializeField] protected TMP_Text RoomNameText;
    [SerializeField] protected PlayerList PlayerList;
    [SerializeField] protected GameObject StartFightButton;

    public virtual void InteractionServer()
    {
    }

    public virtual void InitializePlayerList()
    {
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < PlayerList.PlayerContainer.childCount; i++)
            Destroy(PlayerList.PlayerContainer.GetChild(i).gameObject);

        for (int i = 0; i < players.Length; i++)
            Instantiate(PlayerList.PlayerPrefub, PlayerList.PlayerContainer).Init(players[i]);    

        StartFightButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnJoinedRoom()
    {
        RoomNameText.text = PhotonNetwork.CurrentRoom.Name;
        InitializePlayerList();

        ServerWait.SetActive(true);
        gameObject.SetActive(false);
        LoadingManager.Instance.SetLoadingEnable(false);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ErrorCreationText.text = $"Error: {message}";
        StartCoroutine(ShowErrorText());
    }

    public virtual IEnumerator ShowErrorText()
    {
        ErrorCreationText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        ErrorCreationText.gameObject.SetActive(false);
    }
}

public interface IConnectsToServer
{
    public abstract void InteractionServer();
    public void InitializePlayerList();
    public IEnumerator ShowErrorText();
}
