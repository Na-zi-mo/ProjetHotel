﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace hotel24Eq5.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public virtual bool IsEnabled => ActionModeActuel != ACTIONMODE.DISPLAY;
        public virtual bool IsReadOnly => ActionModeActuel == ACTIONMODE.DISPLAY;
        public virtual bool IsEnabledListNavigation => ActionModeActuel == ACTIONMODE.DISPLAY;

        public enum ACTIONMODE { ADD, EDIT, DISPLAY };

        private ACTIONMODE _actionModeActuel = ACTIONMODE.DISPLAY;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }



        public virtual ACTIONMODE ActionModeActuel
        {
            get => _actionModeActuel;
            set
            {
                if (_actionModeActuel != value)
                {
                    _actionModeActuel = value;

                    OnPropertyChanged();

                    OnPropertyChanged("IsEnabled");
                    OnPropertyChanged("IsReadOnly");
                    OnPropertyChanged("IsEnabledListNavigation");
                }
            }

        }

        public virtual bool CanEndEdit(object obj)
        { 
            return ActionModeActuel != ACTIONMODE.DISPLAY;
        }
        public virtual bool CanBeginEdit(object obj)

        {
            return ActionModeActuel == ACTIONMODE.DISPLAY;
        }
    }
}
