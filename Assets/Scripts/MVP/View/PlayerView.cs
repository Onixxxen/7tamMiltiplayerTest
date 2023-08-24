using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text _coinCountText;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Weapon _weapon;

    private Player _player;
    private PhotonTeamsManager _teamsManager;
    private PhotonView _photonView;
    private EndGameScreen _endGameScreen;

    public event Action Joined;
    public event Action<CoinView> EnterInCoin;
    //public event Action<int> GotShoot;

    public Slider HealthSlider => _healthSlider;
    public Player Player => _player;

    public void Init()
    {
        Joined?.Invoke();
    }

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
            if (player.IsLocal)
            {
                _player = player;
                _weapon.SetOwnerName(_player.NickName);
            }

        _teamsManager = FindObjectOfType<PhotonTeamsManager>();
        _endGameScreen = FindObjectOfType<EndGameScreen>();
    }

    private void Update()
    {
        TryEndGame();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CoinView coin))
            EnterInCoin?.Invoke(coin);
    }  

    public void TryGotShoot(int damage)
    {
        int newHealth = (int)(_healthSlider.value - damage);
        ChangeHealth(newHealth);
    }

    public void SetCoinCount(int count)
    {
        GetComponent<PhotonView>().RPC("ChangeCoinCount", RpcTarget.AllBuffered, count);
    }

    public void ChangeCoinView(CoinView coin, int count)
    {
        coin.GetComponent<PhotonView>().RPC("ChangeCoinVisible", RpcTarget.AllBuffered, false);
        SetCoinCount(count);
    }

    public void ChangeHealth(int newCount)
    {
        GetComponent<PhotonView>().RPC("ChangeHealthValue", RpcTarget.AllBuffered, newCount);
    }

    public void TryDead()
    {
        if (_photonView.IsMine)
            if (_healthSlider.value <= 0)
                _player.SwitchTeam("Dead");
    }

    public void TryEndGame()
    {
        byte teamFightPlayers = (byte)_teamsManager.GetTeamMembersCount(3);

        if (teamFightPlayers == 1)
            if (_teamsManager.TryGetTeamMembers(3, out Player[] winner))
                for (int i = 0; i < winner.Length; i++)
                    if (winner[i].NickName == _player.NickName)
                        _endGameScreen.StartEndGame(_player.NickName, _player.GetScore());
    }

    [PunRPC]
    private void ChangeCoinCount(int count)
    {
        _coinCountText.text = $"{count}";
        _player.SetScore(count);
    }

    [PunRPC]
    private void ChangeHealthValue(int value)
    {
        _healthSlider.value = value;
        TryDead();
    }
}
