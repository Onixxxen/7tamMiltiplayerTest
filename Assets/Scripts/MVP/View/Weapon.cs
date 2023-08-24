using Photon.Pun;
using UnityEngine;

public abstract class Weapon : MonoBehaviourPunCallbacks, IShooting, IHaveOwner
{
    [SerializeField] protected int Damage;
    [SerializeField] protected int Speed;
    [SerializeField] protected float Delay;

    protected float ElapsedTime;
    protected string OwnerName;

    protected string Folder = "PhotonPrefabs";
    protected string File;

    public abstract void TryShoot(Vector2 move);
    public abstract void SetOwnerName(string ownerName);
}

public interface IShooting
{
    public void TryShoot(Vector2 move);
}

public interface IHaveOwner
{
    public void SetOwnerName(string ownerName);
}


