using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Race : BackgroundCharacter, IHistoryCharacter
{
    private string _god = "Неделимый";
    private int _amountCubes;
    private int _startCharacteristic;
    private List<IName> _foreverFriendly = new List<IName>();
    private List<IName> _foreverHostile = new List<IName>();

    private int _experienceCost = 0;
    private bool _canChangeGod = true;

    public Race(string path, CreatorSkills creatorSkills, CreatorTalents creatorTalents, CreatorTraits creatorTraits) : 
        base(path, creatorSkills, creatorTalents, creatorTraits)
    {


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
                AddSkills(path + "/Get/Skills");

            if (Directory.Exists(path + "/Get/Traits"))
                AddTrait(path + "/Get/Traits");

            if (Directory.Exists(path + "/Get/Talents"))
                AddTalent(path + "/Get/Talents");

            if (Directory.Exists(path + "/Get/Equipments"))
                AddEquipments(path + "/Get/Equipments");

            if (Directory.Exists(path + "/Get/Implants"))
                AddImplant(path + "/Get/Implants");

            if (File.Exists(path + "/Get/Experience.txt"))
                int.TryParse(GameStat.ReadText(path + "/Get/Experience.txt"), out _experienceCost);

            if(File.Exists(path + "/Get/God.txt"))
            {
                _canChangeGod = false;
                _god = GameStat.ReadText(path + "/Get/God.txt");
            }

        }
    }

   
    public string God => _god;
    public int ExperienceCost => _experienceCost;

    public List<IName> ForeverFriendly => _foreverFriendly;
    public List<IName> ForeverHostile => _foreverHostile;    

    public int StartCharacteristic => _startCharacteristic; 
    public int AmountCubes => _amountCubes;

    public bool CanChangeGod => _canChangeGod; 

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

    
}
