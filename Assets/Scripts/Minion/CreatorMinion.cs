using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CreatorMinion : ICreator
{
    private List<TypeMinion> _typeMinions = new List<TypeMinion>();

    public CreatorMinion(CreatorTraits creatorTraits)
    {
        List<string> dirs = new List<string>();
        dirs.AddRange(Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/TypeMinions"));
        foreach (string path in dirs)
        {
            _typeMinions.Add(new TypeMinion(path, creatorTraits));
        }
    }

    public List<TypeMinion> Minions => _typeMinions; 

    public TypeMinion Get(string name)
    {
        foreach (var item in _typeMinions)
        
            if(string.Compare(name, item.Name,true) == 0)
                return item;

        Debug.Log($"!!!!Не нашли миньона {name}");
        return null;
    }
}
