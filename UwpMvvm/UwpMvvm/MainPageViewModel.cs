using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UwpMvvm
{
    public class MainPageViewModel
    {
        public ObservableCollection<Book> Books { get; set } = new ObservableCollection<Book> { 
            new Book{Title = "one", Pages = 100 },
            new Book{Title = "two", Pages = 200 },
            new Book {Title = "three", Pages = 300 },             
            new Book {Title= "four", Pages = 400 },
            new Book  {Title = "five", Pages=500 },
            new Book {Title= "six", Pages = 600 },
            new Book {Title= "seven", Pages = 700 },
        };
    }
}
