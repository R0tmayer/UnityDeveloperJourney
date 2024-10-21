using Core.Board;
using Core.TilesFolder;
using UnityEngine;

namespace Core.BallsFolder
{
    public class MatchBallsSearcher
    {
        private readonly BallsReplacer _ballsReplacer;
        private GeneratorConfig _generatorConfig;
        private int _boardSize;
        private TilesHolder _tilesHolder;
        private BallsHolder _ballsHolder;

        public MatchBallsSearcher(BallsHolder ballsHolder, TilesHolder tilesHolder)
        {
            _tilesHolder = tilesHolder;
            _ballsHolder = ballsHolder;
            Debug.Log(_ballsHolder.Balls.Length);
        }
        
        

        // private void OnTwoBallsReplaced(Ball fistBall, Ball secondBall)
        // {
        //     Debug.Log(_ballsHolder.Balls.Length);
        //     Debug.Log("OnTwoBallsReplaced");
        // }
        //
        // private void FindMatch(Ball ball)
        // {
        //     var position = ball.BoardPosition;
        //
        //     var topPosition = position + Vector2Int.up;
        //     if(topPosition.y > _boardSize - 1) return;
        //     
        // }
        
    }
}