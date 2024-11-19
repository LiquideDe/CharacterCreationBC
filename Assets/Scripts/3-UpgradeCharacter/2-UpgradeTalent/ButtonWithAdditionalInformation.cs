using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class ButtonWithAdditionalInformation : Button, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI textName;
    [SerializeField] Sprite _active, _nonActive;
    [SerializeField] Image _image;
    public event Action<string> ShowDescription;
    public event Action<string> ShowThisCategory;
    public event Action HidePanel;
    private string _textDescription, _category;


    public override void OnPointerEnter(PointerEventData eventData)
    {
        ShowDescription?.Invoke(_textDescription);
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        HidePanel?.Invoke();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        ShowThisCategory?.Invoke(_category);
    }

    public void Initialize(string category)
    {
        _category = category;
        _textDescription = $"Показать таланты из {category}";
        textName.text = category;
    }

    public void SetActive() => _image.sprite = _active;

    public void SetDeactive() => _image.sprite = _nonActive;
}
