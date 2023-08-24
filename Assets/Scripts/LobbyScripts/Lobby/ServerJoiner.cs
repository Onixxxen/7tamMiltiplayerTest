using UnityEngine;
using Photon.Pun;
using TMPro;

public class ServerJoiner : ServerConnect
{
    [SerializeField] private TMP_InputField _serverInputField;

    public override void InteractionServer()
    {
        if (string.IsNullOrEmpty(_serverInputField.text))
            return;

        PhotonNetwork.JoinRoom(_serverInputField.text);
        LoadingManager.Instance.SetLoadingEnable(true);
    }
}
