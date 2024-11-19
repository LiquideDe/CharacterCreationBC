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
                character.SetPride("�������");
                character.SetInfamy(2);
                character.Characteristics[CharacteristicToInt["���� ����"]].Amount -= 4; break;

            case 1:
                character.SetPride("�������");
                character.Characteristics[CharacteristicToInt["�������������"]].Amount += 5;
                character.Characteristics[CharacteristicToInt["������������"]].Amount -= 5; break;

            case 2:
                character.SetPride("����������");
                character.SetInfamy(1);
                character.Characteristics[CharacteristicToInt["��������"]].Amount += 3;
                character.Characteristics[CharacteristicToInt["���������"]].Amount += 3;
                character.Characteristics[CharacteristicToInt["����� ����������"]].Amount -= 3;
                character.Characteristics[CharacteristicToInt["����� ��������"]].Amount -= 3; break;

            case 3:
                character.SetPride("����������");
                character.Characteristics[CharacteristicToInt["���� ����"]].Amount += 5;
                character.Characteristics[CharacteristicToInt["����"]].Amount -= 5; break;

            case 4:
                character.SetPride("���������");
                character.Characteristics[CharacteristicToInt["����"]].Amount += 5;
                character.Characteristics[CharacteristicToInt["��������"]].Amount -= 3;
                character.Characteristics[CharacteristicToInt["���������"]].Amount -= 3; break;

            case 5:
                character.SetPride("�������������");
                character.Characteristics[CharacteristicToInt["�������������"]].Amount -= 5;
                character.Characteristics[CharacteristicToInt["����������"]].Amount += 5; break;

            case 6:
                character.SetPride("����������������");
                character.Characteristics[CharacteristicToInt["����������"]].Amount -= 5;
                character.Characteristics[CharacteristicToInt["���������"]].Amount += 5; break;

            case 7:
                character.SetPride("�������� ���������");
                character.Characteristics[CharacteristicToInt["����� ����������"]].Amount += 5;
                character.Characteristics[CharacteristicToInt["���������"]].Amount -= 5; break;

            case 8:
                character.SetPride("���������");
                character.Characteristics[CharacteristicToInt["��������"]].Amount += 5;
                character.Characteristics[CharacteristicToInt["����� ��������"]].Amount -= 5; break;

            case 9:
                character.SetPride("���������");
                character.Characteristics[CharacteristicToInt["���� ����"]].Amount -= 3; break;
        }
    }

    private void SetBonusFromDisgrace(CharacterWithMotivations character, int id)
    {
        switch (id)
        {
            case 0:
                character.SetDisgrace("�����������");
                character.SetCorruptionPoints(5); break;

            case 1:
                character.SetDisgrace("��������");
                character.SetInfamy(2);
                character.Characteristics[CharacteristicToInt["����������"]].Amount -= 4; break;

            case 2:
                character.SetDisgrace("��������");
                character.Characteristics[CharacteristicToInt["����������"]].Amount += 3;
                character.Characteristics[CharacteristicToInt["���� ����"]].Amount -= 5; break;

            case 3:
                character.SetDisgrace("����������");
                character.SetInfamy(2);
                character.Characteristics[CharacteristicToInt["�������������"]].Amount -= 4; break;

            case 4:
                character.SetDisgrace("������������");
                character.SetWounds(2);
                character.Characteristics[CharacteristicToInt["��������"]].Amount -= 5; break;

            case 5:
                character.SetDisgrace("��������");
                character.SetCorruptionPoints(4); break;

            case 6:
                character.SetDisgrace("�������");
                character.SetInfamy(2);
                character.Characteristics[CharacteristicToInt["���������"]].Amount -= 4; break;

            case 7:
                character.SetDisgrace("���������");
                character.SetCorruptionPoints(5); break;

            case 8:
                character.SetDisgrace("�����������");
                character.SetInfamy(2);
                character.Characteristics[CharacteristicToInt["���� ����"]].Amount -= 4; break;

            case 9:
                character.SetDisgrace("����");
                character.Characteristics[CharacteristicToInt["����������"]].Amount += 5;
                character.Characteristics[CharacteristicToInt["���� ����"]].Amount -= 2; 
                character.SetWounds(-1); break;
        }
    }

    private void SetBonusFromMotivation(CharacterWithMotivations character, int id)
    {
        switch (id)
        {
            case 0:
                character.SetMotivation("������ ������");
                character.SetCorruptionPoints(4);
                character.Characteristics[CharacteristicToInt["���������"]].Amount += 2;
                character.Characteristics[CharacteristicToInt["����"]].Amount -= 3; break;

            case 1:
                character.SetMotivation("����������");
                character.SetWounds(-2);
                character.Characteristics[CharacteristicToInt["���� ����"]].Amount -= 3; break;

            case 2:
                character.SetMotivation("������");
                character.SetInfamy(1);
                character.Characteristics[CharacteristicToInt["���� ����"]].Amount += 2;
                character.Characteristics[CharacteristicToInt["�������������"]].Amount += 2;
                character.Characteristics[CharacteristicToInt["��������"]].Amount -= 4;
                character.SetWounds(-1); break;

            case 3:
                character.SetMotivation("����������");
                character.Characteristics[CharacteristicToInt["����� ����������"]].Amount -= 5;
                character.SetWounds(2); break;

            case 4:
                character.SetMotivation("���������");
                character.SetCorruptionPoints(2);
                character.Characteristics[CharacteristicToInt["���������"]].Amount += 3;
                character.SetWounds(-2); break;

            case 5:
                character.SetMotivation("��������");
                character.SetInfamy(2);
                character.Characteristics[CharacteristicToInt["���������"]].Amount -= 4; break;

            case 6:
                character.SetMotivation("����������������");
                character.SetCorruptionPoints(5);
                character.Characteristics[CharacteristicToInt["���� ����"]].Amount -= 3; break;

            case 7:
                character.SetMotivation("������������");
                break;

            case 8:
                character.SetMotivation("�����");
                character.SetWounds(2);
                character.Characteristics[CharacteristicToInt["����������"]].Amount -= 5; break;

            case 9:
                character.SetMotivation("�������");
                character.SetCorruptionPoints(5);
                character.Characteristics[CharacteristicToInt["���������"]].Amount -= 3; break;
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
