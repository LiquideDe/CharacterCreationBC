using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MotivationsView : ViewWithButtonsDoneAndCancel
{
    [SerializeField] private List<Toggle> _prides = new List<Toggle>();
    [SerializeField] private List<Toggle> _disgraces = new List<Toggle>();
    [SerializeField] private List<Toggle> _motivations = new List<Toggle>();
    [SerializeField] private GameObject _buttonDoneGameObject;

    public event Action ToggleChoose;

    public List<Toggle> Prides => _prides;

    public List<Toggle> Disgrace => _disgraces; 
    public List<Toggle> Motivations => _motivations;

    private void OnDisable()
    {
        RemoveListenerFromToggle(_prides);
        RemoveListenerFromToggle(_disgraces);
        RemoveListenerFromToggle(_motivations);
    }

    public void Initialize()
    {
        AddListenerToToggle(_prides);
        AddListenerToToggle(_disgraces);
        AddListenerToToggle(_motivations);
    }

    private void AddListenerToToggle(List<Toggle> toggles)
    {
        foreach (Toggle toggle in toggles)        
            toggle.onValueChanged.AddListener(SomeToggleChoosed);        
    }

    private void RemoveListenerFromToggle(List<Toggle> toggles)
    {
        foreach (Toggle toggle in toggles)
            toggle.onValueChanged.RemoveAllListeners();
    }

    public void ShowButtonDone() => _buttonDoneGameObject.SetActive(true);

    private void SomeToggleChoosed(bool arg0) => ToggleChoose?.Invoke();

    
}
