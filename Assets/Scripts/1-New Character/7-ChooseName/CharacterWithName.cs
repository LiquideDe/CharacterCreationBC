using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWithName : CharacterDecorator, ICharacter
{
    string _name, _description;
    public CharacterWithName(ICharacter character) : base(character)
    {
    }

    public int Wounds => _character.Wounds;

    public int CorruptionPoints => _character.CorruptionPoints;

    public int PsyRating => _character.PsyRating;

    public int HalfMove => _character.HalfMove;

    public int FullMove => _character.FullMove;

    public int Natisk => _character.Natisk;

    public int Fatigue => _character.Fatigue;

    public float CarryWeight => _character.CarryWeight;

    public float LiftWeight => _character.LiftWeight;

    public float PushWeight => _character.PushWeight;

    public int ExperienceTotal => _character.ExperienceTotal;

    public int ExperienceUnspent => _character.ExperienceUnspent;

    public int ExperienceSpent => _character.ExperienceSpent;

    public List<Characteristic> Characteristics => _character.Characteristics;

    public List<string> MentalDisorders => _character.MentalDisorders;

    public int Run => _character.Run;

    public List<string> Mutation => _character.Mutation;

    public string Name => _name;

    public int BonusToughness => _character.BonusToughness;

    public ICharacter GetCharacter => _character;

    public List<IName> ForeverFriendly => _character.ForeverFriendly;

    public List<IName> ForeverHostile => _character.ForeverHostile;

    public int Infamy => _character.Infamy;

    public string Race => _character.Race;

    public string Archetype => _character.Archetype;

    public List<string> EliteAcrhetypes => _character.EliteAcrhetypes;

    public string Motivation => _character.Motivation;

    public string Disgrace => _character.Disgrace;

    public string Pride => _character.Pride;

    public string Stereotype => _character.Stereotype;

    public bool CanChageGod => _character.CanChageGod;

    public string God => _character.God;

    public string Description => _description;

    public string GodGifts => _character.GodGifts;

    public void SetName(string name) => _name = name;

    public void SetDescription(string text) => _description = text;
}
