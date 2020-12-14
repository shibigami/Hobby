using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnalogTouchControls : MonoBehaviour
{
    public GameObject analog;
    private RectTransform analogTransform;
    private Vector2 analogPoint;
    private RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer) gameObject.SetActive(false);
        analogTransform = analog.GetComponent<RectTransform>();
        analogPoint = new Vector2();
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(WithinBounds())analogTransform.anchoredPosition=analogPoint;
        }
        else
            analogTransform.anchoredPosition = new Vector2(0,0);
    }

    private bool WithinBounds()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, Camera.main, out analogPoint);
        if (rect.rect.Contains(analogPoint))
        {
            analogPoint -= rect.sizeDelta / 2;
            analogPoint = new Vector2(Mathf.Clamp(analogPoint.x, -70, 70), Mathf.Clamp(analogPoint.y, -70, 70));
            Debug.Log(analogPoint);
            return true;
        }
        else return false;
    }

    public Vector2 AnalogPosition() 
    {
        return analogTransform.anchoredPosition.normalized;
    }
}
