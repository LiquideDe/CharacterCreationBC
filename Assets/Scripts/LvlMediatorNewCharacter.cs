using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Zenject;

public class LvlMediatorNewCharacter
{
    public event Action ReturnToMenu;
    private LvlFactory _lvlFactory;
    private PresenterFactory _presenterFactory;
    private CharacterFactory _characterFactory;
    private ICharacter _character;

    private delegate void ShowWindow();

    public LvlMediatorNewCharacter(LvlFactory lvlFactory, PresenterFactory presenterFactory, CharacterFactory characterFactory)
    {
        _lvlFactory = lvlFactory;
        _presenterFactory = presenterFactory;
        _characterFactory = characterFactory;
    }

    public void NewCharacter()
    {
        _character = _characterFactory.Get();
        string text = "Это сорок первое тысячелетие. " +
            "Вот уже сотню веков Император недвижимо восседает на Золотом троне Земли. " +
            "По воле богов он – Повелитель Человечества, господин миллионов миров и вождь неисчислимых армий. " +
            "Он – гниющий труп, незримо поддерживаемый силой Тёмной Эры Технологий. " +
            "Он – Великий падальщик, которому каждый день приносят в жертву тысячу душ, чтобы он не мог умереть. " +
            "Быть человеком в такие времена – значит быть одним из бессчётных миллиардов. Это значит жить при самом жестоком и кровавом режиме, который можно вообразить. " +
            "Эта история о тех временах. Забудьте о могуществе технологии и науки, ибо было забыто столь многое, что уже никогда не будет открыто вновь. " +
            "Забудьте об обещаниях прогресса и взаимопонимания, ибо в беспросветном мраке будущего есть только война. " +
            "Нет мира среди звезд, есть лишь вечность бойни и резни под смех кровожадных богов. " +
            "Но есть и те, кто возвышается над роящимися толпами, отвергая господствующие раболепие и конформизм. " +
            "Невежды называют их предателями, еретиками, заблудшими и проклятыми, но они считают себя последователями Тёмных Богов.";
        ShowMessageBetween(text, ShowMessageBeforeWorld);
    }

    private void ShowMessageBeforeWorld() => ShowMessageBetween("В следующем окне вам нужно выбрать свой вид", ShowRace);

    private void ShowRace()
    {
        RaceView raceView = _lvlFactory.Get( TypeScene.Race).GetComponent<RaceView>();
        RacePresenter racePresenter = (RacePresenter)_presenterFactory.Get(TypeScene.Race);
        racePresenter.CharacterDone += CharacterHasRace;
        racePresenter.Initialize(raceView, _character);
    }


    private void CharacterHasRace(ICharacter character)
    {
        _character = character;
        ShowMessageBeforeArchetype();        
    }

    private void ShowMessageBeforeArchetype() => ShowMessageBetween("В следующем окне вам нужно выбрать свой архетип", ShowAcrhetypes);

    private void ShowAcrhetypes()
    {
        ArchetypeView view = _lvlFactory.Get(TypeScene.Archetype).GetComponent<ArchetypeView>();
        ArchetypePresenter presenter = (ArchetypePresenter)_presenterFactory.Get(TypeScene.Archetype);
        presenter.CharacterDone += CharacterHasArchetype;
        presenter.ReturnBackToRace += ShowRace;
        presenter.Initialize(view, _character);
    }

    private void CharacterHasArchetype(ICharacter character)
    {
        _character = character;
        ShowMessageBeforeMotivation();
    }

    private void ShowMessageBeforeMotivation()
    {
        string text = "В следующем окне вам нужно выбрать Гордость, Порок и Мотивацию. " +
            "Гордость и порок – две величайшие слабости персонажа. Благодаря таким граням личности, он лучше вписывается в истории. " +
            "Они рассказывают, из-за чего персонаж решил отдать свою душу Тёмным богам. " +
            "Эти черты важны как для перерождения обычного человека в слугу Хаоса, так и для дальнейшего падения в пучину скверны.";
        ShowMessageBetween(text, ShowMotivations);
    }

    private void ShowMotivations()
    {
        MotivationsView motivationsView = _lvlFactory.Get( TypeScene.Motivations).GetComponent<MotivationsView>();
        MotivationPresenter motivationPresenter = (MotivationPresenter)_presenterFactory.Get( TypeScene.Motivations);

        motivationPresenter.GoBack += ShowAcrhetypes;
        motivationPresenter.ReturnCharacterWithMotivations += CharacterHasMotivation;

        motivationPresenter.Initialize(_character, motivationsView);
    }

    private void CharacterHasMotivation(ICharacter character)
    {
        _character = character;
        ShowMessageBeforeRandomCharacteristics();
    }

    private void Zatichka(ICharacter character)
    {
        ShowRandom();
    }

    private void ShowMessageBeforeRandomCharacteristics()  => ShowMessageBetween(
        "В следующем окне вам нужно сгенерировать значения и расставить их по характеристикам", ShowRandom);
    

    private void ShowRandom()
    {
        
        CharacteristicRandomView characteristicRandomView = _lvlFactory.Get(TypeScene.RandomCharacteristic).GetComponent<CharacteristicRandomView>();
        CharacteristicRandomPresenter characteristicRandomPresenter = (CharacteristicRandomPresenter)_presenterFactory.Get(TypeScene.RandomCharacteristic);
        characteristicRandomPresenter.ReturnToMotivation += ShowMotivations;
        characteristicRandomPresenter.ReturnCharacterWithCharacteristics += CharacterHasCharacteristics;
        characteristicRandomPresenter.Initialize(_character, characteristicRandomView);
    }

    private void CharacterHasCharacteristics(ICharacter character)
    {
        _character = character;
        if (_character.PsyRating > 0)
            ShowMessageBeforeTrainingPsy();
        else
            ShowMessageBeforeTrainingCharacteristics();
    }

    private void ShowMessageBeforeTrainingPsy() => ShowMessageBetween("В следующем окне выберите пси силы", UpgradePsypowerFree);

    private void UpgradePsypowerFree()
    {
        GameObject gameObject = _lvlFactory.Get(TypeScene.UpgradePsycana);
        UpgradePsycanaView upgradePsycana = gameObject.GetComponent<UpgradePsycanaView>();
        PsycanaCreatorView psycanaCreatorView = gameObject.GetComponent<PsycanaCreatorView>();
        UpgradePsycanaPresenter presenter = (UpgradePsycanaPresenter)_presenterFactory.Get(TypeScene.UpgradePsycana);

        presenter.ReturnToTalent += CharacterHasCharacteristics;
        presenter.GoNext += CharacterHasPsyPowers;
        presenter.Initialize(_character, psycanaCreatorView, upgradePsycana);
    }

    private void CharacterHasPsyPowers(ICharacter character)
    {
        _character = character;
        ShowMessageBeforeTrainingCharacteristics();
    }

    private void ShowMessageBeforeTrainingCharacteristics()
    {
        string text = "В следующих трех окнах вы можете прокачать персонажа: вам доступно 500 или 1000 Очков Опыта, которые можно потратить как на " +
            "улучшение храктеристик, так и на навыки и таланты или пси силы. Вы можете свободно переключаться между окнами прокачки. ";
        ShowMessageBetween(text, PrepareCharacterAnus);
    }

    private void PrepareCharacterAnus()
    {
        CharacterWithUpgrade character = new CharacterWithUpgrade(_character);
        if (character.ExperienceUnspent > 0)
            character.SetExperience(-1 * character.ExperienceUnspent);
        if(string.Compare(_character.Race, "Космодесантник", true) == 0)
            character.SetExperience(500);
        else
            character.SetExperience(1000);
        ShowUpgradeCharacteristics(character);
    }

    private void ShowUpgradeCharacteristics(ICharacter character)
    {  
        UpgradeCharacteristicsView upgradeCharacteristicsView = _lvlFactory.Get(TypeScene.UpgradeCharacteristic).GetComponent<UpgradeCharacteristicsView>();
        UpgradeCharacteristicsPresenter characteristicsPresenter = (UpgradeCharacteristicsPresenter)_presenterFactory.Get(TypeScene.UpgradeCharacteristic);
        characteristicsPresenter.GoNext += ShowUpgradeSkill;
        characteristicsPresenter.ReturnToPrev += Zatichka;
        characteristicsPresenter.Initialize(character, upgradeCharacteristicsView, true);
    }

    private void ShowUpgradeSkill(ICharacter character)
    {
        GameObject gameObject = _lvlFactory.Get(TypeScene.UpgradeSkill);
        UpgradeSkillCreatorView creatorView = gameObject.GetComponent<UpgradeSkillCreatorView>();
        UpgradeSkillView skillView = gameObject.GetComponent<UpgradeSkillView>();

        UpgradeSkillPresenter skillPresenter = (UpgradeSkillPresenter)_presenterFactory.Get(TypeScene.UpgradeSkill);
        skillPresenter.GoToTalent += ShowUpgradeTalent;
        skillPresenter.ReturnToCharacteristics += ShowUpgradeCharacteristics;
        skillPresenter.Initialize(skillView, creatorView, character);
    }

    private void ShowUpgradeTalent(ICharacter character)
    {
        UpgradeTalentView upgradeTalent = _lvlFactory.Get(TypeScene.UpgradeTalent).GetComponent<UpgradeTalentView>();
        UpgradeTalentPresenter talentPresenter = (UpgradeTalentPresenter)_presenterFactory.Get(TypeScene.UpgradeTalent);
        talentPresenter.ReturnToSkill += ShowUpgradeSkill;

        if (character.PsyRating > 0)
            talentPresenter.GoNext += CharacterHasUpgrades;
        else
            talentPresenter.GoNext += CharacterHasUpgrades;

        talentPresenter.Initialize(upgradeTalent, character);

    }

    private void ShowPsycana(ICharacter character)
    {
        GameObject gameObject = _lvlFactory.Get(TypeScene.UpgradePsycana);
        UpgradePsycanaView upgradePsycana = gameObject.GetComponent<UpgradePsycanaView>();
        PsycanaCreatorView psycanaCreatorView = gameObject.GetComponent<PsycanaCreatorView>();
        UpgradePsycanaPresenter presenter = (UpgradePsycanaPresenter)_presenterFactory.Get(TypeScene.UpgradePsycana);

        presenter.ReturnToTalent += ShowUpgradeTalent;
        presenter.GoNext += CharacterHasUpgrades;
        presenter.Initialize(character, psycanaCreatorView, upgradePsycana);
    }

    private void CharacterHasUpgrades(ICharacter character)
    {
        _character = character;
        string text = "В следующем окне введите имя и описание персонажа.";
        ShowMessageBetween(text, ShowChooseName);
    }


    private void ShowChooseName()
    {
        
        ChooseNameView chooseNameView = _lvlFactory.Get(TypeScene.Name).GetComponent<ChooseNameView>();
        ChooseNamePresenter namePresenter = (ChooseNamePresenter)_presenterFactory.Get( TypeScene.Name);

        namePresenter.GoNext += TakePictures;
        namePresenter.Initialize(chooseNameView, _character);
    }

    private void TakePictures(ICharacter character)
    {
        FirstCharacterSheet firstCharacterSheet = _lvlFactory.Get(TypeScene.FirstPage).GetComponent<FirstCharacterSheet>();
        SecondCharacterSheet secondCharacterSheet = _lvlFactory.Get(TypeScene.SecondPage).GetComponent<SecondCharacterSheet>();
        ThirdCharacterSheet thirdCharacterSheet = _lvlFactory.Get(TypeScene.ThirdPage).GetComponent<ThirdCharacterSheet>();
        FourthCharacterSheet fourthCharacterSheet = _lvlFactory.Get( TypeScene.FourthPage).GetComponent<FourthCharacterSheet>();

        firstCharacterSheet.gameObject.SetActive(false);
        secondCharacterSheet.gameObject.SetActive(false);
        thirdCharacterSheet.gameObject.SetActive(false);
        fourthCharacterSheet.gameObject.SetActive (false);

        TakePicturesPresenter picturesPresenter = (TakePicturesPresenter)_presenterFactory.Get(TypeScene.Pictures);
        picturesPresenter.WorkIsFinished += SaveCharacterAndExit;
        picturesPresenter.Initialize(firstCharacterSheet, secondCharacterSheet, thirdCharacterSheet, fourthCharacterSheet, character);
    }

    private void SaveCharacterAndExit(ICharacter character)
    {
        new Save(character);
        ReturnToMenu?.Invoke();
    }

    private void ShowMessageBetween(string text, CanvasIntermediate.NextTask nextTask)
    {
        CanvasIntermediate canvasIntermediate = _lvlFactory.Get(TypeScene.Intermediate).GetComponent<CanvasIntermediate>();
        canvasIntermediate.gameObject.SetActive(true);
        canvasIntermediate.OpenIntermediatePanel(nextTask, text);
    }

}
