using UnityEngine;

public class PlayerPresenter
{
    private PlayerView _playerView;
    private PlayerModel _playerModel;

    public PlayerPresenter(PlayerView playerView, PlayerModel playerModel)
    {
        _playerView = playerView;
        _playerModel = playerModel;
    }

    public void Enable()
    {
        _playerView.Joined += OnInitialize;
        _playerView.EnterInCoin += OnAddCoin;
        //_playerView.GotShoot += OnGotShoot;

        _playerModel.CoinsChanged += OnGetCoin;
        _playerModel.ReturnData += OnGetData;
        _playerModel.HealthChanged += OnHealthChanged;
    }

    public void Disable()
    {
        _playerView.Joined -= OnInitialize;
        _playerView.EnterInCoin -= OnAddCoin;
        //_playerView.GotShoot -= OnGotShoot;

        _playerModel.CoinsChanged -= OnGetCoin;
        _playerModel.ReturnData -= OnGetData;
        _playerModel.HealthChanged -= OnHealthChanged;
    }

    private void OnInitialize()
    {
        _playerModel.GetData();
    }

    private void OnGetData(int count, int health)
    {
        _playerView.SetCoinCount(count);
        _playerView.ChangeHealth(health);
    }

    private void OnAddCoin(CoinView coin)
    {
        _playerModel.AddCoin(coin);
    }

    private void OnGetCoin(CoinView coin, int count)
    {
        _playerView.ChangeCoinView(coin, count);
    }

    private void OnGotShoot(int damage)
    {
        _playerModel.TakeDamage(damage);
    }

    private void OnHealthChanged(int health)
    {
        _playerView.ChangeHealth(health);
    }
}
