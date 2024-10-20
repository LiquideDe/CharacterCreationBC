using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : ICharacter
{
    private string _name, _god;
    private int _corruptionPoints, _wounds, _psyRating, _halfMove, _fullMove, _natisk, _run, _fatigue,
        _experienceTotal, _experienceUnspent, _experienceSpent;
    private float _carryWeight, _liftWeight, _pushWeight;
    private List<Characteristic> _characteristics = new List<Characteristic>();
    private List<Talent> _talents = new List<Talent>();
    private List<Skill> _skills = new List<Skill>();
    private List<string> _mentalDisorders = new List<string>();
    private List<string> _mutation = new List<string>();
    private List<PsyPower> _psyPowers = new List<PsyPower>();
    private List<Equipment> _equipments = new List<Equipment>();
    private List<MechImplant> _implants = new List<MechImplant>();
    private List<Trait> _traits = new List<Trait>();
    private List<IName> _foreverFriendly = new List<IName>();
    private List<IName> _foreverHostile = new List<IName>();
    private List<string> _eliteArhetypes = new List<string>();
    private List<string> _minions = new List<string>();
    private string _motivation, _disgrace, _pride, _stereotype;
    private string _race;
    private string _archetype;
    private bool _canChangeGod;

    public Character(CreatorSkills skills)
    {
        _skills = new List<Skill>(skills.Skills);
        CreateCharacteristics();
    }

    #region Свойства

    public List<Equipment> Equipments => _equipments;
    public int Wounds => _wounds;
    public int CorruptionPoints => _corruptionPoints;
    public int PsyRating => _psyRating;
    public int HalfMove => _halfMove;
    public int FullMove => _fullMove;
    public int Natisk => _natisk;
    public int Fatigue => _fatigue;
    public float CarryWeight => _carryWeight;
    public float LiftWeight => _liftWeight;
    public float PushWeight => _pushWeight;
    public int ExperienceTotal => _experienceTotal;
    public int ExperienceUnspent => _experienceUnspent;
    public int ExperienceSpent => _experienceSpent;
    public List<Characteristic> Characteristics => _characteristics;
    public List<Skill> Skills => _skills;
    public List<Talent> Talents => _talents;
    public List<MechImplant> Implants => _implants;
    public List<string> MentalDisorders => _mentalDisorders;
    public int Run => _run;
    public List<string> Mutation => _mutation;
    public List<PsyPower> PsyPowers => _psyPowers;
    public List<Trait> Traits => _traits;
    public string Name => _name;
    public int BonusToughness => GetBonusToughness();
    public ICharacter GetCharacter => this;

    public List<IName> ForeverFriendly => _foreverFriendly;

    public List<IName> ForeverHostile => _foreverHostile;

    public string Race => _race;

    public string Archetype => _archetype;

    public List<string> EliteAcrhetypes => _eliteArhetypes;

    public string Motivation => _motivation;

    public string Disgrace => _disgrace;

    public string Pride => _pride;

    public string Stereotype => _stereotype;

    public bool CanChageGod => _canChangeGod;

    public string God => _god;

    #endregion


    public void LoadData(SaveLoadCharacter loadCharacter, List<SaveLoadCharacteristic> characteristics, List<SaveLoadSkill> skills, List<MechImplant> implants , List<Equipment> equipments)
    {
        List<string> listFeat = loadCharacter.traits.Split(new char[] { '/' }).ToList();
        if (CheckString(listFeat))
        {
            List<string> featureLvl = loadCharacter.traitsLvl.Split(new char[] { '/' }).ToList();
            for(int i = 0; i < listFeat.Count; i++)
            {
                int.TryParse(featureLvl[i], out int lvl);
                _traits.Add(new Trait(listFeat[i], lvl));
            }
        }

        _experienceSpent = loadCharacter.experienceSpent;
        _experienceTotal = loadCharacter.experienceTotal;
        _experienceUnspent = loadCharacter.experienceUnspent;
        _fatigue = loadCharacter.fatigue;
        _fullMove = loadCharacter.fullMove;
        _halfMove = loadCharacter.halfMove;

        _implants.AddRange(implants);

        
        _liftWeight = loadCharacter.liftWeight;

        if(loadCharacter.mentalDisorders.Length > 0)
        {
            _mentalDisorders = loadCharacter.mentalDisorders.Split(new char[] { '/' }).ToList();
        }
        
        if(loadCharacter.mutation.Length > 0)
        {
            _mutation = loadCharacter.mutation.Split(new char[] { '/' }).ToList();
        }
        
        _name = loadCharacter.name;
        _natisk = loadCharacter.natisk;
        
        _pushWeight = loadCharacter.pushWeight;
        _run = loadCharacter.run;
        _wounds = loadCharacter.wounds;

        List<string> tl = new List<string>();
        tl = loadCharacter.talents.Split(new char[] { '/' }).ToList();
        if(CheckString(tl))
        foreach(string talent in tl)
        {
            _talents.Add(new Talent(talent));
        }

        List<string> psy = new List<string>();
        psy = loadCharacter.psyPowers.Split(new char[] { '/' }).ToList();
        if(CheckString(psy))
        foreach(string p in psy)
        {
            _psyPowers.Add(new PsyPower(p));
        }

        for(int i = 0; i < characteristics.Count; i++)
        {
            this._characteristics[i].Amount = characteristics[i].amount;
            this._characteristics[i].LvlLearned = characteristics[i].lvlLearnedChar;
        }

        foreach(Skill skill in _skills)
        {
            foreach(SaveLoadSkill loadSkill in skills)
            {
                if(string.Compare(skill.Name, loadSkill.name) == 0)
                {
                    skill.LvlLearned = loadSkill.lvlLearned;
                    break;
                }
            }
        }

        _psyRating = loadCharacter.psyRating;
        _equipments.AddRange(equipments);
    }

    public void ChangeCorruption(int amount) => _corruptionPoints += amount;

    public void ChangeFullmove(int amount) => _fullMove += amount;

    public void ChangeHalfmove(int amount) => _halfMove += amount;

    public void ChangeNatisk(int amount) => _natisk += amount;

    public void ChangeRun(int amount) => _run += amount;

    public void ChangeWounds(int amount) => _wounds += amount;

    private bool CheckString(List<string> list)
    {
        if(list.Count > 0)
        {
            if(list[0].Length > 0)
            {
                return true;
            }
        }

        return false;
    }

    private int GetBonusToughness()
    {
        int bToughness = _characteristics[3].Amount / 10;
        foreach(Trait feature in _traits)
        {
            if (string.Compare(feature.Name, "Сверхъестественная Выносливость") == 0 || string.Compare(feature.Name, "Демонический") == 0)
            {
                bToughness += feature.Lvl;
            }
        }

        return bToughness;
    }

    private void CreateCharacteristics()
    {
        _characteristics.Add(new Characteristic(GameStat.CharacteristicName.WeaponSkill)); //0
        _characteristics.Add(new Characteristic(GameStat.CharacteristicName.BallisticSkill)); //1
        _characteristics.Add(new Characteristic(GameStat.CharacteristicName.Strength)); //2
        _characteristics.Add(new Characteristic(GameStat.CharacteristicName.Toughness)); //3
        _characteristics.Add(new Characteristic(GameStat.CharacteristicName.Agility)); //4
        _characteristics.Add(new Characteristic(GameStat.CharacteristicName.Intelligence)); //5
        _characteristics.Add(new Characteristic(GameStat.CharacteristicName.Perception)); //6
        _characteristics.Add(new Characteristic(GameStat.CharacteristicName.Willpower)); //7
        _characteristics.Add(new Characteristic(GameStat.CharacteristicName.Fellowship)); //8
    }
}
