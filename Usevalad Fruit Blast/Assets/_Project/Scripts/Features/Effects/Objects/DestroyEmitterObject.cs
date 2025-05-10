namespace _Project.Scripts.Features.Effects.Objects
{
    public class DestroyEmitterObject : EffectEmitterObject
    {
        public bool IsActive { get; set; } = false;
        
        private void OnDestroy()
        {
            if (!IsActive)
            {
                return;
            }
            
            Emit();
        }
    }
}