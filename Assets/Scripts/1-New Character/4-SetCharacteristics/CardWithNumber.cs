using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CardWithNumber : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private TextMeshProUGUI _textAmount;
    private int _amount;
    private Vector3 _startPos;
    private CanvasGroup _canvasGroup;
    private bool _cantReplace;

    public int Amount => _amount;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //dragRect.anchoredPosition += eventData.delta/canvas.scaleFactor;
        transform.position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPos = transform.position;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_cantReplace)
        {
            transform.position = _startPos;
            _canvasGroup.blocksRaycasts = true;
        }
    }

    public void Initialize(int amount)
    {        
        _amount = amount;
        _textAmount.text = amount.ToString();
    }   

    public void SetNewParent(Transform transform)
    {
        _cantReplace = true;
        this.transform.SetParent(transform);
    }
}
