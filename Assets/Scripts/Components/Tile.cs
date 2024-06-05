using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public class Tile:MonoBehaviour
    {
        [SerializeField] private Vector2Int _coords;
        public Vector2Int Coords => _coords;
        [SerializeField] private int _id;
        public int ID => _id ;

        public void Construct(Vector2Int coords)
        {
            _coords = coords;
        }
    }
}