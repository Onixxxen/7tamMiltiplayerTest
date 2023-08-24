using Photon.Pun;
using UnityEngine;

public class CoinView : MonoBehaviour
{
    [PunRPC]
    private void ChangeCoinVisible(bool coinIsActive)
    {
        gameObject.SetActive(coinIsActive);
    }
}
