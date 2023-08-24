using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private GameObject _loadingPanel;

    public static LoadingManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetLoadingEnable(bool isActive)
    {
        _loadingPanel.SetActive(isActive);
    }
}
