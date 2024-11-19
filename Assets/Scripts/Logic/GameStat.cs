using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class GameStat
{
    private static readonly List<int> _characteristicCostWithTwo = new List<int>() { 100, 250, 500, 750, 1000 };
    private static readonly List<int> _characteristicCostWithOne = new List<int>() { 250, 500, 750, 1000, 1500 };
    private static readonly List<int> _characteristicCostWithZero = new List<int>() { 500, 750, 1000, 1500, 2500 };
    private static List<List<int>> _listWithCharacteristicCosts = new List<List<int>>() { _characteristicCostWithZero, _characteristicCostWithOne, _characteristicCostWithTwo };

    private static readonly List<int> _skillCostWithZero = new List<int>() {250,500,750,1000 };
    private static readonly List<int> _skillCostWithOne = new List<int>() {200,350,500,750 };
    private static readonly List<int> _skillCostWithTwo = new List<int>() {100,200,400,600 };
    private static readonly List<List<int>> _listWithSkillCosts = new List<List<int>>() 
    {
        _skillCostWithZero, _skillCostWithOne, _skillCostWithTwo
    };

    private static readonly List<int> _talentCostWithZero = new List<int>() {500, 750, 1000};
    private static readonly List<int> _talentCostWithOne = new List<int>() {250, 500, 750 };
    private static readonly List<int> _talentCostWithTwo = new List<int>() {200, 300, 400 };
    private static readonly List<List<int>> _listWithTalentCosts = new List<List<int>>()
    {
        _talentCostWithZero, _talentCostWithOne, _talentCostWithTwo
    };

    public static Dictionary<CharacteristicName, string> characterTranslate = new Dictionary<CharacteristicName, string>()
    {
        { CharacteristicName.WeaponSkill,"����� ����������" },
        { CharacteristicName.BallisticSkill,"����� ��������" },
        { CharacteristicName.Strength,"����" },
        { CharacteristicName.Toughness,"������������" },
        { CharacteristicName.Agility,"��������" },
        { CharacteristicName.Intelligence,"���������" },
        { CharacteristicName.Perception,"����������" },
        { CharacteristicName.Willpower,"���� ����" },
        { CharacteristicName.Fellowship,"�������������" }

    };

    public static Dictionary<string, int> CharacteristicToInt = new Dictionary<string, int>()
    {
        { "����� ����������", 0 },
        { "����� ��������", 1 },
        { "����" , 2},
        { "������������", 3 },
        { "��������", 4 },
        { "���������",5 },
        { "����������" , 6 },
        { "���� ����", 7 },
        { "�������������", 8 }

    };

    public static Dictionary<string, string> KnowledgeTranslate = new Dictionary<string, string>()
    {
        {"Trade", "�������" },
        {"CommonLore", "����� ������" },
        {"ForbiddenLore", "��������� ������" },
        {"ScholasticLore", "������ ������" },
        {"Linguistics", "�����������" }
    };

    public enum CharacteristicName { None, WeaponSkill, BallisticSkill, Strength, Toughness, Agility, Intelligence, Perception, Willpower, Fellowship}    

    public static string ReadText(string nameFile)
    {
        string txt;
        using (StreamReader _sw = new StreamReader(nameFile, Encoding.Default))
        {
            txt = (_sw.ReadToEnd());
            _sw.Close();
        }
        return txt;
    }

    public static int CalculateCostCharacteristic(Characteristic characteristic, ICharacter character) =>
        _listWithCharacteristicCosts[AmountMult(characteristic.Name, characteristic.God, 
            character.God, character.ForeverHostile, character.ForeverFriendly)][characteristic.LvlLearned];

    public static int CalculateCostSkill(Skill skill, ICharacter character) =>
    _listWithSkillCosts[AmountMult(skill.Name, skill.God, character.God, character.ForeverHostile, character.ForeverFriendly)][skill.LvlLearned];


    public static int CalculateCostTalent(Talent talent, ICharacter character) =>
        _listWithTalentCosts[AmountMult(talent.Name, talent.God, character.God, character.ForeverHostile, character.ForeverFriendly)][talent.Rank-1];

    private static int AmountMult(string nameSkill, string skillGod, string characterGod, List<IName> foreverHostile, List<IName> foreverFriendly)
    {
        int amountMult;
        foreach (IName name in foreverFriendly)
        {
            if (string.Compare(name.Name, nameSkill, true) == 0)
                return 2;
        }

        foreach (IName name in foreverHostile)
        {
            if (string.Compare(name.Name, nameSkill, true) == 0)
                return 0;
        }
        
        if (string.Compare(skillGod, "���������", true) == 0 || string.Compare(characterGod, "���������", true) == 0)
        {
            amountMult = 1;
        }
        else if(string.Compare(skillGod, characterGod, true) == 0)
        {
            amountMult = 2;
        }         
        else if (string.Compare(skillGod, "�����", true) == 0 && string.Compare(characterGod, "�����") == 0)
        {
            amountMult = 1;
        }
        else if (string.Compare(skillGod, "�����", true) == 0 && string.Compare(characterGod, "�����") == 0)
        {
            amountMult = 1;
        }
        else if (string.Compare(skillGod, "�������", true) == 0 && string.Compare(characterGod, "�����") == 0)
        {
            amountMult = 1;
        }
        else if (string.Compare(skillGod, "�����", true) == 0 && string.Compare(characterGod, "�������") == 0)
        {
            amountMult = 1;
        }
        else
            amountMult = 0;

        return amountMult;
    }
}
