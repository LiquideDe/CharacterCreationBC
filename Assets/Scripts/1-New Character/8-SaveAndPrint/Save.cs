using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Save
{
    public Save(ICharacter character)
    {        
        Directory.CreateDirectory($"{Application.dataPath}/StreamingAssets/CharacterSheets/{character.Name}");
        var path = Path.Combine($"{Application.dataPath}/StreamingAssets/CharacterSheets/{character.Name}", character.Name + ".JSON");
        List<string> data = new List<string>();

        SaveLoadCharacter saveLoad = new SaveLoadCharacter();

        saveLoad.godGifts = character.GodGifts;
        saveLoad.carryWeight = character.CarryWeight;
        saveLoad.corruption = character.CorruptionPoints;
        saveLoad.experienceSpent = character.ExperienceSpent;
        saveLoad.experienceTotal = character.ExperienceTotal;
        saveLoad.experienceUnspent = character.ExperienceUnspent;
        saveLoad.fatigue = character.Fatigue;
        saveLoad.fullMove = character.FullMove;
        saveLoad.halfMove = character.HalfMove;
        saveLoad.name = character.Name;
        saveLoad.natisk = character.Natisk;
        saveLoad.psyRating = character.PsyRating;
        saveLoad.pushWeight = character.PushWeight;
        saveLoad.run = character.Run;
        saveLoad.wounds = character.Wounds;
        saveLoad.description = character.Description;
        saveLoad.infamy = character.Infamy;
        saveLoad.god = character.God;
        saveLoad.canChangeGod = character.CanChageGod;

        saveLoad.talents = "";
        foreach (Talent talent in character.Talents)
        {
            saveLoad.talents += talent.Name;
            saveLoad.talents += "/";
        }
        saveLoad.talents = DeleteLastChar(saveLoad.talents);

        saveLoad.mentalDisorders = "";
        foreach (string mental in character.MentalDisorders)
        {
            saveLoad.mentalDisorders += mental;
            saveLoad.mentalDisorders += "/";
        }
        saveLoad.mentalDisorders = DeleteLastChar(saveLoad.mentalDisorders);

        saveLoad.mutation = "";
        foreach (string mutation in character.Mutation)
        {
            saveLoad.mutation += mutation;
            saveLoad.mutation += "/";
        }
        saveLoad.mutation = DeleteLastChar(saveLoad.mutation);

        saveLoad.amountImplants = character.Implants.Count;

        saveLoad.psyPowers = "";
        foreach (PsyPower psyPower in character.PsyPowers)
        {
            saveLoad.psyPowers += psyPower.Name;
            saveLoad.psyPowers += "/";
        }
        saveLoad.psyPowers = DeleteLastChar(saveLoad.psyPowers);

        saveLoad.traits = "";
        saveLoad.traitsLvl = "";
        foreach(Trait feature in character.Traits)
        {
            saveLoad.traits += feature.Name;
            saveLoad.traits += "/";
            saveLoad.traitsLvl += feature.Lvl;
            saveLoad.traitsLvl += "/";
        }
        saveLoad.traits = DeleteLastChar(saveLoad.traits); 
        saveLoad.traitsLvl = DeleteLastChar(saveLoad.traitsLvl);

        saveLoad.upgrades = "";
        foreach(Trait upgrade in character.Upgrades)
        {
            saveLoad.upgrades += upgrade.Name;
            saveLoad.upgrades += "/";
            saveLoad.upgradesLvl += upgrade.Lvl;
            saveLoad.upgradesLvl += "/";
        }
        saveLoad.upgrades = DeleteLastChar(saveLoad.upgrades);
        saveLoad.upgradesLvl = DeleteLastChar(saveLoad.upgradesLvl);

        int counter = 0;
        List<string> dataSkills = new List<string>();
        foreach (Skill skill in character.Skills)
        {
            if (skill.LvlLearned > 0)
            {
                SaveLoadSkill saveSkill = new SaveLoadSkill();
                saveSkill.name = skill.Name;
                saveSkill.lvlLearned = skill.LvlLearned;
                dataSkills.Add(JsonUtility.ToJson(saveSkill, true));
                counter++;
            }
        }
        saveLoad.amountSkills = counter;
        data.Add(JsonUtility.ToJson(saveLoad,true));
        

        foreach (Characteristic characteristic in character.Characteristics)
        {
            SaveLoadCharacteristic saveCharacteristic = new SaveLoadCharacteristic();
            saveCharacteristic.name = characteristic.Name;
            saveCharacteristic.amount = characteristic.Amount;
            saveCharacteristic.lvlLearnedChar = characteristic.LvlLearned;
            data.Add(JsonUtility.ToJson(saveCharacteristic,true));
        }

        data.AddRange(dataSkills);

        foreach (MechImplant implant in character.Implants)
        {
            SaveLoadImplant saveImplant = new SaveLoadImplant();
            saveImplant.armor = implant.Armor;
            saveImplant.bonusToughness = implant.BonusToughness;
            saveImplant.description = implant.Description;
            saveImplant.name = implant.Name;
            saveImplant.partsOfBody = implant.Place.ToString();
            data.Add(JsonUtility.ToJson(saveImplant, true));
        }

        foreach(Equipment equipment in character.Equipments)
        {
            if(equipment.TypeEq == Equipment.TypeEquipment.Armor)
            {
                JSONArmorReader armorReader = new JSONArmorReader();
                Armor armor = (Armor)equipment;
                armorReader.amount = armor.Amount;
                armorReader.armorPoint = armor.ArmorPoint;
                armorReader.body = armor.DefBody;
                armorReader.description = armor.Description;
                armorReader.descriptionArmor = armor.PlaceArmor;
                armorReader.hands = armor.DefHands;
                armorReader.head = armor.DefHead;
                armorReader.legs = armor.DefLegs;
                armorReader.maxAgility = armor.MaxAgil;
                armorReader.name = armor.Name;
                armorReader.typeEquipment = armor.TypeEq.ToString(); ;
                armorReader.weight = armor.Weight;
                data.Add(JsonUtility.ToJson(armorReader, true));
            }
            else if(equipment.TypeEq == Equipment.TypeEquipment.Grenade)
            {
                JSONGrenadeReader grenadeReader = new JSONGrenadeReader();
                Weapon grenade = (Weapon)equipment;
                grenadeReader.amount = grenade.Amount;
                grenadeReader.damage = grenade.Damage;
                grenadeReader.description = grenade.Description;
                grenadeReader.name = grenade.Name;
                grenadeReader.penetration = grenade.Penetration;
                grenadeReader.properties = grenade.Properties;
                grenadeReader.rarity = grenade.Rarity;
                grenadeReader.typeEquipment = grenade.TypeEq.ToString();
                grenadeReader.weaponClass = grenade.ClassWeapon;
                grenadeReader.weight = grenade.Weight;
                data.Add(JsonUtility.ToJson(grenadeReader, true));
            }
            else if (equipment.TypeEq == Equipment.TypeEquipment.Melee)
            {
                JSONMeleeReader meleeReader = new JSONMeleeReader();
                Weapon melee = (Weapon)equipment;
                meleeReader.amount = melee.Amount;
                meleeReader.damage = melee.Damage;
                meleeReader.description = melee.Description;
                meleeReader.name = melee.Name;
                meleeReader.penetration = melee.Penetration;
                meleeReader.properties = melee.Properties;
                meleeReader.rarity = melee.Rarity;
                meleeReader.typeEquipment = melee.TypeEq.ToString();
                meleeReader.type = melee.TypeEq.ToString();
                meleeReader.weaponClass = melee.ClassWeapon;
                meleeReader.weight = melee.Weight;
                data.Add(JsonUtility.ToJson(meleeReader, true));
            }
            else if (equipment.TypeEq == Equipment.TypeEquipment.Range)
            {
                JSONRangeReader rangeReader = new JSONRangeReader();
                Weapon range = (Weapon)equipment;
                rangeReader.amount = range.Amount;
                rangeReader.damage = range.Damage;
                rangeReader.description = range.Description;
                rangeReader.name = range.Name;
                rangeReader.penetration = range.Penetration;
                rangeReader.properties = range.Properties;
                rangeReader.rarity = range.Rarity;
                rangeReader.typeEquipment = range.TypeEq.ToString();
                rangeReader.weaponClass = range.ClassWeapon;
                rangeReader.weight = range.Weight;
                rangeReader.clip = range.Clip;
                rangeReader.range = range.Range;
                rangeReader.reload = range.Reload;
                rangeReader.rof = range.Rof;
                data.Add(JsonUtility.ToJson(rangeReader, true));
            }
            else if (equipment.TypeEq == Equipment.TypeEquipment.Thing)
            {
                JSONEquipmentReader equipmentReader = new JSONEquipmentReader();
                equipmentReader.amount = equipment.Amount;
                equipmentReader.description = equipment.Description;
                equipmentReader.name = equipment.Name;
                equipmentReader.rarity = equipment.Rarity;
                equipmentReader.typeEquipment = equipment.TypeEq.ToString();
                equipmentReader.weight = equipment.Weight;
                data.Add(JsonUtility.ToJson(equipmentReader, true));
            }
        }

        File.WriteAllLines(path, data);
    }

    private string DeleteLastChar(string text)
    {
        if(text.Length > 0)
        {
            string tex = text.TrimEnd('/');
            return tex;
        }
        else
        {
            return text;
        }  
        
    }
}
