using System;

[Serializable]
public class SaveLoadCharacter
{
    public string name, talents, mutation, equipments, psyPowers, 
        traits, traitsLvl, elite, foreverFriendly, foreverHostile, race, archetype, motivation, disgrace, pride, minions, mentalDisorders, 
        upgrades, upgradesLvl, description, godGifts, god;
    public int wounds, psyRating, halfMove, fullMove, natisk, run, fatigue,
        experienceTotal, experienceUnspent, experienceSpent, amountImplants, amountSkills, corruption, infamy;
    public float carryWeight, liftWeight, pushWeight;
    public bool canChangeGod;

}
