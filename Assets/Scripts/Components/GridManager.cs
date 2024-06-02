using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using Unity.VisualScripting;
using UnityEngine;

namespace Components
{
    public class GridManager : SerializedMonoBehaviour
    {
        [BoxGroup(Order = 999)] [TableMatrix(SquareCells = true, DrawElementMethod = nameof(DrawTile))] [OdinSerialize]
        private Tile[,] _grid;

        [SerializeField] private List<GameObject> _tilePrefabs;


        private int _gridSizeX;
        private int _gridSizeY;

        private Tile DrawTile(Rect rect, Tile tile)
        { 
            Sprite spriteTile = tile.GetComponent<SpriteRenderer>().sprite;
            Rect textureRect = rect;

            textureRect.Padding(3);
            UnityEditor.EditorGUI.DrawPreviewTexture(textureRect, spriteTile.texture);

            return tile;
        }

        [Button]
        private void CreateGrid(int sizeX, int sizeY)
        {
            _gridSizeX = sizeX;
            _gridSizeY = sizeY;

            if (_grid != null)
                foreach (Tile o in _grid)
                    DestroyImmediate(o.GameObject());

            _grid = new Tile[_gridSizeX, _gridSizeY];

            for (int x = 0; x < _gridSizeX; x++)
            for (int y = 0; y < _gridSizeY; y++)
            {
                InstantiateNotMatching(x , y);
            }
        }

        private void InstantiateNotMatching(int x, int y)
        {
            int randomIndex = Random.Range(0, _tilePrefabs.Count);
            GameObject tilePrefabRandom = _tilePrefabs[randomIndex];

            for (int attempts = 0; attempts < 4; attempts++)
            {
                //if it returns false it means there's no matched 3 at the beginning so break the loop and execute the func
                //if it returns true it generate a random prefab again 
                if (!IsMatchingAdjacent(x, y, randomIndex)) break;
                
                randomIndex = Random.Range(0, _tilePrefabs.Count);
                tilePrefabRandom = _tilePrefabs[randomIndex];
            }
            CreateTileItem(x, y, tilePrefabRandom);
        }

        private void CreateTileItem(int x, int y, GameObject selectedGem)
        {
            if (selectedGem != null)
            {
                Tile tile = _grid[x, y];
                Vector2Int coord = new (x, _gridSizeY - y - 1);
                Vector3 pos = new (coord.x, coord.y, 0f);
                GameObject gem = Instantiate(selectedGem, pos, Quaternion.identity);
                tile = gem.GetComponent<Tile>();
                tile.Construct(coord);
                _grid[x, y] = tile;
            }
        }
        

        private bool IsMatchingAdjacent(int x, int y, int id)
        {
            if (y <= 1 || x <= 1)
            {
                if (y > 1)
                {
                    if (_grid[x, y - 1].ID == id && _grid[x, y - 2].ID == id)
                    {
                        return true;
                    }
                }
                if (x > 1)
                {
                    if (_grid[x - 1, y].ID == id && _grid[x - 2, y].ID == id)
                    {
                        return true;
                    }
                }
            }

            if (x > 1)
            {
                if (_grid[x - 1, y].ID == id && _grid[x - 2, y].ID == id)
                {
                    return true;
                }
            }

            if (y > 1)
            {
                if (_grid[x, y - 1].ID == id && _grid[x, y - 2].ID == id)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}