using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFind = NesScripts.Controls.PathFind;

/* The PathFind scripts are found from 
 * https://github.com/Vahv1/ai_tehtava/tree/master/Assets/PathFinding/2dTileBasedPathFinding
 * ---
 * https://github.com/Vahv1/ai_tehtava/tree/master/Assets/PathFinding -folder 
 * is required to make this work.
 */

//Make your generation algorithm here
public class Generate : MonoBehaviour {

    public TileType[] tileTypes; // all the tiletypes given in the editor
    public GameObject player;
    public GameObject enemy;

    //the size of the generated map

    public int mapN = 4;
    public int enemy_count = 4;
    // Diamond square requires a square with the width and height in the form of 2^n + 1
    private int mapSizeX;
    private int mapSizeY;



    void Start()
    {
        mapSizeX = (int)Mathf.Pow(2, mapN) + 1;
        mapSizeY = (int)Mathf.Pow(2, mapN) + 1;
        GenerateMap();
    }
    
    /*
     * Generates a map
     * Uses diamond square algorithm for the map and places the player 
     * to a random point in the map. If there is nowhere to place the
     * end point, regenerates the map. Generates also N amount of enemies.
     */ 
    void GenerateMap()
    {
        // Generate a diamond square -map
        float[,] map = diamond_square(mapSizeX, mapSizeY);
        // Set up the pathfinding map
        float[,] tilesmap = new float[mapSizeX, mapSizeY];
        // We need a list of every generated object in case of non-suitable
        // map.
        List<GameObject> generatedObjects = new List<GameObject>();

        // Find the min and the max
        float max = map[0, 0];
        float min = map[0, 0];
        // Finds the minimum and the maximum of the DS map.
        for (int x = 0; x < mapSizeX-1; x++)
        {
            for (int y = 0; y < mapSizeY-1; y++)
            {
                if (map[x, y] > max)
                    max = map[x, y];
                if (map[x, y] < min)
                    min = map[x, y];
            }
        }

        // Translate the DS map to a real map
        for (int x = 0; x < mapSizeX-1; x++)
        {
            for (int y = 0; y < mapSizeY-1; y++)
            {
                // Tile is a grass
                TileType tt = tileTypes[0];
                // If the tile is above average, it is a wall
                if (map[x, y] < (max + min) / 2)
                {
                    tt = tileTypes[1];
                    // Set it as an impassable wall for the pathfinding
                    tilesmap[x, y] = 0.0f;
                }
                else
                    tilesmap[x, y] = 1.0f;
                // Let's create the tile and add it to the generatedObjects -list
                GameObject go =Instantiate(tt.tileVisual, new Vector3(x, y,0), Quaternion.identity);
                generatedObjects.Add(go);
            }
        }
        // We need a grid for the pathfinding.
        PathFind.Grid grid = new PathFind.Grid(tilesmap);
        // Let's assume the map is unsolvable
        int player_x = 0;
        int player_y = 0;
        // Let's search for a suitable random point in the map.
        while (true)
        {
            player_x = Random.Range(0, mapSizeX-1);
            player_y = Random.Range(0, mapSizeY-1);
            if (map[player_x, player_y] < (max + min) / 2)
                continue;
            else
                break;
        }
        // This Vector2 is used to find out if the endpoint is far enough from 
        // the player.
        Vector2 player_location = new Vector2(player_x, player_y);
        // goal_set means if the endpoint is set on the map.
        bool goal_set = false;
        
        GameObject newPlayer, newGoal;
        // where the end location is.
        Vector3 targetPosition = new Vector3(0, 0, -0.5f);
        // Spawn a player
        newPlayer = Instantiate(player, new Vector3(player_x, player_y, -0.5f), Quaternion.identity); 
        generatedObjects.Add(newPlayer);
        // Add the player location to pathfinding
        PathFind.Point _from = new PathFind.Point(player_x, player_y);
        // Let's search the grid for a suitable end location.
        for (int i = 0; i < mapSizeX-1; i++)
        {
            for (int j = 0; j < mapSizeY-1; j++)
            {
                // If the location is not a wall
                if (map[i,j] >= (max+min)/2)
                {
                    // If we don't have an end location and if the location is far enough from the player
                    if (!goal_set && (Vector2.Distance(player_location, new Vector2(i, j)) > mapSizeX / 2))
                    {
                        // Let's try to find out, if the end location is actually reachable
                        PathFind.Point _to = new PathFind.Point(i, j);
                        List<PathFind.Point> path = PathFind.Pathfinding.FindPath(grid, _from, _to);
                        if (path.Count > 0)
                        {
                            // Debug.Log("Found an end-location!");
                            goal_set = true;
                            newGoal = Instantiate(tileTypes[3].tileVisual, new Vector3(i, j, -0.5f), Quaternion.identity);
                            targetPosition = new Vector3(i, j, -0.5f);
                        }
                    }
                }
            }
        }
        if (!goal_set)
        {
            // Debug.Log("Couldn't find an endlocation matching the criteria.\n Destroying all generated objects and re-generating the map.");
            // The map does not meet the criteria, reset!
            foreach (GameObject obj in generatedObjects)
            {
                Object.Destroy(obj);
            }
            GenerateMap();
        }
        else 
        {
            // The map fits the criteria. 
            // Debug.Log("Trying to raycast enemy locations.");
            // Let's try to add the enemies
            for (int i = 0; i < enemy_count; i++)
            {
                bool addedEnemy = false;
                while(!addedEnemy)
                {
                    int enemy_x = Random.Range(0, mapSizeX - 1);
                    int enemy_y = Random.Range(0, mapSizeY - 1);
                    // If the random location is not a wall
                    if (map[enemy_x, enemy_y] >= (max + min) / 2)
                    {
                        Vector3 enemy_location = new Vector3(enemy_x, enemy_y, -0.5f);
                        // Let's check if there is a wall between the chosen location and the player location
                        if (Physics.Raycast(enemy_location, new Vector3(player_x,player_y,-0.5f)- enemy_location,(new Vector3(player_x, player_y, -0.5f) - enemy_location).magnitude))
                        {
                            Instantiate(enemy, enemy_location, Quaternion.identity);
                            addedEnemy = true;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Generates a 2D array of floats by using Diamond-Square algorithm
    /// </summary>
    /// <param name="width">Width of the map, must be in the format of 2^n + 1</param>
    /// <param name="height">Height of the map, must be in the format of 2^n + 1</param>
    /// <returns>A 2D array of floats</returns>
    float[,] diamond_square(int width, int height)
    {
        float[,] A = new float[width, height];

        int step_size = width - 1;
        float lower =   -0.1f;
        float upper =   0.1f;

        // Initialize the corners
        A[0, 0] = 0.5f;
        A[width - 1, 0] = 0.5f;
        A[0, height - 1] = 0.5f;
        A[width - 1, height - 1] = 0.5f;

        while (step_size > 1)
        {
            int half_step = Mathf.FloorToInt(step_size / 2);
            // Diamond
            for (int i = 0; i < height - 1; i+= step_size)
            {
                for (int j = 0; j < width - 1; j+= step_size)
                {
                    float topL = A[j, i];
                    float topR = A[j + step_size, i];
                    float botL = A[j, i + step_size];
                    float botR = A[j + step_size, i + step_size];

                    float avg = (topL + topR + botL + botR) / 4;
                    float random = Random.Range(lower, upper);
                    A[j + half_step, i + half_step] = avg + random;
                }
            }
            // Square
            bool even = true;
            for (int i = 0; i < height; i += half_step)
            {
                int x_start = even ? 0 : half_step;
                for (int j = x_start; j < width; j += half_step)
                {
                    float left  = j - half_step < 0         ? 0 : A[j - half_step, i];
                    float right = j + half_step >= width    ? 0 : A[j + half_step, i];
                    float up    = i + half_step >= height   ? 0 : A[j, i + half_step];
                    float down  = i - half_step < 0         ? 0 : A[j, i - half_step];

                    float avg = (left + right + up + down) / 4;
                    float random = Random.Range(lower, upper);
                    A[j, i] = avg + random;
                }
                even = !even;
            }
            step_size /= 2;
            lower += 0.005f;
            upper += 0.005f;
        }

        return A;
    }
}
