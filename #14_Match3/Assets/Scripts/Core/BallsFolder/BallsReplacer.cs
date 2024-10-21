using System;
using Core.Board;
using DG.Tweening;
using UnityEngine;

namespace Core.BallsFolder
{
    public class BallsReplacer : MonoBehaviour
    {
        private BoardGenerator _boardGenerator;
        private Ball _currentBall;
        private BallsHolder _ballsHolder;

        private bool _ballSelected;
        public bool CanClick { get; private set; } = true;

        public event Action<Ball,Ball> TwoBallsReplaced;

        public void Construct(BoardGenerator boardGenerator, BallsHolder ballsHolder)
        {
            _boardGenerator = boardGenerator;
            _ballsHolder = ballsHolder;
        }

        public void SelectNewBall(Ball ball)
        {
            if (_ballSelected)
            {
                if (SecondBallTooFar(ball))
                {
                    _currentBall.SetReleased();
                    _currentBall = ball;
                    _currentBall.SetPressed();
                    return;
                }
                
                ReleasedBalls(ball);
                ReplaceBalls(ball);
                _ballSelected = false;
                return;
            }

            _ballSelected = true;
            _currentBall = ball;
            _currentBall.SetPressed();
        }

        private void ReleasedBalls(Ball ball)
        {
            _currentBall.SetReleased();
            ball.SetReleased();
        }
        private void ReplaceBalls(Ball ball)
        {
            _ballsHolder.ReplaceTwoBalls(_currentBall, ball);
            
            var newBall = ball.transform;
            var currentBall = _currentBall.transform;

            DOTween.Sequence()
                .AppendCallback(() => CanClick = false)
                .Join(newBall.DOLocalMove(currentBall.localPosition, 0.3f))
                .Join(currentBall.DOLocalMove(newBall.localPosition, 0.3f))
                .AppendCallback(() =>
                {
                    CanClick = true;
                    TwoBallsReplaced?.Invoke(_currentBall, ball);
                })
                .SetEase(Ease.Linear);
        }

        private bool SecondBallTooFar(Ball newBall)
        {
            return Vector2.Distance(_currentBall.BoardPosition, newBall.BoardPosition) > 1;
        }
    }
}