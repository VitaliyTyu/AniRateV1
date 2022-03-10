using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AniRateV1.Models
{
    internal class AnimeCollection : INotifyPropertyChanged
    {
        public string? Name { get; set; }
        public IList<AnimeTitle>? AnimeTitles { get; set; } // IList для того, чтобы в поле могла быть, ObservableCollection

        #region IsSelected : bool
        private bool _IsSelected;
        public bool IsSelected
        {
            get => _IsSelected;
            set
            {
                _IsSelected = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

    }
}
