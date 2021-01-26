using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEnabler : MonoBehaviour
{
    public bool enableOnNotNull;
    public GameObject toBeNullObject;
    public GameObject enableObject;

    // Start is called before the first frame update
    void Start()
    {
        enableObject.SetActive(false);

        InvokeRepeating("CheckObjectGone",0,0.5f);
    }

    // Update is called once per frame
    private void CheckObjectGone()
    {
        if (enableOnNotNull)
        {
            if (toBeNullObject!=null) enableObject.SetActive(true);
        }
        else 
        {
            if (toBeNullObject==null) enableObject.SetActive(true);
        }
    }
}
