using System;
using System.Globalization;
using System.Windows.Data;

namespace FreshBox.Converter
{
    public class EnumEqualsMultiConverter : IMultiValueConverter
    {
        // Convert: 두 값을 비교해서 같으면 true, 다르면 false
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2 || values[0] == null || values[1] == null)
                return false;

            return values[0].Equals(values[1]);
        }

        // ConvertBack: 체크된 경우 첫 번째 값은 그대로, 두 번째 값은 선택된 enum 값 반환
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is bool b && b)
            {
                // 첫 번째 값은 무시, 두 번째 값 (parameter로 전달된 enum) 반환
                return new object[] { Binding.DoNothing, parameter };
            }

            return new object[] { Binding.DoNothing, Binding.DoNothing };
        }
    }
}