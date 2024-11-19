using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TakePicturesPresenter : IPresenter
{
    public event Action<ICharacter> WorkIsFinished; 
    private FirstCharacterSheet _first;
    private SecondCharacterSheet _second;
    private ThirdCharacterSheet _third;
    private FourthCharacterSheet _fourth;
    private ICharacter _character;

    public void Initialize(FirstCharacterSheet first, SecondCharacterSheet second, ThirdCharacterSheet third,FourthCharacterSheet fourth, ICharacter character)
    {
        _first = first;
        _second = second;
        _third = third;
        _fourth = fourth;
        _character = character;
        _first.WorkIsFinished += FirstIsFinished;
        _second.WorkIsFinished += SecondIsFinished;
        _third.WorkIsFinished += ThirdIsFinished;
        _fourth.WorkIsFinished += FourthIsFinished;
        _first.gameObject.SetActive(true);
        _first.Initialize(_character);
    }

    private void FirstIsFinished()
    {
        _second.gameObject.SetActive(true);
        _second.Initialize(_character);
    }

    private void SecondIsFinished()
    {
        _third.gameObject.SetActive(true);
        _third.Initialize(_character);
    }

    private void ThirdIsFinished()
    {
        _fourth.gameObject.SetActive(true);
        _fourth.Initialize(_character);
    }

    private void FourthIsFinished() 
    {
        WorkIsFinished?.Invoke(_character);
    }
}
