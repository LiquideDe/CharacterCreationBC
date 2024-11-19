using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class FourthCharacterSheet : TakeScreenshot
{
    [SerializeField]
    private TextMeshProUGUI _textUnspentExperience, _textSpentExperience, _textTotalExperience, _textKhorne, _textSlaanesh,
        _textNurgle, _textTzeentch, _textUndevided, _textGod, _textUpgrade1, _textCostUpgrade1, _textGodUpgrade1,
        _textUpgrade2, _textCostUpgrade2, _textGodUpgrade2;
    private readonly int _amountLines = 38;
    private int _khorn = 0, _slaanesh = 0, _nurgl = 0, _tzeentch = 0, _undevided = 0;
    private CreatorTalents _creatorTalents;

    [Inject]
    private void Construct(CreatorTalents creatorTalents) => _creatorTalents = creatorTalents;

    public void Initialize(ICharacter character)
    {
        _character = character;
        _textUnspentExperience.text = character.ExperienceUnspent.ToString();
        _textSpentExperience.text = character.ExperienceSpent.ToString();
        _textTotalExperience.text = character.ExperienceTotal.ToString();

        _textGod.text = character.God;

        int maxLines = 0;
        bool isLineFull = false;
        Debug.Log($"Count Upgrades = {character.Upgrades.Count}");
        if (_amountLines > character.Upgrades.Count)
            maxLines = character.Upgrades.Count;
        else
        {
            maxLines = _amountLines;
            isLineFull = true;
        }

        FillUpgrades(_textUpgrade1, _textCostUpgrade1, _textGodUpgrade1, 0, maxLines);

        if (isLineFull)
            FillUpgrades(_textUpgrade2, _textCostUpgrade2, _textGodUpgrade2, _amountLines, character.Upgrades.Count - _amountLines);

        _textKhorne.text = _khorn.ToString();
        _textSlaanesh.text = _slaanesh.ToString();
        _textNurgle.text = _nurgl.ToString();
        _textTzeentch.text = _tzeentch.ToString();
        _textUndevided.text = _undevided.ToString();

        StartScreenshot(PageName.Fourth.ToString());
    }

    private void FillUpgrades(TextMeshProUGUI textUpgrade, TextMeshProUGUI textCost, TextMeshProUGUI textGod, int startId, int endId)
    {
        for (int i = startId; i < endId; i++)
        {
            Characteristic characteristic = IsCharacteristic(_character.Upgrades[i].Name);
            Skill skill = IsSkill(_character.Upgrades[i].Name);
            if (characteristic != null)
            {
                Characteristic characteristic1 = new Characteristic(characteristic);
                characteristic1.LvlLearned = _character.Upgrades[i].Lvl - 1;
                textUpgrade.text += $"{characteristic.Name} \n";
                textCost.text += $"{GameStat.CalculateCostCharacteristic(characteristic1, _character)} \n";
                textGod.text += $"{characteristic1.God} \n";
                CalculateGod(characteristic1.God);
            }
            else if (skill != null)
            {
                Skill skill1 = new Skill(skill, _character.Upgrades[i].Lvl - 1);
                textUpgrade.text += $"{skill1.Name} \n";
                textCost.text += $"{GameStat.CalculateCostSkill(skill1, _character)} \n";
                textGod.text += $"{skill1.God} \n";
                CalculateGod(skill1.God);
            }
            else
            {
                //Talent talent = IsTalent(_character.Upgrades[i].Name);
                Talent talent = new Talent(_creatorTalents.GetTalent(_character.Upgrades[i].Name));
                if (talent != null)
                {
                    Debug.Log($"{talent.Name}, {talent.Rank}");
                    textUpgrade.text += $"{talent.Name} \n";
                    textCost.text += $"{GameStat.CalculateCostTalent(talent, _character)} \n";
                    textGod.text += $"{talent.God} \n";
                    CalculateGod(talent.God);
                }
            }
        }
    }

    private Characteristic IsCharacteristic(string name)
    {
        foreach (Characteristic characteristic in _character.Characteristics)
            if (string.Compare(characteristic.Name, name, true) == 0)
                return characteristic;

        return null;
    }

    private Skill IsSkill(string name)
    {
        foreach(Skill skill in _character.Skills)
            if (string.Compare(skill.Name, name, true) == 0)
                return skill;

        return null;
    }

    private Talent IsTalent(string name)
    {
        foreach(Talent talent in _character.Talents)
            if (string.Compare(talent.Name, name, true) == 0)
                return talent;

        return null;
    }

    private void CalculateGod(string god)
    {
        if (string.Compare("Кхорн", god, true) == 0)
            _khorn++;
        else if (string.Compare("Нургл", god, true) == 0)
            _nurgl++;
        else if(string.Compare("Слаанеш", god, true) == 0)
            _slaanesh++;
        else if(string.Compare("Тзинч", god, true) == 0)
            _tzeentch++;
        else
            _undevided++;
    }
}
