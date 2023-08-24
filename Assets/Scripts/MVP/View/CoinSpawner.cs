using Photon.Pun;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private int _poolCount = 5;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private bool _autoExpand = false;

    private string _folder = "PhotonPrefabs";
    private string _file = "Coin";

    private float _elapsedTime;

    private ObjectPool _pool;

    private void Start()
    {
        _pool = new ObjectPool(_file, _folder, _poolCount);
        _pool.AutoExpand = _autoExpand;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _secondsBetweenSpawn)
            if (_pool.TryGetObject(out GameObject coin))
            {
                _elapsedTime = 0;
                ActivateCoin(coin);
            }
    }

    private void ActivateCoin(GameObject coin)
    {
        Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));

        CoinView coinView = coin.GetComponent<CoinView>();

        coinView.transform.position = randomPositionOnScreen;
        coinView.GetComponent<PhotonView>().RPC("ChangeCoinVisible", RpcTarget.AllBuffered, true);
    }
}
