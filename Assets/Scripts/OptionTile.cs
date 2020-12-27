using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionTile : EventTrigger
{
    RectTransform recTrans;
    Vector3 pos;
    Vector3 targetPosition;
    OptionTile targetUI;
    Vector3 currenPosition;
    GameManager gameManager;
    Canvas canvas;

    private void Start()
    {
        recTrans = GetComponent<RectTransform>();
        canvas = GetComponent<Canvas>();
        gameManager = FindObjectOfType<GameManager>();
        pos = Input.mousePosition;
        pos.z = transform.position.z - Camera.main.transform.position.z;
        targetUI = this;
        targetPosition = currenPosition = transform.position;
    }

    #region Events

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        canvas.sortingOrder = 10;
        transform.position += (Vector3)eventData.delta;
        foreach (OptionTile tile in gameManager.OptionList)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(tile.recTrans, Input.mousePosition) && tile != this)
            {
                
                targetUI = tile;
                targetPosition = tile.transform.position;
                targetUI.transform.position = Vector3.Lerp(targetUI.transform.position, this.currenPosition, 1f);
                targetUI.currenPosition = transform.position;
                currenPosition = targetPosition;
                return;
            }
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        canvas.sortingOrder = 0;
        transform.position = Vector3.Lerp(transform.position, targetPosition, 1f);
        targetUI.currenPosition = targetUI.transform.position;
        currenPosition = transform.position;
    }

    #endregion
}
