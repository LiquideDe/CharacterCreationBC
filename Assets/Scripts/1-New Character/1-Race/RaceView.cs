using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Security.AccessControl;

public class RaceView : ViewWithButtonsDoneAndCancel
{
    [SerializeField] private TextMeshProUGUI _textNameRace, _textDescriptionRace, _textBetterCharacteristics, _textDescriptionSkill, _textNameSkills;
    [SerializeField] private Transform _contentWithSkills, _contentWithCellsSkills;
    [SerializeField] private GameObject _skillList, _descriptionSkill, _gameObjectButtonDone;
    [SerializeField] private ItemInList _chooseSkillPrefab;
    [SerializeField] private CellSkill _cellSkillPrefab;
    [SerializeField] private Button _buttonChooseSkill, _buttonNextRace, _buttonPrevRace;
    [SerializeField] private ImageShower _shower;

    public event Action ClickSound, NextRace, PrevRace;

    private List<ItemInList> _skillInList = new List<ItemInList>();
    private List<CellSkill> _cells = new List<CellSkill>();
    private CellSkill _chosenCell;
    private IName _chosenSkill;

    public List<CellSkill> Cells => _cells;

    private void OnEnable()
    {
        _buttonChooseSkill.onClick.AddListener(ChooseSkill);
        _buttonNextRace.onClick.AddListener(NextRacePressed);
        _buttonPrevRace.onClick.AddListener(PrevRacePressed);
    }    

    private void OnDisable()
    {
        _buttonChooseSkill.onClick.RemoveListener(ChooseSkill);
        _buttonNextRace.onClick.RemoveListener(ChooseSkill);
        _buttonPrevRace.onClick.RemoveListener(ChooseSkill);
    }

    public void Initialize(Race race)
    {

        ResetImages(race.Path);
        string advantage = $"Характеристики {race.StartCharacteristic} + 2к10 \n";
        HideWindows();
        advantage += ModifierCharacteristicToText(race.ModifierCharacteristics);
        for (int i = 0; i < race.Traits.Count; i++)
            for (int j = 0; j < race.Traits[i].Count; j++)
                advantage += $"{race.Traits[i][j].Name}({race.Traits[i][j].Lvl}) \n";

        if (race.ExperienceCost > 0)
            advantage += $"Опыт -{race.ExperienceCost}";

        SetTexts(race.Name, race.Description, advantage);

        ClearCells();

        List<List<IName>> send = new List<List<IName>>();
        for(int i = 0; i < race.Skills.Count; i++)
        {
            send.Add(new List<IName>());
            send[i].AddRange(race.Skills[i]);
        }
        PackToINameAndCreateCell(send);
        send.Clear();


        for(int i = 0; i < race.Talents.Count; i++)
        {
            send.Add(new List<IName>());
            send[i].AddRange(race.Talents[i]);
        }
        PackToINameAndCreateCell(send);
        send.Clear();

        for (int i = 0; i < race.Equipments.Count; i++)
        {
            send.Add(new List<IName>());
            send[i].AddRange(race.Equipments[i]);
        }
        PackToINameAndCreateCell(send);
        send.Clear();
        CheckAllCellsAreDone();
    }

    protected string ModifierCharacteristicToText(List<List<Trait>> traits)
    {
        string advantage = "";
        for (int i = 0; i < traits.Count; i++)
            for (int j = 0; j < traits[i].Count; j++)
            {
                if (traits[i][j].Lvl > 0)
                    advantage += $"{traits[i][j].Name} +{traits[i][j].Lvl} \n";
                else
                    advantage += $"{traits[i][j].Name} {traits[i][j].Lvl} \n";
            }

        return advantage;
    }

    protected void ResetImages(string path)
    {
        _shower.ResetImages();
        _shower.SetPathImage(path);
    }

    protected void HideWindows()
    {
        _descriptionSkill.gameObject.SetActive(false);
        _skillList.gameObject.SetActive(false);
        _gameObjectButtonDone.SetActive(false);
    }

    protected void SetTexts(string name, string description, string advantages)
    {
        _textNameRace.text = name;
        _textDescriptionRace.text = description;
        _textBetterCharacteristics.text = advantages;
    }

    protected void ClearCells()
    {
        foreach (CellSkill cell in _cells)
            Destroy(cell.gameObject);

        _cells.Clear();
    }

    protected void PackToINameAndCreateCell(List<List<IName>> names)
    {
        for (int i = 0; i < names.Count; i++)
        {
            List<IName> namesToCell = new List<IName>(names[i]);
            CreateCell(namesToCell);
        }
    }

    private void CreateCell(List<IName> names)
    {
        CellSkill cellSkill = Instantiate(_cellSkillPrefab, _contentWithCellsSkills);
        cellSkill.Initialize(names);
        cellSkill.ShowSkills += ShowSkillPressed;
        _cells.Add(cellSkill);
        if (names.Count == 1)
            cellSkill.SetSkill(names[0]);
    }

    protected void ShowSkillPressed(List<IName> list, CellSkill cellSkill)
    {
        ClickSound?.Invoke();
        _textDescriptionSkill.text = "";
        _descriptionSkill.gameObject.SetActive(false);
        if (_skillInList.Count > 0)
        {
            foreach (ItemInList itemInList in _skillInList)
                Destroy(itemInList.gameObject);
            _skillInList.Clear();
        }

        _skillList.gameObject.SetActive(true);
        List<IName> chosenSkills = new List<IName>();
        foreach(CellSkill cell in _cells)
            if(cell.ChosenSkill != null)
                chosenSkills.Add(cell.ChosenSkill);

        _chosenCell = cellSkill;
        foreach(IName skill in list)
        {
            Debug.Log(skill.Name);
            if (CheckDontRepeatNames(chosenSkills, skill) || list.Count == 1)
            {
                ItemInList itemInList = Instantiate(_chooseSkillPrefab, _contentWithSkills);
                itemInList.Initialize(skill.Name);
                itemInList.ChooseThis += ShowDescriptionSkill;
                _skillInList.Add(itemInList);
            }
        }

        if (list.Count > 1)
            if (list[0] is Skill skill1)
            {
                if (string.Compare(skill1.TypeSkill, "CommonLore", true) == 0)
                    _textNameSkills.text = "Общие знания";
                else if (string.Compare(skill1.TypeSkill, "ForbiddenLore", true) == 0)
                    _textNameSkills.text = "Запретные знания";
                else if (string.Compare(skill1.TypeSkill, "Linguistics", true) == 0)
                    _textNameSkills.text = "Языки";
                else if (string.Compare(skill1.TypeSkill, "ScholasticLore", true) == 0)
                    _textNameSkills.text = "Ученые знания";
                else if (string.Compare(skill1.TypeSkill, "Trade", true) == 0)
                    _textNameSkills.text = "Ремесло";
                else
                    _textNameSkills.text = "Навыки";
            }
            else if (list[0] is Talent)
                _textNameSkills.text = "Таланты";
            else if (list[0] is Trait)
                _textNameSkills.text = "Черты";
            else if (list[0] is Weapon)
                _textNameSkills.text = "Оружие";
            else if (list[0] is Armor)
                _textNameSkills.text = "Броня";
            else if (list[0] is Equipment)
                _textNameSkills.text = "Снаряжение";
            else if (list[0] is MechImplant)
                _textNameSkills.text = "Импланты";
            else
                _textNameSkills.text = "Не смогли распознать";
    }    

    protected void ShowDescriptionSkill(string nameSkill)
    {
        ClickSound?.Invoke();
        _descriptionSkill.gameObject.SetActive(true);
        foreach (IName skill in _chosenCell.Skills)
        {
            if (string.Compare(skill.Name, nameSkill, true) == 0)
            {
                if(skill is Skill temp)
                {
                    if (temp.LvlLearned == 1)
                        _textDescriptionSkill.text = $"{temp.Name} \n {temp.Description}";
                    else
                        _textDescriptionSkill.text = $"{temp.Name} (+{temp.LvlLearned * 10 - 10})\n {temp.Description}";
                    _chosenSkill = temp;
                }
                else if(skill is Talent talent)
                {
                    _textDescriptionSkill.text = $"{talent.Name} \n {talent.Description}";
                    _chosenSkill = talent;
                }
                else if(skill is Weapon weapon)
                {
                    _textDescriptionSkill.text = $"{weapon.Name} \n {weapon.Description} \n Урон - {weapon.Damage}\n Бронепробитие {weapon.Penetration} \n Особенности - {weapon.Properties}";
                    _chosenSkill = weapon;
                }
                else if(skill is Armor armor)
                {
                    _textDescriptionSkill.text = $"{armor.Name} \n {armor.Description} \n Броня головы - {armor.DefHead}, \nБроня тела - {armor.DefBody}, " +
                        $"\nБроня рук - {armor.DefHands},\nБроня ног - {armor.DefLegs}";
                    _chosenSkill = armor;
                }
                else if(skill is Equipment equipment)
                {
                    _textDescriptionSkill.text = $"{equipment.Name} \n {equipment.Description}";
                    _chosenSkill = equipment;
                }
                else if(skill is MechImplant implant)
                {
                    _textDescriptionSkill.text = $"{implant.Name} \n {implant.Description}";
                    _chosenSkill = implant;
                }
                
                break;
            }
        }            
    }


    protected bool CheckDontRepeatNames(List<IName> names, IName possibleSkill)
    {
        foreach (IName skill in names)
            if (string.Compare(skill.Name, possibleSkill.Name, true) == 0)
                if (CheckLvlIsSame(skill, possibleSkill))
                    return false;

        return true;
    }

    private bool CheckLvlIsSame(IName possibleSkillInList, IName possibleSkill)
    {
        if(possibleSkill is Skill skill)
        {
            Skill skillInList = (Skill)possibleSkillInList;
            if (skillInList.LvlLearned != skill.LvlLearned)
                return false;
        }
        return true;
        
    }

    private void ChooseSkill()
    {
        ClickSound?.Invoke();
        _descriptionSkill.gameObject.SetActive(false);
        _skillList.gameObject.SetActive(false);
        _chosenCell.SetSkill(_chosenSkill);
        CheckAllCellsAreDone();
    }

    protected void CheckAllCellsAreDone()
    {
        foreach(CellSkill cellSkill in _cells)
        {
            if (cellSkill.IsSkillAdded == false)
                return;
        }   
        _gameObjectButtonDone.SetActive(true);        
    }

    private void PrevRacePressed() => NextRace?.Invoke();

    private void NextRacePressed() => PrevRace?.Invoke();
}
