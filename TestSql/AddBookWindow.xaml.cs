using System.Windows;

namespace TestSql
{
    public partial class AddBookWindow : Window
    {
        public Book NewBook { get; private set; }

        public AddBookWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NewBook = new Book
            {
                Title = txtTitle.Text,
                Author = txtAuthor.Text,
                ISBN = txtISBN.Text,
                Genre = txtGenre.Text,
                PublicationDate = dpPublicationDate.SelectedDate ?? DateTime.Now
            };
            DialogResult = true;
            Close();
        }
    }
}
