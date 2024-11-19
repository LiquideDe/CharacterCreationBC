using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class RacePresenter : IPresenter
{
    public event Action<ICharacter> CharacterDone;
    private AudioManager _audioManager;
    private RaceView _view;
    private int _currentRace = 0;
    private CreatorRace _creatorRace;
    private Character _character;

    [Inject]
    private void Construct(CreatorFactory creatorFactory, AudioManager audioManager)
    {
        _creatorRace = (CreatorRace)creatorFactory.Get(TypeCreator.Race);
        _audioManager = audioManager;
    }

    public void Initialize(RaceView view, ICharacter character)
    {
        _view = view;
        _character = FindCharacter(character);
        Subscribe();
        ShowRace();
    }

    private Character FindCharacter(ICharacter character)
    {
        if(character is Character finder)
            return finder;

        return FindCharacter(character.GetCharacter);
    }

    private void ShowRace()
    {
        _view.Initialize(_creatorRace.Races[_currentRace]);
    }

    private void Subscribe()
    {
        _view.ClickSound += _audioManager.PlayClick;
        _view.Done += CreateCharacter;
        _view.NextRace += NextRace;
        _view.PrevRace += PrevRace;
    }

    private void Unscribe()
    {
        _view.ClickSound -= _audioManager.PlayClick;
        _view.Done -= CreateCharacter;
        _view.NextRace -= NextRace;
        _view.PrevRace -= PrevRace;
    }

    private void PrevRace()
    {
        _audioManager.PlayClick();
        if(_currentRace > 0)
            _currentRace--;
        else
            _currentRace = _creatorRace.Races.Count - 1;

        ShowRace();
    }

    private void NextRace()
    {
        _audioManager.PlayClick();
        if (_currentRace + 1 == _creatorRace.Races.Count)
            _currentRace = 0;
        else
            _currentRace++;

        ShowRace();
    }

    private void CreateCharacter()
    {
        _audioManager.PlayDone();
        Race race = _creatorRace.Races[_currentRace];
        CharacterWithRace characterWithRace = new CharacterWithRace(_character);
        characterWithRace.SetAmountCubes(race.AmountCubes);
        characterWithRace.SetRace(race.Name);
        characterWithRace.SetGod(race.God);
        characterWithRace.SetStartCharacteristics(race.StartCharacteristic);
        characterWithRace.SetAmountCubes(race.AmountCubes);
        characterWithRace.SetForeverFriendly(race.ForeverFriendly);
        characterWithRace.SetForeverHostile(race.ForeverHostile);

        List<Trait> traits = new List<Trait>();
        for(int i = 0; i < race.Traits.Count; i++) 
            for(int j = 0; j < race.Traits[i].Count; j++)
                traits.Add(race.Traits[i][j]);

        characterWithRace.SetTraits(traits);
        
        List<Skill> skills = new List<Skill>();
        List<Talent> talents = new List<Talent>();
        List<Equipment> equipments = new List<Equipment>();
        foreach (CellSkill cellSkill in _view.Cells)
        {
            if (cellSkill.ChosenSkill is Skill)
                skills.Add((Skill)cellSkill.ChosenSkill);
            else if (cellSkill.ChosenSkill is Talent)
                talents.Add((Talent)cellSkill.ChosenSkill);
            else if (cellSkill.ChosenSkill is Equipment equipment)
                equipments.Add(equipment);
        }            

        characterWithRace.SetSkills(skills);
        characterWithRace.SetTalents(talents);
        characterWithRace.SetEquipments(equipments);
        characterWithRace.SetExperienceCost(race.ExperienceCost);
        characterWithRace.SetCanChangeGod(race.CanChangeGod);
        List<Trait> modifiers = new List<Trait>();

        for (int i = 0; i < race.ModifierCharacteristics.Count; i++)
            for (int j = 0; j < race.ModifierCharacteristics[i].Count; j++)
                modifiers.Add(race.ModifierCharacteristics[i][j]);

        if(modifiers.Count > 0)
            characterWithRace.ModifierCharacteristics(modifiers);

        CharacterDone?.Invoke(characterWithRace);
        Unscribe();
        _view.DestroyView();
    }
}

