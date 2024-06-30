using FireProhobition.Lib;

namespace FireProhobition.Threads.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ProhobitionAPI api = new();
            var r = await api.GetFireProhobitionsAsync();

            Console.WriteLine(r.Count);
        }
    }
}
