Imports EdgePulse.Data
Imports EdgePulse.Entities
Imports EdgePulse.Infrastructure
Public Class StatusReportBL
    Function GenerateStatusReport(ByVal superstorGroupId As Int16, processMonth As String) As Boolean
        Try


        Catch ex As Exception
            Throw
        End Try
        Return True

    End Function

End Class
