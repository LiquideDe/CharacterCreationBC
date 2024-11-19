using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWithMotivations : CharacterDecorator, ICharacter
{
    private int _wounds, _corruptionPoints, _infamy;
    private string _pride, _disgrace, _motivation;
    private List<Characteristic> _characteristics;
    public CharacterWithMotivations(ICharacter character) : base(character)
    {
        _wounds = character.Wounds;
        _characteristics = new List<Characteristic>(_character.Characteristics);
    }

    public int Wounds => _wounds;

    public int CorruptionPoints => _corruptionPoints;

    public int PsyRating => _character.PsyRating;

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

    public int BonusToughness => throw new System.NotImplementedException();

    public ICharacter GetCharacter => _character;

    public List<IName> ForeverFriendly => _character.ForeverFriendly;

    public List<IName> ForeverHostile => _character.ForeverHostile;

    public int Infamy => _infamy;

    public string Race => _character.Race;

    public string Archetype => _character.Archetype;

    public List<string> EliteAcrhetypes => throw new System.NotImplementedException();

    public string Motivation => _motivation;

    public string Disgrace => _disgrace;

    public string Pride => _pride;

    public string Stereotype => throw new System.NotImplementedException();

    public bool CanChageGod => _character.CanChageGod;

    public string God => _character.God;

    public string Description => throw new System.NotImplementedException();

    public void SetWounds(int amount) => _wounds += amount;

    public void SetCorruptionPoints(int amount) => _corruptionPoints += amount;

    public void SetInfamy(int amount) => _infamy += amount;

    public void SetPride(string pride) => _pride = pride;

    public void SetDisgrace(string disgrace) => _disgrace = disgrace;

    public void SetMotivation(string motivation) => _motivation = motivation;

    public string GodGifts => _character.GodGifts;
}
