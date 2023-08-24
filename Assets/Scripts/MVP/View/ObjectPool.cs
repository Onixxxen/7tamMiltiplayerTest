using Photon.Pun;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviourPunCallbacks
{
    [SerializeField] public bool AutoExpand { get; set; }

    private List<GameObject> _pool;

    private string _folder;
    private string _file;

    public ObjectPool(string file, string folder, int count)
    {
        _folder = folder;
        _file = file;

        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _pool = new List<GameObject>();

        for (int i = 0; i < count; i++)
            CreateObject();
    }

    private GameObject CreateObject(bool isActiveByDefault = false)
    {
        Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(UnityEngine.Random.value, UnityEngine.Random.value));

        var createdObject = PhotonNetwork.Instantiate(Path.Combine(_folder, _file), randomPositionOnScreen, Quaternion.identity);

        if (createdObject.TryGetComponent(out CoinView coin))
            coin.GetComponent<PhotonView>().RPC("ChangeCoinVisible", RpcTarget.AllBuffered, isActiveByDefault);
        else
            throw new InvalidOperationException();

        _pool.Add(createdObject);
        return createdObject;
    }

    private bool HasFreeElement(out GameObject element)
    {
        foreach (var mono in _pool)
        {
            if (!mono.activeInHierarchy)
            {
                element = mono;

                if (element.TryGetComponent(out CoinView coin))
                    coin.GetComponent<PhotonView>().RPC("ChangeCoinVisible", RpcTarget.AllBuffered, true);
                else
                    throw new InvalidOperationException();

                return true;
            }
        }

        element = null;
        return false;
    }

    public GameObject GetFreeElement()
    {
        if(HasFreeElement(out var element))
            return element;

        if (AutoExpand)
            return CreateObject(true);

        return null;
    }

    public bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }
}
