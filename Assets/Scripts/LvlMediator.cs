using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlMediator
{
    private LvlFactory _lvlFactory;
    private PresenterFactory _presenterFactory;
    private LvlMediatorNewCharacter _mediatorNewCharacter;
    private LvlMediatorUpgradeCharacter _mediatorUpgrade;
    private LvlVediatorEditCharacter _mediatorEditCharacter;
    private LvlMediatorMinion _mediatorMinion;

    public LvlMediator(LvlFactory lvlFactory, PresenterFactory presenterFactory, LvlMediatorNewCharacter mediatorNewCharacter, 
        LvlMediatorUpgradeCharacter mediatorUpgrade, LvlVediatorEditCharacter mediatorEditCharacter, LvlMediatorMinion mediatorMinion)
    {
        _lvlFactory = lvlFactory;
        _presenterFactory = presenterFactory;
        _mediatorNewCharacter = mediatorNewCharacter;
        _mediatorUpgrade = mediatorUpgrade;
        _mediatorEditCharacter = mediatorEditCharacter;
        _mediatorMinion = mediatorMinion;
        Subscribe();
    }

    public void MainMenu()
    {
        MainMenuView mainMenuView = _lvlFactory.Get(TypeScene.MainMenu).GetComponent<MainMenuView>();
        MainMenuPresenter mainMenuPresenter = (MainMenuPresenter)_presenterFactory.Get(TypeScene.MainMenu);
        mainMenuPresenter.NewCharacter += NewCharacterOpen;
        mainMenuPresenter.UpgradeCharacter += UpgradeCharacterOpen;
        //mainMenuPresenter.EditCharacter += EditCharacterOpen;
        mainMenuPresenter.NewMinion += NewMinion;
        mainMenuPresenter.Initialize(mainMenuView);
    }    

    private void Subscribe()
    {
        _mediatorNewCharacter.ReturnToMenu += MainMenu;
        _mediatorUpgrade.ReturnToMenu += MainMenu;
        _mediatorEditCharacter.ReturnToMenu += MainMenu;
    }

    private void NewCharacterOpen()
    {
        _mediatorNewCharacter.NewCharacter();
    }

    private void UpgradeCharacterOpen() => ShowLoads(true);

    private void EditCharacterOpen() => ShowLoads(false);

    private void ShowLoads(bool isUpgrade)
    {
        CharacterLoadsView loadsView = _lvlFactory.Get(TypeScene.Loads).GetComponent<CharacterLoadsView>();
        CharacterLoadsPresenter loadsPresenter = (CharacterLoadsPresenter)_presenterFactory.Get(TypeScene.Loads);
        loadsPresenter.Cancel += MainMenu;
        if (isUpgrade)
            loadsPresenter.ReturnCharacter += ShowUpgradeCharacter;
        else
            loadsPresenter.ReturnCharacter += ShowEditCharacter;

        loadsPresenter.Initialize(loadsView);
    }

    private void ShowEditCharacter(ICharacter character) => _mediatorEditCharacter.Initialize(character);

    private void ShowUpgradeCharacter(ICharacter character) => _mediatorUpgrade.Initialize(character);

    private void NewMinion()
    {
        _mediatorMinion.ShowChooseTypeMinion();
        _mediatorMinion.ReturnToMenu += MainMenu;
    }

}
