using UnityEngine;
using Zenject;

public class CreatorFactory
{
    private DiContainer _diContainer;

    public CreatorFactory(DiContainer diContainer) => _diContainer = diContainer;
    

    public ICreator Get(TypeCreator typeCreator)
    {
        switch (typeCreator)
        {
            case TypeCreator.Race:
                return _diContainer.Instantiate<CreatorRace>();

            case TypeCreator.Arhetype:
                return _diContainer.Instantiate<CreatorArchetype>();

            default:
                throw new System.Exception("Нет такого типа TypeCreator");
        }
    }
}

public enum TypeCreator
{
    Race,
    Arhetype
}
