using Core.BallsFolder;
using UnityEngine;

namespace Core.Board
{
    [CreateAssetMenu(fileName = "BoardGenerator", menuName = "ScriptableObjects/BoardGenerator")]
    public class GeneratorConfig : ScriptableObject
    {
        [SerializeField] private int _boardSize;
        [SerializeField] private int _emptyTilesAmount;
        [SerializeField] private Ball[] _ballsPull;
        [SerializeField] private GameObject _tilePrefab;

        public int BoardSize => _boardSize;
        public int EmptyTilesAmount => _emptyTilesAmount;
        public Ball[] BallsPull => _ballsPull;

        public GameObject TilePrefab => _tilePrefab;
    }
}