using System;
using Zenject;

public class ChooseTypeMinionPresenter : IPresenter
{
    private ChooseTypeMinionView _view;
    private AudioManager _audioManager;
    private CreatorFactory _creatorFactory;
    private CreatorMinion _creatorMinion;

    public event Action<TypeMinion> ReturnMinionToManual;
    public event Action<TypeMinion> ReturnMinionToRandom;

    [Inject]
    private void Construct(AudioManager audioManager, CreatorFactory creatorFactory)
    {
        _audioManager = audioManager;
        _creatorFactory = creatorFactory;
    }

    public void Initialize(ChooseTypeMinionView view)
    {
        _view = view;
        _creatorMinion = (CreatorMinion)_creatorFactory.Get(TypeCreator.Minion);
        Subscribe();
    }

    private void Subscribe()
    {
        _view.ChooseBeast += ChooseBeast;
        _view.ChooseDaemon += ChooseDaemon;
        _view.ChooseHuman += ChooseHuman;
        _view.ChooseMachine += ChooseMachine;
    }

    private void Unscribe()
    {
        _view.ChooseBeast -= ChooseBeast;
        _view.ChooseDaemon -= ChooseDaemon;
        _view.ChooseHuman -= ChooseHuman;
        _view.ChooseMachine -= ChooseMachine;
    }

    private void ChooseMachine(string type)
    {
        TypeMinion minion = _creatorMinion.Get($"Машина {type}");
        CloseWindow(minion);
    }

    private void ChooseHuman(string type)
    {
        TypeMinion minion = _creatorMinion.Get($"Человек {type}");
        _audioManager.PlayClick();
        ReturnMinionToRandom?.Invoke(minion);
        Unscribe();
        _view.DestroyView();
    }

    private void ChooseDaemon(string type)
    {
        TypeMinion minion = _creatorMinion.Get($"Демон {type}");
        CloseWindow(minion);
    }

    private void ChooseBeast(string type)
    {        
        TypeMinion minion = _creatorMinion.Get($"Зверь {type}");
        CloseWindow(minion);
    }

    private void CloseWindow(TypeMinion minion)
    {
        _audioManager.PlayClick();
        ReturnMinionToManual?.Invoke( minion );
        Unscribe();
        _view.DestroyView();
    }
}
