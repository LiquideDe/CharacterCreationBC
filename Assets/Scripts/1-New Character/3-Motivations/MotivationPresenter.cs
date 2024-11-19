using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using UnityEngine.UI;
using static GameStat;

public class MotivationPresenter : IPresenter
{
    public event Action GoBack;
    public event Action<ICharacter> ReturnCharacterWithMotivations;

    private AudioManager _audioManager;
    private MotivationsView _view;
    private ICharacter _character;


    [Inject]
    private void Construct(AudioManager audioManager) => _audioManager = audioManager;

    public void Initialize(ICharacter character, MotivationsView view)
    {
        _character = character;
        _view = view;
        Subscribe();
        _view.Initialize();
    }

    private void Subscribe()
    {
        _view.Cancel += GoBackDown;
        _view.Done += SetMotivationToCharacter;
        _view.ToggleChoose += ToggleChoose;
    }

    private void ToggleChoose()
    {
        _audioManager.PlayClick();
        int amount = 0;
        if (IsToggleChoosed(_view.Prides))
            amount++;
        if(IsToggleChoosed(_view.Disgrace))
            amount++;
        if(IsToggleChoosed(_view.Motivations))
            amount++;

        if(amount == 3)
            _view.ShowButtonDone(); 
    }

    private bool IsToggleChoosed(List<Toggle> toggles)
    {
        foreach (Toggle toggle in toggles)
            if (toggle.isOn)
                return true;

        return false;
    }

    private void Unscribe()
    {
        _view.Cancel -= GoBackDown;
        _view.Done -= SetMotivationToCharacter;
        _view.ToggleChoose -= ToggleChoose;
    }

    private void SetMotivationToCharacter()
    {
        _audioManager.PlayDone();
        CharacterWithMotivations character = new CharacterWithMotivations(_character);
        for (int i = 0; i < _view.Prides.Count; i++)        
            if (_view.Prides[i].isOn)
            {
                SetBonusFromPride(character, i);
                break;
            }

        for (int i = 0; i < _view.Disgrace.Count; i++)
            if (_view.Disgrace[i].isOn)
            {
                SetBonusFromDisgrace(character, i);
                break;
            }

        for (int i = 0; i < _view.Motivations.Count; i++)
            if (_view.Motivations[i].isOn)
            {
                SetBonusFromMotivation(character, i);
                break;
            }

        Unscribe();
        _view.DestroyView();
        ReturnCharacterWithMotivations(character);
    }

    private void SetBonusFromPride(CharacterWithMotivations character, int id)
    {
        switch (id)
        {
            case 0:
                character.SetPride("Красота");
                character.SetInfamy(2);
                character.Characteristics[CharacteristicToInt["Сила Воли"]].Amount -= 4; break;

            case 1:
                character.SetPride("Обаяние");
                character.Characteristics[CharacteristicToInt["Общительность"]].Amount += 5;
                character.Characteristics[CharacteristicToInt["Выносливость"]].Amount -= 5; break;

            case 2:
                character.SetPride("Мастерство");
                character.SetInfamy(1);
                character.Characteristics[CharacteristicToInt["Ловкость"]].Amount += 3;
                character.Characteristics[CharacteristicToInt["Интеллект"]].Amount += 3;
                character.Characteristics[CharacteristicToInt["Навык Рукопашной"]].Amount -= 3;
                character.Characteristics[CharacteristicToInt["Навык Стрельбы"]].Amount -= 3; break;

            case 3:
                character.SetPride("Набожность");
                character.Characteristics[CharacteristicToInt["Сила Воли"]].Amount += 5;
                character.Characteristics[CharacteristicToInt["Сила"]].Amount -= 5; break;

            case 4:
                character.SetPride("Стойкость");
                character.Characteristics[CharacteristicToInt["Сила"]].Amount += 5;
                character.Characteristics[CharacteristicToInt["Ловкость"]].Amount -= 3;
                character.Characteristics[CharacteristicToInt["Интеллект"]].Amount -= 3; break;

            case 5:
                character.SetPride("Прозорливость");
                character.Characteristics[CharacteristicToInt["Общительность"]].Amount -= 5;
                character.Characteristics[CharacteristicToInt["Восприятие"]].Amount += 5; break;

            case 6:
                character.SetPride("Рассудительность");
                character.Characteristics[CharacteristicToInt["Восприятие"]].Amount -= 5;
                character.Characteristics[CharacteristicToInt["Интеллект"]].Amount += 5; break;

            case 7:
                character.SetPride("Воинское искусство");
                character.Characteristics[CharacteristicToInt["Навык Рукопашной"]].Amount += 5;
                character.Characteristics[CharacteristicToInt["Интеллект"]].Amount -= 5; break;

            case 8:
                character.SetPride("Изящество");
                character.Characteristics[CharacteristicToInt["Ловкость"]].Amount += 5;
                character.Characteristics[CharacteristicToInt["Навык Стрельбы"]].Amount -= 5; break;

            case 9:
                character.SetPride("Богатство");
                character.Characteristics[CharacteristicToInt["Сила Воли"]].Amount -= 3; break;
        }
    }

    private void SetBonusFromDisgrace(CharacterWithMotivations character, int id)
    {
        switch (id)
        {
            case 0:
                character.SetDisgrace("Вероломство");
                character.SetCorruptionPoints(5); break;

            case 1:
                character.SetDisgrace("Лживость");
                character.SetInfamy(2);
                character.Characteristics[CharacteristicToInt["Восприятие"]].Amount -= 4; break;

            case 2:
                character.SetDisgrace("Трусость");
                character.Characteristics[CharacteristicToInt["Восприятие"]].Amount += 3;
                character.Characteristics[CharacteristicToInt["Сила Воли"]].Amount -= 5; break;

            case 3:
                character.SetDisgrace("Жестокость");
                character.SetInfamy(2);
                character.Characteristics[CharacteristicToInt["Общительность"]].Amount -= 4; break;

            case 4:
                character.SetDisgrace("Ненасытность");
                character.SetWounds(2);
                character.Characteristics[CharacteristicToInt["Ловкость"]].Amount -= 5; break;

            case 5:
                character.SetDisgrace("Алчность");
                character.SetCorruptionPoints(4); break;

            case 6:
                character.SetDisgrace("Гордыня");
                character.SetInfamy(2);
                character.Characteristics[CharacteristicToInt["Интеллект"]].Amount -= 4; break;

            case 7:
                character.SetDisgrace("Сожаления");
                character.SetCorruptionPoints(5); break;

            case 8:
                character.SetDisgrace("Транжирство");
                character.SetInfamy(2);
                character.Characteristics[CharacteristicToInt["Сила Воли"]].Amount -= 4; break;

            case 9:
                character.SetDisgrace("Гнев");
                character.Characteristics[CharacteristicToInt["Восприятие"]].Amount += 5;
                character.Characteristics[CharacteristicToInt["Сила Воли"]].Amount -= 2; 
                character.SetWounds(-1); break;
        }
    }

    private void SetBonusFromMotivation(CharacterWithMotivations character, int id)
    {
        switch (id)
        {
            case 0:
                character.SetMotivation("Тайные знания");
                character.SetCorruptionPoints(4);
                character.Characteristics[CharacteristicToInt["Интеллект"]].Amount += 2;
                character.Characteristics[CharacteristicToInt["Сила"]].Amount -= 3; break;

            case 1:
                character.SetMotivation("Могущество");
                character.SetWounds(-2);
                character.Characteristics[CharacteristicToInt["Сила Воли"]].Amount -= 3; break;

            case 2:
                character.SetMotivation("Власть");
                character.SetInfamy(1);
                character.Characteristics[CharacteristicToInt["Сила Воли"]].Amount += 2;
                character.Characteristics[CharacteristicToInt["Общительность"]].Amount += 2;
                character.Characteristics[CharacteristicToInt["Ловкость"]].Amount -= 4;
                character.SetWounds(-1); break;

            case 3:
                character.SetMotivation("Бессмертие");
                character.Characteristics[CharacteristicToInt["Навык Рукопашной"]].Amount -= 5;
                character.SetWounds(2); break;

            case 4:
                character.SetMotivation("Новшества");
                character.SetCorruptionPoints(2);
                character.Characteristics[CharacteristicToInt["Интеллект"]].Amount += 3;
                character.SetWounds(-2); break;

            case 5:
                character.SetMotivation("Наследие");
                character.SetInfamy(2);
                character.Characteristics[CharacteristicToInt["Интеллект"]].Amount -= 4; break;

            case 6:
                character.SetMotivation("Вседозволенность");
                character.SetCorruptionPoints(5);
                character.Characteristics[CharacteristicToInt["Сила Воли"]].Amount -= 3; break;

            case 7:
                character.SetMotivation("Совершенство");
                break;

            case 8:
                character.SetMotivation("Месть");
                character.SetWounds(2);
                character.Characteristics[CharacteristicToInt["Восприятие"]].Amount -= 5; break;

            case 9:
                character.SetMotivation("Насилие");
                character.SetCorruptionPoints(5);
                character.Characteristics[CharacteristicToInt["Интеллект"]].Amount -= 3; break;
        }
    }

    private void GoBackDown(CanDestroyView view)
    {
        _audioManager.PlayCancel();
        Unscribe();
        _view.DestroyView();
        GoBack?.Invoke();
    }
}
