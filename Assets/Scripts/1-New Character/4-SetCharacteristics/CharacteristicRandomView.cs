using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Zenject;

public class CharacteristicRandomView : CanDestroyView
{
    [SerializeField]
    private CharacteristicCard _weaponSkill, _ballisticSkill, _strength, _toughness, _agility, _intelligence, _perception, _willpower,
        _social, _wounds, _infamy;

    [SerializeField] private Button _buttonDone, _buttonPrev, _buttonRandom;

    [SerializeField] private GameObject _randomCardPrefab;
    [SerializeField] private Transform _content;

    private List<GameObject> _randomCards = new List<GameObject>();

    public event Action ReturnToMotivation;
    public event Action CharacteristicsIsReady, DonePressed;
    public event Action GenerateRandom;

    public CharacteristicCard WeaponSkill => _weaponSkill; 
    public CharacteristicCard BallisticSkill => _ballisticSkill; 
    public CharacteristicCard Strength => _strength;
    public CharacteristicCard Toughness => _toughness;
    public CharacteristicCard Agility => _agility;
    public CharacteristicCard Intelligence => _intelligence;
    public CharacteristicCard Perception => _perception;
    public CharacteristicCard Willpower => _willpower;
    public CharacteristicCard Social => _social;  
    public CharacteristicCard Wounds => _wounds;  
    public CharacteristicCard Infamy => _infamy;  

    private void OnEnable()
    {
        _buttonDone.onClick.AddListener(DonePress);
        _buttonPrev.onClick.AddListener(PrevPress);
        _buttonRandom.onClick.AddListener(GenerateRandomPressed);
        _weaponSkill.AmountFromRandomIsSeted += CheckAllRandomsAreSeted;
        _ballisticSkill.AmountFromRandomIsSeted += CheckAllRandomsAreSeted;
        _strength.AmountFromRandomIsSeted += CheckAllRandomsAreSeted;
        _toughness.AmountFromRandomIsSeted += CheckAllRandomsAreSeted;
        _agility.AmountFromRandomIsSeted += CheckAllRandomsAreSeted;
        _intelligence.AmountFromRandomIsSeted += CheckAllRandomsAreSeted;
        _perception.AmountFromRandomIsSeted += CheckAllRandomsAreSeted;
        _willpower.AmountFromRandomIsSeted += CheckAllRandomsAreSeted;
        _social.AmountFromRandomIsSeted += CheckAllRandomsAreSeted;
        _wounds.AmountFromRandomIsSeted += CheckAllRandomsAreSeted;
        _infamy.AmountFromRandomIsSeted += CheckAllRandomsAreSeted;
    }

    private void OnDisable()
    {
        _buttonDone.onClick.RemoveAllListeners();
        _buttonPrev.onClick.RemoveAllListeners();
        _buttonRandom.onClick.RemoveAllListeners();
        _weaponSkill.AmountFromRandomIsSeted -= CheckAllRandomsAreSeted;
        _ballisticSkill.AmountFromRandomIsSeted -= CheckAllRandomsAreSeted;
        _strength.AmountFromRandomIsSeted -= CheckAllRandomsAreSeted;
        _toughness.AmountFromRandomIsSeted -= CheckAllRandomsAreSeted;
        _agility.AmountFromRandomIsSeted -= CheckAllRandomsAreSeted;
        _intelligence.AmountFromRandomIsSeted -= CheckAllRandomsAreSeted;
        _perception.AmountFromRandomIsSeted -= CheckAllRandomsAreSeted;
        _willpower.AmountFromRandomIsSeted -= CheckAllRandomsAreSeted;
        _social.AmountFromRandomIsSeted -= CheckAllRandomsAreSeted;
        _wounds.AmountFromRandomIsSeted -= CheckAllRandomsAreSeted;
        _infamy.AmountFromRandomIsSeted -= CheckAllRandomsAreSeted;
    }

    public void Initialize(List<int> generatedNumbers)
    {
        if (_randomCards.Count > 0)
        {
            foreach (var card in _randomCards)
                Destroy(card);
            _randomCards.Clear();


            _weaponSkill.ResetAmount();
            _ballisticSkill.ResetAmount();
            _strength.ResetAmount();
            _toughness.ResetAmount();
            _agility.ResetAmount();
            _intelligence.ResetAmount();
            _perception.ResetAmount();
            _willpower.ResetAmount();
            _social.ResetAmount();
            _wounds.ResetAmount();
            _infamy.ResetAmount();
        }

        for(int i = 0; i < generatedNumbers.Count; i++)
        {
            GameObject randomCard = Instantiate(_randomCardPrefab, _content);
            randomCard.SetActive(true);
            CardWithNumber cardWithNumber = randomCard.GetComponentInChildren<CardWithNumber>();
            cardWithNumber.Initialize(generatedNumbers[i]);
            _randomCards.Add(randomCard);
        }

    }

    public void ShowButtonDone() => _buttonDone.gameObject.SetActive(true);

    private void DonePress() => DonePressed?.Invoke();

    private void CheckAllRandomsAreSeted()
    {
        if (_weaponSkill.IsSetAmountFromRandomCard && _ballisticSkill.IsSetAmountFromRandomCard && _strength.IsSetAmountFromRandomCard &&
            _toughness.IsSetAmountFromRandomCard && _agility.IsSetAmountFromRandomCard && _intelligence.IsSetAmountFromRandomCard &&
            _perception.IsSetAmountFromRandomCard && _willpower.IsSetAmountFromRandomCard && _social.IsSetAmountFromRandomCard)
        {

            CharacteristicsIsReady?.Invoke();
        }
    }

    private void PrevPress() => ReturnToMotivation?.Invoke();

    private void GenerateRandomPressed() => GenerateRandom?.Invoke();
}
