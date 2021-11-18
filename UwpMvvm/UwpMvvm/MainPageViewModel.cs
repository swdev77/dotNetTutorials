using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace UwpMvvm
{
    public class MainPageViewModel : ObservableObject
    {
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book> {
            new Book {Title = "one", Pages = 100 },
            new Book {Title = "two", Pages = 200 },
            new Book {Title = "three", Pages = 300 },             
            new Book {Title= "four", Pages = 400 },
            new Book {Title = "five", Pages=500 },
            new Book {Title= "six", Pages = 600 },
            new Book {Title= "seven", Pages = 700 },
            new Book {Title= "eight", Pages = 800 },
            new Book {Title= "nine", Pages = 900 },
            new Book {Title= "ten", Pages = 1000 },
        };

        public string SampleText { get; set; } = "This is text. And I'll show you how to change DataContext if you need.";

        private int counter;

        public int Counter
        {
            get => counter;
            set => SetProperty(ref counter, value);
        }

        private async void IncrementCounter()
        {
            while(true)
            {
                await Task.Delay(1000);
                Counter++;
            }
        }

        public MainPageViewModel()
        {
            IncrementCounter();
        }
    }
}
