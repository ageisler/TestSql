using SQLitePCL;
using System.Windows;

namespace TestSql
{
    public partial class MainWindow : Window
    {
        private BookRepository repository;

        public MainWindow()
        {
            InitializeComponent();
            Batteries_V2.Init();
            repository = new BookRepository();
            LoadBooks();
        }

        private void LoadBooks(string filter = "")
        {
            List<Book> books = repository.GetBooks();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                books = books.Where(b =>
                    b.Title.ToLower().Contains(filter.ToLower()) ||
                    b.Author.ToLower().Contains(filter.ToLower()) ||
                    b.ISBN.ToLower().Contains(filter.ToLower())).ToList();
            }
            dgBooks.ItemsSource = books;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string filter = txtSearch.Text;
            LoadBooks(filter);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addBookWindow = new AddBookWindow();
            if (addBookWindow.ShowDialog() == true)
            {
                repository.AddBook(addBookWindow.NewBook);
                LoadBooks();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgBooks.SelectedItem is Book selectedBook)
            {
                var editBookWindow = new EditBookWindow(selectedBook);
                if (editBookWindow.ShowDialog() == true)
                {
                    repository.UpdateBook(editBookWindow.EditedBook);
                    LoadBooks();
                }
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgBooks.SelectedItem is Book selectedBook)
            {
                // Optional: Rückfrage mittels MessageBox
                var result = MessageBox.Show("Möchten Sie dieses Buch wirklich löschen?", "Bestätigung", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    repository.DeleteBook(selectedBook.Id);
                    LoadBooks();
                }
            }
        }
    }
}
