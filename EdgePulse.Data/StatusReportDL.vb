Imports System.Data
Imports System.Data.SqlClient
Imports EdgePulse.Entities
Imports EdgePulse.Infrastructure
Public Class StatusReportDL
    Inherits EdgePulseBaseDL
    Public Function GetStatusReportClientStoreRecord(ByVal edgeCustomerNumber As Int16, storeNum As Int16, reportMonthFirstDay As Date) As StatusReportEntity
        Dim _statusReportEntity As StatusReportEntity = Nothing
        Try
            Dim sqlParams(3) As SqlParameter
            sqlParams(0) = New SqlParameter("@EdgeCustomerNumber", SqlDbType.Int)
            sqlParams(1) = New SqlParameter("@StoreNum", SqlDbType.Int)
            sqlParams(2) = New SqlParameter("@ReportMonthFirstDay", SqlDbType.Date)
            sqlParams(0).Value = edgeCustomerNumber
            sqlParams(1).Value = storeNum
            sqlParams(2).Value = reportMonthFirstDay

            Dim reader As SqlDataReader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, StoredProcNames.getClientDetails, sqlParams)

        Catch ex As Exception
            Throw
        End Try
        Return _statusReportEntity
    End Function
End Class
