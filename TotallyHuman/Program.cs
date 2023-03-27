

namespace TotallyHuman
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            string token = File.ReadAllText(@"..\..\..\..\Token.txt");

            TotallyHuman totallyHuman = new TotallyHuman(token);

            await totallyHuman.SetupClientAsync();
        }
    }
}
