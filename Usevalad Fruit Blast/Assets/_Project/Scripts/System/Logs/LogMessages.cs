namespace _Project.Scripts.System.Logs
{
    public static class LogMessages
    {
        public static string DependencyNotFound(string where, string dependencyName)
        {
            return $"{where} - Dependency not found: {dependencyName}!";
        }
    }
}