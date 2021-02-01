using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBehavior : MonoBehaviour
{
    public GameObject[] houseBricks;
    private Vector2[] initialBrickPosition;
    public GameObject backGroundWall;

    private int currentBuildIndex;

    // Start is called before the first frame update
    void Start()
    {
        //if (!GameData.fakeWallJoined) houseBricks[0].SetActive(false);

        initialBrickPosition = new Vector2[houseBricks.Length];

        for (int i = 0; i < houseBricks.Length; i++)
        {
            //get the brick placement in scene
            initialBrickPosition[i] = houseBricks[i].transform.localPosition;
            //set all bricks to behind first wall
            if (i != 0) houseBricks[i].transform.localPosition = new Vector3(0, 0, 0);
        }
        backGroundWall.transform.localScale = new Vector3(backGroundWall.transform.localScale.x,0,backGroundWall.transform.localScale.z);


        currentBuildIndex = 0;

        StartCoroutine("GrowHouse");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (currentBuildIndex < houseBricks.Length)
        {
            houseBricks[currentBuildIndex].transform.localPosition = Vector2.MoveTowards(houseBricks[currentBuildIndex].transform.localPosition,
                initialBrickPosition[currentBuildIndex], 1 * Time.deltaTime);
        }
        else
        {
            if (!HubData.wallHouseBuilt) HubData.WallHouseFinalized();
            if (backGroundWall.transform.localScale.y < 0.5f) backGroundWall.transform.localScale += new Vector3(0, 0.25f * Time.deltaTime, 0);
        }
    }

    private IEnumerator GrowHouse()
    {
        yield return new WaitForSeconds(3f);
        while (currentBuildIndex < HubData.GetHouseBricksBuilt())
        {
            currentBuildIndex++;
            yield return new WaitForSeconds(1.5f);
        }
    }

    public void UpdateBuild()
    {
        StartCoroutine("GrowHouse");
    }
}
