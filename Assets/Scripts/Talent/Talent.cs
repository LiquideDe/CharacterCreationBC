using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Talent : ISkillTalentEtcForList, IName
{
    private string _name;
    private string _textDescription, _shortDescription, _listOfRequrements;
    private List<Characteristic> _requirementCharacteristics = new List<Characteristic>();
    private List<Skill> _requirementSkills = new List<Skill>();
    private List<MechImplant> _requirementImplants = new List<MechImplant>();
    private List<Talent> _requirementTalents = new List<Talent>();
    private int _requirementPsyRate;
    private int _requirementInsanity = 0, _requirementCorruption = 0;
    private bool _isRepeatable, _isCanTaken;
    private int _rank;
    private string _god, _category;

    public Talent(string path, string category)
    {
        
        string[] data = File.ReadAllLines(path + "/Param.JSON");
        _category = category;
        JSONTalentReader talentReader = JsonUtility.FromJson<JSONTalentReader>(data[0]);
        _god = talentReader.god;
        _name = talentReader.name;
        _rank = talentReader.rank;
        _isCanTaken = talentReader.canActivate;
        _isRepeatable = talentReader.repeatable;
        _textDescription = GameStat.ReadText(path + "/��������.txt");
        _shortDescription = GameStat.ReadText(path + "/������� ��������.txt");
        if(Directory.Exists(path + "/Req"))
        {
            _listOfRequrements = "���������� ��� �������.\n";
            path += "/Req";
            if (Directory.Exists(path + "/Characteristics"))
            {
                _listOfRequrements += "��������������:";
                foreach (GameStat.CharacteristicName charName in Enum.GetValues(typeof(GameStat.CharacteristicName)))
                {                    
                    string searchFile = $"{path}/Characteristics/{charName}.txt";
                    if (File.Exists(searchFile))
                    {
                        _requirementCharacteristics.Add(new Characteristic(charName,int.Parse(GameStat.ReadText(searchFile))));
                        _listOfRequrements += $" {_requirementCharacteristics[^1].Name} - {_requirementCharacteristics[^1].Amount},";
                    }
                }
            }

            _listOfRequrements = CheckLastSymbol(_listOfRequrements);

            if(Directory.Exists(path + "/Skills"))
            {
                _listOfRequrements += "������:";
                string[] files = Directory.GetFiles(path + "/Skills", "*.JSON");
                foreach(string file in files)
                {
                    string[] jSonData = File.ReadAllLines(file);
                    JSONSmallSkillLoader jSONSmall = JsonUtility.FromJson<JSONSmallSkillLoader>(jSonData[0]);
                    _requirementSkills.Add(new Skill(jSONSmall.name, jSONSmall.lvl));
                    _listOfRequrements += $" {_requirementSkills[^1].Name} - {_requirementSkills[^1].LvlLearned},";
                }
            }

            _listOfRequrements = CheckLastSymbol(_listOfRequrements);
            if (File.Exists(path + "/ReqImplants.txt"))
            {
                string textImplants = GameStat.ReadText(path + "/ReqImplants.txt");
                var implants = textImplants.Split(new char[] { '/' }).ToList();
                _listOfRequrements += " ��������:";
                foreach (string implant in implants)
                {
                    _requirementImplants.Add(new MechImplant(implant));
                    _listOfRequrements += $" {_requirementImplants[^1].Name},";
                }
            }

            _listOfRequrements = CheckLastSymbol(_listOfRequrements);

            if (File.Exists(path + "/ReqTalents.txt"))
            {
                string textTalents = GameStat.ReadText(path + "/ReqTalents.txt");
                var talents = textTalents.Split(new char[] { '/' }).ToList();
                _listOfRequrements += "�������:";
                foreach (string talent in talents)
                {
                    _requirementTalents.Add(new Talent(talent));
                    _listOfRequrements += $" {_requirementTalents[^1].Name},";
                }
            }
            _listOfRequrements = CheckLastSymbol(_listOfRequrements);

            if (File.Exists(path + "/ReqCorruption.txt"))
            {
                _requirementCorruption = int.Parse(GameStat.ReadText(path + "/ReqCorruption.txt"));
                _listOfRequrements += $" ����� ����� {_requirementCorruption}.";
            }

            if (File.Exists(path + "/ReqInsanity.txt"))
            {
                _requirementInsanity = int.Parse(GameStat.ReadText(path + "/ReqInsanity.txt"));
                _listOfRequrements += $" ����� ������� {_requirementInsanity}.";
            }

            if(File.Exists(path + "/ReqPsy.txt"))
            {
                _requirementPsyRate = int.Parse(GameStat.ReadText(path + "/ReqPsy.txt"));
                _listOfRequrements += $" ��� ������� {_requirementPsyRate}.";
            }
            else
            {
                _requirementPsyRate = 0;
            }
        }

    }

    public string Name => _name;
    public string Description => _textDescription;
    public string ListRequirments => _listOfRequrements;
    public string ShortDescription => _shortDescription; 
    public bool IsCanTaken => _isCanTaken;
    public bool IsRepeatable => _isRepeatable;

    public List<Characteristic> RequirementCharacteristics => _requirementCharacteristics;

    public List<Skill> RequirementSkills => _requirementSkills;

    public List<MechImplant> RequirementImplants => _requirementImplants;

    public List<Talent> RequirementTalents => _requirementTalents;

    public int RequirementPsyRate => _requirementPsyRate; 
    public int RequirementInsanity => _requirementInsanity;
    public int RequirementCorruption => _requirementCorruption;

    public int Rank => _rank;

    public string God => _god;

    public string Category => _category;

    
    public Talent(string name)
    {
        _name = name;
    }

    public Talent(Talent talent)
    {
        _textDescription = talent.Description;
        _shortDescription = talent.ShortDescription;
        _name = talent.Name;
        _isCanTaken = talent.IsCanTaken;
        _isRepeatable = talent.IsRepeatable;
        _god = talent.God;
        _category = talent.Category;
        _rank = talent.Rank;
    }

    public bool CheckTalentRepeat(List<Talent> talentsOfCharacter)
    {
        if (!_isRepeatable)
        {
            foreach (Talent talent in talentsOfCharacter)
            {
                if (string.Compare(talent.Name, _name, true) == 0)
                {
                    return false;
                }
            }
        }
        
        return true;
    }

    private string CheckLastSymbol(string text)
    {
        if (text.EndsWith(","))
        {
            string tempstring = text.TrimEnd(',');
            return tempstring + ". ";
        }
        else
        {
            return text;
        }
    }


}
