Imports System.Configuration

Public Module HelperFile
    Public Function GetMonthsListForStatusReport() As List(Of Object)
        Dim monthsCollection = New List(Of Object)
        Try
            Dim noOfMonths = ConfigurationManager.AppSettings("NoOfMonthsForStatusReport")

            Dim currentDate As DateTime = DateTime.Now
            Dim startDate As DateTime = currentDate.AddMonths(noOfMonths)

            For i As Integer = 0 To noOfMonths
                Dim dateToAdd As DateTime = currentDate.AddMonths(-i)
                monthsCollection.Add(New With {.Name = dateToAdd.ToString("Y"), .Value = New DateTime(dateToAdd.Year, dateToAdd.Month, 1).ToString("d")})

            Next

        Catch ex As Exception
            Throw
        End Try
        Return monthsCollection
    End Function
End Module
