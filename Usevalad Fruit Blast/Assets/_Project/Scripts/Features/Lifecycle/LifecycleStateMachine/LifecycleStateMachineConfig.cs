using System;
using _Project.Scripts.Common.UI.Text;
using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine
{
    [Serializable]
    public class LifecycleStateMachineConfig : IFeatureConfig
    {
        [Header("Start Game State")] 
        [SerializeField] private TextPopup _textPopup;
        [SerializeField] private string[] _startGamePhrases;

        [SerializeField] private float _phrasesDuration;
        
        [Header("Core Game State")]
        [SerializeField] private int _minHpAmount = 0;
        
        public TextPopup TextPopup => _textPopup;
        public string[] StartGamePhrases => _startGamePhrases;
        public float PhrasesDuration => _phrasesDuration;
        public int MinHpAmount => _minHpAmount;
    }
}