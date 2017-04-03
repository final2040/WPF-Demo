using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using WpfApplicationPractice.Helpers;

namespace WpfApplicationPractice.ViewModels
{
    public abstract class ViewModelBase:INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly ValidationContext _validationContext;

        public ViewModelBase()
        {
            _validationContext = new ValidationContext(this);
        }

        public string ViewModelName { get; set; }

        public bool ValidateViewModel()
        {
            return Validator.TryValidateObject(this, _validationContext, new List<ValidationResult>(), true);
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region IDataErrorInfo Implementation

        public string this[string columnName]
        {
            get { return ValidateProperty(columnName); }
        }

        private string ValidateProperty(string propertyName)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();

            // obtener valor de la propiedad a validar via reflection
            var propertyValue = GetType().GetProperty(propertyName).GetValue(this);
            
            // setear el nombre de la propiedad a validar.
            _validationContext.MemberName = propertyName;

            // retornar lista de errores encontrados.
            if (!Validator.TryValidateProperty(propertyValue, _validationContext, validationResults))
            {
                return string.Join(Environment.NewLine, validationResults.Select(vr => vr.ErrorMessage).ToArray());
            }
            return "";
        }

        public string Error
        {
            get { return null; }
        }

        #endregion

    }
}