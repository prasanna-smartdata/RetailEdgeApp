Imports EdgePulse.Data
Imports EdgePulse.Entities
Imports EdgePulse.Infrastructure
Public Class StatusReportBL
    Function GenerateStatusReport(ByVal superstorGroupId As Int16, processMonth As String, ByRef LogText As String) As List(Of StatusReportEntity)
        Try
            ' Get Client store list linked to Superstore
            Dim _superstoreDL As New SuperstoreDL()
            Dim _statusReportDL As New StatusReportDL()

            Dim _edgeCustomerNumber As String
            Dim _storeNumber As Int16
            Dim _reportMonthFirstDay As Date
            Dim _reportMonth As Int16
            Dim _reportYear As Int16

            'Get list of Client stores linked to selected Superstore
            Dim _ssClientStoreList As List(Of Report_SSClientStoresDetailsEntity) = Nothing
            LogText += "Client Status Report"
            Dim _statusReportCSLine As StatusReportEntity = Nothing
            Dim _statusReportCSLines As List(Of StatusReportEntity) = Nothing


            '----change to clientStoreSuperstoreEntity after getting DL methods
            _ssClientStoreList = _superstoreDL.GetSuperstoreClientStoresDetails(superstorGroupId)

            '  processMonth = "012023" 'For testing only . Remove this later

            _reportMonth = Convert.ToInt16(processMonth.Substring(0, 2))
            _reportYear = Convert.ToInt16(processMonth.Substring(2, 4))

            _statusReportCSLines = New List(Of StatusReportEntity)

            ' Loop through each client store and generate Report Line
            For Each clientStore As Report_SSClientStoresDetailsEntity In _ssClientStoreList

                _edgeCustomerNumber = clientStore.ClientNum
                _storeNumber = clientStore.StoreID
                _reportMonthFirstDay = New Date(_reportYear, _reportMonth, 1)
                LogText += "Processing client store " + _edgeCustomerNumber.ToString() + "-" + _storeNumber.ToString()

                _statusReportCSLine = _statusReportDL.GetStatusReportClientStoreRecord(_edgeCustomerNumber, _storeNumber, _reportMonthFirstDay)

                If Not IsNothing(_statusReportCSLine) Then
                    _statusReportCSLines.Add(_statusReportCSLine)
                    LogText += "Report line generated for client " + _edgeCustomerNumber.ToString() + "-" + _storeNumber.ToString()
                Else
                    LogText += "Error in generating Report for client " + _edgeCustomerNumber.ToString() + "-" + _storeNumber.ToString()
                End If

            Next
            Return _statusReportCSLines
        Catch ex As Exception
            Throw
        End Try

    End Function

End Class
