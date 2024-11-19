using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static GameStat;

public class ArchetypePresenter : IPresenter
{
    public event Action<ICharacter> CharacterDone;
    public event Action ReturnBackToRace;
    private AudioManager _audioManager;
    private ArchetypeView _view;
    private int _currentArchetype = 0;
    private CreatorArchetype _creatorArchetype;
    private ICharacter _character;
    private List<Archetype> _archetypes = new List<Archetype>();    

    [Inject]
    private void Construct(CreatorFactory creatorFactory, AudioManager audioManager)
    {
        _creatorArchetype = (CreatorArchetype)creatorFactory.Get(TypeCreator.Arhetype);
        _audioManager = audioManager;
    }

    public void Initialize(ArchetypeView view, ICharacter character)
    {
        _view = view;
        _character = FindCharacterWithRace(character);
        Subscribe();
        AddArchetypes();
        ShowArchetype();
    }    

    private CharacterWithRace FindCharacterWithRace(ICharacter character)
    {
        if (character is CharacterWithRace)
            return (CharacterWithRace)character;
        else
            return FindCharacterWithRace(character.GetCharacter);
    }

    private void Subscribe()
    {
        _view.ClickSound += _audioManager.PlayClick;
        _view.Done += SetArchetype;
        _view.NextRace += NextArchetype;
        _view.PrevRace += PrevArchetype;
        _view.Cancel += ReturnBack;
    }    

    private void Unscribe()
    {
        _view.ClickSound -= _audioManager.PlayClick;
        _view.Done -= SetArchetype;
        _view.NextRace -= NextArchetype;
        _view.PrevRace -= PrevArchetype;
        _view.Cancel -= ReturnBack;
    }

    private void AddArchetypes()
    {
        bool isSpacemarine = false;

        if(string.Compare(_character.Race, "Космодесантник") == 0)
            isSpacemarine = true;

        
        foreach (var archetype in _creatorArchetype.Archetypes) 
        {
            if(archetype.IsSpaceMarine && isSpacemarine)
                _archetypes.Add(archetype);
            else if(archetype.IsSpaceMarine == false && isSpacemarine == false)
                _archetypes.Add(archetype);
        }
    }

    private void ShowArchetype()
    {
        _view.Initialize(_archetypes[_currentArchetype]);
    }

    private void PrevArchetype()
    {
        _audioManager.PlayClick();
        if (_currentArchetype > 0)
            _currentArchetype--;
        else
            _currentArchetype = _archetypes.Count - 1;

        ShowArchetype();
    }

    private void NextArchetype()
    {
        _audioManager.PlayClick();
        if (_currentArchetype + 1 == _archetypes.Count)
            _currentArchetype = 0;
        else
            _currentArchetype++;

        ShowArchetype();
    }

    private void SetArchetype()
    {
        _audioManager.PlayDone();
        Archetype archetype = _archetypes[_currentArchetype];
        
        CharacterWithArchetype character = new CharacterWithArchetype(_character);
        character.SetArchetype(archetype.Name);
        character.SetWounds(archetype.Wounds);
        character.SetExpForPsy(archetype.ExpForPsy);
        character.SetPsyRate(archetype.PsyRate);
        character.SetTrait(archetype.Trait);
        List<Skill> skills = new List<Skill>();
        List<Talent> talents = new List<Talent>();
        List<MechImplant> implants = new List<MechImplant>();
        List<Equipment> equipments = new List<Equipment>();
        foreach (CellSkill cellSkill in _view.Cells)
        {
            if (cellSkill.ChosenSkill is Skill skill)
                skills.Add(skill);
            else if (cellSkill.ChosenSkill is Talent talent)
                talents.Add(talent);
            else if (cellSkill.ChosenSkill is MechImplant implant)
                implants.Add(implant);
            else if(cellSkill.ChosenSkill is Equipment equipment)
                equipments.Add(equipment);
        }

        character.SetSkills(skills);
        character.SetTalents(talents);
        character.SetImplants(implants);
        character.SetEquipmets(equipments);

        for (int i = 0; i < archetype.ModifierCharacteristics.Count; i++)
            for (int j = 0; j < archetype.ModifierCharacteristics[i].Count; j++)
                character.Characteristics[CharacteristicToInt[archetype.ModifierCharacteristics[i][j].Name]].Amount += archetype.ModifierCharacteristics[i][j].Lvl;

        Unscribe();
        _view.DestroyView();
        CharacterDone?.Invoke(character);

    }

    private void ReturnBack(CanDestroyView view)
    {
        _audioManager.PlayCancel();
        Unscribe();
        _view.DestroyView();
        ReturnBackToRace?.Invoke();
    }
}
