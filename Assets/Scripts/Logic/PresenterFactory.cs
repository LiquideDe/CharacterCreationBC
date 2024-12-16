using Zenject;
using System;
using Unity.VisualScripting;

public class PresenterFactory
{
    private DiContainer _diContainer;

    public PresenterFactory(DiContainer diContainer) => _diContainer = diContainer;

    public IPresenter Get(TypeScene type)
    {
        switch (type)
        {

            case TypeScene.MainMenu:
                return _diContainer.Instantiate<MainMenuPresenter>();

            case TypeScene.Race:
                return _diContainer.Instantiate<RacePresenter>();

            case TypeScene.Archetype:
                return _diContainer.Instantiate<ArchetypePresenter>();

            case TypeScene.Motivations:
                return _diContainer.Instantiate<MotivationPresenter>();

            case TypeScene.RandomCharacteristic:
                return _diContainer.Instantiate<CharacteristicRandomPresenter>();

            case TypeScene.UpgradeCharacteristic:
                return _diContainer.Instantiate<UpgradeCharacteristicsPresenter>();

            case TypeScene.UpgradeSkill:
                return _diContainer.Instantiate<UpgradeSkillPresenter>();

            case TypeScene.UpgradeTalent:
                return _diContainer.Instantiate<UpgradeTalentPresenter>();

            case TypeScene.UpgradePsycana:
                return _diContainer.Instantiate<UpgradePsycanaPresenter>();

            case TypeScene.Name:
                return _diContainer.Instantiate<ChooseNamePresenter>();

            case TypeScene.Pictures:
                return _diContainer.Instantiate<TakePicturesPresenter>();

            case TypeScene.Loads:
                return _diContainer.Instantiate<CharacterLoadsPresenter>();

            case TypeScene.InputExperience:
                return _diContainer.Instantiate<SetExperiencePresenter>();

            case TypeScene.FinalMenu:
                return _diContainer.Instantiate<FinalMenuPresenter>();

            case TypeScene.EditProperties:
                return _diContainer.Instantiate<EditPropertyCharacterPresenter>();

            case TypeScene.EditCharacteristicsAndEquipments:
                return _diContainer.Instantiate<EditCharacteristicsAndEquipmentsPresenter>();

            case TypeScene.ChooseTypeMinion:
                return _diContainer.Instantiate<ChooseTypeMinionPresenter>();

            case TypeScene.ManualCharacteristicMinion:
                return _diContainer.Instantiate<ManualCharacteristicsMinionPresenter>();

            case TypeScene.RandomCharacteristicMinion:
                return _diContainer.Instantiate<SetRandomCharacteristicMinionPresenter>();

            case TypeScene.RedistributionMinion:
                return _diContainer.Instantiate<RedistributionPointsMinionPresenter>();

            case TypeScene.ChooseSkillMinion:
                return _diContainer.Instantiate<MinionUpgradeSkillPresenter>();

            case TypeScene.ChooseTalentMinion:
                return _diContainer.Instantiate<UpgradeTalentMinionPresenter>();

            case TypeScene.ChooseTraitMinion:
                return _diContainer.Instantiate<ChooseTraitsMinionPresenter>();

            case TypeScene.ChooseNameMinion:
                return _diContainer.Instantiate<ChooseNameMinionPresenter>();

            case TypeScene.ChooseArmorMinion:
                return _diContainer.Instantiate<ChooseArmorWeaponMinionPresenter>();

            default:
                throw new ArgumentException(nameof(type));
        }
    }
}
