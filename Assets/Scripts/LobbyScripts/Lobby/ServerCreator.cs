using UnityEngine;
using Photon.Pun;
using TMPro;

public class ServerCreator : ServerConnect
{
    [SerializeField] private TMP_InputField _serverInputField;

    public override void InteractionServer()
    {
        if (string.IsNullOrEmpty(_serverInputField.text))
            return;

        PhotonNetwork.CreateRoom(_serverInputField.text);
        LoadingManager.Instance.SetLoadingEnable(true);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ErrorCreationText.text = $"Error: {message}";
        StartCoroutine(ShowErrorText());
    }
}
