using Core.BallsFolder;
using Core.TilesFolder;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Board
{
    public class BoardGenerator : MonoBehaviour
    {
        private GeneratorConfig _boardGeneratorConfig;
        [SerializeField] private Transform _ballsContainer;
        [SerializeField] private Transform _tilesContainer;

        private int _emptyTilesCounter;
        private int _boardSize;
        private bool _lineExistEmptyTile;
        private BallsHolder _ballsHolder;
        private TilesHolder _tilesHolder;
        private BallsReplacer _ballsReplacer;

        public void Construct(GeneratorConfig generatorConfig, BallsHolder ballsHolder, TilesHolder tilesHolder, BallsReplacer ballsReplacer)
        {
            _boardGeneratorConfig = generatorConfig;
            _ballsHolder = ballsHolder;
            _tilesHolder = tilesHolder;
            _ballsReplacer = ballsReplacer;
        }

        private void Start()
        {
            _boardSize = _boardGeneratorConfig.BoardSize;
            Generate();
        }

        private void Generate()
        {
            InitAndSpawnTiles();
            InitBalls();

            while (CheckMatchInRows() || CheckMatchInColumns())
            {
                InitBalls();
            }
            
            SpawnBalls();
        }
        

        private void InitAndSpawnTiles()
        {
            var emptyTilesAmount = _boardGeneratorConfig.EmptyTilesAmount;
            var tilePrefab = _boardGeneratorConfig.TilePrefab;

            for (int x = 0; x < _boardSize; x++)
            {
                for (int y = 0; y < _boardSize; y++)
                {
                    var tile = new Tile();
                    _tilesHolder.Tiles[x, y] = tile;

                    if (CurrentTileIsLast(x, y))
                        if (AllEmptyTilesSpawned(emptyTilesAmount))
                        {
                            tile.Empty = true;
                            continue;
                        }

                    if (ShouldSkipCurrentTile(emptyTilesAmount))
                    {
                        tile.Empty = true;
                        continue;
                    }

                    SpawnTile(x, y, tilePrefab);
                }

                _lineExistEmptyTile = false;
            }
        }

        
        private void InitBalls()
        {
            var ballsPull = _boardGeneratorConfig.BallsPull;

            for (int x = 0; x < _boardSize; x++)
            {
                for (int y = 0; y < _boardSize; y++)
                {
                    if (CheckTileForEmpty(x, y)) continue;

                    var ball = ballsPull[Random.Range(0, ballsPull.Length)];
                    _ballsHolder.InsertBallToArrayAtPosition(x, y, ball);
                }
            }
        }

        private bool CheckMatchInRows()
        {
            for (int y = 0; y < _boardSize; y++)
            {
                for (int x = 0; x < _boardSize; x++)
                {
                    var counter = x;
                    if (CheckTileForEmpty(counter, y)) continue;
                    var ball = _ballsHolder.Balls[counter, y];
                    
                    counter++;
                    if (IsOutsideBoundsOfArray(counter) || CheckTileForEmpty(counter, y)) continue;
                    var nextBall = _ballsHolder.Balls[counter, y];

                    if (ball.Type == nextBall.Type)
                    {
                        counter++;
                        if (IsOutsideBoundsOfArray(counter) || CheckTileForEmpty(counter, y)) continue;
                        nextBall = _ballsHolder.Balls[counter, y];

                        if (ball.Type == nextBall.Type)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool CheckMatchInColumns()
        {
            for (int x = 0; x < _boardSize; x++)
            {
                for (int y = 0; y < _boardSize; y++)
                {
                    var counter = y;
                    if (CheckTileForEmpty(x, counter)) continue;
                    var ball = _ballsHolder.Balls[x, counter];
                    
                    counter++;
                    if (IsOutsideBoundsOfArray(counter) || CheckTileForEmpty(x, counter)) continue;
                    var nextBall = _ballsHolder.Balls[x, counter];

                    if (ball.Type == nextBall.Type)
                    {
                        counter++;
                        if (IsOutsideBoundsOfArray(counter) || CheckTileForEmpty(x, counter)) continue;
                        nextBall = _ballsHolder.Balls[x, counter];

                        if (ball.Type == nextBall.Type)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool IsOutsideBoundsOfArray(int index) => index >= _boardSize;

        private void SpawnBalls()
        {
            for (int x = 0; x < _boardSize; x++)
            {
                for (int y = 0; y < _boardSize; y++)
                {
                    if (CheckTileForEmpty(x, y)) continue;
                    var currentBall = _ballsHolder.Balls[x, y];
                    SpawnBall(x, y, currentBall);
                }
            }
        }
        
        private bool CheckTileForEmpty(int x, int y)
        {
            var tile = _tilesHolder.Tiles[x, y];
            return tile.Empty;
        }

        private bool AllEmptyTilesSpawned(int emptyTilesAmount) => _emptyTilesCounter < emptyTilesAmount;
        private bool CurrentTileIsLast(int x, int y) => x == _boardSize - 1 && y == _boardSize - 1;

        private bool ShouldSkipCurrentTile(int emptyTilesAmount)
        {
            if (_lineExistEmptyTile)
                return false;

            var AllEmptyTilesSpawned = _emptyTilesCounter >= emptyTilesAmount;

            if (AllEmptyTilesSpawned)
                return false;

            if (TileShouldBeEmpty())
            {
                _emptyTilesCounter++;
                _lineExistEmptyTile = true;
                return true;
            }

            return false;
        }

        private bool TileShouldBeEmpty() => Random.Range(0, 9) == 1;

        private void SpawnBall(int x, int y, Ball ball)
        {
            var spawnedBall = Instantiate(ball, new Vector3Int(x, y, 0), Quaternion.identity, _ballsContainer);
            spawnedBall.BoardPosition = new Vector2Int(x, y);
            spawnedBall.InitBallReplacer(_ballsReplacer);
            spawnedBall.transform.localPosition = GetSamePositionWithZeroZ(spawnedBall.transform.localPosition);
        }

        private void SpawnTile(int x, int y, GameObject tilePrefab)
        {
            var spawnedTile = Instantiate(tilePrefab, new Vector3Int(x, y, 0), Quaternion.identity, _tilesContainer);
            spawnedTile.transform.localPosition = GetSamePositionWithZeroZ(spawnedTile.transform.localPosition);
        }

        private Vector3 GetSamePositionWithZeroZ(Vector3 position) => new Vector3(position.x, position.y, 0);
    }
}