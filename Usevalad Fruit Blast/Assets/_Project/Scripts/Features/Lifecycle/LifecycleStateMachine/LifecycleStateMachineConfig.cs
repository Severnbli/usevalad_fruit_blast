using System;
using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine
{
    [Serializable]
    public class LifecycleStateMachineConfig : IFeatureConfig
    {
        [Header("Start Game State")] 
        [SerializeField] private string[] _startGamePhrases;
        [SerializeField] private float _startGamePhrasesDuration;
        
        [Header("Core Game State")]
        [SerializeField] private int _minHpAmount = 0;

        [Header("End Game State")] 
        [SerializeField] private float _noProgressBarUpdatesDelay = 3f;
        
        [Header("Defeat Dialog State")] 
        [SerializeField] private string[] _defeatDialogPhrases;
        [SerializeField] private float _defeatDialogPhrasesDuration;
        
        
        public string[] StartGamePhrases => _startGamePhrases;
        public float StartGamePhrasesDuration => _startGamePhrasesDuration;
        public int MinHpAmount => _minHpAmount;
        public float NoProgressBarUpdatesDelay => _noProgressBarUpdatesDelay;
        public string[] DefeatDialogPhrases => _defeatDialogPhrases;
        public float DefeatDialogPhrasesDuration => _defeatDialogPhrasesDuration;
    }
}