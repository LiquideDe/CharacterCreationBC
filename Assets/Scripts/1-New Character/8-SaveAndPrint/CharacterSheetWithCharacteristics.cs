using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterSheetWithCharacteristics : TakeScreenshot
{
    [SerializeField]
    private TextMeshProUGUI textWeapon, textBallistic, textStrength, textToughness, textAgility, textIntelligence, textPerception, textWillpower,
        textFelloweship, textInfamy, textWeaponBonus, textBallisticBonus, textStrengthBonus, textToughnessBonus,
        textAgilityBonus, textIntelligenceBonus, textPerceptionBonus, textWillpowerBonus, textFelloweshipBonus;

    [SerializeField] private GameObject[] circlesWeapon;
    [SerializeField] private GameObject[] circlesBallistic;
    [SerializeField] private GameObject[] circlesStrength;
    [SerializeField] private GameObject[] circlesToughness;
    [SerializeField] private GameObject[] circlesAgility;
    [SerializeField] private GameObject[] circlesIntelligence;
    [SerializeField] private GameObject[] circlesPerception;
    [SerializeField] private GameObject[] circlesWillpower;
    [SerializeField] private GameObject[] circlesFellowship;
    private List<GameObject[]> circlesGroups;

    public string BonusStrength => textStrengthBonus.text;
    public string BonusToughness => textToughnessBonus.text;
    public string BonusAgility => textAgilityBonus.text;
    public string Strength => textStrength.text;
    public string BonusWillpower => textWillpowerBonus.text;

    public virtual void Initialize(List<Characteristic> characteristics, List<Equipment> equipments, List<Trait> traits, int infamy)
    {
        circlesGroups = new List<GameObject[]>
        {
            circlesWeapon,
            circlesBallistic,
            circlesStrength,
            circlesToughness,
            circlesAgility,
            circlesIntelligence,
            circlesPerception,
            circlesWillpower,
            circlesFellowship
        };

        int bonusStrenthFromArmor = 0;
        foreach (Equipment equipment in equipments)
            if (equipment is Armor armor)
                bonusStrenthFromArmor += armor.BonusStrength;

        InsertTwoDigitNumber(textWeapon,characteristics[0].Amount);
        InsertTwoDigitNumber(textBallistic, characteristics[1].Amount);
        InsertTwoDigitNumber(textStrength, characteristics[2].Amount + bonusStrenthFromArmor);
        InsertTwoDigitNumber(textToughness, characteristics[3].Amount);
        InsertTwoDigitNumber(textAgility, characteristics[4].Amount);
        InsertTwoDigitNumber(textIntelligence, characteristics[5].Amount);
        InsertTwoDigitNumber(textPerception, characteristics[6].Amount);
        InsertTwoDigitNumber(textWillpower, characteristics[7].Amount);
        InsertTwoDigitNumber(textFelloweship, characteristics[8].Amount);
        InsertTwoDigitNumber(textInfamy, infamy);

        for (int i = 0; i < circlesGroups.Count; i++)
        {
            for (int j = 0; j < characteristics[i].LvlLearned; j++)
            {
                circlesGroups[i][j].SetActive(true);
            }
        }

        //int superWeapon = 0;
        //int superBallistic = 0;
        int superStrength = 0;
        int superToughness = 0;
        int superAgility = 0;
        int superIntelligence = 0;
        int superPerception = 0;
        int superWillpower = 0;
        int superFelloweship = 0;

        foreach(Trait trait in traits)
        {
            if (string.Compare(trait.Name, "Сверхъестественная Сила") == 0)            
                superStrength = trait.Lvl;
            
            else if (string.Compare(trait.Name, "Сверхъестественная Выносливость") == 0)
                superToughness += trait.Lvl;

            else if(string.Compare(trait.Name, "Сверхъестественная Ловкость") == 0)
                superAgility += trait.Lvl;

            else if(string.Compare(trait.Name, "Сверхъестественный Интеллект") == 0)
                superIntelligence += trait.Lvl;

            else if(string.Compare(trait.Name, "Сверхъестественное Восприятие") == 0)
                superPerception += trait.Lvl;

            else if(string.Compare(trait.Name, "Сверхъестественная Сила Воли") == 0)
                superWillpower += trait.Lvl;

            else if(string.Compare(trait.Name, "Сверхъестественная Общительность") ==0)
                superFelloweship += trait.Lvl;

            else if(string.Compare(trait.Name, "Демонический") == 0)
                superToughness += trait.Lvl;
        }

        if (superStrength > 0)
            textStrengthBonus.text = $"{superStrength + (int)(characteristics[GameStat.CharacteristicToInt["Сила"]].Amount + bonusStrenthFromArmor )/ 10}";
        else textStrengthBonus.text = "";

        if (superToughness > 0)
            textToughnessBonus.text = $"{superToughness + (int)characteristics[GameStat.CharacteristicToInt["Выносливость"]].Amount / 10}";
        else textToughnessBonus.text = "";

        if (superAgility > 0)
            textAgilityBonus.text = $"{superAgility + (int)characteristics[GameStat.CharacteristicToInt["Ловкость"]].Amount / 10}";
        else textAgilityBonus.text = "";

        if (superIntelligence > 0)
            textIntelligenceBonus.text = $"{superIntelligence + (int)characteristics[GameStat.CharacteristicToInt["Интеллект"]].Amount / 10}";
        else textIntelligenceBonus.text = "";

        if (superPerception > 0)
            textPerceptionBonus.text = $"{superPerception + (int)characteristics[GameStat.CharacteristicToInt["Восприятие"]].Amount / 10}";
        else textPerceptionBonus.text = "";

        if (superWillpower > 0)
            textWillpowerBonus.text = $"{superWillpower + (int)characteristics[GameStat.CharacteristicToInt["Сила Воли"]].Amount / 10}";
        else textWillpowerBonus.text = "";

        if (superFelloweship > 0)
            textFelloweshipBonus.text = $"{superFelloweship + (int)characteristics[GameStat.CharacteristicToInt["Общительность"]].Amount / 10}";
        else textFelloweshipBonus.text = "";

    }

    private void InsertTwoDigitNumber(TextMeshProUGUI textMesh, int amount)
    {
        if (amount < 10)
            textMesh.text = $"0{amount}";
        else
            textMesh.text = $"{amount}";
    }
}
