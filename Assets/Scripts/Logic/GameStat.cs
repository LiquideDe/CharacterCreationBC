using System.Collections.Generic;
using System.IO;
using System.Text;

public static class GameStat
{
    public static Dictionary<CharacteristicName, string> characterTranslate = new Dictionary<CharacteristicName, string>()
    {
        { CharacteristicName.WeaponSkill,"����� ����������" },
        { CharacteristicName.BallisticSkill,"����� ��������" },
        { CharacteristicName.Strength,"����" },
        { CharacteristicName.Toughness,"������������" },
        { CharacteristicName.Agility,"��������" },
        { CharacteristicName.Intelligence,"���������" },
        { CharacteristicName.Perception,"����������" },
        { CharacteristicName.Willpower,"���� ����" },
        { CharacteristicName.Fellowship,"�������������" }

    };

    public static Dictionary<string, string> KnowledgeTranslate = new Dictionary<string, string>()
    {
        {"Trade", "�������" },
        {"CommonLore", "����� ������" },
        {"ForbiddenLore", "��������� ������" },
        {"ScholasticLore", "������ ������" },
        {"Linguistics", "�����������" }
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
