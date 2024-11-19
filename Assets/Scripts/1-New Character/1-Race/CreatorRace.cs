using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

public class CreatorRace : ICreator
{
    private List<Race> _races = new List<Race>();


    public CreatorRace(CreatorSkills creatorSkills, CreatorTalents creatorTalents, CreatorTraits creatorTraits)
    {
        List<string> dirs = new List<string>();
        dirs.AddRange(Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/Races"));
        for (int i = 0; i < dirs.Count; i++)
        {
            Race race = new Race(dirs[i], creatorSkills, creatorTalents, creatorTraits);
            _races.Add(race);
        }
    }

    public List<Race> Races => _races;
}
