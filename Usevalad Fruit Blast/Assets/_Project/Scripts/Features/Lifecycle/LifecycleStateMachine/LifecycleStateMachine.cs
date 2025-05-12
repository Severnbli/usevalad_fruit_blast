using System;
using System.Collections.Generic;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.Lifecycle.LifecycleStateMachine.States;

namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine
{
    public class LifecycleStateMachine : BaseFeature, IConfigurableFeature<LifecycleStateMachineConfig>, IUpdatableFeature
    {
        private Dictionary<Type, LifecycleState> _states;
        private LifecycleState _currentState;
        
        public LifecycleContainer LifecycleContainer { get; private set; }
        public LifecycleStateMachineConfig LifecycleStateMachineConfig { get; private set; }

        public override void Init()
        {
            base.Init();

            LifecycleContainer = new LifecycleContainer(this);
            SetupStates();
        }
        
        public void Configure(LifecycleStateMachineConfig lifecycleStateMachineConfig)
        {
            LifecycleStateMachineConfig = lifecycleStateMachineConfig;
            
            EnterIn<BootstrapState>();
        }

        private void SetupStates()
        {
            _states = new Dictionary<Type, LifecycleState>();
            
            AddState(new BootstrapState());
            AddState(new StartGameState());
            AddState(new CoreGameState());
            AddState(new EndGameState());
            AddState(new DefeatDialogState());
            AddState(new ResetGameState());
        }

        private void AddState(LifecycleState state)
        {
            _states.Add(state.GetType(), state);
            state.SetupState(this);
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void EnterIn<T>() where T : LifecycleState
        {
            if (!_states.TryGetValue(typeof(T), out var state))
            {
                return;
            }

            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }
    }
}