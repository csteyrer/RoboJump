using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTiles : MonoBehaviour
{
    //array of prefabs for the tiles
    public GameObject[] tile;

    //start position of the tile
    public Vector3 tileStartPos;

    //tile spacing
    Vector2 tileSpacing;

    //width of the grid
    public int gridWidth;

    //height of the grid
    public int gridHeight;

    void Start()
    {
        //get the exact size of our tiles
        tileSpacing = tile[0].GetComponent<Renderer>().bounds.size;

        //loop the number of rows height
        for(int i = 0; i < gridHeight; i++)
        {
            //loop the number of colums wide
            for(int j = 0; j < gridWidth; j++)
            {
                //grab a random number between 0 and however many tiles there are
                int randomTile = Random.Range(0, tile.Length);

                //spawn new game object based on the tile chosen using the random number
                //starting at our start pos.x plus the tile spacing.x width times the count of the grid width
                //by our start pos.y plus the tile spacing.y height times the count of the grid height
                //using a quaternion.identity so that there is no rotation in the vector3
                GameObject go = Instantiate(tile[randomTile], new Vector3(tileStartPos.x + (j * tileSpacing.x), tileStartPos.y + (i * tileSpacing.y)), Quaternion.identity) as GameObject;

                //add all the game objects as a child of BGTiles
                go.transform.parent = GameObject.Find("BGTiles").transform;
            }
        }
    }
}
