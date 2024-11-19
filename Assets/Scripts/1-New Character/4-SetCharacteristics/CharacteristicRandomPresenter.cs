using System;
using System.Collections.Generic;
using System.Diagnostics;
using Zenject;
using UnityEngine;
using Random = System.Random;

public class CharacteristicRandomPresenter : IPresenter
{
    public event Action<ICharacter> ReturnCharacterWithCharacteristics;
    public event Action ReturnToMotivation;
    private ICharacter _character;
    private CharacteristicRandomView _view;
    private AudioManager _audioManager;
    private CharacterWithRace _race;

    [Inject]
    private void Construct(AudioManager audioManager) => _audioManager = audioManager;

    public void Initialize(ICharacter character, CharacteristicRandomView view)
    {
        _view = view;
        Subscribe();
        _character = SearchCharacter(character);
        _race = SearchRace(character);
        SetAmountCharacteristics();
    }

    private void Subscribe()
    {
        _view.CharacteristicsIsReady += ShowButtonDone;
        _view.DonePressed += SetCharacterWithCharacteristics;
        _view.GenerateRandom += GenerateRandom;
        _view.ReturnToMotivation += ReturnToMotivationPressed;
    }    

    private void Unscribe()
    {
        _view.CharacteristicsIsReady -= ShowButtonDone;
        _view.DonePressed -= SetCharacterWithCharacteristics;
        _view.GenerateRandom -= GenerateRandom;
        _view.ReturnToMotivation -= ReturnToMotivationPressed;
    }    

    private CharacterWithMotivations SearchCharacter(ICharacter character)
    {
        if (character is CharacterWithMotivations)
            return (CharacterWithMotivations)character;

        else return SearchCharacter(character.GetCharacter);
    }

    private CharacterWithRace SearchRace(ICharacter character)
    {
        if (character is CharacterWithRace)
            return (CharacterWithRace)character;

        else return SearchRace(character.GetCharacter);
    }

    private void SetAmountCharacteristics()
    {
        _view.WeaponSkill.SetAmount(_character.Characteristics[0].Amount);
        _view.BallisticSkill.SetAmount(_character.Characteristics[1].Amount);
        _view.Strength.SetAmount(_character.Characteristics[2].Amount);
        _view.Toughness.SetAmount(_character.Characteristics[3].Amount);
        _view.Agility.SetAmount(_character.Characteristics[4].Amount);
        _view.Intelligence.SetAmount(_character.Characteristics[5].Amount);
        _view.Perception.SetAmount(_character.Characteristics[6].Amount);
        _view.Willpower.SetAmount(_character.Characteristics[7].Amount);
        _view.Social.SetAmount(_character.Characteristics[8].Amount);
        _view.Wounds.SetAmount(_character.Wounds);
        _view.Infamy.SetAmount(_character.Infamy + 19);
    }

    private void GenerateRandom()
    {
        _audioManager.PlayClick();
        Random rand = new Random();

        List<int> generatedNumbers = new List<int>();

        for(int i = 0; i < _race.AmountCubes; i++)
        {
            int number = rand.Next(3, 11) + rand.Next(3, 11);
            generatedNumbers.Add(number);
        }

        generatedNumbers.Sort();
        generatedNumbers.Reverse();
        generatedNumbers.RemoveRange(9, generatedNumbers.Count - 9);
        _view.Initialize(generatedNumbers);
        _view.Wounds.PlusAmount(rand.Next(1, 6));
        _view.Infamy.PlusAmount(rand.Next(1, 6));
    }

    private void ShowButtonDone() => _view.ShowButtonDone();

    private void ReturnToMotivationPressed()
    {
        _audioManager.PlayCancel();
        Unscribe();
        _view.DestroyView();
        ReturnToMotivation?.Invoke();
    }

    private void SetCharacterWithCharacteristics()
    {
        _audioManager.PlayDone();
        CharacterWithCharacteristics character = new CharacterWithCharacteristics(_character);
        List<int> characteristics = new List<int>() {_view.WeaponSkill.Amount, _view.BallisticSkill.Amount, _view.Strength.Amount, _view.Toughness.Amount
        , _view.Agility.Amount, _view.Intelligence.Amount, _view.Perception.Amount, _view.Willpower.Amount, _view.Social.Amount};

        character.SetCharacteristics(characteristics);
        character.SetWounds(_view.Wounds.Amount);
        character.SetInfamy(_view.Infamy.Amount);

        Unscribe();
        _view.DestroyView();
        ReturnCharacterWithCharacteristics?.Invoke(character);
    }
}
