using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadPanel : MonoBehaviour
{
    public event Action SaveButtonClicked;
    public event Action LoadButtonClicked;
    
    [SerializeField] private Button _save;
    [SerializeField] private Button _load;

    private void OnEnable()
    {
        _save.onClick.AddListener(() => SaveButtonClicked?.Invoke());
        _load.onClick.AddListener(() => LoadButtonClicked?.Invoke());
    }
}
