using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class WeaponBlock : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textName, textClass, textRange, textRoF, textDamage,
        textPenetration, textClip, textReload, textProp;
    private bool isEmpty = true;
    public bool IsEmpty { get => isEmpty; }
    public void FillBlock(Weapon weapon)
    {
        if(weapon.TypeEq == Equipment.TypeEquipment.Range)
        {
            textRange.text = weapon.Range.ToString();
            textClip.text = weapon.Clip.ToString();
            textReload.text = weapon.Reload;
            textRoF.text = weapon.Rof;
        }
        textName.text = weapon.NameWithAmount;
        textClass.text = weapon.ClassWeapon;   
        textDamage.text = weapon.Damage;
        textPenetration.text = weapon.Penetration.ToString();
        textProp.text = weapon.Properties;
        isEmpty = false;
    }
}
