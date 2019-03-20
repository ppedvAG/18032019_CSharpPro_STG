using ppedv.BookManager2000.Logic;
using ppedv.BookManager2000.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ppedv.BookManager2000.UI.WPF.ViewModel
{
    public class BooksViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Book> BücherListe { get; set; }
        Book selectedBook;

        public ICommand SaveCommand { get; set; }


        public Book SelectedBook
        {
            get
            {
                return selectedBook;
            }

            set
            {
                selectedBook = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ZeichenImTitel)));

            }
        }

        public string Title
        {
            get
            {
                if (SelectedBook == null)
                    return "---";
                return SelectedBook.Title;
            }
            set
            {
                if (SelectedBook != null)
                    SelectedBook.Title = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ZeichenImTitel)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }

        public int ZeichenImTitel
        {
            get
            {
                if (SelectedBook == null)
                    return -1;
                return SelectedBook.Title.Length;
            }
        }

        Core core = new Core();

        public event PropertyChangedEventHandler PropertyChanged;

        public BooksViewModel()
        {
            BücherListe = new ObservableCollection<Book>(core.Repository.GetAll<Book>());


            SaveCommand = new RelayCommand(o => core.Repository.SaveChanges());
        }
    }
}
