using Photon.Pun;
using TMPro;
using UnityEngine;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _winnerName;
    [SerializeField] private TMP_Text _winnerCoins;

    public void StartEndGame(string name, int coins)
    {
        GetComponent<PhotonView>().RPC("SetWinnerName", RpcTarget.AllBuffered, name);
        GetComponent<PhotonView>().RPC("SetWinnerCoins", RpcTarget.AllBuffered, coins);
        GetComponent<PhotonView>().RPC("TryShowEndScreen", RpcTarget.AllBuffered, 1);
    }

    [PunRPC]
    private void SetWinnerName(string name)
    {
        _winnerName.text = name;
    }

    [PunRPC]
    private void SetWinnerCoins(int coins)
    {
        _winnerCoins.text = coins.ToString();
    }

    [PunRPC]
    private void TryShowEndScreen(int alpha)
    {
        gameObject.GetComponent<CanvasGroup>().alpha = alpha;
    }
}
