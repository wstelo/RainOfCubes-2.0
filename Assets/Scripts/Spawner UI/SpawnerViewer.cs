using UnityEngine;
using TMPro;

public abstract class SpawnerViewer <T> : MonoBehaviour where T : Item
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private TMP_Text _header;
    [SerializeField] private InfoPanel _totalPanel;
    [SerializeField] private InfoPanel _createdPanel;
    [SerializeField] private InfoPanel _activePanel;

    private void Start()
    {
        _header.text = _spawner.GetType().Name;
    }

    private void OnEnable()
    {
        _spawner.TotalCountChanged += RefreshTotalCounter;
        _spawner.CreatedCountChanged += RefreshCreatedCounter;
        _spawner.ActiveCountChanged += RefreshActiveCounter;
    }

    private void OnDisable()
    {
        _spawner.TotalCountChanged -= RefreshTotalCounter;
        _spawner.CreatedCountChanged -= RefreshCreatedCounter;
        _spawner.ActiveCountChanged -= RefreshActiveCounter;
    }
    
    private void RefreshTotalCounter(int count)
    {
        _totalPanel.RefreshCount(count);
    }

    private void RefreshCreatedCounter(int count)
    {
        _createdPanel.RefreshCount(count);
    }

    private void RefreshActiveCounter(int count)
    {
        _activePanel.RefreshCount(count);
    }
}
