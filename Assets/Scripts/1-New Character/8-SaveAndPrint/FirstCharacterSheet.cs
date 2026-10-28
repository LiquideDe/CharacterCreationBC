using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;


public class FirstCharacterSheet : CharacterSheetWithCharacteristics
{
    [SerializeField] private SkillList[] skillSquares;
    [SerializeField]
    private TextMeshProUGUI textName, _textDescription;

    public void Initialize(ICharacter character)
    {
        base.Initialize(character.Characteristics, character.Equipments, character.Traits, character.Infamy);
        gameObject.SetActive(true);
        textName.text = character.Name;
        _textDescription.text = $"Архетип: <b>{character.Archetype}</b>. Гордость: <b>{character.Pride}</b>. Порок: <b>{character.Disgrace}</b>. " +
            $"Мотивация: <b>{character.Motivation}</b>. Описание: <b>{character.Description}</b>.";

        foreach (Skill skill in character.Skills)
        {
            if (skill.LvlLearned > 0)
            {
                ActivateSquare(skill);
            }
        }

        StartScreenshot(PageName.First.ToString(), character.Name);
    }

    private void ActivateSquare(Skill skill)
    {
        if (!skill.IsKnowledge)
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
