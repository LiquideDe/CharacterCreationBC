using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ManualCharacteristicsMinionPresenter : IPresenter
{
    public event Action<IMinion> GoNext;
    private IMinion _minion;
    private TypeMinion _typeMinion;
    private ManualCharacteristicsMinionView _view;
    private AudioManager _audioManager;

    [Inject]
    private void Construct(AudioManager audioManager) => _audioManager = audioManager;

    public void Initialize(IMinion minionCharacter, TypeMinion minion, ManualCharacteristicsMinionView view)
    {
        _minion = minionCharacter;
        _typeMinion = minion;
        _view = view;
        Subscribe();
        _view.SetWillpower(_minion.Characteristics[GameStat.CharacteristicToInt["Сила Воли"]].Amount);
        _view.SetWeapon(_minion.Characteristics[GameStat.CharacteristicToInt["Навык Рукопашной"]].Amount);
        _view.SetToughness(_minion.Characteristics[GameStat.CharacteristicToInt["Выносливость"]].Amount);
        _view.SetStrength(_minion.Characteristics[GameStat.CharacteristicToInt["Сила"]].Amount);
        _view.SetPerception(_minion.Characteristics[GameStat.CharacteristicToInt["Восприятие"]].Amount);
        _view.SetIntelligence(_minion.Characteristics[GameStat.CharacteristicToInt["Интеллект"]].Amount);
        _view.SetFellowship(_minion.Characteristics[GameStat.CharacteristicToInt["Общительность"]].Amount);
        _view.SetBallistic(_minion.Characteristics[GameStat.CharacteristicToInt["Навык Стрельбы"]].Amount);
        _view.SetAgility(_minion.Characteristics[GameStat.CharacteristicToInt["Ловкость"]].Amount);

        
        if(_typeMinion.UpperLimitCharacteristics.Count > 0)
        {
            _view.SetMinMax($"Характеристики не могут быть выше {_typeMinion.MaxCharacteristic}. Следующие характеристики не могут быть выше {_typeMinion.UpperLimitCharacteristics[0].Lvl}",
                _typeMinion.UpperLimitCharacteristics);
        }

        if(_typeMinion.LowerLimitCharacteristics.Count > 0)
        {
            _view.SetMinMax($"Характеристики не могут быть выше {_typeMinion.MaxCharacteristic}. Следующие характеристики не могут быть ниже {_typeMinion.LowerLimitCharacteristics[0].Lvl}", 
                _typeMinion.LowerLimitCharacteristics);
        }
    }

    private void Subscribe()
    {
        _view.GoNext += GoNextPressed;
        _view.MinusAgility += MinusAgility;
        _view.MinusBallistic += MinusBallistic;
        _view.MinusFellowship += MinusFellowship;
        _view.MinusIntelligence += MinusIntelligence;
        _view.MinusPerception += MinusPerception;
        _view.MinusStrength += MinusStrength;
        _view.MinusToughness += MinusToughness;
        _view.MinusWeapon += MinusWeapon;
        _view.MinusWillpower += MinusWillpower;

        _view.PlusAgility += PlusAgility;
        _view.PlusBallistic += PlusBallistic;
        _view.PlusFellowship += PlusFellowship;
        _view.PlusIntelligence += PlusIntelligence;
        _view.PlusPerception += PlusPerception;
        _view.PlusStrength += PlusStrength;
        _view.PlusToughness += PlusToughness;
        _view.PlusWeapon += PlusWeapon;
        _view.PlusWillpower += PlusWillpower;
    }

    private void Unscribe()
    {
        _view.GoNext -= GoNextPressed;
        _view.MinusAgility -= MinusAgility;
        _view.MinusBallistic -= MinusBallistic;
        _view.MinusFellowship -= MinusFellowship;
        _view.MinusIntelligence -= MinusIntelligence;
        _view.MinusPerception -= MinusPerception;
        _view.MinusStrength -= MinusStrength;
        _view.MinusToughness -= MinusToughness;
        _view.MinusWeapon -= MinusWeapon;
        _view.MinusWillpower -= MinusWillpower;

        _view.PlusAgility -= PlusAgility;
        _view.PlusBallistic -= PlusBallistic;
        _view.PlusFellowship -= PlusFellowship;
        _view.PlusIntelligence -= PlusIntelligence;
        _view.PlusPerception -= PlusPerception;
        _view.PlusStrength -= PlusStrength;
        _view.PlusToughness -= PlusToughness;
        _view.PlusWeapon -= PlusWeapon;
        _view.PlusWillpower -= PlusWillpower;
    }

    private void GoNextPressed()
    {
        if(_minion.CharacteristicPoints == 0)
        {
            _audioManager.PlayDone();
            Unscribe();
            GoNext?.Invoke(_minion);
            _view.DestroyView();
        }
        else
        {
            _audioManager.PlayWarning();
        }
    }

    private void MinusAgility()
    {
        if (IsCanCharacteristicLess("Ловкость"))
        {
            MinusCharacteristic("Ловкость");
            _view.SetAgility(_minion.Characteristics[GameStat.CharacteristicToInt["Ловкость"]].Amount);            
        }
        else 
            _audioManager.PlayWarning();
    }

    private void MinusBallistic()
    {
        if (IsCanCharacteristicLess("Навык Стрельбы"))
        {
            MinusCharacteristic("Навык Стрельбы");
            _view.SetBallistic(_minion.Characteristics[GameStat.CharacteristicToInt["Навык Стрельбы"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void MinusFellowship()
    {
        if (IsCanCharacteristicLess("Общительность"))
        {
            MinusCharacteristic("Общительность");
            _view.SetFellowship(_minion.Characteristics[GameStat.CharacteristicToInt["Общительность"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void MinusIntelligence()
    {
        if (IsCanCharacteristicLess("Интеллект"))
        {
            MinusCharacteristic("Интеллект");
            _view.SetIntelligence(_minion.Characteristics[GameStat.CharacteristicToInt["Интеллект"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void MinusPerception()
    {
        if (IsCanCharacteristicLess("Восприятие"))
        {
            MinusCharacteristic("Восприятие");
            _view.SetPerception(_minion.Characteristics[GameStat.CharacteristicToInt["Восприятие"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void MinusStrength()
    {
        if (IsCanCharacteristicLess("Сила"))
        {
            MinusCharacteristic("Сила");
            _view.SetStrength(_minion.Characteristics[GameStat.CharacteristicToInt["Сила"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void MinusToughness()
    {
        if (IsCanCharacteristicLess("Выносливость"))
        {
            MinusCharacteristic("Выносливость");
            _view.SetToughness(_minion.Characteristics[GameStat.CharacteristicToInt["Выносливость"]].Amount);
            _view.SetWounds(_minion.Characteristics[GameStat.CharacteristicToInt["Выносливость"]].Amount / 10 * 2 + 2);
        }
        else
            _audioManager.PlayWarning();
    }

    private void MinusWeapon()
    {
        if (IsCanCharacteristicLess("Навык Рукопашной"))
        {
            MinusCharacteristic("Навык Рукопашной");
            _view.SetWeapon(_minion.Characteristics[GameStat.CharacteristicToInt["Навык Рукопашной"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void MinusWillpower()
    {
        if (IsCanCharacteristicLess("Сила Воли"))
        {
            MinusCharacteristic("Сила Воли");
            _view.SetWillpower(_minion.Characteristics[GameStat.CharacteristicToInt["Сила Воли"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusAgility()
    {
        if (IsCanCharacteristicMore("Ловкость"))
        {
            PlusCharacteristic("Ловкость");
            _view.SetAgility(_minion.Characteristics[GameStat.CharacteristicToInt["Ловкость"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusBallistic()
    {
        if (IsCanCharacteristicMore("Навык Стрельбы"))
        {
            PlusCharacteristic("Навык Стрельбы");
            _view.SetBallistic(_minion.Characteristics[GameStat.CharacteristicToInt["Навык Стрельбы"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusFellowship()
    {
        if (IsCanCharacteristicMore("Общительность"))
        {
            PlusCharacteristic("Общительность");
            _view.SetFellowship(_minion.Characteristics[GameStat.CharacteristicToInt["Общительность"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusIntelligence()
    {
        if (IsCanCharacteristicMore("Интеллект"))
        {
            PlusCharacteristic("Интеллект");
            _view.SetIntelligence(_minion.Characteristics[GameStat.CharacteristicToInt["Интеллект"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusPerception()
    {
        if (IsCanCharacteristicMore("Восприятие"))
        {
            PlusCharacteristic("Восприятие");
            _view.SetPerception(_minion.Characteristics[GameStat.CharacteristicToInt["Восприятие"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusStrength()
    {
        if (IsCanCharacteristicMore("Сила"))
        {
            PlusCharacteristic("Сила");
            _view.SetStrength(_minion.Characteristics[GameStat.CharacteristicToInt["Сила"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusToughness()
    {
        if (IsCanCharacteristicMore("Выносливость"))
        {
            PlusCharacteristic("Выносливость");
            _view.SetToughness(_minion.Characteristics[GameStat.CharacteristicToInt["Выносливость"]].Amount);
            _view.SetWounds(_minion.Characteristics[GameStat.CharacteristicToInt["Выносливость"]].Amount/10 * 2 + 2);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusWeapon()
    {
        if (IsCanCharacteristicMore("Навык Рукопашной"))
        {
            PlusCharacteristic("Навык Рукопашной");
            _view.SetWeapon(_minion.Characteristics[GameStat.CharacteristicToInt["Навык Рукопашной"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusWillpower()
    {
        if (IsCanCharacteristicMore("Сила Воли"))
        {
            PlusCharacteristic("Сила Воли");
            _view.SetWillpower(_minion.Characteristics[GameStat.CharacteristicToInt["Сила Воли"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void MinusCharacteristic(string name)
    {
        _audioManager.PlayClick();
        _minion.CharacteristicPoints++;
        _minion.Characteristics[GameStat.CharacteristicToInt[name]].Amount--;
        _view.SetPoints(_minion.CharacteristicPoints);        
    }

    private void PlusCharacteristic(string name)
    {
        _audioManager.PlayClick();
        _minion.CharacteristicPoints--;
        _minion.Characteristics[GameStat.CharacteristicToInt[name]].Amount++;
        _view.SetPoints(_minion.CharacteristicPoints);
        if (_minion.CharacteristicPoints == 0)
            _view.ShowButton();
    }

    private bool IsCanCharacteristicLess(string name)
    {
        if(_minion.Characteristics[GameStat.CharacteristicToInt[name]].Amount > 0 && IsLowerRestrictions(name))
            return true;

        return false;
    }

    private bool IsLowerRestrictions(string name)
    {
        if (_typeMinion.LowerLimitCharacteristics.Count > 0)
        {
            foreach (var item in _typeMinion.LowerLimitCharacteristics)
                if (string.Compare(name, item.Name, true) == 0)
                    if (_minion.Characteristics[GameStat.CharacteristicToInt[name]].Amount > item.Lvl)
                        return true;
                    else return false;
        }

        return true;            
    }

    private bool IsUpperRestrictions(string name)
    {
        if (_typeMinion.UpperLimitCharacteristics.Count > 0)
        {
            foreach (var item in _typeMinion.UpperLimitCharacteristics)
                if (string.Compare(name, item.Name, true) == 0)
                    if (_minion.Characteristics[GameStat.CharacteristicToInt[name]].Amount < item.Lvl)
                        return true;
                    else return false;
        }

        return true;
    }

    private bool IsCanCharacteristicMore(string name)
    {
        if(_minion.CharacteristicPoints > 0 && IsUpperRestrictions(name) && 
            _typeMinion.MaxCharacteristic > _minion.Characteristics[GameStat.CharacteristicToInt[name]].Amount)
            return true;

        return false;
    }
}
