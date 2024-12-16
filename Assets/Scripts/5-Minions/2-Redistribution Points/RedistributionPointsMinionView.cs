using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class RedistributionPointsMinionView : CanDestroyView
{
    [SerializeField] private TextMeshProUGUI _textSkillPoints, _textTalentPoints, _textTraitPoints;
    [SerializeField] private Button _buttonFromTalentToSkill, _buttonFromTraitToTalent, _buttonCancel, _buttonNext;

    public event Action FromTalentToSkill;
    public event Action FromTraitToTalent;
    public event Action Cancel;
    public event Action Next;

    private void OnEnable()
    {
        _buttonFromTalentToSkill.onClick.AddListener(FromTalentToSkillPressed);
        _buttonFromTraitToTalent.onClick.AddListener(FromTraitToTalentPressed);
        _buttonCancel.onClick.AddListener(CancelPressed);
        _buttonNext.onClick.AddListener(NextPressed);
    }    

    private void OnDisable()
    {
        _buttonFromTalentToSkill.onClick.RemoveAllListeners();
        _buttonFromTraitToTalent.onClick.RemoveAllListeners();
        _buttonCancel.onClick.RemoveAllListeners();
        _buttonNext.onClick.RemoveAllListeners();
    }

    public void SetSkillPoints(int amount) => _textSkillPoints.text = $"{amount}";

    public void SetTalentPoints(int amount) => _textTalentPoints.text = $"{amount}";

    public void SetTraitPoints(int amount) => _textTraitPoints.text = $"{amount}";

    private void FromTalentToSkillPressed() => FromTalentToSkill?.Invoke();

    private void FromTraitToTalentPressed() => FromTraitToTalent?.Invoke();

    private void CancelPressed() => Cancel?.Invoke();

    private void NextPressed() => Next?.Invoke();
}
