using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createMap : MonoBehaviour {

    private enum Terrains { PLAINS,WATER };
    private GameObject[] trees;
    private GameObject[] plainsTiles;
    bool wizardIsPlaced;

    public int mapLength = 10;
    public GameObject wizard;
    public GameObject plainsTilePrefab;
    public GameObject waterTilePrefab;
    public GameObject treePrefab;

	// Use this for initialization
	void Start () {
        // resize map as needed and reposition to the start at the (0,0,0) corner
        transform.localScale = new Vector3(mapLength * 0.1f, 1, mapLength * 0.1f);
        transform.position = new Vector3(0 + mapLength / 2f, 0, 0 + mapLength / 2f);

        wizardIsPlaced = false;
        CoverBaseWithTerrainTiles();
	}

    private void CoverBaseWithTerrainTiles()
    {
        int plainsCount = (int) (mapLength * mapLength * 0.8); // 80 % for plains
        int waterCount = (int)(mapLength * mapLength * 0.2); // 20 % water

        Vector3 tilePosition = transform.position;
        for (int i = 0; i < mapLength; i++)
        {
            for (int j = 0; j < mapLength; j++)
            {
                tilePosition = new Vector3(i + 0.5f, 0.001f, j + 0.5f);
                System.Random randTile = new System.Random();
                int nextTile = randTile.Next(2);
                if (nextTile == (int)Terrains.PLAINS)
                {
                    if (plainsCount < 0)
                    {
                        j--;
                        continue;
                    }
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
                    Instantiate(waterTilePrefab, tilePosition, Quaternion.identity);
                    waterCount--;
                }
            }
        }
        if (!wizardIsPlaced)
        {
            // place wizard in the middle of the map
            tilePosition = transform.position;
            AddWizard(tilePosition);
        }
    }

    private void AddWizard(Vector3 tilePosition)
    {
        tilePosition.y += 0.45f;
        Instantiate(wizard, tilePosition, Quaternion.identity);
        wizardIsPlaced = true;
    }

    private void AddTrees(Vector3 tilePosition)
    {
        // randomly place trees
        System.Random randTrees = new System.Random();
        int nextTree = randTrees.Next(9);
        if(nextTree % 3 == 0)
        {
            Instantiate(treePrefab, tilePosition, Quaternion.identity);
        }
        else if(nextTree == 1 && !wizardIsPlaced)
        {
            AddWizard(tilePosition);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
