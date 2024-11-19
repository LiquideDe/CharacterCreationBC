using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FirstCharacterSheet : CharacterSheetWithCharacteristics
{
    [SerializeField] private SkillList[] skillSquares;
    [SerializeField]
    private TextMeshProUGUI textName, textArchetype, textPride, textMotivation, textDisgrace, textDescription, textTalents;

    public override void Initialize(ICharacter character)
    {
        base.Initialize(character);
        gameObject.SetActive(true);
        _character = character;
        textName.text = character.Name;
        textArchetype.text = character.Archetype;
        textPride.text = character.Pride;
        textMotivation.text = character.Motivation;
        textDisgrace.text = character.Disgrace;
        textDescription.text = character.Description;

        
        foreach (Talent talent in character.Talents)        
                textTalents.text += $", {talent.Name}";  
        
        foreach (Trait feature in character.Traits)
        {
            if(feature.Lvl > 0)
            {
                textTalents.text += $", {feature.Name}({feature.Lvl})";
            }
            else
            {
                textTalents.text += $", {feature.Name}";
            }
            
        }
        char[] myChar = { ' ', ',' };
        textTalents.text = textTalents.text.TrimStart(myChar);

        foreach (Skill skill in character.Skills)
        {
            if(skill.LvlLearned > 0)
            {
                ActivateSquare(skill);
            }
        }

        StartScreenshot(PageName.First.ToString());
    }

    private void ActivateSquare(Skill skill)
    {
        if(!skill.IsKnowledge)
        {
            foreach (SkillList skillList in skillSquares)
            {
                if (string.Compare(skillList.SkillName, skill.Name, true) == 0)
                {

                    skillList.SetLvlLearned(skill.LvlLearned);
                    break;
                }
            }
        }
        else
        {
            foreach (SkillList skillList in skillSquares)
            {
                if (string.Compare(skillList.SkillName, skill.TypeSkill, true) == 0)
                {
                    if (skillList.KnowledgeTextName.Length == 0)
                    {
                        skillList.KnowledgeTextName = skill.Name;
                        skillList.SetLvlLearned(skill.LvlLearned);
                        break;
                    }
                }
            }
        }        
    }

    
}
