namespace SimpleSoftProject.StaticData
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class SessionData
    {
        public static string currentPath = Directory.GetCurrentDirectory();
        public static HashSet<Task> taskPool = new HashSet<Task>();
    }
}
