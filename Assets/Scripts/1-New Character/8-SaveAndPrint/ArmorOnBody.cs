using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArmorOnBody : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textArmorHead, textArmorLeftHand, textArmorRightHand, textArmorBody, textArmorLeftLeg, textArmorRightLeg;
    int _machine;
    List<MechImplant> implants = new List<MechImplant>();
    public void SetArmor(Armor armor, List<MechImplant> mechImplants, int traitMachine = 0)
    {
        _machine = traitMachine;
        implants = mechImplants;
        SetBigger(armor.DefHead, textArmorHead, MechImplant.PartsOfBody.Head);
        SetBigger(armor.DefHands, textArmorLeftHand, MechImplant.PartsOfBody.LeftHand);
        SetBigger(armor.DefHands, textArmorRightHand, MechImplant.PartsOfBody.RightHand);
        SetBigger(armor.DefBody, textArmorBody, MechImplant.PartsOfBody.Body);
        SetBigger(armor.DefLegs, textArmorLeftLeg, MechImplant.PartsOfBody.LeftLeg);
        SetBigger(armor.DefLegs, textArmorRightLeg,  MechImplant.PartsOfBody.RightLeg);
    }

    private void SetBigger(int armorPoint, TextMeshProUGUI textArmor, MechImplant.PartsOfBody partOfBody)
    {
        
        int armorFromImplant = CalculateArmorFromImplant(partOfBody);
        //int additionalBonusToughness = AdditionalBonusToughnessFromImplant(partOfBody);
        if(textArmor.text.Length == 0 && armorPoint > 0)
        {
            textArmor.text = (armorPoint + armorFromImplant + _machine).ToString();
        }
        else if(textArmor.text.Length == 0 && armorPoint == 0)
        {
            textArmor.text = "";
        }
        else
        {
            int.TryParse(textArmor.text, out int prevArmor);
            prevArmor -= CalculateArmorFromImplant(partOfBody);
            if (prevArmor < armorPoint)
            {
                textArmor.text = (armorPoint + armorFromImplant + _machine).ToString();
            }            
        }
        if(armorPoint == 0 && textArmor.text.Length == 0)
        {
            textArmor.text = $"{armorFromImplant + _machine}";
        }
    }

    private int CalculateArmorFromImplant(MechImplant.PartsOfBody partOfBody)
    {
        int addingArmor = 0;
        foreach(MechImplant implant in implants)
        {
            if(implant.Place == partOfBody || implant.Place == MechImplant.PartsOfBody.All)
            {
                addingArmor += implant.Armor;
            }
        }
        return addingArmor;
    }

    private int AdditionalBonusToughnessFromImplant(MechImplant.PartsOfBody partOfBody)
    {
        int addingToughness = 0;
        foreach (MechImplant implant in implants)
        {
            if (implant.Place == partOfBody || implant.Place == MechImplant.PartsOfBody.All)
            {
                addingToughness += implant.BonusToughness;
            }
        }
        return addingToughness;
    }
}
