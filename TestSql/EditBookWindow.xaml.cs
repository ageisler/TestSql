using System.Windows;

namespace TestSql
{
    public partial class EditBookWindow : Window
    {
        public Book EditedBook { get; private set; }

        public EditBookWindow(Book book)
        {
            InitializeComponent();
            EditedBook = book;
            txtTitle.Text = book.Title;
            txtAuthor.Text = book.Author;
            txtISBN.Text = book.ISBN;
            txtGenre.Text = book.Genre;
            dpPublicationDate.SelectedDate = book.PublicationDate;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            EditedBook.Title = txtTitle.Text;
            EditedBook.Author = txtAuthor.Text;
            EditedBook.ISBN = txtISBN.Text;
            EditedBook.Genre = txtGenre.Text;
            EditedBook.PublicationDate = dpPublicationDate.SelectedDate ?? DateTime.Now;
            DialogResult = true;
            Close();
        }
    }
}
