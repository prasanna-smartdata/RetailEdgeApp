Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data

Public NotInheritable Class BooleanToVisibilityConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Dim bValue As Boolean = False

        If TypeOf value Is Boolean Then
            bValue = CBool(value)
        ElseIf TypeOf value Is Nullable(Of Boolean) Then
            Dim tmp As Nullable(Of Boolean) = CType(value, Nullable(Of Boolean))
            bValue = If(tmp.HasValue, tmp.Value, False)
        End If

        Return If((bValue), Visibility.Visible, Visibility.Collapsed)
    End Function



    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        If TypeOf value Is Visibility Then
            Return CType(value, Visibility) = Visibility.Visible
        Else
            Return False
        End If
    End Function
End Class
