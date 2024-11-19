using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreatorTalents
{
    private List<Talent> _talents = new List<Talent>();
    private List<string> _categories = new List<string>();    

    public CreatorTalents()
    {
        List<string> dirsCategory = new List<string>();
        dirsCategory.AddRange(Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/Talents"));
        for (int i = 0; i < dirsCategory.Count; i++)
        {
            if (!dirsCategory[i].Contains("Example"))
            {
                List<string> dirs = new List<string>();
                dirs.AddRange(Directory.GetDirectories(dirsCategory[i]));
                DirectoryInfo directoryInfo = new DirectoryInfo(dirsCategory[i]);
                _categories.Add(directoryInfo.Name);
                foreach (string dir in dirs) 
                {

                    _talents.Add(new Talent(dir, directoryInfo.Name));
                }
                
            }
        }
    }

    public List<Talent> Talents => _talents;

    public List<string> Categories => _categories; 

    public Talent GetTalent(string name)
    {
        foreach (Talent talent in _talents)
        {
            if (string.Compare(talent.Name, name,true) == 0)
            {
                return talent;
            }
        }

        Debug.Log($"!!!!! Не нашли талантант !!!! Искали {name}");
        return null;
    }
}
