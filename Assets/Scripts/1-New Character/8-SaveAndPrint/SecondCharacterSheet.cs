using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;

public class SecondCharacterSheet : CharacterSheetWithCharacteristics
{
    [SerializeField] TextMeshProUGUI textWound, textEquipments, textMoveHalf, textMoveFull, textNatisk, textRun, 
        textFatigue, textPsyRate, textPsyPowers, textCorruption, textGodGift;
    [SerializeField] WeaponBlock[] weaponBlocks;
    [SerializeField] ArmorOnBody onBody;

    public override void Initialize(ICharacter character)
    {
        base.Initialize(character);
        gameObject.SetActive(true);
        _character = character;
        textWound.text = character.Wounds.ToString();
        foreach(Equipment eq in character.Equipments)
        {
            if(eq is Weapon)
            { }
            else
                textEquipments.text += $"{eq.NameWithAmount}, ";
        }
        textMoveHalf.text = character.HalfMove.ToString();
        textMoveFull.text = character.FullMove.ToString();
        textNatisk.text = character.Natisk.ToString();
        textRun.text = character.Run.ToString();
        textFatigue.text = character.Fatigue.ToString();
        foreach(Equipment equipment in character.Equipments)
        {
            if(equipment.TypeEq == Equipment.TypeEquipment.Melee || equipment.TypeEq == Equipment.TypeEquipment.Range || equipment.TypeEq == Equipment.TypeEquipment.Grenade)
            {
                foreach (WeaponBlock block in weaponBlocks)
                {
                    if (block.IsEmpty)
                    {
                        Weapon weapon = (Weapon)equipment;
                        block.FillBlock(weapon);
                        break;
                    }
                }
            }
            else if (equipment.TypeEq == Equipment.TypeEquipment.Armor)
            {
                Armor armor = (Armor)equipment;
                onBody.SetArmor(armor, _character.Implants, LvlTraitMachine());
            }
        }

        foreach(MechImplant implant in character.Implants)
        {
            textEquipments.text += $"{implant.Name}, ";
        }

        if(character.PsyRating > 0)
        {
            textPsyRate.text = character.PsyRating.ToString();

            foreach(PsyPower psyPower in character.PsyPowers)
            {
                textPsyPowers.text += $"{psyPower.Name}, ";
            }
        }
        else
        {
            textPsyRate.text = "";
        }

        StartScreenshot(PageName.Second.ToString());
    }

    private int LvlTraitMachine()
    {
        foreach (Trait trait in _character.Traits)
            if (string.Compare(trait.Name, "Машина") == 0)
                return trait.Lvl;

        return 0;
    }

}
