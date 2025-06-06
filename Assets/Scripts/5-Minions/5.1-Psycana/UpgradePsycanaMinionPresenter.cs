using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UpgradePsycanaMinionPresenter : IPresenter
{
    public event Action<IMinion> ReturnToTalent;
    public event Action<IMinion> GoNext;

    private IMinion _character;
    private AudioManager _audioManager;
    private int _school = 0;
    private PsyPower _psyPower;
    private PsycanaCreatorView _creatorView;
    private UpgradePsycanaView _view;
    private CreatorPsyPowers _creatorPsyPowers;

    [Inject]
    private void Construct(AudioManager audioManager, CreatorPsyPowers creatorPsyPowers)
    {
        _audioManager = audioManager;
        _creatorPsyPowers = creatorPsyPowers;
    }

    public void Initialize(IMinion character, PsycanaCreatorView creatorView, UpgradePsycanaView view)
    {
        _character = character;
        _creatorView = creatorView;
        _view = view;
        SendListWithSchoolNames();
        Subscribe();
        ShowSchool(_school);
    }

    private void Subscribe()
    {
        _view.BuyPsyPower += BuyPsyPower;
        _view.Cancel += CancelUpgrade;
        _view.ClosePanel += ClosePanel;
        _view.Next += GoToNext;
        _view.Prev += ReturnToTalentDown;
        _view.ShowThisPsyPower += ShowThisPsyPower;
        _view.ShowThisSchool += ShowSchool;
    }

    private void Unscribe()
    {
        _view.BuyPsyPower -= BuyPsyPower;
        _view.Cancel -= CancelUpgrade;
        _view.ClosePanel -= ClosePanel;
        _view.Next -= GoToNext;
        _view.Prev -= ReturnToTalentDown;
        _view.ShowThisPsyPower -= ShowThisPsyPower;
        _view.ShowThisSchool -= ShowSchool;
    }

    private void SendListWithSchoolNames() => _view.BuildSchoolViewList(_creatorPsyPowers.GetNamesSchool());

    private void ShowSchool(int id)
    {
        _audioManager.PlayClick();
        _school = id;
        UpdateTextInView();
        _creatorView.Initialize(_creatorPsyPowers.GetPowers(_school), _character.PsyPowers, _creatorPsyPowers.GetConnections(_school), _creatorPsyPowers.GetSizeSpacing(_school));
    }

    private void ShowThisPsyPower(string name)
    {
        _psyPower = _creatorPsyPowers.GetPsyPower(name, _school);
        bool isPossible = false;

        if (_character.PsyPowerPoints >= 1 && _character.PsyRating >= _psyPower.PsyRateRequire && TryPsyPowerForReqPsyPowers(_psyPower) &&
            TryCheckCharacteristics(_psyPower.RequireCharacteristics, _character.Characteristics) && TryCheckSkills(_psyPower.RequireSkills, _character.Skills) 
            && TryNotDoublePsyPower(_psyPower))
            isPossible = true;


        _view.ShowPsyPower(_psyPower, isPossible);
    }

    private bool TryPsyPowerForReqPsyPowers(PsyPower psyPower)
    {
        if (psyPower.Lvl == 0)
            return true;

        PsyPower parentPsyPower = _creatorPsyPowers.GetPsyPowerById(_school, psyPower.IdParent);
        foreach (PsyPower power in _character.PsyPowers)
        {
            if (string.Compare(power.Name, parentPsyPower.Name) == 0)
                return true;
        }

        return false;
    }

    private bool TryCheckCharacteristics(List<Characteristic> psyReq, List<Characteristic> characteristics)
    {
        if (psyReq != null)
            foreach (Characteristic psyReqCharacteristic in psyReq)
                foreach (Characteristic characteristic in characteristics)
                    if (psyReqCharacteristic.InternalName == characteristic.InternalName)
                        if (characteristic.Amount < psyReqCharacteristic.Amount)
                            return false;



        return true;
    }

    private bool TryCheckSkills(List<Skill> reqSkills, List<Skill> characterSkills)
    {
        if (reqSkills != null)
            foreach (Skill reqSkill in reqSkills)
                foreach (Skill charSkill in characterSkills)
                    if (string.Compare(reqSkill.Name, charSkill.Name) == 0)
                        if (charSkill.LvlLearned < reqSkill.LvlLearned)
                            return false;

        return true;
    }

    private bool TryNotDoublePsyPower(PsyPower psyPower)
    {
        foreach (PsyPower power in _character.PsyPowers)
        {
            if (string.Compare(psyPower.Name, power.Name, true) == 0)
                return false;
        }

        return true;
    }

    private void BuyPsyPower()
    {
        _audioManager.PlayDone();
        MinionDecorator minionDecorator = new MinionDecorator(_character);
        minionDecorator.UpgradePsyPower(_psyPower);
        _character = minionDecorator;
        ClosePanel();
        ShowSchool(_school);
    }

    private void ClosePanel()
    {
        _audioManager.PlayCancel();
        _view.ClosePanelWithInformation();
    }

    private void ReturnToTalentDown()
    {
        _audioManager.PlayClick();
        ReturnToTalent?.Invoke(_character);
        Unscribe();
        _view.DestroyView();
    }

    private void GoToNext()
    {
        _audioManager.PlayDone();
        GoNext?.Invoke(_character);
        Unscribe();
        _view.DestroyView();
    }

    private void CancelUpgrade()
    {
        if (_character.Minion is MinionDecorator)
        {
            _audioManager.PlayCancel();
            _character = _character.Minion;
            ShowSchool(_school);
        }
        else
            _audioManager.PlayWarning();
    }

    private void UpdateTextInView() => _view.SetExperience($"{_character.PsyPowerPoints} �", $"�� {_character.PsyRating}",
        $"", _creatorPsyPowers.GetNameSchool(_school));
}
