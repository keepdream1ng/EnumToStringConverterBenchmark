using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Workflow.ComponentModel;

namespace Tekla.Extension.Services;
[ValueConversion(typeof(Enum), typeof(string[]))]
public class EnumValuesConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value == null ? Binding.DoNothing : Enum.GetValues((Type)value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

[ValueConversion(typeof(Enum), typeof(string))]
public class EnumToStringValueConverter : DependencyObject, IValueConverter//TODO localize
{
    #region IValueConverter Members
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value == null) return Binding.DoNothing;
        return ((Enum)value).StringValue();
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
    #endregion
}
public static class StringValueExtensions
{
    public static string StringValue(this Enum This)
    {
        System.Reflection.FieldInfo fieldInfo = This.GetType().GetField(This.ToString());
        DescriptionAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
        return attribs.Length == 0 ? null : attribs[0].Description;
    }
}
