using FireProhobition.Lib;

namespace FireProhobition.Threads.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProhobitionAPI api = new();
            api.GetMunicipalities();
        }
    }
}
