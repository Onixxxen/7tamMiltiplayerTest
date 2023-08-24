using Photon.Pun;
using System.IO;
using UnityEngine;

public class Pistol : Weapon
{
    private void Start()
    {
        File = "Bullet";
    }

    public override void TryShoot(Vector2 move)
    {
        ElapsedTime += Time.deltaTime;

        if (ElapsedTime >= Delay)
        {
            var bullet = PhotonNetwork.Instantiate(Path.Combine(Folder, File), gameObject.transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().Init(Speed, Damage, move, OwnerName);
            ElapsedTime = 0;
        }            
    }

    public override void SetOwnerName(string ownerName)
    {
        OwnerName = ownerName;
    }
}
