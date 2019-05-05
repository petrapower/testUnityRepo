using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createMap : MonoBehaviour {

    private enum Terrains { PLAINS,WATER };

    public int mapLength = 10;
    public GameObject plainsTilePrefab;
    public GameObject waterTilePrefab;
    public GameObject treePrefab;

	// Use this for initialization
	void Start () {
        // resize map as needed and reposition to the start at the (0,0,0) corner
        transform.localScale = new Vector3(mapLength * 0.1f, 1, mapLength * 0.1f);
        transform.position = new Vector3(0 + mapLength / 2f, 0, 0 + mapLength / 2f);

        CoverBaseWithTerrainTiles();
	}

    private void CoverBaseWithTerrainTiles()
    {
        int plainsCount = (int) (mapLength * mapLength * 0.8); // 80 % for plains
        int waterCount = (int)(mapLength * mapLength * 0.2); // 20 % water
        Debug.Log(plainsCount + " " + waterCount);

        for (int i = 0; i < mapLength; i++)
        {
            for (int j = 0; j < mapLength; j++)
            {
                System.Random randTile = new System.Random();
                int nextTile = randTile.Next(2);
                if (nextTile == (int)Terrains.PLAINS)
                {
                    if (plainsCount < 0)
                    {
                        j--;
                        continue;
                    }
                    Vector3 tilePosition = new Vector3(i + 0.5f, 0.01f, j + 0.5f);
                    Instantiate(plainsTilePrefab, tilePosition, Quaternion.identity);
                    plainsCount--;
                    AddTrees(tilePosition);
                }
                else if (nextTile == (int)Terrains.WATER)
                {
                    if(waterCount < 0)
                    {
                        j--;
                        continue;
                    }
                    Instantiate(waterTilePrefab, new Vector3(i + 0.5f, 0.01f, j + 0.5f), Quaternion.identity);
                    waterCount--;
                }
            }
        }
    }

    private void AddTrees(Vector3 tilePosition)
    {
        // randomly place trees
        System.Random randTrees = new System.Random();
        int nextTree = randTrees.Next(3);
        if(nextTree == 1)
        {
            Instantiate(treePrefab, tilePosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
