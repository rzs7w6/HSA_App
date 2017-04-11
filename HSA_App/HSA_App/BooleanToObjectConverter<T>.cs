using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HSA_App
{
	public class BooleanToObjectConverter<T> : IValueConverter
    {
		//When creating this class the panel was correct but the class named itself "BooleanToObjectConverter_T_
		//if anything goes wrong with referencing there's a good chance it's here
        public T FalseObject { set; get; }

		public T TrueObject { set; get; }

		public object Convert(object value, Type targetType,
							  object parameter, CultureInfo culture)
		{
			return (bool)value ? this.TrueObject : this.FalseObject;
		}

		public object ConvertBack(object value, Type targetType,
								  object parameter, CultureInfo culture)
		{
			return ((T)value).Equals(this.TrueObject);
		}

	}
}
