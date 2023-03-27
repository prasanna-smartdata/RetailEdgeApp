Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Xml
Imports System.Runtime.InteropServices


Module SqlHelper
        Private Sub AttachParameters(ByVal command As SqlCommand, ByVal commandParameters As IEnumerable(Of SqlParameter))
            If command Is Nothing Then Throw New ArgumentNullException("command")

            If commandParameters IsNot Nothing Then

                For Each p As SqlParameter In commandParameters

                    If p IsNot Nothing Then

                        If (p.Direction = ParameterDirection.InputOutput OrElse p.Direction = ParameterDirection.Input) AndAlso (p.Value Is Nothing) Then
                            p.Value = DBNull.Value
                        End If

                        command.Parameters.Add(p)
                    End If
                Next
            End If
        End Sub

        Private Sub AssignParameterValues(ByVal commandParameters As IEnumerable(Of SqlParameter), ByVal dataRow As DataRow)
            If (commandParameters Is Nothing) OrElse (dataRow Is Nothing) Then
                Return
            End If

            Dim i As Integer = 0

            For Each commandParameter As SqlParameter In commandParameters
                If commandParameter.ParameterName Is Nothing OrElse commandParameter.ParameterName.Length <= 1 Then Throw New Exception(String.Format("Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.", i, commandParameter.ParameterName))
                If dataRow.Table.Columns.IndexOf(commandParameter.ParameterName.Substring(1)) <> -1 Then commandParameter.Value = dataRow(commandParameter.ParameterName.Substring(1))
                i += 1
            Next
        End Sub

        Private Sub AssignParameterValues(ByVal commandParameters As SqlParameter(), ByVal parameterValues As Object())
            If (commandParameters Is Nothing) OrElse (parameterValues Is Nothing) Then
                Return
            End If

            If commandParameters.Length <> parameterValues.Length Then
                Throw New ArgumentException("Parameter count does not match Parameter Value count.")
            End If

            Dim i As Integer = 0, j As Integer = commandParameters.Length

            While i < j
                Dim value = TryCast(parameterValues(i), IDbDataParameter)

                If value IsNot Nothing Then
                    Dim paramInstance As IDbDataParameter = value
                    commandParameters(i).Value = If(paramInstance.Value, DBNull.Value)
                ElseIf parameterValues(i) Is Nothing Then
                    commandParameters(i).Value = DBNull.Value
                Else
                    commandParameters(i).Value = parameterValues(i)
                End If

                i += 1
            End While
        End Sub

        Private Sub PrepareCommand(ByVal command As SqlCommand, ByVal connection As SqlConnection, ByVal transaction As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String, ByVal commandParameters As SqlParameter(), <Out> ByRef mustCloseConnection As Boolean)
            If command Is Nothing Then Throw New ArgumentNullException("command")
            If String.IsNullOrEmpty(commandText) Then Throw New ArgumentNullException("commandText")

            If connection.State <> ConnectionState.Open Then
                mustCloseConnection = True
                connection.Open()
            Else
                mustCloseConnection = False
            End If

            command.Connection = connection
            command.CommandText = commandText

            If transaction IsNot Nothing Then
                If transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
                command.Transaction = transaction
            End If

            command.CommandType = commandType

            If commandParameters IsNot Nothing Then
                AttachParameters(command, commandParameters)
            End If

            Return
        End Sub

        Function ExecuteNonQuery(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String) As Integer
            Return ExecuteNonQuery(connectionString, commandType, commandText, Nothing)
        End Function

        Function ExecuteNonQuery(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ParamArray commandParameters As SqlParameter()) As Integer
            If commandParameters Is Nothing Then Throw New ArgumentNullException("commandParameters")
            If String.IsNullOrEmpty(connectionString) Then Throw New ArgumentNullException("connectionString")

            Using connection = New SqlConnection(connectionString)
                connection.Open()
                Return ExecuteNonQuery(connection, commandType, commandText, commandParameters)
            End Using
        End Function

        Function ExecuteNonQuery(ByVal connectionString As String, ByVal spName As String, ParamArray parameterValues As Object()) As Integer
            If String.IsNullOrEmpty(connectionString) Then Throw New ArgumentNullException("connectionString")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If (parameterValues IsNot Nothing) AndAlso (parameterValues.Length > 0) Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteNonQuery(ByVal connection As SqlConnection, ByVal commandType As CommandType, ByVal commandText As String) As Integer
            Return ExecuteNonQuery(connection, commandType, commandText, Nothing)
        End Function

        Function ExecuteNonQuery(ByVal connection As SqlConnection, ByVal commandType As CommandType, ByVal commandText As String, ParamArray commandParameters As SqlParameter()) As Integer
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            If commandParameters Is Nothing Then Throw New ArgumentNullException("commandParameters")
            Dim cmd = New SqlCommand()
            Dim mustCloseConnection As Boolean = False
            PrepareCommand(cmd, connection, Nothing, commandType, commandText, commandParameters, mustCloseConnection)
            Dim retval As Integer = cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            If mustCloseConnection Then connection.Close()
            Return retval
        End Function

        Function ExecuteNonQuery(ByVal connection As SqlConnection, ByVal spName As String, ParamArray parameterValues As Object()) As Integer
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If (parameterValues IsNot Nothing) AndAlso (parameterValues.Length > 0) Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteNonQuery(ByVal transaction As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String) As Integer
            Return ExecuteNonQuery(transaction, commandType, commandText, Nothing)
        End Function

        Function ExecuteNonQuery(ByVal transaction As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String, ParamArray commandParameters As SqlParameter()) As Integer
            If transaction Is Nothing Then Throw New ArgumentNullException("transaction")
            If commandParameters Is Nothing Then Throw New ArgumentNullException("commandParameters")
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            Dim cmd = New SqlCommand()
            Dim mustCloseConnection As Boolean = False
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)
            Dim retval As Integer = cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            Return retval
        End Function

        Function ExecuteNonQuery(ByVal transaction As SqlTransaction, ByVal spName As String, ParamArray parameterValues As Object()) As Integer
            If transaction Is Nothing Then Throw New ArgumentNullException("transaction")
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If (parameterValues IsNot Nothing) AndAlso (parameterValues.Length > 0) Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteDataset(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String) As DataSet
            Return ExecuteDataset(connectionString, commandType, commandText, Nothing)
        End Function

        Function ExecuteDataset(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ParamArray commandParameters As SqlParameter()) As DataSet
            If commandParameters Is Nothing Then Throw New ArgumentNullException("commandParameters")
            If String.IsNullOrEmpty(connectionString) Then Throw New ArgumentNullException("connectionString")

            Using connection = New SqlConnection(connectionString)
                connection.Open()
                Return ExecuteDataset(connection, commandType, commandText, commandParameters)
            End Using
        End Function

        Function ExecuteDataset(ByVal connectionString As String, ByVal spName As String, ParamArray parameterValues As Object()) As DataSet
            If String.IsNullOrEmpty(connectionString) Then Throw New ArgumentNullException("connectionString")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If (parameterValues IsNot Nothing) AndAlso (parameterValues.Length > 0) Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteDataset(ByVal connection As SqlConnection, ByVal commandType As CommandType, ByVal commandText As String) As DataSet
            Return ExecuteDataset(connection, commandType, commandText, Nothing)
        End Function

        Function ExecuteDataset(ByVal connection As SqlConnection, ByVal commandType As CommandType, ByVal commandText As String, ParamArray commandParameters As SqlParameter()) As DataSet
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            If commandParameters Is Nothing Then Throw New ArgumentNullException("commandParameters")
            Dim cmd = New SqlCommand()
            Dim mustCloseConnection As Boolean = False
            PrepareCommand(cmd, connection, Nothing, commandType, commandText, commandParameters, mustCloseConnection)

            Using da = New SqlDataAdapter(cmd)
                Dim ds = New DataSet()
                da.Fill(ds)
                cmd.Parameters.Clear()
                If mustCloseConnection Then connection.Close()
                Return ds
            End Using
        End Function

        Function ExecuteDataset(ByVal connection As SqlConnection, ByVal spName As String, ParamArray parameterValues As Object()) As DataSet
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If (parameterValues IsNot Nothing) AndAlso (parameterValues.Length > 0) Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteDataset(connection, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteDataset(ByVal transaction As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String) As DataSet
            Return ExecuteDataset(transaction, commandType, commandText, Nothing)
        End Function

        Function ExecuteDataset(ByVal transaction As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String, ParamArray commandParameters As SqlParameter()) As DataSet
            If transaction Is Nothing Then Throw New ArgumentNullException("transaction")
            If commandParameters Is Nothing Then Throw New ArgumentNullException("commandParameters")
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            Dim cmd = New SqlCommand()
            Dim mustCloseConnection As Boolean = False
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)

            Using da = New SqlDataAdapter(cmd)
                Dim ds = New DataSet()
                da.Fill(ds)
                cmd.Parameters.Clear()
                Return ds
            End Using
        End Function

        Function ExecuteDataset(ByVal transaction As SqlTransaction, ByVal spName As String, ParamArray parameterValues As Object()) As DataSet
            If transaction Is Nothing Then Throw New ArgumentNullException("transaction")
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If (parameterValues IsNot Nothing) AndAlso (parameterValues.Length > 0) Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteDataset(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function

    Private Function ExecuteReader(ByVal connection As SqlConnection, ByVal transaction As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String, ByVal commandParameters As SqlParameter(), ByVal connectionOwnership As SqlConnectionOwnership) As SqlDataReader
        If connection Is Nothing Then Throw New ArgumentNullException("connection")
        Dim mustCloseConnection As Boolean = False
        Dim cmd = New SqlCommand()

        Try
            PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)
            Dim dataReader = If(connectionOwnership = SqlConnectionOwnership.External, cmd.ExecuteReader(), cmd.ExecuteReader(CommandBehavior.CloseConnection))
            Dim canClear As Boolean = True

            For Each commandParameter As SqlParameter In cmd.Parameters.Cast(Of SqlParameter)().Where(Function(commandParameterTemp) commandParameterTemp.Direction <> ParameterDirection.Input)
                canClear = False
            Next

            If canClear Then
                cmd.Parameters.Clear()
            End If

            Return dataReader
        Catch
            If mustCloseConnection Then connection.Close()
            Throw
        End Try
    End Function
    Function ExecuteReader(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String) As SqlDataReader
            Return ExecuteReader(connectionString, commandType, commandText, Nothing)
        End Function

        Function ExecuteReader(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ParamArray commandParameters As SqlParameter()) As SqlDataReader
            If connectionString Is Nothing OrElse connectionString.Length = 0 Then Throw New ArgumentNullException("connectionString")
            Dim connection As SqlConnection = Nothing

            Try
                connection = New SqlConnection(connectionString)
                connection.Open()
                Return ExecuteReader(connection, Nothing, commandType, commandText, commandParameters, SqlConnectionOwnership.Internal)
            Catch
                If connection IsNot Nothing Then connection.Close()
                Throw
            End Try
        End Function

        Function ExecuteReader(ByVal connectionString As String, ByVal spName As String, ParamArray parameterValues As Object()) As SqlDataReader
            If String.IsNullOrEmpty(connectionString) Then Throw New ArgumentNullException("connectionString")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If (parameterValues IsNot Nothing) AndAlso (parameterValues.Length > 0) Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteReader(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteReader(ByVal connection As SqlConnection, ByVal commandType As CommandType, ByVal commandText As String) As SqlDataReader
            Return ExecuteReader(connection, commandType, commandText, Nothing)
        End Function

        Function ExecuteReader(ByVal connection As SqlConnection, ByVal commandType As CommandType, ByVal commandText As String, ParamArray commandParameters As SqlParameter()) As SqlDataReader
            Return ExecuteReader(connection, Nothing, commandType, commandText, commandParameters, SqlConnectionOwnership.External)
        End Function

        Function ExecuteReader(ByVal connection As SqlConnection, ByVal spName As String, ParamArray parameterValues As Object()) As SqlDataReader
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If (parameterValues IsNot Nothing) AndAlso (parameterValues.Length > 0) Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteReader(connection, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteReader(ByVal transaction As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String) As SqlDataReader
            Return ExecuteReader(transaction, commandType, commandText, Nothing)
        End Function

        Function ExecuteReader(ByVal transaction As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String, ParamArray commandParameters As SqlParameter()) As SqlDataReader
            If transaction Is Nothing Then Throw New ArgumentNullException("transaction")
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            Return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, SqlConnectionOwnership.External)
        End Function

        Function ExecuteReader(ByVal transaction As SqlTransaction, ByVal spName As String, ParamArray parameterValues As Object()) As SqlDataReader
            If transaction Is Nothing Then Throw New ArgumentNullException("transaction")
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If (parameterValues IsNot Nothing) AndAlso (parameterValues.Length > 0) Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteReader(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function

        Private Enum SqlConnectionOwnership
            Internal
            External
        End Enum

        Function ExecuteScalar(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String) As Object
            Return ExecuteScalar(connectionString, commandType, commandText, Nothing)
        End Function

        Function ExecuteScalar(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ParamArray commandParameters As SqlParameter()) As Object
            If String.IsNullOrEmpty(connectionString) Then Throw New ArgumentNullException("connectionString")

            Using connection = New SqlConnection(connectionString)

                Try
                    connection.Open()
                Catch e As Exception
                End Try

                Return ExecuteScalar(connection, commandType, commandText, commandParameters)
            End Using
        End Function

        Function ExecuteScalar(ByVal connectionString As String, ByVal spName As String, ParamArray parameterValues As Object()) As Object
            If String.IsNullOrEmpty(connectionString) Then Throw New ArgumentNullException("connectionString")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If (parameterValues IsNot Nothing) AndAlso (parameterValues.Length > 0) Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteScalar(ByVal connection As SqlConnection, ByVal commandType As CommandType, ByVal commandText As String) As Object
            Return ExecuteScalar(connection, commandType, commandText, Nothing)
        End Function

        Function ExecuteScalar(ByVal connection As SqlConnection, ByVal commandType As CommandType, ByVal commandText As String, ParamArray commandParameters As SqlParameter()) As Object
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            Dim cmd = New SqlCommand()
            Dim mustCloseConnection As Boolean = False
            PrepareCommand(cmd, connection, Nothing, commandType, commandText, commandParameters, mustCloseConnection)
            Dim retval As Object = cmd.ExecuteScalar()
            cmd.Parameters.Clear()
            If mustCloseConnection Then connection.Close()
            Return retval
        End Function

        Function ExecuteScalar(ByVal connection As SqlConnection, ByVal spName As String, ParamArray parameterValues As Object()) As Object
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If (parameterValues IsNot Nothing) AndAlso (parameterValues.Length > 0) Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteScalar(connection, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteScalar(ByVal transaction As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String) As Object
            Return ExecuteScalar(transaction, commandType, commandText, Nothing)
        End Function

        Function ExecuteScalar(ByVal transaction As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String, ParamArray commandParameters As SqlParameter()) As Object
            If transaction Is Nothing Then Throw New ArgumentNullException("transaction")
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            Dim cmd = New SqlCommand()
            Dim mustCloseConnection As Boolean = False
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)
            Dim retval As Object = cmd.ExecuteScalar()
            cmd.Parameters.Clear()
            Return retval
        End Function

        Function ExecuteScalar(ByVal transaction As SqlTransaction, ByVal spName As String, ParamArray parameterValues As Object()) As Object
            If transaction Is Nothing Then Throw New ArgumentNullException("transaction")
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If spName Is Nothing OrElse spName.Length = 0 Then Throw New ArgumentNullException("spName")

            If (parameterValues IsNot Nothing) AndAlso (parameterValues.Length > 0) Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteScalar(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteXmlReader(ByVal connection As SqlConnection, ByVal commandType As CommandType, ByVal commandText As String) As XmlReader
            Return ExecuteXmlReader(connection, commandType, commandText, Nothing)
        End Function

        Function ExecuteXmlReader(ByVal connection As SqlConnection, ByVal commandType As CommandType, ByVal commandText As String, ParamArray commandParameters As SqlParameter()) As XmlReader
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            Dim mustCloseConnection As Boolean = False
            Dim cmd = New SqlCommand()

            Try
                PrepareCommand(cmd, connection, Nothing, commandType, commandText, commandParameters, mustCloseConnection)
                Dim retval As XmlReader = cmd.ExecuteXmlReader()
                cmd.Parameters.Clear()
                Return retval
            Catch
                If mustCloseConnection Then connection.Close()
                Throw
            End Try
        End Function

        Function ExecuteXmlReader(ByVal connection As SqlConnection, ByVal spName As String, ParamArray parameterValues As Object()) As XmlReader
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If (parameterValues IsNot Nothing) AndAlso (parameterValues.Length > 0) Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteXmlReader(ByVal transaction As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String) As XmlReader
            Return ExecuteXmlReader(transaction, commandType, commandText, Nothing)
        End Function

        Function ExecuteXmlReader(ByVal transaction As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String, ParamArray commandParameters As SqlParameter()) As XmlReader
            If transaction Is Nothing Then Throw New ArgumentNullException("transaction")
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            Dim cmd = New SqlCommand()
            Dim mustCloseConnection As Boolean = False
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)
            Dim retval As XmlReader = cmd.ExecuteXmlReader()
            cmd.Parameters.Clear()
            Return retval
        End Function

        Function ExecuteXmlReader(ByVal transaction As SqlTransaction, ByVal spName As String, ParamArray parameterValues As Object()) As XmlReader
            If transaction Is Nothing Then Throw New ArgumentNullException("transaction")
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If spName Is Nothing OrElse spName.Length = 0 Then Throw New ArgumentNullException("spName")

            If (parameterValues IsNot Nothing) AndAlso (parameterValues.Length > 0) Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function

        Sub FillDataset(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames As String())
            If String.IsNullOrEmpty(connectionString) Then Throw New ArgumentNullException("connectionString")
            If dataSet Is Nothing Then Throw New ArgumentNullException("dataSet")

            Using connection = New SqlConnection(connectionString)
                connection.Open()
                FillDataset(connection, commandType, commandText, dataSet, tableNames)
            End Using
        End Sub

        Sub FillDataset(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames As String(), ParamArray commandParameters As SqlParameter())
            If String.IsNullOrEmpty(connectionString) Then Throw New ArgumentNullException("connectionString")
            If dataSet Is Nothing Then Throw New ArgumentNullException("dataSet")

            Using connection = New SqlConnection(connectionString)
                connection.Open()
                FillDataset(connection, commandType, commandText, dataSet, tableNames, commandParameters)
            End Using
        End Sub

        Sub FillDataset(ByVal connectionString As String, ByVal spName As String, ByVal dataSet As DataSet, ByVal tableNames As String(), ParamArray parameterValues As Object())
            If String.IsNullOrEmpty(connectionString) Then Throw New ArgumentNullException("connectionString")
            If dataSet Is Nothing Then Throw New ArgumentNullException("dataSet")

            Using connection = New SqlConnection(connectionString)
                connection.Open()
                FillDataset(connection, spName, dataSet, tableNames, parameterValues)
            End Using
        End Sub

        Sub FillDataset(ByVal connection As SqlConnection, ByVal commandType As CommandType, ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames As String())
            FillDataset(connection, commandType, commandText, dataSet, tableNames, Nothing)
        End Sub

        Sub FillDataset(ByVal connection As SqlConnection, ByVal commandType As CommandType, ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames As String(), ParamArray commandParameters As SqlParameter())
            If commandParameters Is Nothing Then Throw New ArgumentNullException("commandParameters")
            FillDataset(connection, Nothing, commandType, commandText, dataSet, tableNames, commandParameters)
        End Sub

        Sub FillDataset(ByVal connection As SqlConnection, ByVal spName As String, ByVal dataSet As DataSet, ByVal tableNames As String(), ParamArray parameterValues As Object())
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            If dataSet Is Nothing Then Throw New ArgumentNullException("dataSet")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If (parameterValues IsNot Nothing) AndAlso (parameterValues.Length > 0) Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters)
            Else
                FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames)
            End If
        End Sub

        Sub FillDataset(ByVal transaction As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames As String())
            FillDataset(transaction, commandType, commandText, dataSet, tableNames, Nothing)
        End Sub

        Sub FillDataset(ByVal transaction As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames As String(), ParamArray commandParameters As SqlParameter())
            If commandParameters Is Nothing Then Throw New ArgumentNullException("commandParameters")
            FillDataset(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames, commandParameters)
        End Sub

        Sub FillDataset(ByVal transaction As SqlTransaction, ByVal spName As String, ByVal dataSet As DataSet, ByVal tableNames As String(), ParamArray parameterValues As Object())
            If transaction Is Nothing Then Throw New ArgumentNullException("transaction")
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If dataSet Is Nothing Then Throw New ArgumentNullException("dataSet")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If (parameterValues IsNot Nothing) AndAlso (parameterValues.Length > 0) Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters)
            Else
                FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames)
            End If
        End Sub

        Private Sub FillDataset(ByVal connection As SqlConnection, ByVal transaction As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames As String(), ParamArray commandParameters As SqlParameter())
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            If dataSet Is Nothing Then Throw New ArgumentNullException("dataSet")
            Dim command = New SqlCommand()
            Dim mustCloseConnection As Boolean = False
            PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)

            Using dataAdapter = New SqlDataAdapter(command)

                If tableNames IsNot Nothing AndAlso tableNames.Length > 0 Then
                    Dim tableName As String = "Table"

                    For index As Integer = 0 To tableNames.Length - 1
                        If tableNames(index) Is Nothing OrElse tableNames(index).Length = 0 Then Throw New ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames")
                        dataAdapter.TableMappings.Add(tableName, tableNames(index))
                        tableName += (index + 1).ToString()
                    Next
                End If

                dataAdapter.Fill(dataSet)
                command.Parameters.Clear()
            End Using

            If mustCloseConnection Then connection.Close()
        End Sub

        Sub UpdateDataset(ByVal insertCommand As SqlCommand, ByVal deleteCommand As SqlCommand, ByVal updateCommand As SqlCommand, ByVal dataSet As DataSet, ByVal tableName As String)
            If insertCommand Is Nothing Then Throw New ArgumentNullException("insertCommand")
            If deleteCommand Is Nothing Then Throw New ArgumentNullException("deleteCommand")
            If updateCommand Is Nothing Then Throw New ArgumentNullException("updateCommand")
            If String.IsNullOrEmpty(tableName) Then Throw New ArgumentNullException("tableName")

            Using dataAdapter = New SqlDataAdapter()
                dataAdapter.UpdateCommand = updateCommand
                dataAdapter.InsertCommand = insertCommand
                dataAdapter.DeleteCommand = deleteCommand
                dataAdapter.Update(dataSet, tableName)
                dataSet.AcceptChanges()
            End Using
        End Sub

        Function CreateCommand(ByVal connection As SqlConnection, ByVal spName As String, ParamArray sourceColumns As String()) As SqlCommand
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")
            Dim cmd = New SqlCommand(spName, connection) With {
                .CommandType = CommandType.StoredProcedure
            }

            If (sourceColumns IsNot Nothing) AndAlso (sourceColumns.Length > 0) Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connection, spName)

                For index As Integer = 0 To sourceColumns.Length - 1
                    commandParameters(index).SourceColumn = sourceColumns(index)
                Next

                AttachParameters(cmd, commandParameters)
            End If

            Return cmd
        End Function

        Function ExecuteNonQueryTypedParams(ByVal connectionString As String, ByVal spName As String, ByVal dataRow As DataRow) As Integer
            If String.IsNullOrEmpty(connectionString) Then Throw New ArgumentNullException("connectionString")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If dataRow IsNot Nothing AndAlso dataRow.ItemArray.Length > 0 Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)
                AssignParameterValues(commandParameters, dataRow)
                Return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteNonQueryTypedParams(ByVal connection As SqlConnection, ByVal spName As String, ByVal dataRow As DataRow) As Integer
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If dataRow IsNot Nothing AndAlso dataRow.ItemArray.Length > 0 Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                Return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteNonQueryTypedParams(ByVal transaction As SqlTransaction, ByVal spName As String, ByVal dataRow As DataRow) As Integer
            If transaction Is Nothing Then Throw New ArgumentNullException("transaction")
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If dataRow IsNot Nothing AndAlso dataRow.ItemArray.Length > 0 Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                Return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteDatasetTypedParams(ByVal connectionString As String, ByVal spName As String, ByVal dataRow As DataRow) As DataSet
            If String.IsNullOrEmpty(connectionString) Then Throw New ArgumentNullException("connectionString")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If dataRow IsNot Nothing AndAlso dataRow.ItemArray.Length > 0 Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)
                AssignParameterValues(commandParameters, dataRow)
                Return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteDatasetTypedParams(ByVal connection As SqlConnection, ByVal spName As String, ByVal dataRow As DataRow) As DataSet
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If dataRow IsNot Nothing AndAlso dataRow.ItemArray.Length > 0 Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                Return ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteDataset(connection, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteDatasetTypedParams(ByVal transaction As SqlTransaction, ByVal spName As String, ByVal dataRow As DataRow) As DataSet
            If transaction Is Nothing Then Throw New ArgumentNullException("transaction")
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If dataRow IsNot Nothing AndAlso dataRow.ItemArray.Length > 0 Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                Return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteDataset(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteReaderTypedParams(ByVal connectionString As String, ByVal spName As String, ByVal dataRow As DataRow) As SqlDataReader
            If String.IsNullOrEmpty(connectionString) Then Throw New ArgumentNullException("connectionString")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If dataRow IsNot Nothing AndAlso dataRow.ItemArray.Length > 0 Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)
                AssignParameterValues(commandParameters, dataRow)
                Return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteReader(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteReaderTypedParams(ByVal connection As SqlConnection, ByVal spName As String, ByVal dataRow As DataRow) As SqlDataReader
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If dataRow IsNot Nothing AndAlso dataRow.ItemArray.Length > 0 Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                Return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteReader(connection, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteReaderTypedParams(ByVal transaction As SqlTransaction, ByVal spName As String, ByVal dataRow As DataRow) As SqlDataReader
            If transaction Is Nothing Then Throw New ArgumentNullException("transaction")
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If dataRow IsNot Nothing AndAlso dataRow.ItemArray.Length > 0 Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                Return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteReader(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteScalarTypedParams(ByVal connectionString As String, ByVal spName As String, ByVal dataRow As DataRow) As Object
            If String.IsNullOrEmpty(connectionString) Then Throw New ArgumentNullException("connectionString")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If dataRow IsNot Nothing AndAlso dataRow.ItemArray.Length > 0 Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)
                AssignParameterValues(commandParameters, dataRow)
                Return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteScalarTypedParams(ByVal connection As SqlConnection, ByVal spName As String, ByVal dataRow As DataRow) As Object
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If dataRow IsNot Nothing AndAlso dataRow.ItemArray.Length > 0 Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                Return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteScalar(connection, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteScalarTypedParams(ByVal transaction As SqlTransaction, ByVal spName As String, ByVal dataRow As DataRow) As Object
            If transaction Is Nothing Then Throw New ArgumentNullException("transaction")
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If dataRow IsNot Nothing AndAlso dataRow.ItemArray.Length > 0 Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                Return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteScalar(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteXmlReaderTypedParams(ByVal connection As SqlConnection, ByVal spName As String, ByVal dataRow As DataRow) As XmlReader
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If dataRow IsNot Nothing AndAlso dataRow.ItemArray.Length > 0 Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                Return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName)
            End If
        End Function

        Function ExecuteXmlReaderTypedParams(ByVal transaction As SqlTransaction, ByVal spName As String, ByVal dataRow As DataRow) As XmlReader
            If transaction Is Nothing Then Throw New ArgumentNullException("transaction")
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

            If dataRow IsNot Nothing AndAlso dataRow.ItemArray.Length > 0 Then
                Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                Return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function
    End Module

