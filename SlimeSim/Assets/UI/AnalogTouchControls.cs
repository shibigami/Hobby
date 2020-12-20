using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnalogTouchControls : MonoBehaviour
{
    public GameObject analog;
    private RectTransform analogTransform;
    private Vector2 analogPoint;
    private Vector2 initialClickPoint;
    private RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        //if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer) 
            gameObject.SetActive(false);
        analogTransform = analog.GetComponent<RectTransform>();
        rect = GetComponent<RectTransform>();
        ResetAnalog();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && WithinBounds()) initialClickPoint = analogPoint;
        else if (Input.GetMouseButton(0) && initialClickPoint.x > 0)
        {
            GetAnalogPoint();
            if ((analogPoint - initialClickPoint).magnitude > 100) analogPoint = ((analogPoint-initialClickPoint).normalized * 100)+initialClickPoint; 
            analogTransform.anchoredPosition = analogPoint;
        }
        else ResetAnalog();
    }

    private void ResetAnalog() 
    {
        initialClickPoint = new Vector2(-100, -100);
        analogTransform.anchoredPosition = new Vector2(-100, -100);
    }

    private bool WithinBounds()
    {
        if (rect.rect.Contains(Input.mousePosition))
        {
            GetAnalogPoint();
            return true;
        }
        else return false;
    }

    private void GetAnalogPoint()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, Camera.main, out analogPoint);
    }

    public Vector2 AnalogPosition() 
    {
        if (initialClickPoint.x > 0)
            return (analogPoint - initialClickPoint).normalized;
        else return Vector2.zero;
    }
}
