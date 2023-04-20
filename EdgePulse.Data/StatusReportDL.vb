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

            Dim reader As SqlDataReader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, StoredProcNames.getClientStoreStatusReportRecord, sqlParams)

            While reader.Read()

                _statusReportEntity = New StatusReportEntity()

                With _statusReportEntity
                    .ClientMembership = reader.Item("clientMemebership")
                    .SystemID = reader.Item("systemId")
                    .AvailableMonthsOfData = reader.Item("availableMonthsOfData")
                    .LastSalesDate = reader.Item("lastSalesDate")
                    .DeptCoding = reader.Item("deptCoding")
                    .Sales12Months = reader.Item("sales12Months")
                    .SalesPrevMonth = reader.Item("salesPrevMonth")
                    .SalesPercent = reader.Item("salesPercent")
                    .CurrentStock = reader.Item("currentStock")
                End With

            End While

        Catch ex As Exception
            Throw
        End Try

        Return _statusReportEntity

    End Function
End Class
