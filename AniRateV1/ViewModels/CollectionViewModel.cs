using AniRateV1.Models;
using AniRateV1.ViewModels.Base;
using System;
using System.Windows.Input;

namespace AniRateV1.ViewModels
{

    internal class CollectionViewModel : ViewModel
    {
        public MainWindowViewModel MainWindowViewModel { get; internal set; }

        #region SelectedAnimeCollection : AnimeCollection - выбранная аниме коллекция
        private AnimeCollection _SelectedAnimeCollection;
        public AnimeCollection SelectedAnimeCollection
        {
            get => _SelectedAnimeCollection;
            set
            {
                Set(ref _SelectedAnimeCollection, value);
            }
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
                MainWindowViewModel.SelectedAnimeTitle = value;
            }
        }
        #endregion

        public ICommand ExactAnimeTitleCommand => MainWindowViewModel.ExactAnimeTitleCommand;

        public CollectionViewModel()
        {

        }
    }
}
