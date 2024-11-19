using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class ChooseNamePresenter : IPresenter
{ 
    public event Action<ICharacter> GoNext;

    private ChooseNameView _view;
    private ICharacter _character;
    private AudioManager _audioManager;
    private string _name;

    [Inject]
    private void Construct(AudioManager audioManager) => _audioManager = audioManager;

    public void Initialize(ChooseNameView view, ICharacter character)
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
            CharacterWithName character = new CharacterWithName(_character);
            character.SetName(_name);
            if (_view.InputDescription.Length > 0)
                character.SetDescription(_view.InputDescription);

            GoNext?.Invoke(character);
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
