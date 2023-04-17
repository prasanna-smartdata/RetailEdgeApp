Imports EdgePulse.Data
Imports EdgePulse.Entities
Imports EdgePulse.Infrastructure
Public Class StatusReportBL
    Function GenerateStatusReport(ByVal superstorGroupId As Int16, processMonth As String) As Boolean
        Try
            ' Get Client store list linked to Superstore
            Dim clientDL As New ClientDL()
            Dim statusReportDL As New StatusReportDL()

            Dim _edgeCustomerNumber As String
            Dim _storeNumber As Int16
            Dim _reportMonthFirstDay As Date
            Dim _reportMonth As Int16
            Dim _reportYear As Int16

            '----change to clientStoreSuperstoreEntity after getting DL methods
            Dim _clientStoreSuperstoreList As List(Of ClientStoreEntity)  ' List(Of ClientStoreSuperstoreEntity)

            Dim _statusReportCSLine As StatusReportEntity = Nothing

            '----change to clientStoreSuperstoreEntity after getting DL methods
            _clientStoreSuperstoreList = clientDL.GetClientStores()

            processMonth = "012023"
            _reportMonth = Convert.ToInt16(processMonth.Substring(0, 2))
            _reportYear = Convert.ToInt16(processMonth.Substring(2, 4))

            ' Loop through each client store and generate Report Line
            For Each clientStore As ClientStoreEntity In _clientStoreSuperstoreList
                ' how do we get clientnum 
                '_edgeCustomerNumber = clientDL.GetClientStores(  ' 1002 'ClientStoreEntity.
                _edgeCustomerNumber = 1002
                _storeNumber = 1
                _reportMonthFirstDay = New Date(_reportYear, _reportMonth, 1)

                _statusReportCSLine = statusReportDL.GetStatusReportClientStoreRecord(_edgeCustomerNumber, _storeNumber, _reportMonthFirstDay)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return True

    End Function

End Class
