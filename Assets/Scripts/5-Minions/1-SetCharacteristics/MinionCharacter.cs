using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCharacter : IMinion
{
    private List<TraitWithCost> _traits = new List<TraitWithCost>();
    private string _name, _description;
    protected List<Skill> _skills = new List<Skill>();
    protected List<Talent> _talents = new List<Talent>();
    protected List<PsyPower> _psyPowers = new List<PsyPower>();
    private int _characteristicPoints, _skillPoints, _talentsPoints, _traitPoints, _psyPowerPoints;
    private List<Armor> _armors = new List<Armor>();
    private List<Weapon> _weapons = new List<Weapon>();
    private List<Characteristic> _characteristics;
    private int _skillsOverPrice, _psyRating;
    private string _typeMinion;

    public MinionCharacter(TypeMinion minion)
    {
        _characteristics = new List<Characteristic>()
        {
            new Characteristic(GameStat.CharacteristicName.WeaponSkill, "Неделимый"), //0
        new Characteristic(GameStat.CharacteristicName.BallisticSkill, "Неделимый"), //1
        new Characteristic(GameStat.CharacteristicName.Strength, "Кхорн"), //2
        new Characteristic(GameStat.CharacteristicName.Toughness, "Нургл"), //3
        new Characteristic(GameStat.CharacteristicName.Agility, "Неделимый"), //4
        new Characteristic(GameStat.CharacteristicName.Intelligence, "Неделимый"), //5
        new Characteristic(GameStat.CharacteristicName.Perception, "Неделимый"), //6
        new Characteristic(GameStat.CharacteristicName.Willpower, "Тзинч"), //7
        new Characteristic(GameStat.CharacteristicName.Fellowship, "Слаанеш"), //8
        };
        _characteristicPoints = minion.CharacteristicPoints;
        _skillPoints = minion.SkillPoints;
        _talentsPoints = minion.TalentsPoints;
        _traitPoints = minion.TraitPoints;
        _skillsOverPrice = minion.AmountSkillsDoublePrice;
        _typeMinion = minion.Name;

        if (minion.RequiredTrait != null)
        {
            _traits.Add(minion.RequiredTrait);
            _traitPoints -= minion.RequiredTrait.Cost;
        }

        if(minion.LowerLimitCharacteristics.Count > 0)
            foreach (var item in minion.LowerLimitCharacteristics)
            {
                _characteristics[GameStat.CharacteristicToInt[item.Name]].Amount += item.Lvl;
                _characteristicPoints -= item.Lvl;
            }

    }

    public IMinion Character => this;
    public int CharacteristicPoints => _characteristicPoints; 
    public int SkillPoints => _skillPoints; 
    public int TalentsPoints => _talentsPoints; 
    public int TraitPoints => _traitPoints;
    public List<Armor> Armors => _armors; 
    public List<Weapon> Weapons => _weapons;

    public List<PsyPower> PsyPowers => _psyPowers;

    public List<Skill> Skills => _skills;

    public List<Talent> Talents => _talents;

    public List<TraitWithCost> Traits => _traits;

    public IMinion Minion => this;

    public List<Characteristic> Characteristics => _characteristics;

    public int TalentPoints => _talentsPoints;

    public string Name => _name;

    public string Description => _description;

    int IMinion.CharacteristicPoints { get => _characteristicPoints; set => _characteristicPoints = value; }
    int IMinion.SkillPoints { get => _skillPoints; set => _skillPoints = value; }
    int IMinion.TalentPoints { get => _talentsPoints; set => _talentsPoints = value; }
    int IMinion.TraitPoints { get => _traitPoints; set => _traitPoints = value; }
    string IMinion.Name { get => _name; set => _name = value; }
    string IMinion.Description { get => _description; set => _description = value; }
    public int SkillsOverPrice { get => _skillsOverPrice; set => _skillsOverPrice = value; }
    public int PsyPowerPoints { get => _psyPowerPoints; set => _psyPowerPoints = value; }

    public int PsyRating => _psyRating;

    public string TypeMinion => _typeMinion;

    public void SetSkills(List<Skill> skills) => _skills = new List<Skill>(skills);

    
}
