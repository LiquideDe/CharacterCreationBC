using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreatorTraits
{
    private List<Trait> _traits = new List<Trait>();
    public List<Trait> Traits { get => _traits; }

    public CreatorTraits()
    {
        List<string> dirs = new List<string>();
        dirs.AddRange(Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/Features"));
        foreach(string path in dirs)
        {
            _traits.Add(new Trait(GameStat.ReadText(path + "/Название.txt"), GameStat.ReadText(path + "/Описание.txt")));
        }
    }

    public Trait GetTrait(string name)
    {
        foreach(Trait trait in _traits)
        {
            if (string.Compare(name, trait.Name, true) == 0)
                return trait;
        }

        Debug.Log($"Не смогли найти feature {name}");
        return null;
    }
}
