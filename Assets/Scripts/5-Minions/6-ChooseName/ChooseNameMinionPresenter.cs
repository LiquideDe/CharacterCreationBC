using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ChooseNameMinionPresenter : IPresenter
{
    public event Action<IMinion> GoNext;

    private ChooseNameView _view;
    private IMinion _character;
    private AudioManager _audioManager;
    private string _name;

    [Inject]
    private void Construct(AudioManager audioManager) => _audioManager = audioManager;

    public void Initialize(ChooseNameView view, IMinion character)
    {
        _view = view;
        _character = character;
        Subscribe();
    }

    private void Subscribe()
    {
        _view.ChooseName += ChooseNameInputed;
        _view.GoNext += GoNextDown;
    }

    private void GoNextDown()
    {
        if (_name.Length > 1)
        {
            _audioManager.PlayDone();

            Unscribe();
            _character.Name = _name;
            if (_view.InputDescription.Length > 0)
                _character.Description = _view.InputDescription;

            GoNext?.Invoke(_character);
            _view.DestroyView();
        }
        else
            _audioManager.PlayWarning();
    }

    private void ChooseNameInputed(string obj)
    {
        _audioManager.PlayClick();
        _name = obj;
    }

    private void Unscribe()
    {
        _view.ChooseName -= ChooseNameInputed;
        _view.GoNext -= GoNextDown;
    }
}
