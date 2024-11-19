using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class ArchetypeView : RaceView
{
    public void Initialize(Archetype archetype)
    {
        ResetImages(archetype.Path);
        string advantage = "";
        advantage += ModifierCharacteristicToText(archetype.ModifierCharacteristics);
        advantage += $"{archetype.Trait.Name} \n {archetype.Trait.Description} \n";
        advantage += $"Ран {archetype.Wounds} + 1к5";
        SetTexts(archetype.Name, archetype.Description, advantage);
        HideWindows();
        ClearCells();

        List<List<IName>> send = new List<List<IName>>();
        for (int i = 0; i < archetype.Skills.Count; i++)
        {
            send.Add(new List<IName>());
            send[i].AddRange(archetype.Skills[i]);
        }
        PackToINameAndCreateCell(send);
        send.Clear();

        for (int i = 0; i < archetype.Talents.Count; i++)
        {
            send.Add(new List<IName>());
            send[i].AddRange(archetype.Talents[i]);
        }
        PackToINameAndCreateCell(send);
        send.Clear();

        for (int i = 0; i < archetype.Implants.Count; i++)
        {
            send.Add(new List<IName>());
            send[i].AddRange(archetype.Implants[i]);
        }
        PackToINameAndCreateCell(send);
        send.Clear();


        for (int i = 0; i < archetype.Equipments.Count; i++)
        {
            send.Add(new List<IName>());
            send[i].AddRange(archetype.Equipments[i]);
        }
        PackToINameAndCreateCell(send);
        send.Clear();

        CheckAllCellsAreDone();
    }
}
