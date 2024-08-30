namespace Single_Responsability_Principle_ASP.Service
{
    public class LogService
    {
        private readonly string name = "log.txt";

        public async void Save(string text)
        {
            await File.AppendAllTextAsync("Saving in database...", name);
            await File.AppendAllTextAsync(text, name);
            await File.AppendAllTextAsync("Saved. Closing database...", name);
        }
    }
}
