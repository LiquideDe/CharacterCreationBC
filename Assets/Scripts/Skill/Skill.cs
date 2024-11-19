using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Skill : IName
{
    protected string _name;
    private int _lvlLearned;
    protected bool _isKnowledge;
    private string _description, _typeSkill, _god;
    public string Description => _description; 
    public string Name => _name;
    public int LvlLearned { get => _lvlLearned; set => _lvlLearned = value; }
    public bool IsKnowledge  => _isKnowledge; 
    public string TypeSkill => _typeSkill;

    public string God => _god;

    public Skill(JSONSkillLoader skillLoader,string typeSkill)
    {
        _name = skillLoader.name;
        _typeSkill = typeSkill;
        _description = skillLoader.description;
        _god = skillLoader.god;
    }

    public Skill(JSONSkillLoader skillLoader, string typeSkill, int lvl)
    {
        _name = skillLoader.name;
        _typeSkill = typeSkill;
        _description = skillLoader.description;
        _god = skillLoader.god;
        _lvlLearned = lvl;
    }

    public Skill(Skill skill, int lvlLearned)
    {
        _name = skill.Name;
        _lvlLearned = lvlLearned;
        _typeSkill = skill.TypeSkill;
        _isKnowledge = skill.IsKnowledge;
        _description = skill.Description;
        _god = skill.God;
    }

    public Skill(string name, int lvl)
    {
        _name = name;
        _lvlLearned = lvl;
    }

}
