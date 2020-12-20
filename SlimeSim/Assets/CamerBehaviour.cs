using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerBehaviour : MonoBehaviour
{
    public float speed;
    private GameObject player;
    private bool moveToPlayer;
    public int[] cameraYPositions;
    private int tempPos;
    private int direction;

    // Start is called before the first frame update
    void Start()
    {
        moveToPlayer = false;
        player = GameObject.FindGameObjectWithTag("Player");
        if (cameraYPositions == null) cameraYPositions = new int[3] { 0, 3, 6 };
        tempPos = 0;
        direction = 1;

        InvokeRepeating("UpdateCameraPosition",1,0.1f);
    }

    private void UpdateCameraPosition()
    {
        //check player position
        //if player outside camera bounds
        //update camera to array position interval
        for (int i = 0; i < cameraYPositions.Length - 1; i++)
        {
            if (player.transform.position.y >= cameraYPositions[i] &&
                player.transform.position.y < cameraYPositions[i + 1] &&
                tempPos != i)
            {
                StopAllCoroutines();
                if (i > tempPos) direction = 1;
                else if (i < tempPos) direction = -1;
                else direction = 0;

                moveToPlayer = true;
                tempPos = i;
            }
            else
                moveToPlayer = false;
        }

    }

    public void Update()
    {
        if (moveToPlayer) StartCoroutine("CameraMovement");
        else
        {
            StopAllCoroutines();
            if (transform.position.y != cameraYPositions[tempPos]) 
                transform.position = new Vector3(0, cameraYPositions[tempPos], transform.position.z); 
        }
    }

    private IEnumerator CameraMovement() 
    {
        transform.position += new Vector3(0,speed*direction,0);
        yield return new WaitForSeconds(0.05f);
    }
}
