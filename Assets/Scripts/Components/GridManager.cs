using System.Collections.Generic;
using Events;
using Extensions.System;
using Extensions.Unity;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Components
{
    public class GridManager : SerializedMonoBehaviour
    {
        [Inject] private InputEvents InputEvents{get;set;}
        [BoxGroup(Order = 999)] [TableMatrix(SquareCells = true, DrawElementMethod = nameof(DrawTile))] [OdinSerialize]
        private Tile[,] _grid;

        [SerializeField] private List<GameObject> _tilePrefabs;
        
        
        private int _gridSizeX;
        private int _gridSizeY;

        private Tile _selectedTile;
        private Vector3 _mouseDownPos;
        private Vector3 _mouseUpPos;
        private void OnEnable()
        {
            RegisterEvents();
        }

        private void OnDisable()
        {
            UnRegisterEvents();
        }

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
                foreach (var o in _grid)
                    DestroyImmediate(o.GameObject());

            _grid = new Tile[_gridSizeX, _gridSizeY];

            for (int x = 0; x < _gridSizeX; x++)
            for (int y = 0; y < _gridSizeY; y++)
                InstantiateNotMatching(x, y);
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

        private void CreateTileItem(int x, int y, GameObject _tilePrefabs)
        {
            if (_tilePrefabs != null)
            {
                Tile tile = _grid[x, y];
                Vector2Int coord = new(x, _gridSizeY - y - 1);
                Vector3 pos = new(coord.x, coord.y, 0f);
                GameObject gem = Instantiate(_tilePrefabs, pos, Quaternion.identity);
                tile = gem.GetComponent<Tile>();
                tile.Construct(coord);
                _grid[x, y] = tile;
            }
        }

        private bool IsMatchingAdjacent(int x, int y, int id)
        {
            bool IsVerticalMatch()
            {
                return y > 1 && _grid[x, y - 1].ID == id && _grid[x, y - 2].ID == id;
            }

            bool IsHorizontalMatch()
            {
                return x > 1 && _grid[x - 1, y].ID == id && _grid[x - 2, y].ID == id;
            }

            return IsVerticalMatch() || IsHorizontalMatch();
        }
        
        private void RegisterEvents()
        {
            InputEvents.MouseDownGrid += OnMouseDownGrid;
            InputEvents.MouseUpGrid += OnMouseUpGrid;
        }

        private void OnMouseDownGrid(Tile arg0, Vector3 arg1)
        {
            _selectedTile = arg0;
            _mouseDownPos = arg1;
            EDebug.Method();

        }

        private void OnMouseUpGrid(Vector3 arg0)
        {
            _mouseUpPos = arg0;

            if(_selectedTile)
            {
                EDebug.Method();

                Debug.DrawLine(_mouseDownPos, _mouseUpPos, Color.blue, 2f);
            }
        }

        private void UnRegisterEvents()
        {
            InputEvents.MouseDownGrid -= OnMouseDownGrid;
            InputEvents.MouseUpGrid -= OnMouseUpGrid;
        }
    }
    
}