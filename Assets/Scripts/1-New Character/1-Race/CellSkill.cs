using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CellSkill : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _spriteNotChoosed, _spriteChoosed;

    public event Action<List<IName>, CellSkill> ShowSkills;

    private List<IName> _skills = new List<IName>();
    private IName _chosenSkill;

    private bool _isListenerAdded = false;
    private bool _isSkillAdded = false;

    public List<IName> Skills => _skills;

    public IName ChosenSkill => _chosenSkill;

    public bool IsSkillAdded => _isSkillAdded;

    private void OnEnable()
    {
        _isListenerAdded = true;
        _button.onClick.AddListener(ShowThisSkills);
    }   

    public void Initialize(List<IName> skills)
    {
        gameObject.SetActive(true);
        if (_isListenerAdded == false)
            OnEnable();
        _skills.AddRange(skills);
    }


    public void SetSkill(IName skill)
    {
        _chosenSkill = skill;
        _isSkillAdded = true;
        _image.sprite = _spriteChoosed;
    }

    private void ShowThisSkills() => ShowSkills?.Invoke(_skills, this);
    

}
