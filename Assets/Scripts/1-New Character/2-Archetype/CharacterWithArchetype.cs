using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWithArchetype : CharacterDecorator, ICharacter
{
    private int _wounds, _expForPsy, _psyRate;
    private string _archetype;
    private List<Characteristic> _characteristics;

    public CharacterWithArchetype(ICharacter character) : base(character)
    {
        _characteristics = new List<Characteristic>(_character.Characteristics);
    }

    public int Wounds => _wounds;

    public int CorruptionPoints => _character.CorruptionPoints;

    public int PsyRating => _psyRate;

    public int HalfMove => throw new System.NotImplementedException();

    public int FullMove => throw new System.NotImplementedException();

    public int Natisk => throw new System.NotImplementedException();

    public int Fatigue => throw new System.NotImplementedException();

    public float CarryWeight => throw new System.NotImplementedException();

    public float LiftWeight => throw new System.NotImplementedException();

    public float PushWeight => throw new System.NotImplementedException();

    public int ExperienceTotal => _character.ExperienceTotal;

    public int ExperienceUnspent => _character.ExperienceUnspent;

    public int ExperienceSpent => _character.ExperienceSpent;

    public List<Characteristic> Characteristics => _characteristics;

    public List<string> MentalDisorders => _character.MentalDisorders;

    public int Run => throw new System.NotImplementedException();

    public List<string> Mutation => _character.Mutation;

    public string Name => throw new System.NotImplementedException();

    public int BonusToughness => _character.BonusToughness;

    public ICharacter GetCharacter => _character;

    public List<IName> ForeverFriendly => _character.ForeverFriendly;

    public List<IName> ForeverHostile => _character.ForeverHostile;

    public int Infamy => _character.Infamy;

    public string Race => _character.Race;

    public string Archetype => _archetype;

    public List<string> EliteAcrhetypes => throw new System.NotImplementedException();

    public string Motivation => throw new System.NotImplementedException();

    public string Disgrace => throw new System.NotImplementedException();

    public string Pride => throw new System.NotImplementedException();

    public string Stereotype => throw new System.NotImplementedException();

    public bool CanChageGod => _character.CanChageGod;

    public string God => _character.God;

    public string Description => throw new System.NotImplementedException();

    public string GodGifts => _character.GodGifts;

    public int ExpForPsy => _expForPsy;

    public void SetArchetype(string archetype) => _archetype = archetype;

    public void SetWounds(int amount) => _wounds = amount;

    public void SetExpForPsy(int amount) => _expForPsy = amount;

    public void SetPsyRate(int amount) => _psyRate = amount;

    public void SetTrait(Trait trait) => _traits.Add(trait);

    public void SetSkills(List<Skill> skills) => _skills.AddRange(skills);

    public void SetTalents(List<Talent> talents) => _talents.AddRange(talents);

    public void SetImplants(List<MechImplant> implants) => _mechImplants.AddRange(implants);

    public void SetEquipmets(List<Equipment> equipments) => _equipments.AddRange(equipments);
}
