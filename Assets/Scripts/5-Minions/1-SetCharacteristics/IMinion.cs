using System.Collections.Generic;

public interface IMinion
{
    public IMinion Minion { get; }

    public string TypeMinion { get; }

    public List<Characteristic> Characteristics { get; }

    public List<Skill> Skills { get; }

    public List<Talent> Talents { get; }

    public List<PsyPower> PsyPowers { get; }

    public List<TraitWithCost> Traits { get; }

    public List<Armor> Armors { get; }

    public List<Weapon> Weapons { get; }

    public int CharacteristicPoints { get; set; }

    public int SkillPoints { get; set; }

    public int TalentPoints { get; set; }

    public int TraitPoints { get; set; }

    public int PsyPowerPoints { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int SkillsOverPrice { get; set; }

    public int PsyRating { get;}
}
