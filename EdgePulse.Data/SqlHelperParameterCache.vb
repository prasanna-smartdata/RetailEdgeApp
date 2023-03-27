Imports System.Data
Imports System.Data.SqlClient

Module SqlHelperParameterCache
    Private ReadOnly ParamCache As Hashtable = Hashtable.Synchronized(New Hashtable())

    Private Function DiscoverSpParameterSet(ByVal connection As SqlConnection, ByVal spName As String, ByVal includeReturnValueParameter As Boolean) As SqlParameter()
        If connection Is Nothing Then Throw New ArgumentNullException("connection")
        If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")
        Dim cmd = New SqlCommand(spName, connection) With {
            .CommandType = CommandType.StoredProcedure
        }
        connection.Open()
        SqlCommandBuilder.DeriveParameters(cmd)
        connection.Close()

        If Not includeReturnValueParameter Then
            cmd.Parameters.RemoveAt(0)
        End If

        Dim discoveredParameters = New SqlParameter(cmd.Parameters.Count - 1) {}
        cmd.Parameters.CopyTo(discoveredParameters, 0)

        For Each discoveredParameter As SqlParameter In discoveredParameters
            discoveredParameter.Value = DBNull.Value
        Next

        Return discoveredParameters
    End Function

    Private Function CloneParameters(ByVal originalParameters As SqlParameter()) As SqlParameter()
        Dim clonedParameters = New SqlParameter(originalParameters.Length - 1) {}
        Dim i As Integer = 0, j As Integer = originalParameters.Length

        While i < j
            clonedParameters(i) = CType((CType(originalParameters(i), ICloneable)).Clone(), SqlParameter)
            i += 1
        End While

        Return clonedParameters
    End Function

    Sub CacheParameterSet(ByVal connectionString As String, ByVal commandText As String, ParamArray commandParameters As SqlParameter())
        If String.IsNullOrEmpty(connectionString) Then Throw New ArgumentNullException("connectionString")
        If String.IsNullOrEmpty(commandText) Then Throw New ArgumentNullException("commandText")
        Dim hashKey As String = String.Format("{0}:{1}", connectionString, commandText)
        ParamCache(hashKey) = commandParameters
    End Sub

    Function GetCachedParameterSet(ByVal connectionString As String, ByVal commandText As String) As SqlParameter()
        If String.IsNullOrEmpty(connectionString) Then Throw New ArgumentNullException("connectionString")
        If String.IsNullOrEmpty(commandText) Then Throw New ArgumentNullException("commandText")
        Dim hashKey As String = String.Format("{0}:{1}", connectionString, commandText)
        Dim cachedParameters = TryCast(ParamCache(hashKey), SqlParameter())

        If cachedParameters Is Nothing Then
            Return Nothing
        Else
            Return CloneParameters(cachedParameters)
        End If
    End Function

    Function GetSpParameterSet(ByVal connectionString As String, ByVal spName As String) As SqlParameter()
        Return GetSpParameterSet(connectionString, spName, False)
    End Function

    Function GetSpParameterSet(ByVal connectionString As String, ByVal spName As String, ByVal includeReturnValueParameter As Boolean) As SqlParameter()
        If String.IsNullOrEmpty(connectionString) Then Throw New ArgumentNullException("connectionString")
        If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")

        Using connection = New SqlConnection(connectionString)
            Return GetSpParameterSetInternal(connection, spName, includeReturnValueParameter)
        End Using
    End Function

    Friend Function GetSpParameterSet(ByVal connection As SqlConnection, ByVal spName As String) As SqlParameter()
        Return GetSpParameterSet(connection, spName, False)
    End Function

    Friend Function GetSpParameterSet(ByVal connection As SqlConnection, ByVal spName As String, ByVal includeReturnValueParameter As Boolean) As SqlParameter()
        If connection Is Nothing Then Throw New ArgumentNullException("connection")

        Using clonedConnection = CType((CType(connection, ICloneable)).Clone(), SqlConnection)
            Return GetSpParameterSetInternal(clonedConnection, spName, includeReturnValueParameter)
        End Using
    End Function

    Private Function GetSpParameterSetInternal(ByVal connection As SqlConnection, ByVal spName As String, ByVal includeReturnValueParameter As Boolean) As SqlParameter()
        If connection Is Nothing Then Throw New ArgumentNullException("connection")
        If String.IsNullOrEmpty(spName) Then Throw New ArgumentNullException("spName")
        Dim hashKey As String = String.Format("{0}:{1}{2}", connection.ConnectionString, spName, (If(includeReturnValueParameter, ":include ReturnValue Parameter", "")))
        Dim cachedParameters = TryCast(ParamCache(hashKey), SqlParameter())

        If cachedParameters Is Nothing Then
            Dim spParameters As SqlParameter() = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter)
            ParamCache(hashKey) = spParameters
            cachedParameters = spParameters
        End If

        Return CloneParameters(cachedParameters)
    End Function

End Module
