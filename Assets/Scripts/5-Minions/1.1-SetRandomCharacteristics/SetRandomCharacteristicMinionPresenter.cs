using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Zenject;
using Random = System.Random;

public class SetRandomCharacteristicMinionPresenter : IPresenter
{
    public event Action<IMinion> ReturnCharacterWithCharacteristics;
    private IMinion _minion;
    private CharacteristicRandomView _view;
    private AudioManager _audioManager;
    private TypeMinion _typeMinion;

    [Inject]
    private void Construct(AudioManager audioManager) => _audioManager = audioManager;

    public void Initialize(IMinion minion, TypeMinion typeMinion, CharacteristicRandomView view)
    {
        _view = view;
        Subscribe();
        _typeMinion = typeMinion;
        _minion = minion;
        SetAmountCharacteristics();
    }

    private void Subscribe()
    {
        _view.CharacteristicsIsReady += ShowButtonDone;
        _view.DonePressed += SetCharacterWithCharacteristics;
        _view.GenerateRandom += GenerateRandom;
        _view.ToughnessIsChanged += ToughnessIsChanged;
    }

    private void Unscribe()
    {
        _view.CharacteristicsIsReady -= ShowButtonDone;
        _view.DonePressed -= SetCharacterWithCharacteristics;
        _view.GenerateRandom -= GenerateRandom;
        _view.ToughnessIsChanged -= ToughnessIsChanged;
    }    

    private void SetAmountCharacteristics()
    {
        _view.WeaponSkill.SetAmount(_typeMinion.CharacteristicPoints);
        _view.BallisticSkill.SetAmount(_typeMinion.CharacteristicPoints);
        _view.Strength.SetAmount(_typeMinion.CharacteristicPoints);
        _view.Toughness.SetAmount(_typeMinion.CharacteristicPoints);
        _view.Agility.SetAmount(_typeMinion.CharacteristicPoints);
        _view.Intelligence.SetAmount(_typeMinion.CharacteristicPoints);
        _view.Perception.SetAmount(_typeMinion.CharacteristicPoints);
        _view.Willpower.SetAmount(_typeMinion.CharacteristicPoints);
        _view.Social.SetAmount(_typeMinion.CharacteristicPoints);
        _view.Wounds.SetAmount(_minion.Characteristics[GameStat.CharacteristicToInt["Выносливость"]].Amount/10 * 2 + 2);
    }

    private void GenerateRandom()
    {
        _audioManager.PlayClick();
        Random rand = new Random();

        List<int> generatedNumbers = new List<int>();

        for (int i = 0; i < _typeMinion.MaxCharacteristic; i++)
        {
            int number = rand.Next(3, 11) + rand.Next(3, 11);
            generatedNumbers.Add(number);
        }

        generatedNumbers.Sort();
        generatedNumbers.Reverse();
        generatedNumbers.RemoveRange(9, generatedNumbers.Count - 9);
        _view.Initialize(generatedNumbers);
    }

    private void ShowButtonDone() => _view.ShowButtonDone();

    private void SetCharacterWithCharacteristics()
    {
        _audioManager.PlayDone();
        MinionCharacter minionCharacter = (MinionCharacter)_minion;

        minionCharacter.Characteristics[GameStat.CharacteristicToInt["Навык Рукопашной"]].Amount = _view.WeaponSkill.Amount;
        minionCharacter.Characteristics[GameStat.CharacteristicToInt["Навык Стрельбы"]].Amount = _view.BallisticSkill.Amount;
        minionCharacter.Characteristics[GameStat.CharacteristicToInt["Сила"]].Amount = _view.Strength.Amount;
        minionCharacter.Characteristics[GameStat.CharacteristicToInt["Выносливость"]].Amount = _view.Toughness.Amount;
        minionCharacter.Characteristics[GameStat.CharacteristicToInt["Ловкость"]].Amount = _view.Agility.Amount;
        minionCharacter.Characteristics[GameStat.CharacteristicToInt["Интеллект"]].Amount = _view.Intelligence.Amount;
        minionCharacter.Characteristics[GameStat.CharacteristicToInt["Восприятие"]].Amount = _view.Perception.Amount;
        minionCharacter.Characteristics[GameStat.CharacteristicToInt["Сила Воли"]].Amount = _view.Willpower.Amount;
        minionCharacter.Characteristics[GameStat.CharacteristicToInt["Общительность"]].Amount = _view.Social.Amount;        

        Unscribe();
        _view.DestroyView();
        ReturnCharacterWithCharacteristics?.Invoke(minionCharacter);
    }

    private void ToughnessIsChanged()
    {
        _view.Wounds.SetAmount(_view.Toughness.Amount/10 * 2 + 2);
    }
}
