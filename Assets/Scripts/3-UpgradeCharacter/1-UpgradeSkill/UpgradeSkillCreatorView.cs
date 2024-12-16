using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSkillCreatorView : MonoBehaviour
{
    [SerializeField] GameObject _horizontalPrefab;
    [SerializeField] SkillPanel _skillPanelPrefab;
    [SerializeField] Transform _content;
    [SerializeField] UpgradeSkillView _view;
    private List<SkillPanel> _skillPanels = new List<SkillPanel>();
    private List<GameObject> _horizontalLines = new List<GameObject>();
    private ICharacter _character;

    public void Initialize(List<Skill> skills, ICharacter character)
    {
        _character = character;
        if (_skillPanels.Count > 0)
            ClearAll();

        for ( int i = 0; i < skills.Count; i += 3)
        {
            if (i + 2 < skills.Count)
                SetSkillInHorizontal(new List<Skill>() {skills[i], skills[i+1], skills[i+ 2] });

            else if(i+ 1 < skills.Count)
                SetSkillInHorizontal(new List<Skill>() { skills[i], skills[i + 1] });
            else
                SetSkillInHorizontal(new List<Skill>() { skills[i] });
        }

        _view.Initialize(_skillPanels);
    }

    private void ClearAll()
    {
        for(int i = 0; i < _skillPanels.Count; i++)
        {
            _skillPanels[i].DestroySkillPanel();
        }

        _skillPanels.Clear();

        for(int i =0; i < _horizontalLines.Count; i++)
        {
            Destroy(_horizontalLines[i]);
        }
        _horizontalLines.Clear();
    }

    private void SetSkillInHorizontal(List<Skill> skills)
    {
        GameObject horizontal = Instantiate(_horizontalPrefab, _content);
        _horizontalLines.Add(horizontal);
        horizontal.SetActive(true);
        foreach(Skill skill in skills)
        {
            SkillPanel skillPanel = Instantiate(_skillPanelPrefab, horizontal.transform);
            if(_character == null)
                skillPanel.Initialize(skill, 1);
            else
                skillPanel.Initialize(skill, GameStat.CalculateCostSkill(skill, _character));

            _skillPanels.Add(skillPanel);
        }
    }

}
