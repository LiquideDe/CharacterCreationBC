using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWithRace : CharacterDecorator, ICharacter
{
    private List<IName> _foreverFriendly = new List<IName>();
    private List<IName> _foreverHostile = new List<IName>();
    private string _race;
    private int _infamy;
    private bool _canChangeGod;
    private string _god;
    private int _amountCubes;
    private int _expierenceUnspent;
    private List<Characteristic> _characteristics;

    public CharacterWithRace(ICharacter character) : base(character)
    {
        _characteristics = new List<Characteristic>(character.Characteristics);
    }

    public int Wounds => throw new System.NotImplementedException();

    public int CorruptionPoints => throw new System.NotImplementedException();

    public int PsyRating => throw new System.NotImplementedException();

    public int HalfMove => throw new System.NotImplementedException();

    public int FullMove => throw new System.NotImplementedException();

    public int Natisk => throw new System.NotImplementedException();

    public int Fatigue => throw new System.NotImplementedException();

    public float CarryWeight => throw new System.NotImplementedException();

    public float LiftWeight => throw new System.NotImplementedException();

    public float PushWeight => throw new System.NotImplementedException();

    public int ExperienceTotal => _character.ExperienceTotal;

    public int ExperienceUnspent => _expierenceUnspent;

    public int ExperienceSpent => _character.ExperienceSpent;

    public List<Characteristic> Characteristics => _character.Characteristics;

    public List<string> MentalDisorders => _character.MentalDisorders;

    public int Run => throw new System.NotImplementedException();

    public List<string> Mutation => _character.Mutation;    

    public string Name => throw new System.NotImplementedException();

    public int BonusToughness => _character.BonusToughness;

    public ICharacter GetCharacter => _character;

    public List<IName> ForeverFriendly => _foreverFriendly;

    public List<IName> ForeverHostile => _foreverHostile;

    public int Infamy => _infamy;

    public string Race => _race;

    public string Archetype => throw new System.NotImplementedException();

    public List<string> EliteAcrhetypes => throw new System.NotImplementedException();

    public string Motivation => throw new System.NotImplementedException();

    public string Disgrace => throw new System.NotImplementedException();

    public string Pride => throw new System.NotImplementedException();

    public string Stereotype => throw new System.NotImplementedException();

    public bool CanChageGod => _canChangeGod;

    public string God => _god;

    public int AmountCubes => _amountCubes;

    public string Description => throw new System.NotImplementedException();

    public string GodGifts => _character.GodGifts;

    public void SetRace(string race) =>_race = race;

    public void SetGod(string god) => _god = god;

    public void SetStartCharacteristics(int amount)
    {
        foreach(Characteristic characteristic in _characteristics)        
            characteristic.Amount = amount;        
    }

    public void SetAmountCubes(int amount) => _amountCubes = amount;

    public void SetForeverFriendly(List<IName> names) => _foreverFriendly.AddRange(names);

    public void SetForeverHostile(List<IName> names) => _foreverHostile.AddRange(names);

    public void SetTraits(List<Trait> traits) => _traits.AddRange(traits);

    public void SetSkills(List<Skill> skills) => _skills.AddRange(skills);

    public void SetTalents(List<Talent> talents) => _talents.AddRange(talents);

    public void SetEquipments(List<Equipment> equipments) => _equipments.AddRange(equipments);

    public void SetExperienceCost(int cost) => _expierenceUnspent -= cost;

    public void SetCanChangeGod(bool can) => _canChangeGod = can;

    public void ModifierCharacteristics(List<Trait> modifiers)
    {
        foreach (Trait modifier in modifiers)
            _character.Characteristics[GameStat.CharacteristicToInt[modifier.Name]].Amount += modifier.Lvl;
    }
}
