using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWithUpgrade : CharacterDecorator, ICharacter
{
    private List<Characteristic> _characteristics;
    private int _experienceSpent, _experienceUnspent, _experienceTotal, _psyrate, _infamy;

    public CharacterWithUpgrade(ICharacter character) : base(character)
    {
        _experienceSpent = character.ExperienceSpent;
        _experienceUnspent = character.ExperienceUnspent;
        _experienceTotal = character.ExperienceTotal;
        _psyrate = character.PsyRating;
        _infamy = character.Infamy;

        _characteristics = new List<Characteristic>();
        foreach(Characteristic characteristic in _character.Characteristics)
        {
            _characteristics.Add(new Characteristic(characteristic));
        }
    }

    public int Wounds => _character.Wounds;

    public int CorruptionPoints => _character.CorruptionPoints;

    public int PsyRating => _psyrate;

    public int HalfMove => _character.HalfMove;

    public int FullMove => _character.FullMove;

    public int Natisk => _character.Natisk;

    public int Fatigue => _character.Fatigue;

    public float CarryWeight => _character.CarryWeight;

    public float LiftWeight => _character.LiftWeight;

    public float PushWeight => _character.PushWeight;

    public int ExperienceTotal => _experienceTotal;

    public int ExperienceUnspent => _experienceUnspent;

    public int ExperienceSpent => _experienceSpent;

    public List<Characteristic> Characteristics => _characteristics;

    public List<string> MentalDisorders => _character.MentalDisorders;

    public int Run => _character.Run;

    public List<string> Mutation => _character.Mutation;

    public int BonusToughness => _characteristics[3].Amount/10;

    public ICharacter GetCharacter => _character;

    public string Name => _character.Name;

    public List<IName> ForeverFriendly => _character.ForeverFriendly;

    public List<IName> ForeverHostile => _character.ForeverHostile;

    public string Race => _character.Race;

    public string Archetype => _character.Archetype;

    public List<string> EliteAcrhetypes => _character.EliteAcrhetypes;

    public string Motivation => _character.Motivation;

    public string Disgrace => _character.Disgrace;

    public string Pride => _character.Pride;

    public string Stereotype => _character.Stereotype;

    public bool CanChageGod => _character.CanChageGod;

    public string God => _character.God;

    public int Infamy => _infamy;

    public string Description => _character.Description;
    public string GodGifts => _character.GodGifts;

    public void UpgradeSkill(Skill upgradeSkill, int experienceSpentForSkill)
    {
        //Каждый новый апгрейд мы создаем новую оболочку и добавляем скил, а не апгрейдим его
        if (upgradeSkill.IsKnowledge)
        {            
            _skills.Add(new Knowledge((Knowledge)upgradeSkill, upgradeSkill.LvlLearned + 1));
        }
        else
            _skills.Add(new Skill(upgradeSkill, upgradeSkill.LvlLearned + 1));

        if(experienceSpentForSkill > 0)
            _upgrades.Add(new Trait(upgradeSkill.Name, upgradeSkill.LvlLearned + 1));

        _experienceSpent += experienceSpentForSkill;
        _experienceUnspent -= experienceSpentForSkill;
        _experienceTotal = _experienceSpent + _experienceUnspent;
    }

    public void UpgradeTalent(Talent talent, int cost)
    {
        if(cost > 0)
            _upgrades.Add(new Trait(talent.Name, 1));

        _talents.Add(new Talent(talent));        
        _experienceSpent += cost;
        _experienceUnspent -= cost;
        _experienceTotal = _experienceSpent + _experienceUnspent;
    }

    public void UpgradePsyPower(PsyPower psyPower, bool isEdit)
    {
        _psyPowers.Add(psyPower);
        if(isEdit == false)
        {
            _experienceSpent += psyPower.Cost;
            _experienceUnspent -= psyPower.Cost;
            _experienceTotal = _experienceSpent + _experienceUnspent;
        }        
    }

    public void UpgradePsyrate(int cost)
    {
        _psyrate++;
        _experienceSpent += cost;
        _experienceUnspent -= cost;
        _experienceTotal = _experienceSpent + _experienceUnspent;
    }

    public void UpgradeWeapon(int costExp) => UpgradeCharacteristic(_characteristics[0], costExp);
    public void UpgradeBallistic(int costExp) => UpgradeCharacteristic(_characteristics[1], costExp);
    public void UpgradeStrength(int costExp) => UpgradeCharacteristic(_characteristics[2], costExp);
    public void UpgradeToughness(int costExp) => UpgradeCharacteristic(_characteristics[3], costExp);
    public void UpgradeAgility(int costExp) => UpgradeCharacteristic(_characteristics[4], costExp);
    public void UpgradeIntelligence(int costExp) => UpgradeCharacteristic(_characteristics[5], costExp);
    public void UpgradePerception(int costExp) => UpgradeCharacteristic(_characteristics[6], costExp);
    public void UpgradeWillpower(int costExp) => UpgradeCharacteristic(_characteristics[7], costExp);
    public void UpgradeFellowship(int costExp) => UpgradeCharacteristic(_characteristics[8], costExp);

    public void SetExperience(int experience) => _experienceUnspent += experience;

    public void UpgradeInfamy()
    {
        if (_character.Infamy < 40)
        {
            _infamy += 5;
            _experienceSpent += 500;
            _experienceUnspent -= 500;
            _experienceTotal = _experienceSpent + _experienceUnspent;
        }
    }

    private void UpgradeCharacteristic(Characteristic characteristic, int costExp)
    {
        
        characteristic.UpgradeLvl();
        if(costExp > 0)
            _upgrades.Add(new Trait(characteristic.Name, characteristic.LvlLearned));

        Debug.Log($"cost exp = {costExp}, upgrades.count = {_upgrades.Count}");
        _experienceSpent += costExp;
        _experienceUnspent -= costExp;
        _experienceTotal = _experienceSpent + _experienceUnspent;
    }

    
}
