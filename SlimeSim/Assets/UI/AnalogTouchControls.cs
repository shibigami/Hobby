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
    private Touch touch1;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer) 
            gameObject.SetActive(false);
        analogTransform = analog.GetComponent<RectTransform>();
        rect = GetComponent<RectTransform>();
        ResetAnalog();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount > 1)
            {
                if (Input.GetTouch(0).position.x < Input.GetTouch(1).position.x)
                {
                    touch1 = Input.GetTouch(0);
                }
                else
                {
                    touch1 = Input.GetTouch(1);
                }
            }
            else touch1 = Input.GetTouch(0);
        }

        if (touch1.phase == TouchPhase.Began && WithinBounds(touch1.position)) initialClickPoint = analogPoint;
        else if ((touch1.phase == TouchPhase.Moved||touch1.phase==TouchPhase.Stationary) && initialClickPoint.x > 0)
        {
            GetAnalogPoint(touch1.position);
            if ((analogPoint - initialClickPoint).magnitude > 100) analogPoint = ((analogPoint - initialClickPoint).normalized * 100) + initialClickPoint;
            analogTransform.anchoredPosition = analogPoint;
        }
        else ResetAnalog();
    }

    private void ResetAnalog() 
    {
        initialClickPoint = new Vector2(-100, -100);
        analogTransform.anchoredPosition = new Vector2(-100, -100);
    }

    private bool WithinBounds(Vector2 position)
    {
        if (rect.rect.Contains(position))
        {
            GetAnalogPoint(position);
            return true;
        }
        else return false;
    }

    private void GetAnalogPoint(Vector2 position)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, position, Camera.main, out analogPoint);
    }

    public Vector2 AnalogPosition() 
    {
        if (initialClickPoint.x > 0)
            return (analogPoint - initialClickPoint).normalized;
        else return Vector2.zero;
    }
}
