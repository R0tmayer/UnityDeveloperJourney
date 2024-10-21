using Core.Board;
using UnityEngine;

namespace Core.BallsFolder
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private BallType _ballType;
        [SerializeField] private MeshRenderer _meshRenderer;

        private static readonly int _rampThreshold = Shader.PropertyToID("_Ramp0");

        private BallsReplacer _ballsReplacer;

        public void InitBallReplacer(BallsReplacer ballsReplacer) => _ballsReplacer = ballsReplacer;

        public Vector2Int BoardPosition { get; set; }
        public BallType Type => _ballType;

        private void OnMouseDown()
        {
            if (_ballsReplacer.CanClick)
                _ballsReplacer.SelectNewBall(this);
        }

        public void SetPressed() => _meshRenderer.material.SetFloat(_rampThreshold, 0.5f);
        public void SetReleased() => _meshRenderer.material.SetFloat(_rampThreshold, 0.25f);
    }
}