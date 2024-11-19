using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class CreatorArchetype : ICreator
{
    private List<Archetype> _archetypes = new List<Archetype>();

    public CreatorArchetype(CreatorSkills creatorSkills, CreatorTalents creatorTalents, CreatorTraits creatorTraits)
    {
        List<string> dirs = new List<string>();
        dirs.AddRange(Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/Archetypes"));
        for (int i = 0; i < dirs.Count; i++)
        {
            Archetype archetype = new Archetype(dirs[i], creatorSkills, creatorTalents, creatorTraits);
            _archetypes.Add(archetype);
        }
    }

    public List<Archetype> Archetypes => _archetypes;
}
