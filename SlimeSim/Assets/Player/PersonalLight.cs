using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalLight : MonoBehaviour
{
    private GameObject player;
    public float speed;
    public Vector2 minSize, maxSize;
    public float step;
    private bool expand;
    public float sizeUpdate;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        expand = true;

        InvokeRepeating("VibrateLight",0,sizeUpdate);
    }

    private void VibrateLight()
    {
        if (expand) transform.localScale = new Vector3(transform.localScale.x + step * Time.deltaTime,
            transform.localScale.y + step * Time.deltaTime, 1);
        else transform.localScale = new Vector3(transform.localScale.x - step * Time.deltaTime,
            transform.localScale.y - step * Time.deltaTime, 1);
        if (transform.localScale.x >= maxSize.x) expand = false;
        else if (transform.localScale.x <= minSize.x) expand = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);
    }
}
