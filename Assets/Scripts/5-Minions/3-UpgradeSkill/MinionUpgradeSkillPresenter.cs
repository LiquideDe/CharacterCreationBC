using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class MinionUpgradeSkillPresenter : IPresenter
{
    public event Action<IMinion> GoToTalent;
    public event Action<IMinion> GoPrev;
    private UpgradeSkillCreatorView _creatorView;
    private UpgradeSkillView _view;
    private IMinion _character;
    private TypeMinion _typeMinion;
    private AudioManager _audioManager;
    private CreatorSkills _creatorSkills;

    private enum TypeSkill { Skill, CommonLore, ForbiddenLore, Linguistics, ScholasticLore, Trade }
    private TypeSkill _currentScene = TypeSkill.Skill;

    [Inject]
    private void Construct(AudioManager audioManager, CreatorSkills creatorSkills)
    {
         _audioManager = audioManager;
        _creatorSkills = creatorSkills;
    }

    public void Initialize(UpgradeSkillView view, UpgradeSkillCreatorView creatorView, IMinion character, TypeMinion typeMinion)
    {
        _character = character;
        _view = view;
        _creatorView = creatorView;
        _typeMinion = typeMinion;
        if(_character is MinionCharacter minionCharacter && _character.Skills.Count == 0)
        {            
            minionCharacter.SetSkills(_creatorSkills.Skills);
            MinionDecorator minion = new MinionDecorator(minionCharacter);
            _character = minion;
        }
        
        Subscribe();
        ShowSkills();
        _view.UpgradeExpireinceText($"{_character.SkillPoints} Î");
    }

    private void Subscribe()
    {
        _view.CancelUpgrade += CancelUpgrade;
        _view.NextWindow += NextWindowPressed;
        _view.UpgradeSkill += UpgradeSkill;
        _view.ShowSkill += ShowSkillsDown;
        _view.ShowCommonLore += ShowCommonLoreDown;
        _view.ShowForbiddenlore += ShowForbiddenLoreDown;
        _view.ShowLuingva += ShowLingvaDown;
        _view.ShowSciense += ShowScienseDown;
        _view.ShowTrade += ShowTradeDown;
        _view.PrevWindow += PrevWindow;
    }    

    private void Unscribe()
    {
        _view.CancelUpgrade -= CancelUpgrade;
        _view.NextWindow -= NextWindowPressed;
        _view.UpgradeSkill -= UpgradeSkill;
        _view.ShowSkill -= ShowSkillsDown;
        _view.ShowCommonLore -= ShowCommonLoreDown;
        _view.ShowForbiddenlore -= ShowForbiddenLoreDown;
        _view.ShowLuingva -= ShowLingvaDown;
        _view.ShowSciense -= ShowScienseDown;
        _view.ShowTrade -= ShowTradeDown;
        _view.PrevWindow -= PrevWindow;
    }

    private void NextWindowPressed()
    {
        _audioManager.PlayClick();
        Unscribe();
        _view.DestroyView();
        GoToTalent?.Invoke(_character);
    }

    private void UpgradeSkill(Skill skill, int cost)
    {
        if (_character.SkillPoints > 0)
        {
            if (skill.LvlLearned + 1 <= _typeMinion.MaxSkillLvl)
            {
                _audioManager.PlayClick();
                MinionDecorator minion = new MinionDecorator(_character);
                minion.UpgradeSkill(skill, 1);
                _character = minion;
                ShowSkills();
                _view.UpgradeExpireinceText($"{_character.SkillPoints} Î");
            }
            else if (skill.LvlLearned + 1 == _typeMinion.MaxSkillLvl + 1 && _character.SkillsOverPrice > 0)
            {
                _audioManager.PlayClick();
                MinionDecorator minion = new MinionDecorator(_character);
                minion.UpgradeSkill(skill, 2);
                minion.SkillsOverPrice--;
                _character = minion;
                ShowSkills();
                _view.UpgradeExpireinceText($"{_character.SkillPoints} Î");
            }
            else _audioManager.PlayWarning();
        }
        else _audioManager.PlayWarning();

    }

    private void CancelUpgrade()
    {
        if (_character.Minion is MinionDecorator)
        {
            _audioManager.PlayCancel();
            _character = _character.Minion;
            ShowSkills();
            _view.UpgradeExpireinceText($"{_character.SkillPoints} Î");
        }
        else
            _audioManager.PlayWarning();
    }

    private void ShowSkillsDown()
    {
        _audioManager.PlayClick();
        _currentScene = TypeSkill.Skill;
        ShowSkills();
    }

    private void ShowCommonLoreDown()
    {
        _audioManager.PlayClick();
        _currentScene = TypeSkill.CommonLore;
        ShowSkills();
    }

    private void ShowForbiddenLoreDown()
    {
        _audioManager.PlayClick();
        _currentScene = TypeSkill.ForbiddenLore;
        ShowSkills();
    }

    private void ShowLingvaDown()
    {
        _audioManager.PlayClick();
        _currentScene = TypeSkill.Linguistics;
        ShowSkills();
    }

    private void ShowScienseDown()
    {
        _audioManager.PlayClick();
        _currentScene = TypeSkill.ScholasticLore;
        ShowSkills();
    }

    private void ShowTradeDown()
    {
        _audioManager.PlayClick();
        _currentScene = TypeSkill.Trade;
        ShowSkills();
    }

    private void ShowSkills()
    {
        if (_currentScene == TypeSkill.Skill)
            ShowAverageSkill();
        else
            ShowLoreSkills(_currentScene.ToString());
    }

    private void ShowAverageSkill()
    {
        List<Skill> skills = new List<Skill>();
        foreach (Skill skill in _character.Skills)
        {
            if (skill is Knowledge)
            {

            }
            else
                skills.Add(skill);
        }

        skills.Sort(
            delegate (Skill skill1, Skill skill2)
            {
                return skill1.Name.CompareTo(skill2.Name);
            });

        _creatorView.Initialize(skills, null);
    }


    private void ShowLoreSkills(string internalKnowledgeName)
    {
        List<Skill> skills = new List<Skill>();
        foreach (Skill skill in _character.Skills)
        {
            if (skill is Knowledge)
            {
                Knowledge knowledge = (Knowledge)skill;
                if (knowledge.TypeSkill == internalKnowledgeName)
                    skills.Add(skill);
            }

        }

        skills.Sort(
            delegate (Skill skill1, Skill skill2)
            {
                return skill1.Name.CompareTo(skill2.Name);
            });
        _creatorView.Initialize(skills, null);
    }

    private void PrevWindow()
    {
        _audioManager.PlayClick();
        Unscribe();
        _view.DestroyView();
        GoPrev?.Invoke(_character);
    }
}
