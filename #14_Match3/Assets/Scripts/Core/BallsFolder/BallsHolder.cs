using UnityEngine;

namespace Core.BallsFolder
{
    public class BallsHolder
    {
        public readonly Ball[,] Balls;

        public BallsHolder(int size)
        {
            Balls = new Ball[size, size];
        }

        public void ReplaceTwoBalls(Ball currentBall, Ball newBall)
        {
            var currentBallPosition = new Vector2Int(currentBall.BoardPosition.x, currentBall.BoardPosition.y);

            Balls[newBall.BoardPosition.x, newBall.BoardPosition.y] = currentBall;
            Balls[currentBallPosition.x, currentBallPosition.y] = newBall;

            (currentBall.BoardPosition, newBall.BoardPosition) = (newBall.BoardPosition, currentBall.BoardPosition);
        }
        

        public void InsertBallToArrayAtPosition(int x, int y, Ball ball) => Balls[x, y] = ball;
    }
}