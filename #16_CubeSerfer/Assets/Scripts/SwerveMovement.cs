using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(SwerveInputSystem))]
[RequireComponent(typeof(SplineFollower))]
public class SwerveMovement : MonoBehaviour
{
    [SerializeField] [Range(0.5f, 5)]  private float _swerveSpeed;
    [SerializeField] private float _maxSideMovement;

    private SwerveInputSystem _swerveInputSystem;
    private SplineFollower _splineFollower;

    private void Awake()
    {
        _swerveInputSystem = GetComponent<SwerveInputSystem>();
        _splineFollower = GetComponent<SplineFollower>();
    }

    private void Update()
    {
        float swerveAmount = _swerveSpeed * _swerveInputSystem.MoveFactorX * Time.deltaTime;

        _splineFollower.motion.offset += new Vector2(swerveAmount, 0);

        if (_splineFollower.motion.offset.x > _maxSideMovement)
        {
            _splineFollower.motion.offset = new Vector3(_maxSideMovement, _splineFollower.motion.offset.y);
        }
        else if (_splineFollower.motion.offset.x < -_maxSideMovement)
        {
            _splineFollower.motion.offset = new Vector3(-_maxSideMovement, _splineFollower.motion.offset.y);
        }
    }
}