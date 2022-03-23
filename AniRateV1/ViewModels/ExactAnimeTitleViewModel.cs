using AniRateV1.Models;
using AniRateV1.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AniRateV1.ViewModels
{
    internal class ExactAnimeTitleViewModel : ViewModel
    {
        public MainWindowViewModel MainWindowViewModel { get; internal set; }

        #region SelecteAnimeTitle : AnimeTitle - выбранный аниме тайтл
        private AnimeTitle _SelectedAnimeTitle;
        public AnimeTitle SelectedAnimeTitle
        {
            get => _SelectedAnimeTitle;
            set => Set(ref _SelectedAnimeTitle, value);
        }
        #endregion

        //private ObservableCollection<AnimeCollection> _AnimeCollections;
        //public ObservableCollection<AnimeCollection> AnimeCollections
        //{
        //    get => _AnimeCollections;
        //    set => Set(ref _AnimeCollections, value);
        //}

        public ObservableCollection<AnimeCollection> AnimeCollections => MainWindowViewModel.AnimeCollections;

        public ICommand AddAnimeToManyCollections => MainWindowViewModel.AddAnimeToManyCollections;

        public ExactAnimeTitleViewModel()
        {

        }
    }
}
