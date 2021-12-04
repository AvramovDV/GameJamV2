namespace GameJam
{
    public interface IRequireInjection<T>
    {
        public void InjectDependency(T dependency);
    }
}

