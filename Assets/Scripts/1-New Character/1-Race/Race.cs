using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Race
{
    private string _name, _path;
    private string _description, _god = "Неделимый";
    private int _amountCubes;
    private int _startCharacteristic;
    private List<IName> _foreverFriendly = new List<IName>();
    private List<IName> _foreverHostile = new List<IName>();

    private List<List<Trait>> _modifierCharacteristic = new List<List<Trait>>();
    private List<List<Trait>> _traits = new List<List<Trait>>();
    private List<List<Skill>> _skills = new List<List<Skill>>();
    private List<List<Talent>> _talents = new List<List<Talent>>();

    private int _experienceCost = 0;
    private bool _canChangeGod = true;

    public Race(string path, CreatorSkills creatorSkills, CreatorTalents creatorTalents, CreatorTraits creatorTraits)
    {
        _path = path;
        _name = GameStat.ReadText(path + "/Название.txt");
        _description = GameStat.ReadText(path + "/Описание.txt");
        int.TryParse(GameStat.ReadText(path + "/Количество кубов.txt"), out _amountCubes);
        int.TryParse(GameStat.ReadText(path + "/Стартовые Характеристики.txt"), out _startCharacteristic);

        if (Directory.Exists(path + "/Get"))
        {
            if (Directory.Exists(path + "/Get/AddFriendly"))
                AddFriendlyHostile(path + "/Get/AddFriendly", _foreverFriendly, creatorSkills, creatorTalents);

            if(Directory.Exists(path + "/Get/AddHostile"))
                AddFriendlyHostile(path + "/Get/AddHostile", _foreverHostile, creatorSkills, creatorTalents);

            if (Directory.Exists(path + "/Get/Characteristics"))
                ModifierCharacteristic(path + "/Get/Characteristics");

            if (Directory.Exists(path + "/Get/Skills"))
                AddSkills(path + "/Get/Skills", creatorSkills);
        }
    }

    public string Name => _name;
    public string Path => _path;
    public string Description => _description;
    public string God => _god;

    private void AddFriendlyHostile(string path, List<IName> names, CreatorSkills creatorSkills, CreatorTalents creatorTalents)
    {
        string[] files = Directory.GetFiles(path, "*.JSON");
        foreach (string file in files)
        {
            string[] jSonData = File.ReadAllLines(file);
            JSONReaderName readerName = JsonUtility.FromJson<JSONReaderName>(jSonData[0]);

            Skill skill = null;
            Talent talent = null;

            skill = creatorSkills.GetSkill(readerName.name);
            talent = creatorTalents.GetTalent(readerName.name);

            if (skill != null)
                names.Add(new Skill(skill, 0));

            if (talent != null)
                names.Add(new Talent(talent));

        }
    }

    private void ModifierCharacteristic(string path)
    {
        string[] dirs = Directory.GetDirectories(path);

        foreach (string dir in dirs) 
        {
            _modifierCharacteristic.Add(new List<Trait>());
            string[] files = Directory.GetFiles(path, "*.JSON");
            string[] jSonData = File.ReadAllLines(files[0]);
            JSONSmallSkillLoader readerName = JsonUtility.FromJson<JSONSmallSkillLoader>(jSonData[0]);
            _modifierCharacteristic[^1].Add(new Trait(readerName.name, readerName.lvl));
        }
    }


    private void AddSkills(string path, CreatorSkills creatorSkills)
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
                _skills[^1].Add(new Skill(creatorSkills.GetSkill(readerName.name), readerName.lvl));
            }
        }
    }

    private void AddTalent(string path, CreatorTalents creatorTalents)
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
                _talents[^1].Add(new Talent(creatorTalents.GetTalent(readerName.name)));
            }
        }
    }

    private void AddTrait(string path, CreatorTraits creatorTraits)
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
                _traits[^1].Add(new Trait(creatorTraits.GetTrait(readerName.name), readerName.lvl));
            }
        }
    }
}
