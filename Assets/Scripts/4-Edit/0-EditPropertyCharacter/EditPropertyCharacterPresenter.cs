using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class EditPropertyCharacterPresenter : IPresenter
{
    public event Action<ICharacter> GoNext;

    private EditPropertyCharacterView _view;
    private AudioManager _audioManager;
    private Character _character;
    private ICharacter _characterToReturn;
    private ListWithNewItems _listWithNewItems;
    private InputNewPropertyView _inputNewProperty;
    private LvlFactory _lvlFactory;
    private CreatorTraits _creatorFeatures;

    [Inject]
    private void Construct(AudioManager audioManager, CreatorTraits creatorFeatures)
    {
        _audioManager = audioManager;
        _creatorFeatures = creatorFeatures;
    }

    public void Initialize(EditPropertyCharacterView view, ICharacter character, LvlFactory lvlFactory)
    {
        _view = view;
        _characterToReturn = character;
        _lvlFactory = lvlFactory;
        SearchCharacter(character);
        Subscribe();
        _view.Initialize(_character);
    }

    private void SearchCharacter(ICharacter character)
    {
        if (character is Character realCharacter)
            _character = realCharacter;
        else
            SearchCharacter(character.GetCharacter);
    }

    private void Subscribe()
    {
        _view.AddFeature += ShowNewFeatures;
        _view.AddMental += ShowInputMental;
        _view.AddMutation += ShowInputMutation;
        _view.ChangeFeatureLvl += ChangeFeatureLvl;
        _view.ChangePropertyCharacter += ChangePropertyCharacter;
        _view.Next += Next;
        _view.RemoveFeature += RemoveFeature;
        _view.RemoveMental += RemoveMental;
        _view.RemoveMutation += RemoveMutation;
    }

    private void RemoveMutation(string mutationName)
    {
        _audioManager.PlayCancel();
        Debug.Log($"�� �������� � ��������� ������� {_character.Mutation.Count}");
        _character.Mutation.Remove(mutationName);
        Debug.Log($"����� �������� � ��������� ������� {_character.Mutation.Count}, ������� ������� {mutationName}");
        _view.UpdateMutation(_character.Mutation);
    }

    private void RemoveMental(string mentalName)
    {
        _audioManager.PlayCancel();
        _character.MentalDisorders.Remove(mentalName);
        _view.UpdateMental(_character.MentalDisorders);
    }

    private void RemoveFeature(string featureName)
    {
        _audioManager.PlayCancel();
        foreach(Trait feature in _character.Traits)
        {
            if (string.Compare(featureName, feature.Name) == 0)
            {
                _character.Traits.Remove(feature);
                break;
            }
        }

        _view.UpdateFeatures(_character.Traits);
    }

    private void Next()
    {
        _audioManager.PlayClick();
        Unscribe();
        CloseAllSmallWindows();
        _view.DestroyView();
        GoNext?.Invoke(_characterToReturn);
    }

    private void ChangePropertyCharacter(SaveLoadCharacter saveLoadCharacter)
    {
        _audioManager.PlayDone();
        _character.UpdateData(saveLoadCharacter);
    }

    private void ChangeFeatureLvl(string nameFeature, int lvl)
    {
        _audioManager.PlayClick();
        foreach (Trait feature in _character.Traits)
        {
            if (string.Compare(nameFeature, feature.Name) == 0)
            {
                feature.Lvl = lvl;
                break;
            }
        }

        _view.UpdateFeatures(_character.Traits);
    }

    private void ShowInputMutation()
    {
        _audioManager.PlayClick();
        CloseAllSmallWindows();
        _inputNewProperty = _lvlFactory.Get(TypeScene.InputNewProperty).GetComponent<InputNewPropertyView>();
        _inputNewProperty.CloseInput += CloseInput;
        _inputNewProperty.ReturnThisString += SetNewMutation;
        _inputNewProperty.Initialize("������� �������� ����� �������");
    }

    private void SetNewMutation(string mutation)
    {
        Debug.Log($"��������� ����� ������� {mutation}");
        _audioManager.PlayDone();
        _character.Mutation.Add(mutation);
        _view.UpdateMutation(_character.Mutation);
        CloseAllSmallWindows();
    }
    

    private void ShowInputMental()
    {
        _audioManager.PlayClick();
        CloseAllSmallWindows();
        _inputNewProperty = _lvlFactory.Get(TypeScene.InputNewProperty).GetComponent<InputNewPropertyView>();
        _inputNewProperty.CloseInput += CloseInput;
        _inputNewProperty.ReturnThisString += SetNewMental;
        _inputNewProperty.Initialize("������� �������� ������ ������������");
    }

    private void SetNewMental(string mental)
    {
        _audioManager.PlayDone();
        _character.MentalDisorders.Add(mental);
        _view.UpdateMental(_character.MentalDisorders);
        CloseAllSmallWindows();
    }

    private void ShowNewFeatures()
    {
        _audioManager.PlayClick();
        CloseAllSmallWindows();

        _listWithNewItems = _lvlFactory.Get(TypeScene.ListWithNewItems).GetComponent<ListWithNewItems>();
        List<string> namesFeatures = new List<string>();
        foreach(Trait feature in _creatorFeatures.Traits)
        {
            if (TryNotDoubleFeature(feature))
                namesFeatures.Add(feature.Name);
        }

        _listWithNewItems.ChooseThis += AddFeature;
        _listWithNewItems.CloseList += CloseList;

        _listWithNewItems.Initialize(namesFeatures, "�������� ����� �����");
    }

    private bool TryNotDoubleFeature(Trait feature)
    {
        foreach(Trait feat in _character.Traits)
        {
            if (string.Compare(feat.Name, feature.Name, true) == 0)
                return false;
        }

        return true;
    }

    private void AddFeature(string name)
    {
        _audioManager.PlayDone();
        _character.Traits.Add(new Trait(_creatorFeatures.GetTrait(name)));
        CloseAllSmallWindows();
        _view.UpdateFeatures(_character.Traits);
    }

    private void Unscribe()
    {
        _view.AddFeature -= ShowNewFeatures;
        _view.AddMental -= ShowInputMental;
        _view.AddMutation -= ShowInputMutation;
        _view.ChangeFeatureLvl -= ChangeFeatureLvl;
        _view.ChangePropertyCharacter -= ChangePropertyCharacter;
        _view.Next -= Next;
        _view.RemoveFeature -= RemoveFeature;
        _view.RemoveMental -= RemoveMental;
        _view.RemoveMutation -= RemoveMutation;
    }

    private void CloseList(CanDestroyView view)
    {
        _audioManager.PlayCancel();
        view.DestroyView();
        _listWithNewItems = null;
    }

    private void CloseInput()
    {
        _audioManager.PlayCancel();
        _inputNewProperty.DestroyView();
        _inputNewProperty = null;
    }

    private void CloseAllSmallWindows()
    {
        if(_inputNewProperty != null)
        {
            _inputNewProperty.DestroyView();
            _inputNewProperty = null;
        }

        if(_listWithNewItems != null)
        {
            _listWithNewItems.DestroyView();
            _listWithNewItems = null;
        }
            
    }
    
}
