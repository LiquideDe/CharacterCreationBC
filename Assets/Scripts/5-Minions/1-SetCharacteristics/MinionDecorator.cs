using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionDecorator : IMinion
{
    private IMinion _character;
    private List<TraitWithCost> _traits = new List<TraitWithCost>();
    protected List<Skill> _skills = new List<Skill>();
    protected List<Talent> _talents = new List<Talent>();
    protected List<PsyPower> _psyPowers = new List<PsyPower>();
    private int _characteristicPoints, _skillPoints, _talentsPoints, _traitPoints, _psyPowerPoints;
    private List<Armor> _armors = new List<Armor>();
    private List<Weapon> _weapons = new List<Weapon>();
    private List<Characteristic> _characteristics = new List<Characteristic>();
    private int _skillsOverPrice, _psyRating;

    public MinionDecorator(IMinion character)
    {
        _character = character;
        _characteristicPoints = _character.CharacteristicPoints;
        _skillPoints = _character.SkillPoints;
        _talentsPoints = _character.TalentPoints;
        _traitPoints = _character.TraitPoints;
        _psyPowerPoints = _character.PsyPowerPoints;
        _skillsOverPrice = _character.SkillsOverPrice;
        _psyRating = _character.PsyRating;
        _characteristics = new List<Characteristic>(character.Characteristics);
    }

    public IMinion Minion => _character;

    public List<Characteristic> Characteristics => _characteristics;

    public List<Skill> Skills => GetSkills();

    public List<Talent> Talents => GetTalents();

    public List<PsyPower> PsyPowers => GetPsyPowers();

    public List<TraitWithCost> Traits => GetTraits();

    public List<Armor> Armors => _armors;

    public List<Weapon> Weapons => _weapons;
    public int SkillsOverPrice { get => _skillsOverPrice; set => _skillsOverPrice = value; }

    public int CharacteristicPoints { get => _characteristicPoints; set => _characteristicPoints = value; }
    public int SkillPoints { get => _skillPoints; set => _skillPoints = value; }
    public int TalentPoints { get => _talentsPoints; set => _talentsPoints = value; }
    public int TraitPoints { get => _traitPoints; set => _traitPoints = value; }
    public string Name { get => _character.Name; set => _character.Name = value; }
    public string Description { get => _character.Description; set => _character.Description = value; }
    public int PsyPowerPoints { get => _psyPowerPoints; set => _psyPowerPoints = value; }

    public int PsyRating { get => _psyRating; set => _psyRating = value; }

    public string TypeMinion => _character.TypeMinion;

    public void UpgradeSkill(Skill skill, int cost)
    {
        _skills.Add(new Skill(skill, skill.LvlLearned + 1));
        _skillPoints -= cost;
    }

    public void UpgradeTalent(Talent talent, int cost = 1)
    {
        _talents.Add(talent);
        _talentsPoints -= cost;
    }

    public void UpgradePsyPower(PsyPower power)
    {
        _psyPowers.Add(power);
        _psyPowerPoints--;
    }

    public void UpgradeTrait(TraitWithCost trait, int cost = 1)
    {
        if(trait != null)
        {
            _traits.Add(trait);
            _traitPoints -= cost;
        }        
    }

    private List<TraitWithCost> GetTraits()
    {
        List<TraitWithCost> traits = new List<TraitWithCost>(_character.Traits);

        List<TraitWithCost> unionTraits = new List<TraitWithCost>();

        unionTraits.AddRange(_traits);

        foreach (TraitWithCost trait in traits)
            if (TryNotDouble(_traits, trait.Name))
                unionTraits.Add(trait);
        return unionTraits;
    }

    private List<Skill> GetSkills()
    {
        List<Skill> skills = new List<Skill>(_character.Skills);

        List<Skill> unionSkills = new List<Skill>();

        unionSkills.AddRange(_skills);

        foreach (Skill skill in skills)
            if (TryNotDouble(_skills, skill.Name))
                unionSkills.Add(skill);

        return unionSkills;
    }

    private List<Talent> GetTalents()
    {
        List<Talent> talents = new List<Talent>(_character.Talents);

        List<Talent> unionTalent = new List<Talent>();

        unionTalent.AddRange(_talents);

        foreach (Talent talent in talents)
            if (TryNotDouble(_talents, talent.Name))
                unionTalent.Add(talent);

        return unionTalent;
    }

    private List<PsyPower> GetPsyPowers()
    {
        List<PsyPower> psyPowers = new List<PsyPower>(_character.PsyPowers);

        List<PsyPower> unionPsyPowers = new List<PsyPower>();

        unionPsyPowers.AddRange(_psyPowers);

        foreach (PsyPower psyPower in psyPowers)
        {
            if (TryNotDouble(_psyPowers, psyPower.Name))
                unionPsyPowers.Add(psyPower);
        }

        return unionPsyPowers;
    }

    private bool TryNotDouble<T>(List<T> listCharacter, string name) where T : IName
    {
        foreach (T t in listCharacter)        
            if (string.Compare(name, t.Name, true) == 0)
                return false;        
            
        return true;
    }
}
