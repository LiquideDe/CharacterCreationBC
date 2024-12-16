using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class ChooseArmorWeaponMinionPresenter : IPresenter
{
    public event Action<IMinion> GoNext;
    private IMinion _minion;
    private TypeMinion _typeMinion;
    private ChooseArmorWeaponMinionView _view;
    private AudioManager _audioManager;
    private Armor _armor = null;
    private Weapon _weapon = null;

    [Inject]
    private void Construct(AudioManager audioManager) => _audioManager = audioManager;

    public void Initialize(ChooseArmorWeaponMinionView view, IMinion minion, TypeMinion typeMinion)
    {
        _view = view;
        _typeMinion = typeMinion;
        _minion = minion;
        Subscribe();
        view.Initialize(typeMinion.Armors, typeMinion.Weapons);
    }

    private void Subscribe()
    {
        _view.ChooseThisArmor += ChooseThisArmor;
        _view.ChooseThisWeapon += ChooseThisWeapon;
        _view.GoNext += GoNextDown;
    }

    private void Unscribe()
    {
        _view.ChooseThisArmor -= ChooseThisArmor;
        _view.ChooseThisWeapon -= ChooseThisWeapon;
        _view.GoNext -= GoNextDown;
    }

    private void ChooseThisArmor(string name)
    {
        _audioManager.PlayClick();
        foreach (var item in _typeMinion.Armors)
        {
            if(string.Compare(item.Name, name, true) == 0)
            {
                _armor = item;
                string text = $"{item.Name}. Броня головы - {item.DefHead}, броня тела - {item.DefBody}, броня рук - {item.DefHands}, броня ног - {item.DefLegs}. Максимальная ловкость - {item.MaxAgil}";
                _view.SetArmorDescription(text);
                if (_armor != null && _weapon != null)
                    _view.ShowButtonNext();
                break;
            }

        }
    }

    private void ChooseThisWeapon(string name)
    {
        _audioManager.PlayClick();
        foreach (var item in _typeMinion.Weapons)
        {
            if (string.Compare(item.Name, name, true) == 0)
            {
                _weapon = item;
                string text = $"{item.Name}. Урон - {item.Damage}, Бронепробитие - {item.Penetration}.";
                if (item.TypeEq == Equipment.TypeEquipment.Range)
                    text += $"Скорострельность - {item.Rof}, магазин - {item.Clip}, дальность - {item.Range}";
                _view.SetWeaponDescription(text);
                if (_armor != null && _weapon != null)
                    _view.ShowButtonNext();
                break;
            }

        }
    }

    private void GoNextDown()
    {
        _audioManager.PlayDone();
        Unscribe();
        _minion.Armors.Add(_armor);
        _minion.Weapons.Add(_weapon);
        GoNext?.Invoke(_minion);
        _view.DestroyView();
    }
}
