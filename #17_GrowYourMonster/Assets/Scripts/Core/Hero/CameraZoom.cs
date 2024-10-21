using System;
using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Hero
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] [Required] private CinemachineVirtualCamera _virtualCamera;
        
        private CinemachineTransposer _transposer;
        private HeroScaleConfig _scaleConfig;

        public void Construct(HeroScaleConfig scaleConfig) => _scaleConfig = scaleConfig;

        private void Awake() => _transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        public void ZoomOut(int scaleSheetIndex)
        {
            scaleSheetIndex--;
            var offsetY = _transposer.m_FollowOffset.y + _scaleConfig.scaleSheet[scaleSheetIndex].cameraY;
            var offsetZ = _transposer.m_FollowOffset.z + _scaleConfig.scaleSheet[scaleSheetIndex].cameraZ;
                
            DOTween.To(() => _transposer.m_FollowOffset.y, y => _transposer.m_FollowOffset.y = y, offsetY, 0.5f);
            DOTween.To(() => _transposer.m_FollowOffset.z, z => _transposer.m_FollowOffset.z = z, offsetZ, 0.5f);
        }
    }
}