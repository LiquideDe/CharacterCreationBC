using System.Collections.Generic;

public interface ICharacter
{
    public List<Equipment> Equipments { get; }
    public int Wounds { get; }
    public int CorruptionPoints { get; }
    public int PsyRating { get; }
    public int HalfMove { get; }
    public int FullMove { get; }
    public int Natisk { get; }
    public int Fatigue { get; }
    public float CarryWeight { get; }
    public float LiftWeight { get; }
    public float PushWeight { get; }
    public int ExperienceTotal { get; }
    public int ExperienceUnspent { get; }
    public int ExperienceSpent { get; }
    public List<Characteristic> Characteristics { get; }
    public List<Skill> Skills { get; }
    public List<Talent> Talents { get; }
    public List<MechImplant> Implants { get; }
    public List<string> MentalDisorders { get; }
    public int Run { get; }
    public List<string> Mutation { get; }
    public List<PsyPower> PsyPowers { get; }
    public List<Trait> Traits { get; }
    public string Name { get; }
    public int BonusToughness { get; }
    public ICharacter GetCharacter { get; }

    public List<IName> ForeverFriendly { get; }
    public List<IName> ForeverHostile { get; }

    public string Race {  get; }
    public string Archetype { get; }
    public List<string> EliteAcrhetypes { get; }

    public string Motivation { get; }
    public string Disgrace { get; }
    public string Pride { get; }
    public string Stereotype { get; }
    public bool CanChageGod { get; }
    public string God { get; }

}
    

