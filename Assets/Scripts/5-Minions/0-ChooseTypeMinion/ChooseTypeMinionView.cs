using UnityEngine;
using UnityEngine.UI;
using System;

public class ChooseTypeMinionView : CanDestroyView
{
    [SerializeField] private Button _buttonHumanLow, _buttonHumanHigh, _buttonHumanAverage;
    [SerializeField] private Button _buttonBeastLow, _buttonBeastHigh, _buttonBeastAverage;
    [SerializeField] private Button _buttonMachineLow, _buttonMachineHigh, _buttonMachineAverage;
    [SerializeField] private Button _buttonDaemonLow, _buttonDaemonHigh, _buttonDaemonAverage;

    public event Action<string> ChooseHuman;

    public event Action<string> ChooseBeast;

    public event Action<string> ChooseMachine;

    public event Action<string> ChooseDaemon;

    private void OnEnable()
    {
        _buttonHumanLow.onClick.AddListener(ChooseHumanLowPressed);
        _buttonHumanAverage.onClick.AddListener(ChooseHumanAveragePressed);
        _buttonHumanHigh.onClick.AddListener(ChooseHumanHighPressed);

        _buttonBeastLow.onClick.AddListener(ChooseBeastLowPressed);
        _buttonBeastAverage.onClick.AddListener(ChooseBeastAveragePressed);
        _buttonBeastHigh.onClick.AddListener(ChooseBeastHighPressed);

        _buttonMachineLow.onClick.AddListener(ChooseMachineLowPressed);
        _buttonMachineAverage.onClick.AddListener(ChooseMachineAveragePressed);
        _buttonMachineHigh.onClick.AddListener(ChooseMachineHighPressed);

        _buttonDaemonLow.onClick.AddListener(ChooseDaemonLowPressed);
        _buttonDaemonAverage.onClick.AddListener(ChooseDaemonAveragePressed);
        _buttonDaemonHigh.onClick.AddListener(ChooseDaemonHighPressed);
    }    

    private void OnDisable()
    {
        _buttonHumanLow.onClick.RemoveAllListeners();
        _buttonHumanAverage.onClick.RemoveAllListeners();
        _buttonHumanHigh.onClick.RemoveAllListeners();

        _buttonBeastLow.onClick.RemoveAllListeners();
        _buttonBeastAverage.onClick.RemoveAllListeners();
        _buttonBeastHigh.onClick.RemoveAllListeners();

        _buttonMachineLow.onClick.RemoveAllListeners();
        _buttonMachineAverage.onClick.RemoveAllListeners();
        _buttonMachineHigh.onClick.RemoveAllListeners();

        _buttonDaemonLow.onClick.RemoveAllListeners();
        _buttonDaemonAverage.onClick.RemoveAllListeners();
        _buttonDaemonHigh.onClick.RemoveAllListeners();
    }

    private void ChooseDaemonHighPressed()
    {
        ChooseDaemon?.Invoke("Высший");
        Destroy(gameObject);
    }

    private void ChooseDaemonAveragePressed() { 
        ChooseDaemon?.Invoke("Обычный");
        Destroy(gameObject);
    }

    private void ChooseDaemonLowPressed()  { ChooseDaemon?.Invoke("Низший"); Destroy(gameObject); }

    private void ChooseMachineHighPressed() {
        ChooseMachine?.Invoke("Высший");
        Destroy(gameObject);
    }
    private void ChooseMachineAveragePressed() { ChooseMachine?.Invoke("Обычный"); Destroy(gameObject); }

    private void ChooseMachineLowPressed() { ChooseMachine?.Invoke("Низишй"); Destroy(gameObject); }

    private void ChooseBeastHighPressed() { ChooseBeast?.Invoke("Высший"); Destroy(gameObject); }

    private void ChooseBeastAveragePressed() { ChooseBeast?.Invoke("Обычный"); Destroy(gameObject); }

    private void ChooseBeastLowPressed() { ChooseBeast?.Invoke("Низший"); Destroy(gameObject); }

    private void ChooseHumanHighPressed() { ChooseHuman?.Invoke("Высший"); Destroy(gameObject); }

    private void ChooseHumanAveragePressed() { ChooseHuman?.Invoke("Обычный"); Destroy(gameObject); }

    private void ChooseHumanLowPressed() { ChooseHuman?.Invoke("Низший"); Destroy(gameObject); }

}
