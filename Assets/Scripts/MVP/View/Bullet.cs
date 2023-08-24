using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _damage;
    private int _speed;
    private Vector2 _move;
    private string _ownerName;

    private void Update()
    {
        transform.Translate(_move * _speed * Time.deltaTime, Space.World);
    }

    public void Init(int speed, int damage, Vector2 move, string ownerName)
    {
        _speed = speed;
        _damage = damage;
        _move = move;
        _ownerName = ownerName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerView player))
        {
            if (player.Player.NickName == _ownerName)
            {
                player.TryGotShoot(_damage);
                GetComponent<PhotonView>().RPC("DeleteBullet", RpcTarget.AllBuffered, false);
            }
        }
        else if (!collision.TryGetComponent(out CoinView coin))
        {
            GetComponent<PhotonView>().RPC("DeleteBullet", RpcTarget.AllBuffered, false);
        }
    }

    [PunRPC]
    private void DeleteBullet(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
