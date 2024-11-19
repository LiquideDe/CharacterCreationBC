using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class ThirdCharacterSheet : TakeScreenshot
{
    [SerializeField] private TextMeshProUGUI _text;
    private CreatorTalents _creatorTalents;
    private CreatorPsyPowers _creatorPsyPowers;
    private CreatorTraits _creatorTraits;
    private int _page;

    [Inject]
    private void Construct(CreatorTalents creatorTalents, CreatorPsyPowers creatorPsyPowers, CreatorTraits creatorFeatures)
    {
        _creatorTalents = creatorTalents;
        _creatorPsyPowers = creatorPsyPowers;
        _creatorTraits = creatorFeatures;
    }

    public void Initialize(ICharacter character)
    {
        _character = character;
        foreach(Talent talent in character.Talents)        
            _text.text += $"<b>{talent.Name}</b> - {_creatorTalents.GetTalent(talent.Name).Description} \n \n";        

        foreach(PsyPower psyPower in character.PsyPowers)        
            _text.text += $"<b>{psyPower.Name}</b> - Действие:{_creatorPsyPowers.GetPsyPower(psyPower.Name).Action}, {_creatorPsyPowers.GetPsyPower(psyPower.Name).Description} \n \n";        

        foreach(MechImplant implant in character.Implants)        
            if (implant.Description.Length > 1)
                _text.text += $"<b>{implant.Name}</b> - {implant.Description}\n \n";

        foreach (Trait trait in character.Traits)
            _text.text += $"<b>{trait.Name}</b> - {_creatorTraits.GetTrait(trait.Name).Description} \n \n";


        StartCoroutine(TakePauseForText());



        
    }

    IEnumerator TakePauseForText()
    {
        yield return new WaitForEndOfFrame();
        if(_text.textInfo.pageCount > 1)
        {
            _page = 1;
            base.PageSaved += PageSavedDown;
            StartScreenshot(PageName.Third.ToString(), true);
        }
        else
        {
            StartScreenshot(PageName.Third.ToString());
        }
        
    }

    private void PageSavedDown()
    {
        _page++;
        _text.pageToDisplay = _page;
        StartCoroutine(TakeAnotherPause());
    }

    IEnumerator TakeAnotherPause()
    {
        _text.pageToDisplay = _page;
        yield return new WaitForSeconds(0.2f);
        StartNextScreenshot();
    }

    private void StartNextScreenshot()
    {
        if (_page == _text.textInfo.pageCount)
            StartScreenshot($"{PageName.Third}+{_page}");
        else
            StartScreenshot($"{PageName.Third}+{_page}", true);
    }
}
