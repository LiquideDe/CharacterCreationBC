using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Archetype : BackgroundCharacter, IHistoryCharacter
{
    private int _wounds, _expForPsy, _psyRate;
    private Trait _trait;
    private bool _isSpaceMarine;    

    public Archetype(string path, CreatorSkills creatorSkills, CreatorTalents creatorTalents, CreatorTraits creatorTraits) :
        base(path, creatorSkills, creatorTalents, creatorTraits)
    {
        _trait = new Trait(GameStat.ReadText(path + "/Название Трейта.txt"), GameStat.ReadText(path + "/Описание Трейта.txt"));
        if (Directory.Exists(path + "/Req"))
        {
            if(string.Compare(GameStat.ReadText(path + "/Req/Race.txt"),"Космодесантник") == 0)
                _isSpaceMarine = true;
            else _isSpaceMarine = false;
        }
            
        if (Directory.Exists(path + "/Get"))
        {
            if (Directory.Exists(path + "/Get/Characteristics"))
                ModifierCharacteristic(path + "/Get/Characteristics");

            if (Directory.Exists(path + "/Get/Skills"))
                AddSkills(path + "/Get/Skills");

            if (Directory.Exists(path + "/Get/Traits"))
                AddTrait(path + "/Get/Traits");

            if (Directory.Exists(path + "/Get/Talents"))
                AddTalent(path + "/Get/Talents");

            if (File.Exists(path + "/Get/Experience.txt"))
                int.TryParse(GameStat.ReadText(path + "/Get/Experience.txt"), out _expForPsy);

            if (Directory.Exists(path + "/Get/Equipments"))
                AddEquipments(path + "/Get/Equipments");

            if (Directory.Exists(path + "/Get/Implants"))
                AddImplant(path + "/Get/Implants");

            if (File.Exists(path + "/Get/Wounds.txt"))
                int.TryParse(GameStat.ReadText(path + "/Get/Wounds.txt"), out _wounds);

            if (File.Exists(path + "/Get/Psyrate.txt"))
                int.TryParse(GameStat.ReadText(path + "/Get/Psyrate.txt"), out _psyRate);
        }
    }

    public int Wounds => _wounds;
    public int ExpForPsy => _expForPsy;
    public int PsyRate => _psyRate;
    public Trait Trait => _trait;

    public bool IsSpaceMarine => _isSpaceMarine;
    
    
}
