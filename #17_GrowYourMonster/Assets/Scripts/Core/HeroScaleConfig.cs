using System;
using System.Collections.Generic;
using Core.Upgrades;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Hero Scale Config", menuName = "ScriptableObjects/HeroScaleConfig", order = 1)]
    public class HeroScaleConfig : ScriptableObject
    {
        [TableList]
        public List<RateAndScalePair> scaleSheet = new List<RateAndScalePair>();
    }

    [Serializable]
    public class RateAndScalePair
    {
        [TableColumnWidth(10)]
        public int rate;

        [TableColumnWidth(60)] 
        public float additionalScale;        
        
        [TableColumnWidth(60)] 
        public float cameraY;        
        
        [TableColumnWidth(60)] 
        public float cameraZ;
    }
}