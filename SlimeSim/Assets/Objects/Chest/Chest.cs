using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject animationObject;
    private Animator animator;

    public int gold;
    public GameObject goldPrefab;

    private bool opened;

    // Start is called before the first frame update
    void Start()
    {
        animator = animationObject.GetComponent<Animator>();
        opened = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player"&&!opened)
        {
            opened = true;
            animator.SetTrigger("Open");
            StartCoroutine("GiveGold");
        }
    }

    IEnumerator GiveGold() 
    {
        if (gold > 0) for (int i = 0; i < gold; i++)
            {
                Instantiate(goldPrefab, transform.position, transform.rotation);

                yield return new WaitForSeconds(0.01f);
            }
        else
        {
            Debug.Log("no gold there mate");
            yield return null;
        }
    }
}
