using Acr.UserDialogs;
using BookLib;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace DbGetPost.ViewModels
{
    class Page1VM
    {
        public string author { get; set; } = "";
        public string title { get; set; } = "";
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();

        HttpClient client = new HttpClient();
        public Page1VM()
        {
            Get();
        }

        public ICommand GetBooks => new Command(Get);
        public async void Get()
        {
            var json = await client.GetStringAsync("http://192.168.1.64/Books");
            var collection = JsonConvert.DeserializeObject<ObservableCollection<Book>>(json);
            Books.Clear();
            foreach (var book in collection)
            {
                Books.Add(book);
            }
        }
        public ICommand PostBook => new Command(() =>
        {
            var book = new Book()
            {
                Title = title,
                Author = author
            };

            if (book.Author != "" && book.Title != "")
            {
                client.PostAsJsonAsync("http://192.168.1.64/Books/Post", book);
                Get();
                title = "";
                author = "";
                UserDialogs.Instance.Toast("Book added successfully");
            }
            else
            {
                UserDialogs.Instance.Toast("Please fill the fields");
            }
        });
    }
}
