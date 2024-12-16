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
        _view.SetWillpower(_minion.Characteristics[GameStat.CharacteristicToInt["���� ����"]].Amount);
        _view.SetWeapon(_minion.Characteristics[GameStat.CharacteristicToInt["����� ����������"]].Amount);
        _view.SetToughness(_minion.Characteristics[GameStat.CharacteristicToInt["������������"]].Amount);
        _view.SetStrength(_minion.Characteristics[GameStat.CharacteristicToInt["����"]].Amount);
        _view.SetPerception(_minion.Characteristics[GameStat.CharacteristicToInt["����������"]].Amount);
        _view.SetIntelligence(_minion.Characteristics[GameStat.CharacteristicToInt["���������"]].Amount);
        _view.SetFellowship(_minion.Characteristics[GameStat.CharacteristicToInt["�������������"]].Amount);
        _view.SetBallistic(_minion.Characteristics[GameStat.CharacteristicToInt["����� ��������"]].Amount);
        _view.SetAgility(_minion.Characteristics[GameStat.CharacteristicToInt["��������"]].Amount);

        
        if(_typeMinion.UpperLimitCharacteristics.Count > 0)
        {
            _view.SetMinMax($"�������������� �� ����� ���� ���� {_typeMinion.MaxCharacteristic}. ��������� �������������� �� ����� ���� ���� {_typeMinion.UpperLimitCharacteristics[0].Lvl}",
                _typeMinion.UpperLimitCharacteristics);
        }

        if(_typeMinion.LowerLimitCharacteristics.Count > 0)
        {
            _view.SetMinMax($"�������������� �� ����� ���� ���� {_typeMinion.MaxCharacteristic}. ��������� �������������� �� ����� ���� ���� {_typeMinion.LowerLimitCharacteristics[0].Lvl}", 
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
        if (IsCanCharacteristicLess("��������"))
        {
            MinusCharacteristic("��������");
            _view.SetAgility(_minion.Characteristics[GameStat.CharacteristicToInt["��������"]].Amount);            
        }
        else 
            _audioManager.PlayWarning();
    }

    private void MinusBallistic()
    {
        if (IsCanCharacteristicLess("����� ��������"))
        {
            MinusCharacteristic("����� ��������");
            _view.SetBallistic(_minion.Characteristics[GameStat.CharacteristicToInt["����� ��������"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void MinusFellowship()
    {
        if (IsCanCharacteristicLess("�������������"))
        {
            MinusCharacteristic("�������������");
            _view.SetFellowship(_minion.Characteristics[GameStat.CharacteristicToInt["�������������"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void MinusIntelligence()
    {
        if (IsCanCharacteristicLess("���������"))
        {
            MinusCharacteristic("���������");
            _view.SetIntelligence(_minion.Characteristics[GameStat.CharacteristicToInt["���������"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void MinusPerception()
    {
        if (IsCanCharacteristicLess("����������"))
        {
            MinusCharacteristic("����������");
            _view.SetPerception(_minion.Characteristics[GameStat.CharacteristicToInt["����������"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void MinusStrength()
    {
        if (IsCanCharacteristicLess("����"))
        {
            MinusCharacteristic("����");
            _view.SetStrength(_minion.Characteristics[GameStat.CharacteristicToInt["����"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void MinusToughness()
    {
        if (IsCanCharacteristicLess("������������"))
        {
            MinusCharacteristic("������������");
            _view.SetToughness(_minion.Characteristics[GameStat.CharacteristicToInt["������������"]].Amount);
            _view.SetWounds(_minion.Characteristics[GameStat.CharacteristicToInt["������������"]].Amount / 10 * 2 + 2);
        }
        else
            _audioManager.PlayWarning();
    }

    private void MinusWeapon()
    {
        if (IsCanCharacteristicLess("����� ����������"))
        {
            MinusCharacteristic("����� ����������");
            _view.SetWeapon(_minion.Characteristics[GameStat.CharacteristicToInt["����� ����������"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void MinusWillpower()
    {
        if (IsCanCharacteristicLess("���� ����"))
        {
            MinusCharacteristic("���� ����");
            _view.SetWillpower(_minion.Characteristics[GameStat.CharacteristicToInt["���� ����"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusAgility()
    {
        if (IsCanCharacteristicMore("��������"))
        {
            PlusCharacteristic("��������");
            _view.SetAgility(_minion.Characteristics[GameStat.CharacteristicToInt["��������"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusBallistic()
    {
        if (IsCanCharacteristicMore("����� ��������"))
        {
            PlusCharacteristic("����� ��������");
            _view.SetBallistic(_minion.Characteristics[GameStat.CharacteristicToInt["����� ��������"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusFellowship()
    {
        if (IsCanCharacteristicMore("�������������"))
        {
            PlusCharacteristic("�������������");
            _view.SetFellowship(_minion.Characteristics[GameStat.CharacteristicToInt["�������������"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusIntelligence()
    {
        if (IsCanCharacteristicMore("���������"))
        {
            PlusCharacteristic("���������");
            _view.SetIntelligence(_minion.Characteristics[GameStat.CharacteristicToInt["���������"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusPerception()
    {
        if (IsCanCharacteristicMore("����������"))
        {
            PlusCharacteristic("����������");
            _view.SetPerception(_minion.Characteristics[GameStat.CharacteristicToInt["����������"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusStrength()
    {
        if (IsCanCharacteristicMore("����"))
        {
            PlusCharacteristic("����");
            _view.SetStrength(_minion.Characteristics[GameStat.CharacteristicToInt["����"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusToughness()
    {
        if (IsCanCharacteristicMore("������������"))
        {
            PlusCharacteristic("������������");
            _view.SetToughness(_minion.Characteristics[GameStat.CharacteristicToInt["������������"]].Amount);
            _view.SetWounds(_minion.Characteristics[GameStat.CharacteristicToInt["������������"]].Amount/10 * 2 + 2);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusWeapon()
    {
        if (IsCanCharacteristicMore("����� ����������"))
        {
            PlusCharacteristic("����� ����������");
            _view.SetWeapon(_minion.Characteristics[GameStat.CharacteristicToInt["����� ����������"]].Amount);
        }
        else
            _audioManager.PlayWarning();
    }

    private void PlusWillpower()
    {
        if (IsCanCharacteristicMore("���� ����"))
        {
            PlusCharacteristic("���� ����");
            _view.SetWillpower(_minion.Characteristics[GameStat.CharacteristicToInt["���� ����"]].Amount);
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
