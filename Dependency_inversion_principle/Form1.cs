using System.Configuration;
using System.Text.Json;

namespace Dependency_inversion_principle
{
    public partial class Form1 : Form
    {

        string origin = "somewhere on the pc or api url";
        string dbPath = "another place on the pc";

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //This is the core. I create an interface an initialize it as a specific object. Then i can use it.
            // if anytime i want to change it to InfoByFile i only have to change it in the next line
            IInfo info = new InfoByRequest(origin);

            //If the next line is uncommented, it will work
            //info = new InfoByFile(origin);
            
            Monitor monitor = new Monitor(origin, info);
            await monitor.Show();

            FileDB file = new FileDB(dbPath, origin, info);
            await file.save();
        }

        public class InfoByRequest : IInfo
        {
            private string _url;
            public InfoByRequest(string url)
            {
                _url = url;
            }

            public async Task<IEnumerable<Post>> Get()
            {
                HttpClient client = new HttpClient(); 
                var response = await client.GetAsync(_url);
                var stream = await response.Content.ReadAsStreamAsync();
                List<Post> posts = await JsonSerializer.DeserializeAsync<List<Post>>(stream);
                return posts;
            }
        }

        public class Monitor
        {
            private string _origin;
            private IInfo _info;
            public Monitor(string origin, IInfo info)
            {
                _origin = origin;
                _info = info;
            }

            public async Task Show()
            {
                var posts = await _info.Get();
                foreach (var post in posts) { Console.WriteLine(post); }
            }
        }


        public class InfoByFile : IInfo
        {
            private string _path;
            public InfoByFile(string path)
            {
                _path = path;
            }


            public async Task<IEnumerable<Post>> Get()
            {
                var contentStream = new FileStream(_path, FileMode.Open, FileAccess.Read);
                IEnumerable<Post> posts = await JsonSerializer.DeserializeAsync<IEnumerable<Post>>(contentStream);
                return posts;
            }
        }

        public class Post
        {
            public int userId { get; set; }
            public int id { get; set; }
            public string title { get; set; }
            public bool completed { get; set; }
        }


        public class FileDB
        {
            private string _path;
            private string _origin;
            private IInfo _info;

            public FileDB(string origin, string path, IInfo info)
            {
                _origin = origin;
                _path = path;
                _info = info;
            }

            public async Task save()
            {
                var posts = await _info.Get();
                string json = JsonSerializer.Serialize(posts);
                await File.WriteAllTextAsync(_path, json);
            }

        }



        public interface IInfo
        {
            public Task<IEnumerable<Post>> Get();
        }

    }
}

