using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrintMinion : CharacterSheetWithCharacteristics
{
    [SerializeField]
    private TextMeshProUGUI textWound, textMoveHalf, textMoveFull, textNatisk, textRun,
        textFatigue, _textNameCharacter, _textTalentsAndTraits;
    [SerializeField] private WeaponBlock[] weaponBlocks;
    [SerializeField] private ArmorBlock[] armorBlocks;
    [SerializeField] private ArmorOnBody onBody;
    [SerializeField] private Image characterImage;
    [SerializeField] private SkillList[] skillSquares;
    [SerializeField]
    private TextMeshProUGUI textName, _textDescription;

    public void Initialize(IMinion character)
    {
        List<Equipment> equipments = new List<Equipment>(character.Armors);
        List<Trait> traits = new List<Trait>(character.Traits);
        base.Initialize(character.Characteristics, equipments, traits, 0);       

        gameObject.SetActive(true);

        textName.text = character.Name;
        _textDescription.text = $"Описание: <b>{character.Description}</b>.";
        foreach (Skill skill in character.Skills)
        {
            if (skill.LvlLearned > 0)
            {
                ActivateSquare(skill);
            }
        }

        int.TryParse(BonusStrength, out int strengthBonus);
        int.TryParse(Strength, out int strength);
        int.TryParse(BonusToughness, out int toughness);
        int.TryParse(BonusAgility, out int agility);
        int.TryParse(BonusWillpower, out int willpower);

        if (strengthBonus == 0)
            strength = strength / 10;
        else
            strength = strengthBonus;

        if (toughness == 0)
            toughness = character.Characteristics[GameStat.CharacteristicToInt["Выносливость"]].Amount / 10;

        if (agility == 0)
            agility = character.Characteristics[GameStat.CharacteristicToInt["Ловкость"]].Amount / 10;

        if (willpower == 0)
            willpower = character.Characteristics[GameStat.CharacteristicToInt["Сила Воли"]].Amount / 10;

        List<float> _parametrsForWeight = new List<float>() { 0.9f, 2.25f, 4.5f, 9f, 18f, 27f, 36f, 45f, 56f, 67f, 78f, 90f,
            112f, 225f, 337f, 450f, 675f, 900f, 1350f, 1800f, 2250f, 2900f, 3550f, 4200f, 4850f, 5500f, 6300f, 7250f, 8300f, 9550f, 11000,
        13000, 15000, 17000, 20000, 23000, 26000, 30000, 35000, 40000, 46000, 53000, 70000, 80000, 92000, 106000};

        onBody.SetToughness(toughness, character.Characteristics[GameStat.CharacteristicToInt["Сила Воли"]].Amount / 10);
        int bonusWound = 0;
        foreach (var item in character.Traits)
            if (string.Compare(item.Name, "Огрин", true) == 0)
                bonusWound = 10;
        textWound.text = $"{toughness * 2 + 2 + bonusWound}";

        _textNameCharacter.text = $"Имя персонажа: <u>{character.Name}</u>";
        foreach(Weapon weapon in character.Weapons)
        {
            foreach (WeaponBlock block in weaponBlocks)
            {
                if (block.IsEmpty)
                {                    
                    block.FillBlock(weapon);
                    break;
                }
            }
        }

        if(character.Armors.Count > 0)
            foreach (ArmorBlock armorBlock in armorBlocks)
            {
                if (armorBlock.IsEmpty)
                {
                    List<MechImplant> implants = new List<MechImplant>();
                    armorBlock.FillBlock(character.Armors[0], implants, traits);
                    break;
                }
            }

        textMoveHalf.text = $"{agility}";
        textMoveFull.text = $"{agility * 2}";
        textNatisk.text = $"{agility * 3}";
        textRun.text = $"{agility * 3 * 2}";
        textFatigue.text = $"{toughness + willpower}";

        foreach (Talent talent in character.Talents)
            _textTalentsAndTraits.text += talent.Name + ", ";

        foreach(TraitWithCost trait in character.Traits)
            _textTalentsAndTraits.text += trait.Name + ", ";

        onBody.GenerateQr();
        StartCoroutine(ReadyToStart(character));
    }

    IEnumerator ReadyToStart(IMinion character)
    {
        yield return StartCoroutine(GetImage(character));
        StartScreenshot(PageName.First.ToString(), character.Name);
    }

    IEnumerator GetImage(IMinion character)
    {
        string pathImage = "";
        List<string> pathImages = new List<string>();
        if (Directory.Exists($"{Application.dataPath}/StreamingAssets/MinionsImages/{character.Name}"))
        {
            pathImages = Directory.GetFiles($"{Application.dataPath}/StreamingAssets/MinionsImages/{character.Name}", "*.jpeg").ToList();
            pathImages.AddRange(Directory.GetFiles($"{Application.dataPath}/StreamingAssets/MinionsImages/{character.Name}", "*.jpg").ToList());
            if (pathImages.Count > 0)
            {
                if (pathImages[0] != null)
                    pathImage = pathImages[0];
            }

        }
        if (pathImage.Length < 2)
        {/*
            List<string> nameBackgrounds = new List<string>();
            List<string> dirs = new List<string>();
            int backId = 0;
            dirs.AddRange(Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/Backgrounds"));
            foreach (string dir in dirs)
            {
                nameBackgrounds.Add(GameStat.ReadText($"{dir}/Название.txt"));
            }

            for (int i = 0; i < nameBackgrounds.Count; i++)
            {
                if (string.Compare(nameBackgrounds[i], character.Background, true) == 0)
                {
                    backId = i;
                    break;
                }
            }

            if (character.Gender == "М")
            {
                string[] imagesF = Directory.GetFiles($"{dirs[backId]}/CharacterImage/Male", "*.jpg");
                string[] imagesS = Directory.GetFiles($"{dirs[backId]}/CharacterImage/Male", "*.jpeg");
                string[] images = imagesF.Concat(imagesS).ToArray();
                pathImage = images[0];
            }
            else
            {
                string[] imagesF = Directory.GetFiles($"{dirs[backId]}/CharacterImage/Female", "*.jpg");
                string[] imagesS = Directory.GetFiles($"{dirs[backId]}/CharacterImage/Female", "*.jpeg");
                string[] images = imagesF.Concat(imagesS).ToArray();
                pathImage = images[0];
            }
            */
            bool isImageSet = false;
            foreach(var item in character.Traits)
            {
                if(string.Compare(item.Name, "Геносемя", true) == 0)
                {
                    pathImage = $"{Application.streamingAssetsPath}/MinionsImages/Spacemarine.jpg";
                    isImageSet = true;
                    break;
                }
                else if(string.Compare(item.Name, "Огрин", true) == 0)
                {
                    pathImage = $"{Application.streamingAssetsPath}/MinionsImages/Ogryn.jpg";
                    isImageSet = true;
                    break;
                }
                else if (string.Compare(item.Name, "Сервочереп", true) == 0)
                {
                    pathImage = $"{Application.streamingAssetsPath}/MinionsImages/Servoskull.jpg";
                    isImageSet = true;
                    break;
                }
            }

            if(isImageSet == false)
            {
                if (character.TypeMinion.Contains("Человек"))
                    pathImage = $"{Application.streamingAssetsPath}/MinionsImages/Human.jpg";
                else if(character.TypeMinion.Contains("Машина"))
                    pathImage = $"{Application.streamingAssetsPath}/MinionsImages/servitor.jpg";
                else if (character.TypeMinion.Contains("Зверь"))
                    pathImage = $"{Application.streamingAssetsPath}/MinionsImages/Khymera.jpg";
                else if (character.TypeMinion.Contains("Демон"))
                    pathImage = $"{Application.streamingAssetsPath}/MinionsImages/Daemon.jpg";
            }
        }


        Sprite sprite = ReadImage(pathImage);

        characterImage.sprite = sprite;
        yield return new WaitForSeconds(0.1f);
    }


    private Sprite ReadImage(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        if (System.IO.File.Exists(path))
        {

            int sprite_width = 100;
            int sprite_height = 100;
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(sprite_width, sprite_height, TextureFormat.RGB24, false);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
        return null;
    }

    private void ActivateSquare(Skill skill)
    {
        if (!skill.IsKnowledge)
        {
            foreach (SkillList skillList in skillSquares)
            {
                if (string.Compare(skillList.SkillName, skill.Name, true) == 0)
                {

                    skillList.SetLvlLearned(skill.LvlLearned);
                    break;
                }
            }
        }
        else
        {
            foreach (SkillList skillList in skillSquares)
            {
                if (string.Compare(skillList.SkillName, skill.TypeSkill, true) == 0)
                {
                    if (skillList.KnowledgeTextName.Length == 0)
                    {
                        skillList.KnowledgeTextName = skill.Name;
                        skillList.SetLvlLearned(skill.LvlLearned);
                        break;
                    }
                }
            }
        }
    }
}
