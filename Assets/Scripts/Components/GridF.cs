using Components;
using UnityEngine;

public static class GridF
{
    private const int MatchOffset = 2;
    
    public static bool HasMatchesRight(this Tile[,] grid, Vector2Int abstractCoord, int prefabId)
    {
        if(grid.IsInsideGrid(abstractCoord))
        {

        }

        return default;
    }

    private static bool HasMatchRight(this Tile[,] grid, Vector2Int coord, int prefabId)
    {
        int rightMax = coord.x + MatchOffset;
        rightMax = ClampInsideGrid(rightMax, grid.GetLength(0));

        int matchCounter = 0;

        for(int x = coord.x; x <= rightMax; x ++)
        {
            if(grid[x, coord.y].ID == prefabId)
            {
                matchCounter ++;
            }
            else
            {
                matchCounter = 0;
            }

            if(matchCounter == 3)
            {
                return true;
            }
        }

        return false;
    }

    private static bool HasMatchLeft(this Tile[,] grid, Vector2Int coord, int prefabId)
    {
        int leftMax = coord.x - MatchOffset;
        leftMax = ClampInsideGrid(leftMax, grid.GetLength(0));

        int matchCounter = 0;

        for(int x = leftMax; x <= coord.x; x ++)
        {
            if(grid[x, coord.y].ID == prefabId)
            {
                matchCounter ++;
            }
            else
            {
                matchCounter = 0;
            }

            if(matchCounter == 3)
            {
                return true;
            }
        }

        return false;
    }
    private static bool HasMatchTop(this Tile[,] grid, Vector2Int coord, int prefabId)
    {
        int topMax = coord.y + MatchOffset;
        topMax = ClampInsideGrid(topMax, grid.GetLength(1));

        int matchCounter = 0;

        for(int y = coord.y; y <= topMax; y ++)
        {
            if(grid[coord.x, y].ID == prefabId)
            {
                matchCounter ++;
            }
            else
            {
                matchCounter = 0;
            }

            if(matchCounter == 3)
            {
                return true;
            }
        }

        return false;
    }

    private static bool HasMatchBot(this Tile[,] grid, Vector2Int coord, int prefabId)
    {
        int botMax = coord.y - MatchOffset;
        botMax = ClampInsideGrid(botMax, grid.GetLength(1));

        int matchCounter = 0;

        for(int y = botMax; y <= coord.y; y ++)
        {
            if(grid[coord.x, y].ID == prefabId)
            {
                matchCounter ++;
            }
            else
            {
                matchCounter = 0;
            }

            if(matchCounter == 3)
            {
                return true;
            }
        }

        return false;
    }
    private static int ClampInsideGrid
    (int value, int gridSize)
    {
        return Mathf.Clamp(value, 0, gridSize - 1);
    }

    private static bool IsInsideGrid(this Tile[,] grid, int axis, int axisIndex)
    {
        int min = 0;
        int max = grid.GetLength(axisIndex);

        return axis >= 0 && axis < max;
    }

    private static bool IsInsideGrid(this Tile[,] grid, Vector2Int coord)
    {
        return grid.IsInsideGrid(coord.x, 0) && grid.IsInsideGrid(coord.y, 1);
    }
}