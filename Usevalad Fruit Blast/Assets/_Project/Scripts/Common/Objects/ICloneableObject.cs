namespace _Project.Scripts.Common.Objects
{
    public interface ICloneableObject<out T>
    {
        public T Clone();
    }
}