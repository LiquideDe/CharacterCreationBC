using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TypeMinion : IName
{
    private string _name;
    private List<TraitWithCost> _traits = new List<TraitWithCost>();
    private TraitWithCost _requiredTrait;
    private CreatorTraits _creatorTraits;
    private List<Trait> _upperLimitCharacteristics = new List<Trait>();
    private List<Trait> _lowerLimitCharacteristics = new List<Trait>();
    private int _characteristicPoints, _maxCharacteristic, _skillPoints, _maxSkillLvl, _amountSkillsDoublePrice, _talentsPoints, _maxTalentLvl, _traitPoints;
    private List<Armor> _armors = new List<Armor>();
    private List<Weapon> _weapons = new List<Weapon>();

    public TypeMinion(string path, CreatorTraits creatorTraits)
    {
        string[] data = File.ReadAllLines(path + "/Parameters.JSON");
        _creatorTraits = creatorTraits;
        JSONReaderMinion readerMinion = JsonUtility.FromJson<JSONReaderMinion>(data[0]);
        _name = readerMinion.name;
        _characteristicPoints = readerMinion.characteristicPoints;
        _maxCharacteristic = readerMinion.maxCharacteristic;
        _skillPoints = readerMinion.skillPoints;
        _maxSkillLvl = readerMinion.maxSkillLvl;
        _amountSkillsDoublePrice = readerMinion.amountSkillsDoublePrice;
        _talentsPoints = readerMinion.talentsPoints;
        _maxTalentLvl = readerMinion.maxTalentLvl;
        _traitPoints = readerMinion.traitPoints;

        if (Directory.Exists(path + "/Max"))
        {
            List<string> files = new List<string>();
            files.AddRange(Directory.GetFiles(path + "/Max", "*.JSON"));
            foreach (var item in files)
            {
                string[] jSonData = File.ReadAllLines(item);
                JSONSmallSkillLoader skillLoader = JsonUtility.FromJson<JSONSmallSkillLoader>(jSonData[0]);
                Trait trait = new Trait(skillLoader.name, skillLoader.lvl);
                _upperLimitCharacteristics.Add(trait);
            }
        }

        if(Directory.Exists(path + "/Min"))
        {
            List<string> files = new List<string>();
            files.AddRange(Directory.GetFiles(path + "/Min", "*.JSON"));
            foreach (var item in files)
            {
                string[] jSonData = File.ReadAllLines(item);
                JSONSmallSkillLoader skillLoader = JsonUtility.FromJson<JSONSmallSkillLoader>(jSonData[0]);
                Trait trait = new Trait(skillLoader.name, skillLoader.lvl);
                _lowerLimitCharacteristics.Add(trait);
            }
        }

        if (Directory.Exists(path + "/Traits"))
        {
            List<string> files = new List<string>();
            files.AddRange(Directory.GetFiles(path + "/Traits", "*.JSON"));
            foreach (var item in files)
            {                
                string[] jSonData = File.ReadAllLines(item);
                JSONReaderTraitWithCost traitReader = JsonUtility.FromJson<JSONReaderTraitWithCost>(jSonData[0]);
                TraitWithCost trait = new TraitWithCost(_creatorTraits.GetTrait(traitReader.name), traitReader);
                _traits.Add(trait);
            }

            if(Directory.Exists(path + "/Traits/Get"))
            {
                List<string> filesTrait = new List<string>();
                filesTrait.AddRange(Directory.GetFiles(path + "/Traits/Get", "*.JSON"));
                foreach (var item in filesTrait)
                {
                    string[] jSonData = File.ReadAllLines(item);
                    JSONReaderTraitWithCost traitWithCost = JsonUtility.FromJson<JSONReaderTraitWithCost>(jSonData[0]);
                    _requiredTrait = new TraitWithCost(_creatorTraits.GetTrait(traitWithCost.name), traitWithCost);
                }
            }
        }

        if (Directory.Exists(path + "/Equipments"))
        {
            if(Directory.Exists(path + "/Equipments/Armor"))
            {
                AddEquipment(path + "/Equipments/Armor");
            }
            if (Directory.Exists(path + "/Equipments/Weapon"))
            {
                AddEquipment(path + "/Equipments/Weapon");
            }
        }
    }

    public string Name => _name;

    public TraitWithCost RequiredTrait => _requiredTrait;
    public List<Trait> UpperLimitCharacteristics => _upperLimitCharacteristics; 
    public List<Trait> LowerLimitCharacteristics => _lowerLimitCharacteristics; 
    public int CharacteristicPoints => _characteristicPoints; 
    public int MaxCharacteristic => _maxCharacteristic;
    public int SkillPoints => _skillPoints;
    public int MaxSkillLvl => _maxSkillLvl; 
    public int AmountSkillsDoublePrice => _amountSkillsDoublePrice;
    public int TalentsPoints => _talentsPoints;
    public int MaxTalentLvl => _maxTalentLvl;
    public int TraitPoints => _traitPoints;
    public List<TraitWithCost> Traits => _traits;
    public List<Armor> Armors => _armors;
    public List<Weapon> Weapons => _weapons;

    private void AddEquipment(string path)
    {
        List<string> files = new List<string>();
        files.AddRange(Directory.GetFiles(path, "*.JSON"));
        foreach (var item in files)
        {
            string[] jSonData = File.ReadAllLines(item);
            JSONTypeReader typeReader = JsonUtility.FromJson<JSONTypeReader>(jSonData[0]);
            if (string.Compare(typeReader.typeEquipment, "Armor", true) == 0)
            {
                JSONArmorReader armorReader = JsonUtility.FromJson<JSONArmorReader>(jSonData[0]);
                _armors.Add(new Armor(armorReader));
            }
            else if (string.Compare(typeReader.typeEquipment, "Melee", true) == 0)
            {
                JSONMeleeReader meleeReader = JsonUtility.FromJson<JSONMeleeReader>(jSonData[0]);
                _weapons.Add(new Weapon(meleeReader));
            }
            else if (string.Compare(typeReader.typeEquipment, "Range", true) == 0)
            {
                JSONRangeReader rangeReader = JsonUtility.FromJson<JSONRangeReader>(jSonData[0]);
                _weapons.Add(new Weapon(rangeReader));
            }
            else if (string.Compare(typeReader.typeEquipment, "Grenade", true) == 0)
            {
                JSONGrenadeReader grenadeReader = JsonUtility.FromJson<JSONGrenadeReader>(jSonData[0]);
                _weapons.Add(new Weapon(grenadeReader));
            }
        }
    }
}
