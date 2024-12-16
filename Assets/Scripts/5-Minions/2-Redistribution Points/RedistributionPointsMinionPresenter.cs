using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RedistributionPointsMinionPresenter : IPresenter
{
    public event Action<IMinion> Next;
    private TypeMinion _typeMinion;
    private IMinion _character;
    private RedistributionPointsMinionView _view;
    private AudioManager _audioManager;

    [Inject]
    private void Construct(AudioManager audioManager) => _audioManager = audioManager;

    public void Initialize(TypeMinion typeMinion, IMinion character, RedistributionPointsMinionView view)
    {
        _typeMinion = typeMinion;
        _character = character;
        _view = view;
        Subscribe();
        _view.SetSkillPoints(_character.SkillPoints);
        _view.SetTalentPoints(_character.TalentPoints);
        _view.SetTraitPoints(_character.TraitPoints);
    }

    private void Subscribe()
    {
        _view.Cancel += Cancel;
        _view.FromTalentToSkill += FromTalentToSkill;
        _view.FromTraitToTalent += FromTraitToTalent;
        _view.Next += NextPressed;
    }

    private void Unscribe()
    {
        _view.Cancel -= Cancel;
        _view.FromTalentToSkill -= FromTalentToSkill;
        _view.FromTraitToTalent -= FromTraitToTalent;
        _view.Next -= NextPressed;
    }

    private void FromTalentToSkill()
    {
        if(_character.TalentPoints > 0)
        {
            _audioManager.PlayClick();
            _character.SkillPoints++;
            _character.TalentPoints--;
            _view.SetTalentPoints(_character.TalentPoints);
            _view.SetSkillPoints(_character.SkillPoints);
        }
        else
            _audioManager.PlayWarning();
    }

    private void FromTraitToTalent()
    {
        if(_character.TraitPoints > 0)
        {
            _audioManager.PlayClick();
            _character.TraitPoints--;
            _character.TalentPoints++;
            _view.SetTraitPoints(_character.TraitPoints);
            _view.SetTalentPoints(_character.TalentPoints);
        }
        else _audioManager.PlayWarning();
    }

    private void Cancel()
    {
        _audioManager.PlayCancel();
        if(_character is MinionCharacter)
        {
            
        }
        else
        {
            MinionCharacter minionCharacter = GetMinion(_character);
            _character = minionCharacter;
        }

        _character.SkillPoints = _typeMinion.SkillPoints;
        _character.TalentPoints = _typeMinion.TalentsPoints;
        _character.TraitPoints = _typeMinion.TraitPoints;
        _view.SetSkillPoints(_character.SkillPoints);
        _view.SetTalentPoints(_character.TalentPoints);
        _view.SetTraitPoints(_character.TraitPoints);

    }

    private void NextPressed()
    {
        _audioManager.PlayDone();
        Unscribe();
        _view.DestroyView();
        Next?.Invoke(_character);
    }

    private MinionCharacter GetMinion(IMinion minion)
    {
        if (minion is MinionCharacter character)
            return character;

        return GetMinion(minion.Minion);
    }
}
