using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChooseNameView : CanDestroyView
{
    [SerializeField] TMP_InputField _inputName, _inputDescription;
    [SerializeField] Button _buttonNext;

    public string InputDescription => _inputDescription.text;
    public event Action GenerateName;
    public event Action GoNext;
    public event Action<string> ChooseSex;
    public event Action<string> ChooseName;

    private void OnEnable()
    {
        _buttonNext.onClick.AddListener(GoNextPressed);
        _inputName.onDeselect.AddListener(ChooseNameInputed);
    }

    private void OnDisable()
    {
        _buttonNext.onClick.RemoveAllListeners();
        _inputName.onDeselect.RemoveAllListeners();
    }

    private void GoNextPressed() => GoNext?.Invoke();

    private void ChooseNameInputed(string text) => ChooseName?.Invoke(text);

}
