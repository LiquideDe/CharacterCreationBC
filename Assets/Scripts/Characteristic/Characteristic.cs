public class Characteristic 
{
    private GameStat.CharacteristicName _name;
    private int _lvlLearned;
    private int _amount = 0;
    private string _god;

    public string Name { get { return GameStat.characterTranslate[_name]; } }
    public GameStat.CharacteristicName InternalName { get => _name; }
    public int LvlLearned { get => _lvlLearned; set => _lvlLearned = value; }
    public int Amount { get => _amount; set => _amount = value; }

    public Characteristic(GameStat.CharacteristicName name)
    {
        _name = name;
    }

    public Characteristic(GameStat.CharacteristicName name, int amount)
    {
        _name = name;
        _amount = amount;
    }

    public Characteristic(Characteristic characteristic)
    {
        _name = characteristic.InternalName;
        _lvlLearned = characteristic.LvlLearned;
        _amount = characteristic.Amount;
    }

    public void UpgradeLvl()
    {
        _lvlLearned += 1;
        _amount += 5;
    }
}
