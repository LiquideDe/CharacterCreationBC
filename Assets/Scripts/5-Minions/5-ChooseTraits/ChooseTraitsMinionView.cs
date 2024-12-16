using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChooseTraitsMinionView : CanDestroyView
{
    [SerializeField] private Button _buttonPrev, _buttonAdd, _buttonNext, _buttonCancel;
    [SerializeField] private Transform _contentBuying, _contentCharacter;
    [SerializeField] private ItemInList _itemInBuyListPrefab, _itemInCharacterListPrefab;
    [SerializeField] private TextMeshProUGUI _textPoints, _textDescription;

    public event Action<string> ShowThisTrait;
    public event Action AddTrait;
    public event Action ReturnPrev;
    public event Action CancelChanges;
    public event Action GoNext;
    private List<ItemInList> _itemsInTraitList = new List<ItemInList>();
    private List<ItemInList> _itemsInCharacterList = new List<ItemInList>();

    private void OnEnable()
    {
        _buttonPrev.onClick.AddListener(PrevPressed);
        _buttonAdd.onClick.AddListener(AddPressed);
        _buttonNext.onClick.AddListener(DonePressed);
        _buttonCancel.onClick.AddListener(CancelPressed);
    }    

    private void OnDisable()
    {
        _buttonPrev.onClick.RemoveAllListeners();
        _buttonAdd.onClick.RemoveAllListeners();
    }

    public void ShowTraits(List<TraitWithCost> traitsToBuy, List<TraitWithCost> characterTraits)
    {
        if (_itemsInCharacterList.Count > 0)
            ClearListGameObjects(_itemsInCharacterList);
        if(_itemsInTraitList.Count > 0)
            ClearListGameObjects(_itemsInTraitList);

        foreach (var item in traitsToBuy)
        {
            ItemInList itemInList = Instantiate(_itemInBuyListPrefab, _contentBuying);
            itemInList.ChooseThis += ShowThisTraitPressed;
            itemInList.Initialize(item.Name);
            _itemsInCharacterList.Add(itemInList);
        }

        foreach (var item in characterTraits)
        {
            ItemInList itemInList = Instantiate(_itemInCharacterListPrefab, _contentCharacter);
            if(item.MaxLvl == 0)
                itemInList.Initialize(item.Name);
            else            
                itemInList.Initialize($"{item.Name}({item.Lvl})");
            _itemsInTraitList.Add(itemInList);
        }
    }

    public void SetTraitPoints(int amount) => _textPoints.text = $"Очков: {amount}";

    public void SetDescriptionText(string description) => _textDescription.text = description;

    public void ShowButtonBuy() => _buttonAdd.gameObject.SetActive(true);

    public void HideButtonBuy() => _buttonAdd.gameObject.SetActive(false);

    private void ShowThisTraitPressed(string nameTrait) => ShowThisTrait?.Invoke(nameTrait);

    private void AddPressed() => AddTrait?.Invoke();

    private void PrevPressed() => ReturnPrev?.Invoke();

    private void ClearListGameObjects(List<ItemInList> itemInLists)
    {
        foreach (var item in itemInLists)
        {
            Destroy(item.gameObject);
        }
        itemInLists.Clear();
    }

    private void DonePressed() => GoNext?.Invoke();

    private void CancelPressed() => CancelChanges?.Invoke();

}
