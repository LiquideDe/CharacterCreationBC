using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ManualCharacteristicsMinionView : CanDestroyView
{
    [SerializeField] private Button _buttonPlusWeapon, _buttonMinusWeapon;
    [SerializeField] private Button _buttonPlusBallistic, _buttonMinusBallistic;
    [SerializeField] private Button _buttonPlusStrength, _buttonMinusStrength;
    [SerializeField] private Button _buttonPlusToughness, _buttonMinusToughness;
    [SerializeField] private Button _buttonPlusAgility, _buttonMinusAgility;
    [SerializeField] private Button _buttonPlusIntelligence, _buttonMinusIntelligence;
    [SerializeField] private Button _buttonPlusPerception, _buttonMinusPerception;
    [SerializeField] private Button _buttonPlusWillpower, _buttonMinusWillpower;
    [SerializeField] private Button _buttonPlusFellowship, _buttonMinusFellowship;

    [SerializeField]
    private TextMeshProUGUI _textAmountWeapon, _textAmountBallistic, _textAmountStrength, _textAmountToughness, _textAmountAgility,
        _textAmountIntelligence, _textAmountPerception, _textAmountWillpower, _textAmountFellowship;

    [SerializeField] private TextMeshProUGUI _textDescribeMaxMin, _textPoints, _textWounds;

    [SerializeField] private Button _buttonNext;

    public event Action PlusWeapon, PlusBallistic, PlusStrength, PlusToughness, PlusAgility, PlusIntelligence, PlusPerception, PlusWillpower, PlusFellowship;
    public event Action MinusWeapon, MinusBallistic, MinusStrength, MinusToughness, MinusAgility, MinusIntelligence, MinusPerception, MinusWillpower, MinusFellowship;
    public event Action GoNext;

    private void OnEnable()
    {
        _buttonPlusWeapon.onClick.AddListener(PlusWeaponPressed);
        _buttonPlusBallistic.onClick.AddListener(PlusBallisticPressed);
        _buttonPlusStrength.onClick.AddListener(PlusStrengthPressed);
        _buttonPlusToughness.onClick.AddListener(PlusToughnessPressed);
        _buttonPlusAgility.onClick.AddListener(PlusAgilityPressed);
        _buttonPlusIntelligence.onClick.AddListener(PlusIntelligencePressed);
        _buttonPlusPerception.onClick.AddListener(PlusPerceptionPressed);
        _buttonPlusWillpower.onClick.AddListener(PlusWillpowerPressed);
        _buttonPlusFellowship.onClick.AddListener(PlusFellowshipPressed);

        _buttonMinusWeapon.onClick.AddListener(MinusWeaponPressed);
        _buttonMinusBallistic.onClick.AddListener(MinusBallisticPressed);
        _buttonMinusStrength.onClick.AddListener(MinusStrengthPressed);
        _buttonMinusToughness.onClick.AddListener(MinusToughnessPressed);
        _buttonMinusAgility.onClick.AddListener(MinusAgilityPressed);
        _buttonMinusIntelligence.onClick.AddListener(MinusIntelligencePressed);
        _buttonMinusPerception.onClick.AddListener(MinusPerceptionPressed);
        _buttonMinusWillpower.onClick.AddListener(MinusWillpowerPressed);
        _buttonMinusFellowship.onClick.AddListener(MinusFellowshipPressed);

        _buttonNext.onClick.AddListener(GoNextPressed);
    }
    

    private void OnDisable()
    {
        _buttonPlusWeapon.onClick.RemoveAllListeners();
        _buttonPlusBallistic.onClick.RemoveAllListeners();
        _buttonPlusStrength.onClick.RemoveAllListeners();
        _buttonPlusToughness.onClick.RemoveAllListeners();
        _buttonPlusAgility.onClick.RemoveAllListeners();
        _buttonPlusIntelligence.onClick.RemoveAllListeners();
        _buttonPlusPerception.onClick.RemoveAllListeners();
        _buttonPlusWillpower.onClick.RemoveAllListeners();
        _buttonPlusFellowship.onClick.RemoveAllListeners();

        _buttonMinusWeapon.onClick.RemoveAllListeners();
        _buttonMinusBallistic.onClick.RemoveAllListeners();
        _buttonMinusStrength.onClick.RemoveAllListeners();
        _buttonMinusToughness.onClick.RemoveAllListeners();
        _buttonMinusAgility.onClick.RemoveAllListeners();
        _buttonMinusIntelligence.onClick.RemoveAllListeners();
        _buttonMinusPerception.onClick.RemoveAllListeners();
        _buttonMinusWillpower.onClick.RemoveAllListeners();
        _buttonMinusFellowship.onClick.RemoveAllListeners();

        _buttonNext.onClick.RemoveAllListeners();
    }

    public void SetWeapon(int amount) => _textAmountWeapon.text = $"{amount}";

    public void SetBallistic(int amount) => _textAmountBallistic.text = $"{amount}";

    public void SetStrength(int amount) => _textAmountStrength.text = $"{amount}";
    public void SetToughness(int amount) => _textAmountToughness.text = $"{amount}";
    public void SetAgility(int amount) => _textAmountAgility.text = $"{amount}";
    public void SetIntelligence(int amount) => _textAmountIntelligence.text = $"{amount}";
    public void SetPerception(int amount) => _textAmountPerception.text = $"{amount}";
    public void SetWillpower(int amount) => _textAmountWillpower.text = $"{amount}";
    public void SetFellowship(int amount) => _textAmountFellowship.text = $"{amount}";

    public void SetWounds(int amount) => _textWounds.text = $"Здоровье Слуги {amount}";

    public void SetMinMax(string text,List<Trait> restrictions)
    {
        _textDescribeMaxMin.text = text + "\n";
        foreach (var item in restrictions)
        {
            _textDescribeMaxMin.text += item.Name + "\n";
        }
    }

    public void SetPoints(int points) => _textPoints.text = $"Очков Осталось {points}";

    public void ShowButton() => _buttonNext.gameObject.SetActive(true);

    private void MinusFellowshipPressed() => MinusFellowship?.Invoke();

    private void MinusWillpowerPressed() => MinusWillpower?.Invoke();

    private void MinusPerceptionPressed() => MinusPerception?.Invoke();

    private void MinusIntelligencePressed() => MinusIntelligence?.Invoke();

    private void MinusAgilityPressed() => MinusAgility?.Invoke();

    private void MinusToughnessPressed() => MinusToughness?.Invoke();

    private void MinusStrengthPressed() => MinusStrength?.Invoke();

    private void MinusBallisticPressed() => MinusBallistic?.Invoke();

    private void MinusWeaponPressed() => MinusWeapon?.Invoke();

    private void PlusFellowshipPressed() => PlusFellowship?.Invoke();

    private void PlusWillpowerPressed() => PlusWillpower?.Invoke();

    private void PlusPerceptionPressed() => PlusPerception?.Invoke();

    private void PlusIntelligencePressed() => PlusIntelligence?.Invoke();

    private void PlusAgilityPressed() => PlusAgility?.Invoke();

    private void PlusToughnessPressed() => PlusToughness?.Invoke();

    private void PlusStrengthPressed() => PlusStrength?.Invoke();

    private void PlusBallisticPressed() => PlusBallistic?.Invoke();

    private void PlusWeaponPressed() => PlusWeapon?.Invoke();

    private void GoNextPressed() => GoNext?.Invoke();
}
