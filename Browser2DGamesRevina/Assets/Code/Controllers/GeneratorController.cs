using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PlatformerMVC
{
    public class GeneratorController 
    {
        private Tilemap _tilemap;
        private Tile _tile;
        private int _mapWidth;
        private int _mapHeight;
        private int _smoothFactor;
        private int _fillPercent;
        private bool _borders;

        private int[,] _map;

        private const int _countWall = 4;

        private MarchingSquareGenerator _marchingSquareGenerator = new MarchingSquareGenerator();

        public GeneratorController(GeneratorLevelView levelView)
        {
            _tilemap = levelView.TileMap;
            _tile = levelView.GroundTile;
            _mapWidth = levelView.MapWidth;
            _mapHeight = levelView.MapHeight;
            _smoothFactor = levelView.SmoothFactor;
            _fillPercent = levelView.FillPercent;
            _borders = levelView.Borders;

            _map = new int[_mapWidth, _mapHeight];
        }

        public void Init()
        {
            RandomFillMap();
            for (int i = 0; i < _smoothFactor; i++)
            {
                SmoothMap();
            }

            _marchingSquareGenerator.GenerateGrid(_map, 1);

            _marchingSquareGenerator.DrawTilesOnGrid(_tilemap, _tile);

           // DrawTiles();
        }

        private void RandomFillMap()
        {
            for (int  x= 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if(x==0 || x == _mapWidth-1 || y == 0 || y == _mapHeight-1)
                    {
                        if(_borders)
                        {
                            _map[x, y] = 1;
                        }
                    }
                    else
                    {
                        _map[x, y] = (Random.Range(0, 100) < _fillPercent) ? 1 : 0;
                    }
                }
            }
        }

        private void SmoothMap()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    int neighborCells = GetNeighborCount(x,y);

                    if(neighborCells > _countWall)
                    {
                        _map[x, y] = 1;
                    }
                    else if (neighborCells < _countWall)
                    {
                        _map[x, y] = 0;
                    }
                }
            }
        }

        private int GetNeighborCount(int x, int y)
        {
            int wallCount = 0;

            for (int gridX = x - 1; gridX <= x + 1; gridX++)
            {
                for (int gridY = y - 1; gridY <= y + 1; gridY++)
                {
                    if(gridX >= 0 && gridX < _mapWidth && gridY >= 0 && gridY < _mapWidth)
                    {
                        if(gridX != x || gridY != y)
                        {
                            wallCount += _map[gridX, gridY];
                        }
                    }
                    else
                    {
                        wallCount++;
                    }
                }
            }
            return wallCount;
        }

        private void DrawTiles()
        {
            if (_map == null)
                return;

            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    Vector3Int tilePosition = new Vector3Int(-_mapWidth/2+x, -_mapHeight/2+y, 0);

                    if(_map[x,y] == 1)
                    {
                        _tilemap.SetTile(tilePosition, _tile);
                    }
                }
            }
        }
    }
}