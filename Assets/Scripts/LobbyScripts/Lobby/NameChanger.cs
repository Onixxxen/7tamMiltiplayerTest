using Photon.Pun;
using TMPro;
using UnityEngine;

public class NameChanger : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text _currentName;
    [SerializeField] private TMP_InputField _serverInputField;

    private void Start()
    {
        _currentName.text = PhotonNetwork.NickName;
    }

    public void ChangeName()
    {
        PhotonNetwork.NickName = _serverInputField.text;
        _currentName.text = PhotonNetwork.NickName;
    }
}
