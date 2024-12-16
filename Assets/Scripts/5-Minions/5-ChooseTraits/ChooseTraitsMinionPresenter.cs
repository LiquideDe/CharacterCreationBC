using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class ChooseTraitsMinionPresenter : IPresenter
{
    public event Action<IMinion> GoNext;
    public event Action<IMinion> GoPrev;
    private ChooseTraitsMinionView _view;
    private AudioManager _audioManager;
    private TypeMinion _typeMinion;
    private IMinion _character;
    private TraitWithCost _trait;
    private CreatorTalents _creatorTalents;

    [Inject]
    private void Construct(AudioManager audioManager, CreatorTalents creatorTalents)
    {
        _audioManager = audioManager;
        _creatorTalents = creatorTalents;
    }

    public void Initialize(ChooseTraitsMinionView view, TypeMinion typeMinion, IMinion character)
    {
        _view = view;
        _typeMinion = typeMinion;
        _character = character;
        Subscribe();
        ShowTraits();
        _view.SetTraitPoints(_character.TraitPoints);
    }

    private void Subscribe()
    {
        _view.ReturnPrev += ReturnToTalent;
        _view.AddTrait += AddTrait;
        _view.CancelChanges += Cancel;
        _view.GoNext += NextPressed;
        _view.ShowThisTrait += ShowTrait;
    }

    private void Unscribe()
    {
        _view.ReturnPrev -= ReturnToTalent;
        _view.AddTrait -= AddTrait;
        _view.CancelChanges -= Cancel;
        _view.GoNext -= NextPressed;
        _view.ShowThisTrait -= ShowTrait;
    }

    private void ShowTrait(string name)
    {
        foreach (var item in _typeMinion.Traits)
        {
            if(string.Compare(item.Name, name, true) == 0)
            {
                _trait = item;
                _view.SetDescriptionText($"{item.Name} \n {item.Description}");
                _view.ShowButtonBuy();
            }
        }
    }

    private void ShowTraits()
    {
        List<TraitWithCost> traits = new List<TraitWithCost>(_typeMinion.Traits);
        foreach (var item in _character.Traits)        
            foreach (var trait in traits)            
                if (string.Compare(item.Name, trait.Name) == 0)                
                    if (trait.StartLvl == 0)
                    {
                        traits.Remove(trait);
                        break;
                    }
        _view.ShowTraits(traits, _character.Traits);
    }

    private void NextPressed()
    {
        _audioManager.PlayDone();
        Unscribe();
        GoNext?.Invoke(_character);
        _view.DestroyView();
    }

    private void Cancel()
    {
        if (_character.Minion is MinionDecorator)
        {
            _audioManager.PlayCancel();
            _character = _character.Minion;
            _view.SetTraitPoints(_character.TalentPoints);
        }
        else
            _audioManager.PlayWarning();
    }

    private void AddTrait()
    {
        if (_character.TraitPoints > 0)
        {
            _audioManager.PlayClick();
            MinionDecorator minionDecorator = new MinionDecorator(_character);
            AddUniqeTrait(minionDecorator);
            _character = minionDecorator;            
            ShowTraits();
            _view.HideButtonBuy();
            _view.SetDescriptionText("");
            _view.SetTraitPoints(_character.TraitPoints);
        }
        else
            _audioManager.PlayWarning();
    }

    private void ReturnToTalent()
    {
        _audioManager.PlayDone();
        Unscribe();
        GoPrev?.Invoke(_character);
        _view.DestroyView();
    }

    private void AddUniqeTrait(MinionDecorator minionDecorator)
    {
        TraitWithCost traitWithCost = GetSummTrait(_trait);
        if (string.Compare(_trait.Name, "Геносемя", true) == 0)
        {
            TraitWithCost amhibia = GetSummTrait(GetTraitFromTypeMinion("Амфибия"));
            TraitWithCost strength = GetSummTrait(GetTraitFromTypeMinion("Сверхъестественная Сила"),4);
            TraitWithCost toughness = GetSummTrait(GetTraitFromTypeMinion("Сверхъестественная Выносливость"), 4);
            Talent talent = new Talent(_creatorTalents.GetTalent("Сопротивление Жара"));
            Talent talent1 = new Talent(_creatorTalents.GetTalent("Сопротивление Холод"));
            Talent talent2 = new Talent(_creatorTalents.GetTalent("Сопротивление Яд"));
            Talent talent3 = new Talent(_creatorTalents.GetTalent("Усиленные Чувства Зрение"));
            Talent talent4 = new Talent(_creatorTalents.GetTalent("Усиленные Чувства Слух"));
            minionDecorator.UpgradeTrait(amhibia, 0);
            minionDecorator.UpgradeTrait(strength, 0);
            minionDecorator.UpgradeTrait(toughness, 0);
            minionDecorator.UpgradeTalent(talent, 0);
            minionDecorator.UpgradeTalent(talent1, 0);
            minionDecorator.UpgradeTalent(talent2, 0);
            minionDecorator.UpgradeTalent(talent3, 0);
            minionDecorator.UpgradeTalent(talent4, 0);
            minionDecorator.UpgradeTrait(_trait, 5);
        }
        else if (string.Compare(_trait.Name, "Огрин", true) == 0)
        {
            TraitWithCost amhibia = GetSummTrait(GetTraitFromTypeMinion("Фанатик"));
            TraitWithCost strength = GetSummTrait(GetTraitFromTypeMinion("Сверхъестественная Сила"), 6);
            TraitWithCost toughness = GetSummTrait(GetTraitFromTypeMinion("Сверхъестественная Выносливость"), 6);
            Talent talent = new Talent(_creatorTalents.GetTalent("Сопротивление Жара"));
            Talent talent1 = new Talent(_creatorTalents.GetTalent("Сопротивление Холод"));
            Talent talent2 = new Talent(_creatorTalents.GetTalent("Стальная Челюсть"));
            minionDecorator.Characteristics[GameStat.CharacteristicToInt["Сила"]].Amount += 15;
            minionDecorator.Characteristics[GameStat.CharacteristicToInt["Выносливость"]].Amount += 15;
            minionDecorator.Characteristics[GameStat.CharacteristicToInt["Ловкость"]].Amount -= 15;
            minionDecorator.Characteristics[GameStat.CharacteristicToInt["Интеллект"]].Amount -= 15;
            minionDecorator.UpgradeTrait(amhibia, 0);
            minionDecorator.UpgradeTrait(strength, 0);
            minionDecorator.UpgradeTrait(toughness, 0);
            minionDecorator.UpgradeTalent(talent, 0);
            minionDecorator.UpgradeTalent(talent1, 0);
            minionDecorator.UpgradeTalent(talent2, 0);
            minionDecorator.UpgradeTrait(_trait, 5);
        }
        else if (string.Compare(_trait.Name, "Псайкер", true) == 0)
        {
            minionDecorator.PsyRating = 1;
            minionDecorator.PsyPowerPoints = 4;
            minionDecorator.UpgradeTrait(_trait, 2);
        }
        else if (string.Compare(_trait.Name, "Сервочереп", true) == 0)
        {
            TraitWithCost machine = GetTraitFromTypeMinion("Машина");
            TraitWithCost hoverer = GetTraitFromTypeMinion("Парящий");
            if (_typeMinion.Name.Contains("Низший"))
            {
                machine = GetSummTrait(machine);
                hoverer = GetSummTrait(hoverer);
            }                
            else if (_typeMinion.Name.Contains("Обычный"))
            {
                machine = GetSummTrait(machine, 2);
                hoverer = GetSummTrait(hoverer, 2);
            }                
            else if (_typeMinion.Name.Contains("Высший"))
            {
                machine = GetSummTrait(machine, 3);
                hoverer = GetSummTrait(hoverer, 3);
            }
            minionDecorator.UpgradeTrait(_trait, 2);
        }
        else
        {
            minionDecorator.UpgradeTrait(traitWithCost);
        }
    }

    private TraitWithCost GetSummTrait(TraitWithCost trait, int lvl = 1)
    {
        TraitWithCost traitWithCost = null;
        if(trait.MaxLvl > 0)
        {
            foreach (var item in _character.Traits)
                if (string.Compare(trait.Name, item.Name, true) == 0)
                {
                    traitWithCost = new TraitWithCost(trait, item.Lvl + lvl);
                    break;
                }
            if(traitWithCost == null)
                traitWithCost = new TraitWithCost(trait, lvl);
        }
        
        if (traitWithCost == null)
            traitWithCost = new TraitWithCost(trait, trait.Lvl);

        return traitWithCost;
    }

    private TraitWithCost GetTraitFromTypeMinion(string name)
    {
        foreach (var item in _typeMinion.Traits)        
            if(string.Compare(name, item.Name) == 0)            
                return item;

        return null;
    }
}
