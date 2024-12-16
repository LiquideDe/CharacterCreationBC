using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlMediatorMinion
{
    public event Action ReturnToMenu;
    private LvlFactory _lvlFactory;
    private PresenterFactory _presenterFactory;
    private TypeMinion _typeMinion;

    public LvlMediatorMinion(LvlFactory lvlFactory, PresenterFactory presenterFactory)
    {
        _lvlFactory = lvlFactory;
        _presenterFactory = presenterFactory;
    }

    public void ShowChooseTypeMinion()
    {
        ChooseTypeMinionView view = _lvlFactory.Get(TypeScene.ChooseTypeMinion).GetComponent<ChooseTypeMinionView>();
        ChooseTypeMinionPresenter presenter = (ChooseTypeMinionPresenter)_presenterFactory.Get( TypeScene.ChooseTypeMinion);

        presenter.ReturnMinionToManual += ShowManual;
        presenter.ReturnMinionToRandom += ShowRandom;

        presenter.Initialize(view);
    }    

    private void ShowManual(TypeMinion minion)
    {
        _typeMinion = minion;
        MinionCharacter minionCharacter = new MinionCharacter(minion);

        ManualCharacteristicsMinionView view = _lvlFactory.Get(TypeScene.ManualCharacteristicMinion).GetComponent<ManualCharacteristicsMinionView>();
        ManualCharacteristicsMinionPresenter presenter = (ManualCharacteristicsMinionPresenter)_presenterFactory.Get(TypeScene.ManualCharacteristicMinion);

        presenter.GoNext += ShowRedistribution;
        presenter.Initialize(minionCharacter, minion, view);
    }

    private void ShowRandom(TypeMinion minion)
    {
        _typeMinion = minion;
        MinionCharacter minionCharacter = new MinionCharacter(minion);
        CharacteristicRandomView view = _lvlFactory.Get(TypeScene.RandomCharacteristicMinion).GetComponent<CharacteristicRandomView>();

        SetRandomCharacteristicMinionPresenter presenter = (SetRandomCharacteristicMinionPresenter)_presenterFactory.Get( TypeScene.RandomCharacteristicMinion);

        presenter.ReturnCharacterWithCharacteristics += ShowRedistribution;
        presenter.Initialize(minionCharacter, minion, view);
    }

    private void ShowRedistribution(IMinion minion)
    {
        RedistributionPointsMinionView view = _lvlFactory.Get(TypeScene.RedistributionMinion).GetComponent<RedistributionPointsMinionView>();
        RedistributionPointsMinionPresenter presenter = (RedistributionPointsMinionPresenter)_presenterFactory.Get(TypeScene.RedistributionMinion);

        presenter.Next += ShowSkills;
        presenter.Initialize(_typeMinion, minion, view);
    }

    private void ShowSkills(IMinion minion)
    {
        GameObject gameObject = _lvlFactory.Get(TypeScene.UpgradeSkill);
        UpgradeSkillCreatorView creatorView = gameObject.GetComponent<UpgradeSkillCreatorView>();
        UpgradeSkillView skillView = gameObject.GetComponent<UpgradeSkillView>();

        MinionUpgradeSkillPresenter presenter = (MinionUpgradeSkillPresenter)_presenterFactory.Get( TypeScene.ChooseSkillMinion);
        presenter.GoToTalent += ShowTalents;
        presenter.GoPrev += ShowRedistribution;
        presenter.Initialize(skillView, creatorView, minion, _typeMinion);
    }

    private void ShowTalents(IMinion minion)
    {
        UpgradeTalentView view = _lvlFactory.Get(TypeScene.UpgradeTalent).GetComponent<UpgradeTalentView>();

        UpgradeTalentMinionPresenter presenter = (UpgradeTalentMinionPresenter)_presenterFactory.Get( TypeScene.ChooseTalentMinion);

        presenter.ReturnToSkill += ShowSkills;
        presenter.GoNext += ShowTraits;

        presenter.Initialize(view, minion, _typeMinion);
    }

    private void ShowTraits(IMinion minion)
    {
        ChooseTraitsMinionView view = _lvlFactory.Get( TypeScene.ChooseTraitMinion).GetComponent<ChooseTraitsMinionView>();
        ChooseTraitsMinionPresenter presenter = (ChooseTraitsMinionPresenter)_presenterFactory.Get( TypeScene.ChooseTraitMinion);

        presenter.GoNext += ChekcPsy;
        presenter.GoPrev += ShowTalents;

        presenter.Initialize(view, _typeMinion, minion);
    }

    
    private void ChekcPsy(IMinion minion)
    {
        if(minion.PsyRating > 0)
            ShowPsycana(minion);
        else
            ChooseArmorAndWeapon(minion);
    }
    private void ShowPsycana(IMinion minion)
    {
        UpgradePsycanaView view = _lvlFactory.Get(TypeScene.UpgradePsycana).GetComponent<UpgradePsycanaView>();
        PsycanaCreatorView psycanaCreatorView = view.GetComponent<PsycanaCreatorView>();
        UpgradePsycanaMinionPresenter presenter = (UpgradePsycanaMinionPresenter)_presenterFactory.Get(TypeScene.ChoosePsycanaMinion);

        presenter.GoNext += ChooseArmorAndWeapon;
        presenter.ReturnToTalent += ShowTraits;

        presenter.Initialize(minion, psycanaCreatorView, view);
    }

    private void ChooseArmorAndWeapon(IMinion minion)
    {
        if(_typeMinion.Name.Contains("Зверь") || _typeMinion.Name.Contains("Демон"))
            ChooseName(minion);
        else
        {
            ChooseArmorWeaponMinionView view = _lvlFactory.Get(TypeScene.ChooseArmorMinion).GetComponent<ChooseArmorWeaponMinionView>();
            ChooseArmorWeaponMinionPresenter presenter = (ChooseArmorWeaponMinionPresenter)_presenterFactory.Get(TypeScene.ChooseArmorMinion);
            presenter.GoNext += ChooseName;
            presenter.Initialize(view, minion, _typeMinion);
        }
        
    }

    private void ChooseName(IMinion minion)
    {
        ChooseNameView view = _lvlFactory.Get(TypeScene.Name).GetComponent<ChooseNameView>();
        ChooseNameMinionPresenter presenter = (ChooseNameMinionPresenter)_presenterFactory.Get(TypeScene.ChooseNameMinion);

        presenter.GoNext += PrintMinion;
        presenter.Initialize(view, minion);
    }

    private void PrintMinion(IMinion minion)
    {
        PrintMinion print = _lvlFactory.Get(TypeScene.PrintMinion).GetComponent<PrintMinion>();
        print.WorkIsFinished += ReturnToMenuPressed;
        print.Initialize(minion);
    }

    private void ReturnToMenuPressed() => ReturnToMenu?.Invoke();
}
