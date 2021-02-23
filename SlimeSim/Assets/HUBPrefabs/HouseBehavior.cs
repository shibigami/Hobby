using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBehavior : MonoBehaviour
{
    public GameObject fakeWall;
    public GameObject[] houseBricks;
    private Vector2[] initialBrickPosition;
    public GameObject backgroundWall;

    private int currentBuildIndex;

    // Start is called before the first frame update
    void Start()
    {
        initialBrickPosition = new Vector2[houseBricks.Length];

        for (int i = 0; i < houseBricks.Length; i++)
        {
            //get the brick placement in scene
            initialBrickPosition[i] = houseBricks[i].transform.localPosition;
            //set all bricks to behind first wall
            if (i != 0) houseBricks[i].transform.localPosition = new Vector3(0, 0, 0);
        }
        backgroundWall.transform.localScale = new Vector3(backgroundWall.transform.localScale.x,0,backgroundWall.transform.localScale.z);


        currentBuildIndex = 0;

        StartCoroutine("GrowHouse");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //check if fakewall joined
        if (!GameData.fakeWallJoined)
        {
            fakeWall.SetActive(false);
            houseBricks[0].SetActive(false);
        }
        else
        {
            if (!fakeWall.activeSelf) fakeWall.SetActive(true);
            if (HubData.GetHouseBricksBuilt() > 0 && !houseBricks[0].activeSelf) houseBricks[0].SetActive(true);
        }

        //nuild
        if (currentBuildIndex < houseBricks.Length)
        {
            houseBricks[currentBuildIndex].transform.localPosition = Vector2.MoveTowards(houseBricks[currentBuildIndex].transform.localPosition,
                initialBrickPosition[currentBuildIndex], 1 * Time.deltaTime);
        }
        else
        {
            if (!HubData.wallHouseBuilt) HubData.WallHouseFinalized();
            if (backgroundWall.transform.localScale.y < 0.5f) backgroundWall.transform.localScale += new Vector3(0, 0.25f * Time.deltaTime, 0);
        }
    }

    private IEnumerator GrowHouse()
    {
        yield return new WaitForSeconds(3f);
        while (currentBuildIndex < HubData.GetHouseBricksBuilt())
        {
            currentBuildIndex++;
            yield return new WaitForSeconds(2f);
        }
    }

    public void UpdateBuild()
    {
        StartCoroutine("GrowHouse");
    }
}
