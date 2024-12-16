using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ChooseArmorWeaponMinionView : CanDestroyView
{
    [SerializeField] private TextMeshProUGUI _textArmor, _textWeapon;
    [SerializeField] private Transform _contentArmor, _contentWeapon;
    [SerializeField] private ItemInList _armorInListPrefab, _weaponInListPrefab;
    [SerializeField] private Button _buttonNext;
    public event Action<string> ChooseThisArmor;
    public event Action<string> ChooseThisWeapon;
    public event Action GoNext;

    private void OnEnable() => _buttonNext.onClick.AddListener(NextPressed);

    private void OnDisable() => _buttonNext.onClick.RemoveAllListeners();

    public void Initialize(List<Armor> armors, List<Weapon> weapons)
    {
        foreach (var item in armors)
        {
            ItemInList armor = Instantiate(_armorInListPrefab, _contentArmor);
            armor.Initialize(item.Name);
            armor.ChooseThis += ChooseThisArmorPressed;
        }

        foreach (var item in weapons)
        {
            ItemInList weapon = Instantiate(_weaponInListPrefab, _contentWeapon);
            weapon.Initialize(item.Name);
            weapon.ChooseThis += ChooseThisWeaponPressed;
        }
    }

    public void SetArmorDescription(string text) => _textArmor.text = text;
    
    public void SetWeaponDescription(string text) => _textWeapon.text = text;

    public void ShowButtonNext() => _buttonNext.gameObject.SetActive(true);

    private void ChooseThisWeaponPressed(string name) => ChooseThisWeapon?.Invoke(name);

    private void ChooseThisArmorPressed(string name) => ChooseThisArmor?.Invoke(name);

    private void NextPressed() => GoNext?.Invoke();
}
