using System.Collections.Generic;
using System.IO;
using System.Text;

public static class GameStat
{
    public static Dictionary<CharacteristicName, string> characterTranslate = new Dictionary<CharacteristicName, string>()
    {
        { CharacteristicName.WeaponSkill,"Навык Рукопашной" },
        { CharacteristicName.BallisticSkill,"Навык Стрельбы" },
        { CharacteristicName.Strength,"Сила" },
        { CharacteristicName.Toughness,"Выносливость" },
        { CharacteristicName.Agility,"Ловкость" },
        { CharacteristicName.Intelligence,"Интеллект" },
        { CharacteristicName.Perception,"Восприятие" },
        { CharacteristicName.Willpower,"Сила Воли" },
        { CharacteristicName.Fellowship,"Общительность" }

    };

    public static Dictionary<string, string> KnowledgeTranslate = new Dictionary<string, string>()
    {
        {"Trade", "Ремесло" },
        {"CommonLore", "Общие знания" },
        {"ForbiddenLore", "Запретные знания" },
        {"ScholasticLore", "Ученые знания" },
        {"Linguistics", "Лингвистика" }
    };

    public enum CharacteristicName { None, WeaponSkill, BallisticSkill, Strength, Toughness, Agility, Intelligence, Perception, Willpower, Fellowship}    

    public static string ReadText(string nameFile)
    {
        string txt;
        using (StreamReader _sw = new StreamReader(nameFile, Encoding.Default))
        {
            txt = (_sw.ReadToEnd());
            _sw.Close();
        }
        return txt;
    }
}
