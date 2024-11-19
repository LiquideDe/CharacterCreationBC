using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BackgroundCharacter 
{
    private string _name, _path, _description;
    private List<List<Trait>> _traits = new List<List<Trait>>();
    private List<List<Skill>> _skills = new List<List<Skill>>();
    private List<List<Talent>> _talents = new List<List<Talent>>();
    private List<List<Trait>> _modifierCharacteristics = new List<List<Trait>>();

    private List<List<Equipment>> _equipments = new List<List<Equipment>>();
    private List<List<MechImplant>> _implants = new List<List<MechImplant>>();
    CreatorSkills _creatorSkills;
    CreatorTalents _creatorTalents;
    CreatorTraits _creatorTraits;

    public BackgroundCharacter(string path, CreatorSkills creatorSkills, CreatorTalents creatorTalents, CreatorTraits creatorTraits)
    {
        _path = path;
        _name = GameStat.ReadText(path + "/Название.txt");
        _description = GameStat.ReadText(path + "/Описание.txt");
        _creatorSkills = creatorSkills;
        _creatorTalents = creatorTalents;
        _creatorTraits = creatorTraits;

    }

    public string Name => _name;
    public string Path => _path;
    public string Description => _description;
    public List<List<Trait>> ModifierCharacteristics => _modifierCharacteristics;
    public List<List<Trait>> Traits => _traits;
    public List<List<Skill>> Skills => _skills;
    public List<List<Talent>> Talents => _talents;
    public List<List<Equipment>> Equipments => _equipments;

    public List<List<MechImplant>> Implants => _implants;

    protected void ModifierCharacteristic(string path)
    {
        string[] dirs = Directory.GetDirectories(path);

        foreach (string dir in dirs)
        {
            _modifierCharacteristics.Add(new List<Trait>());
            string[] files = Directory.GetFiles(dir, "*.JSON");
            string[] jSonData = File.ReadAllLines(files[0]);
            JSONSmallSkillLoader readerName = JsonUtility.FromJson<JSONSmallSkillLoader>(jSonData[0]);
            _modifierCharacteristics[^1].Add(new Trait(readerName.name, readerName.lvl));
        }
    }


    protected void AddSkills(string path)
    {
        string[] dirs = Directory.GetDirectories(path);

        foreach (string dir in dirs)
        {
            _skills.Add(new List<Skill>());
            string[] files = Directory.GetFiles(dir, "*.JSON");
            foreach (string file in files)
            {
                string[] jSonData = File.ReadAllLines(file);
                JSONSmallSkillLoader readerName = JsonUtility.FromJson<JSONSmallSkillLoader>(jSonData[0]);
                //Debug.Log($"Ищем {readerName.name} {readerName.lvl} {_name}");
                if (_creatorSkills.IsSkillKnowledge(readerName.name))
                {
                    Knowledge knowledge = (Knowledge)_creatorSkills.GetSkill(readerName.name);
                    _skills[^1].Add(new Knowledge(knowledge, readerName.lvl));
                }
                else
                    _skills[^1].Add(new Skill(_creatorSkills.GetSkill(readerName.name), readerName.lvl));
            }
        }
    }

    protected void AddTalent(string path)
    {
        string[] dirs = Directory.GetDirectories(path);

        foreach (string dir in dirs)
        {
            _talents.Add(new List<Talent>());
            string[] files = Directory.GetFiles(dir, "*.JSON");
            foreach (string file in files)
            {
                string[] jSonData = File.ReadAllLines(file);
                JSONReaderName readerName = JsonUtility.FromJson<JSONReaderName>(jSonData[0]);
                //Debug.Log($"Ищем {readerName.name} {_name}");
                _talents[^1].Add(new Talent(_creatorTalents.GetTalent(readerName.name)));
            }
        }
    }

    protected void AddTrait(string path)
    {
        string[] dirs = Directory.GetDirectories(path);

        foreach (string dir in dirs)
        {
            _traits.Add(new List<Trait>());
            string[] files = Directory.GetFiles(dir, "*.JSON");
            foreach (string file in files)
            {
                string[] jSonData = File.ReadAllLines(file);
                JSONSmallSkillLoader readerName = JsonUtility.FromJson<JSONSmallSkillLoader>(jSonData[0]);
                //Debug.Log($"Ищем {readerName.name} {readerName.lvl} {_name}");
                _traits[^1].Add(new Trait(_creatorTraits.GetTrait(readerName.name), readerName.lvl));
            }
        }
    }

    protected void AddImplant(string path)
    {
        string[] dirs = Directory.GetDirectories(path);

        foreach (string dir in dirs)
        {
            _implants.Add(new List<MechImplant>());
            string[] files = Directory.GetFiles(dir, "*.JSON");
            foreach (string file in files)
            {
                string[] jSonData = File.ReadAllLines(file);
                SaveLoadImplant readerName = JsonUtility.FromJson<SaveLoadImplant>(jSonData[0]);
                _implants[^1].Add(new MechImplant(readerName));
            }
        }
    }

    protected void AddEquipments(string path)
    {
        string[] dirs = Directory.GetDirectories(path);

        foreach (string dir in dirs)
        {
            _equipments.Add(new List<Equipment>());
            string[] files = Directory.GetFiles(dir, "*.JSON");
            foreach (string file in files)
            {
                string[] jSonData = File.ReadAllLines(file);
                JSONTypeReader readerType = JsonUtility.FromJson<JSONTypeReader>(jSonData[0]);
                if (readerType.typeEquipment == Equipment.TypeEquipment.Melee.ToString())
                {
                    JSONMeleeReader reader = JsonUtility.FromJson<JSONMeleeReader>(jSonData[0]);
                    _equipments[^1].Add(new Weapon(reader));
                }
                else if (readerType.typeEquipment == Equipment.TypeEquipment.Range.ToString())
                {
                    JSONRangeReader reader = JsonUtility.FromJson<JSONRangeReader>(jSonData[0]);
                    _equipments[^1].Add(new Weapon(reader));
                }
                else if (readerType.typeEquipment == Equipment.TypeEquipment.Grenade.ToString())
                {
                    JSONGrenadeReader reader = JsonUtility.FromJson<JSONGrenadeReader>(jSonData[0]);
                    _equipments[^1].Add(new Weapon(reader));
                }
                else if (readerType.typeEquipment == Equipment.TypeEquipment.Armor.ToString())
                {
                    JSONArmorReader reader = JsonUtility.FromJson<JSONArmorReader>(jSonData[0]);
                    _equipments[^1].Add(new Armor(reader));
                }
                else if (readerType.typeEquipment == Equipment.TypeEquipment.Thing.ToString())
                {
                    JSONEquipmentReader reader = JsonUtility.FromJson<JSONEquipmentReader>(jSonData[0]);
                    _equipments[^1].Add(new Equipment(reader.name, reader.description, reader.rarity, reader.amount, reader.weight));
                }

            }
        }
    }
}
