using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWithCharacteristics : CharacterDecorator, ICharacter
{
    private List<Characteristic> _characteristics;
    private int _infamy, _wounds;
    private List<float> _parametrsForWeight = new List<float>() { 0.9f, 2.25f, 4.5f, 9f, 18f, 27f, 36f, 45f, 56f, 67f, 78f, 90f, 112f, 225f, 337f, 450f, 675f, 900f, 1350f, 1800f, 2250f };
    public CharacterWithCharacteristics(ICharacter character) : base(character)
    {
        _characteristics = new List<Characteristic>(character.Characteristics);
        _infamy = character.Infamy;
        _wounds = character.Wounds;
    }

    public int Wounds => _wounds;

    public int CorruptionPoints => _character.CorruptionPoints;

    public int PsyRating => _character.PsyRating;

    public int HalfMove => GetHalfMove();

    public int FullMove => GetHalfMove() * 2;

    public int Natisk => GetHalfMove() * 3;

    public int Fatigue => _characteristics[3].Amount / 10 + _characteristics[7].Amount / 10;

    public float CarryWeight => GetCarryWeight();

    public float LiftWeight => GetCarryWeight() * 2;

    public float PushWeight => GetCarryWeight() * 4;

    public int ExperienceTotal => _character.ExperienceTotal;

    public int ExperienceUnspent => _character.ExperienceUnspent;

    public int ExperienceSpent => _character.ExperienceSpent;

    public List<Characteristic> Characteristics => _characteristics;

    public List<string> MentalDisorders => _character.MentalDisorders;

    public int Run => GetHalfMove() * 6;
    public List<string> Mutation => _character.Mutation;

    public int BonusToughness => (int)_characteristics[3].Amount/10;

    public ICharacter GetCharacter => _character;

    public string Name => _character.Name;

    public List<IName> ForeverFriendly => _character.ForeverFriendly;

    public List<IName> ForeverHostile => _character.ForeverHostile;

    public int Infamy => _infamy;

    public string Race => _character.Race;

    public string Archetype => _character.Archetype;

    public List<string> EliteAcrhetypes => throw new System.NotImplementedException();

    public string Motivation => _character.Motivation;

    public string Disgrace => _character.Disgrace;

    public string Pride => _character.Pride;

    public string Stereotype => throw new System.NotImplementedException();

    public bool CanChageGod => _character.CanChageGod;

    public string God => _character.God;

    public string Description => throw new System.NotImplementedException();

    public string GodGifts => _character.GodGifts;

    public void SetCharacteristics(List<int> characteristics)
    {   
        for(int i = 0; i < characteristics.Count; i++)
        {
            _characteristics[i].Amount = characteristics[i];
        }
    }

    public void SetWounds(int wound) => _wounds = wound;

    public void SetInfamy(int infamy) => _infamy = infamy;
    

    private int GetHalfMove() => _characteristics[4].Amount / 10;

    private int GetForce() => _characteristics[2].Amount / 10 + _characteristics[3].Amount / 10;

    private float GetCarryWeight() => _parametrsForWeight[GetForce()];


}
