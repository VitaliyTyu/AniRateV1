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
        public ObservableCollection<AnimeTitle> AnimeTitles { get; }

        #region _SelecteAnimeTitle : AnimeTitle - выбранный аниме тайтл
        private AnimeTitle _SelectedAnimeTitle;
        public AnimeTitle SelectedAnimeTitle
        {
            get => _SelectedAnimeTitle;
            set => Set(ref _SelectedAnimeTitle, value);
        }
        #endregion

        public MainWindowViewModel()
        {
            var titles = Enumerable.Range(1, 20).Select(i => new AnimeTitle
            {
                Name = $"Тайтл {i}",
                Description = $"Description {i}",
                Rating = i,
            });

            AnimeTitles = new ObservableCollection<AnimeTitle>(titles);
        }
    }
}
