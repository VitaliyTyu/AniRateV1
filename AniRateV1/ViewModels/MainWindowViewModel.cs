using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using AniRateV1.Infrostructure.Commands;
using AniRateV1.Models;
using AniRateV1.ViewModels.Base;

namespace AniRateV1.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public static Random Rnd { get; set; } = new Random();
        public ObservableCollection<AnimeCollection> AnimeCollections { get; }
        public ExactAnimeTitleViewModel ExactAnimeTitleVM { get; set; }
        public CollectionViewModel CollectionVM { get; set; }


        #region Fields

        #region CurrentView : object - VM для текущего UserControl'a
        private object _CurrentView;
        public object CurrentView
        {
            get => _CurrentView;
            set => Set(ref _CurrentView, value);
        }
        #endregion

        #region _SelecteAnimeTitle : AnimeTitle - выбранный аниме тайтл
        private AnimeTitle _SelectedAnimeTitle;
        public AnimeTitle SelectedAnimeTitle
        {
            get => _SelectedAnimeTitle;
            set => Set(ref _SelectedAnimeTitle, value);
        }
        #endregion

        #region SelectedAnimeCollection : AnimeCollection - выбранная аниме коллекция
        private AnimeCollection _SelectedAnimeCollection;
        public AnimeCollection SelectedAnimeCollection
        {
            get => _SelectedAnimeCollection;
            set => Set(ref _SelectedAnimeCollection, value);
        }
        #endregion

        #region AnimeTitleFilterText : string - Текст фильтра студентов
        private string _AnimeTitleFilterText;
        public string AnimeTitleFilterText
        {
            get => _AnimeTitleFilterText;
            set
            {
                if (!Set(ref _AnimeTitleFilterText, value)) return;

                _SelecteAnimeCollectionOfTitles.View.Refresh();
            }
        }
        #endregion

        #region SelecteAnimeCollectionOfTitles : ICollectionView - выбранная коллекция
        readonly CollectionViewSource _SelecteAnimeCollectionOfTitles = new CollectionViewSource();
        public ICollectionView SelecteAnimeCollectionOfTitles => _SelecteAnimeCollectionOfTitles?.View;

        private void OnStudentFiltered(object sender, FilterEventArgs e)
        {
            if (!(e.Item is AnimeTitle anime))
            {
                e.Accepted = false;
                return;
            }

            var filter_text = _AnimeTitleFilterText;
            if (string.IsNullOrWhiteSpace(filter_text)) return;

            if (anime.Name is null || anime.Description is null)
            {
                e.Accepted = false;
                return;
            }

            if (anime.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if (anime.Description.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;
        }
        #endregion

        #endregion


        #region Commands


        #region ExactAnimeTitleCommand
        public ICommand ExactAnimeTitleCommand { get; }
        private bool CanExactAnimeTitleCommandExecute(object p) => true;
        private void OnExactAnimeTitleCommandExecuted(object p)
        {
            CurrentView = ExactAnimeTitleVM;
        }
        #endregion

        #region CollectionCommand
        public ICommand CollectionCommand { get; }
        private bool CanCollectionCommandExecute(object p) => true;
        private void OnCollectionCommandExecuted(object p)
        {
            CurrentView = new CollectionViewModel(SelectedAnimeCollection);
            //CurrentView = CollectionVM;
        }
        #endregion



        #endregion

        public MainWindowViewModel()
        {
            var titles = Enumerable.Range(1, 100).Select(i => new AnimeTitle
            {
                Name = $"Тайтл {Rnd.Next(1000)}",
                Description = $"Description {i}",
                Rating = i,
            });

            var collections = Enumerable.Range(1, 100).Select(i => new AnimeCollection
            {
                Name = $"Коллекция {i}",
                AnimeTitles = new ObservableCollection<AnimeTitle>(titles),
            });

            AnimeCollections = new ObservableCollection<AnimeCollection>(collections);



            CollectionVM = new CollectionViewModel(SelectedAnimeCollection);
            //CollectionVM = new CollectionViewModel();
            CurrentView = CollectionVM;

            ExactAnimeTitleVM = new ExactAnimeTitleViewModel();

            ExactAnimeTitleCommand = new LambdaCommand(OnExactAnimeTitleCommandExecuted, CanExactAnimeTitleCommandExecute);
            CollectionCommand = new LambdaCommand(OnCollectionCommandExecuted, CanCollectionCommandExecute);

        }
    }
}
