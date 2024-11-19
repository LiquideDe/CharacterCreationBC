using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UpgradeTalentView : CanDestroyView
{
    [SerializeField] private TalentList _talentList;
    [SerializeField] private Button _buttonPrev, _buttonNext, _buttonStudy, _buttonCancel;
    [SerializeField] private TextMeshProUGUI _textDescription, _textExperience;
    [SerializeField] private Transform _contentCategory;
    [SerializeField] private ItemInList _itemCategoryPrefab;

    public event Action Next, Prev, Cancel;
    public event Action LearnTalent;
    public event Action<Talent> ShowThisTalent;
    public event Action<string> ShowThisCategory;


    private void OnEnable()
    {
        _buttonCancel.onClick.AddListener(CancelPressed);
        _buttonNext.onClick.AddListener(NextPressed);
        _buttonPrev.onClick.AddListener(PrevPressed);
        _buttonStudy.onClick.AddListener(LearnTalentPressed);
        _talentList.ShowThisTalent += ShowThisTalentPressed;
    }

    private void OnDisable()
    {
        _buttonCancel.onClick.RemoveAllListeners();
        _buttonNext.onClick.RemoveAllListeners();
        _buttonPrev.onClick.RemoveAllListeners();
        _buttonStudy.onClick.RemoveAllListeners();
        _talentList.ShowThisTalent -= ShowThisTalentPressed;
    }

    public void Initialize(List<Talent> talents, List<int> costs, List<bool> isCanTaken) => _talentList.Initialize(talents, costs, isCanTaken);    

    public void SetCategories(List<string> categories)
    {
        foreach(string name in categories)
        {
            ItemInList itemInList = Instantiate(_itemCategoryPrefab, _contentCategory);
            itemInList.ChooseThis += ShowThisCategoryPressed;
            itemInList.Initialize(name);
        }        
    }    

    public void ShowTalent(Talent talent, bool isCanTaken, int cost)
    {
        _buttonStudy.gameObject.SetActive(true);
        _textDescription.text = $"{talent.Name} \n Стоимость {cost} ОО \n {talent.Description} \n {talent.ListRequirments}";
        if (isCanTaken == false)
            _buttonStudy.gameObject.SetActive(false);
    }

    public void CleanTalent()
    {
        _textDescription.text = "";
        _buttonStudy.gameObject.SetActive(false);
    }

    public void UpdateExperience(string text) => _textExperience.text = text;

    private void NextPressed() => Next?.Invoke();

    private void PrevPressed() => Prev?.Invoke();

    private void CancelPressed() => Cancel?.Invoke();

    private void ShowThisTalentPressed(Talent talent) => ShowThisTalent?.Invoke(talent);

    private void LearnTalentPressed() => LearnTalent?.Invoke();
   

    private void ShowThisCategoryPressed(string nameCategory) => ShowThisCategory?.Invoke(nameCategory);

}
