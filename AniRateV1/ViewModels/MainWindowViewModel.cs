using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using AniRateV1.DAL.Entities;
using AniRateV1.Infrostructure.Commands;
using AniRateV1.ViewModels.Base;
using AniRateV1.Views.Windows;

namespace AniRateV1.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public static Random Rnd { get; set; } = new Random();
        private readonly ExactAnimeTitleViewModel _ExactAnimeTitleViewModel;
        private readonly CollectionViewModel _CollectionViewModel;


        #region Fields

        #region AnimeCollections : ObservableCollection<AnimeCollection>
        private ObservableCollection<AnimeCollection> _AnimeCollections;
        public ObservableCollection<AnimeCollection> AnimeCollections
        {
            get => _AnimeCollections;
            set => Set(ref _AnimeCollections, value);
        }

        #endregion

        #region CurrentView : ViewModel - VM для текущего UserControl'a
        private ViewModel _CurrentView;
        public ViewModel CurrentView
        {
            get => _CurrentView;
            set => Set(ref _CurrentView, value);
        }
        #endregion

        #region SelecteAnimeTitle : AnimeTitle - выбранный аниме тайтл
        private AnimeTitle _SelectedAnimeTitle;
        public AnimeTitle SelectedAnimeTitle
        {
            get => _SelectedAnimeTitle;
            set
            {
                Set(ref _SelectedAnimeTitle, value);
                _ExactAnimeTitleViewModel.SelectedAnimeTitle = value;
            }
        }
        #endregion

        #region SelectedAnimeCollection : AnimeCollection - выбранная аниме коллекция
        private AnimeCollection _SelectedAnimeCollection;
        public AnimeCollection SelectedAnimeCollection
        {
            get => _SelectedAnimeCollection;
            set
            {
                Set(ref _SelectedAnimeCollection, value);
                _CollectionViewModel.SelectedAnimeCollection = value;
            }
        }
        #endregion

        #region AnimeTitleFilterText : string - Текст фильтра
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

        #region AddAnimeToManyCollections
        public ICommand AddAnimeToManyCollections { get; }
        private bool CanAddAnimeToManyCollectionsExecute(object p) => true;
        private void OnAddAnimeToManyCollectionsExecuted(object p)
        {
            //foreach (var collection in AnimeCollections)
            //{
            //    if (collection.IsSelected && SelectedAnimeTitle != null && !collection.AnimeTitles.Contains(SelectedAnimeTitle))
            //    {
            //        collection.AnimeTitles.Add(SelectedAnimeTitle);
            //    }
            //    collection.IsSelected = false;
            //}
             
        }
        #endregion

        #region ExactAnimeTitleCommand
        public ICommand ExactAnimeTitleCommand { get; }
        private bool CanExactAnimeTitleCommandExecute(object p) => true;
        private void OnExactAnimeTitleCommandExecuted(object p)
        {
            if (!(p is AnimeTitle title)) return;
            SelectedAnimeTitle = title;
            CurrentView = _ExactAnimeTitleViewModel;
        }
        #endregion

        #region CollectionCommand
        public ICommand CollectionCommand { get; }
        private bool CanCollectionCommandExecute(object p) => true;
        private void OnCollectionCommandExecuted(object p)
        {
            if (!(p is AnimeCollection collection)) return;
            SelectedAnimeCollection = collection;
            CurrentView = _CollectionViewModel;
        }
        #endregion

        #region CreateNewCollection
        public ICommand CreateNewCollection { get; }
        private bool CanCreateNewCollectionExecute(object p) => true;
        private void OnCreateNewCollectionExecuted(object p)
        {
            MenuAddCollection menuAddCollection = new MenuAddCollection();
            var titles = Enumerable.Range(1, 100).Select(i => new AnimeTitle
            {
                Name = $"Тайтл {Rnd.Next(1000)}",
                Description = $"Description {i}",
                Rating = i,
            });
            if (menuAddCollection.ShowDialog() == true)
            {
                var collections = new AnimeCollection
                {
                    Name = menuAddCollection.NewCollectionName,
                    AnimeTitles = new ObservableCollection<AnimeTitle>(titles),
                };
                AnimeCollections.Add(collections);
            }
                        
            //var i = AnimeCollections.Count + 1;
            //var titles = Enumerable.Range(1, 100).Select(i => new AnimeTitle
            //{
            //    Name = $"Тайтл {Rnd.Next(1000)}",
            //    Description = $"Description {i}",
            //    Rating = i,
            //});
            //var collections = new AnimeCollection
            //{
            //    Name = $"Коллекция {i}",
            //    AnimeTitles = new ObservableCollection<AnimeTitle>(titles),
            //};
            //AnimeCollections.Add(collections);
        }
        #endregion

        #region DeleteAnimeCollection
        public ICommand DeleteAnimeCollection { get; }

        private bool CanDeleteAnimeCollectionExecute(object p) => p is AnimeCollection group && AnimeCollections.Contains(group);
        private void OnDeleteAnimeCollectionExecuted(object p)
        {
            if (!(p is AnimeCollection  group)) return;
            var group_index = AnimeCollections.IndexOf(group);
            AnimeCollections.Remove(group);
            if (group_index < AnimeCollections.Count)
                SelectedAnimeCollection = AnimeCollections[group_index];
        }
        #endregion

        #endregion

        public MainWindowViewModel(CollectionViewModel collectionViewModel, ExactAnimeTitleViewModel exactAnimeTitleViewModel)
        {
            //var titles = Enumerable.Range(1, 100).Select(i => new AnimeTitle
            //{
            //    Name = $"Тайтл {Rnd.Next(1000)}",
            //    Description = $"Description {i}",
            //    Rating = i,
            //});

            //var collections = Enumerable.Range(1, 10).Select(i => new AnimeCollection
            //{
            //    Name = $"Коллекция {i}",
            //    AnimeTitles = new ObservableCollection<AnimeTitle>(titles),
            //});

            //AnimeCollections = new ObservableCollection<AnimeCollection>(collections);


            _CollectionViewModel = collectionViewModel;
            _CollectionViewModel.MainWindowViewModel = this;

            _ExactAnimeTitleViewModel = exactAnimeTitleViewModel;
            _ExactAnimeTitleViewModel.MainWindowViewModel = this;

            CurrentView = collectionViewModel;



            ExactAnimeTitleCommand = new LambdaCommand(OnExactAnimeTitleCommandExecuted, CanExactAnimeTitleCommandExecute);
            CollectionCommand = new LambdaCommand(OnCollectionCommandExecuted, CanCollectionCommandExecute);
            CreateNewCollection = new LambdaCommand(OnCreateNewCollectionExecuted, CanCreateNewCollectionExecute);
            DeleteAnimeCollection = new LambdaCommand(OnDeleteAnimeCollectionExecuted, CanDeleteAnimeCollectionExecute);
            AddAnimeToManyCollections = new LambdaCommand(OnAddAnimeToManyCollectionsExecuted, CanAddAnimeToManyCollectionsExecute);

        }

    }

}