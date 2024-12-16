public class TraitWithCost : Trait
{
    private int _cost, _maxLvl, _startLvl;
    public TraitWithCost(Trait trait, JSONReaderTraitWithCost traitWithCost) : base(trait, traitWithCost.lvl) 
    {
        _cost = traitWithCost.cost;
        _maxLvl = traitWithCost.maxLvl;
        _startLvl = traitWithCost.lvl;
    }

    public TraitWithCost(TraitWithCost traitWithCost, int lvl) : base(traitWithCost, lvl)
    {
        _cost = traitWithCost.Cost;
        _maxLvl = traitWithCost.MaxLvl;
        _startLvl= traitWithCost.StartLvl;        
    }

    public int Cost => _cost; 
    public int MaxLvl => _maxLvl;
    public int StartLvl => _startLvl;
}
