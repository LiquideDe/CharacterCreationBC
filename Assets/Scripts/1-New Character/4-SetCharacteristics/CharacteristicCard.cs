using UnityEngine;
using TMPro;
using Zenject;
using System;

public class CharacteristicCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textName, _textAmount;
    public event Action AmountFromRandomIsSeted;
    public event Action Reset;
    private AudioManager _audioManager;
    private int _amount;
    private int _baseAmount;
    private bool _isSetAmountFromRandomCard;

    public bool IsSetAmountFromRandomCard => _isSetAmountFromRandomCard;
    public int Amount => _amount;

    [Inject]
    private void Construct(AudioManager audioManager) => _audioManager = audioManager;

    public void SetAmount(int amount)
    {
        _amount = amount;
        _baseAmount = amount;
        _textAmount.text = $"{amount}";
    }

    public void PlusAmount(int amount)
    {
        _audioManager.PlayClick();
        _isSetAmountFromRandomCard = true;
        _amount = amount + _baseAmount;
        _textAmount.color = Color.red;
        _textAmount.text = $"{_amount}";
        AmountFromRandomIsSeted?.Invoke();
    }

    public void ResetAmount()
    {
        _isSetAmountFromRandomCard = false;
        _amount = _baseAmount;
        _textAmount.color = Color.white;
        _textAmount.text = $"{_amount}";
        Reset?.Invoke();
    }

    
}
