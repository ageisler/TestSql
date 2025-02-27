using SQLitePCL;
using System.Windows;

namespace TestSql
{
    public partial class MainWindow : Window
    {
        private BookRepository repository;

        // ...

        public MainWindow()
        {
            InitializeComponent();
            Batteries_V2.Init(); // Ändern Sie diese Zeile
            repository = new BookRepository();
            LoadBooks();
        }

        private void LoadBooks(string filter = "")
        {
            List<Book> books = repository.GetBooks();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                // Filtert nach Titel, Autor oder ISBN (unabhängig von Groß-/Kleinschreibung)
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
                // Öffne ein Bearbeitungsfenster, um die Daten des Buchs zu aktualisieren.
                // Nach dem Bearbeiten:
                // repository.UpdateBook(editedBook);
                // LoadBooks();
                MessageBox.Show("Buch bearbeiten-Funktion wird hier implementiert.");
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
