Imports System.Data
Imports System.Data.SqlClient
Imports EdgePulse.Entities
Imports EdgePulse.Infrastructure

Public Class ClientDL
    Inherits RMHClientBaseDL

    'Geting the client details for the given id 
    Public Function GetClientDetails(ByVal clientId As Int16) As ClientEntity
        Dim _clientEntity As ClientEntity = Nothing
        Try
            Dim sqlParams(0) As SqlParameter
            sqlParams(0) = New SqlParameter("@ClientID", clientId)

            Dim reader As SqlDataReader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, StoredProcNames.getClientDetails, sqlParams)

            While reader.Read()

                _clientEntity = New ClientEntity()
                With _clientEntity

                    .EmailAddress = reader.Item("Email")
                    .BuyingGroupId = reader.Item("BuyingGroupId")
                    .ClientName = reader.Item("ClientName")
                    .ClientNumber = reader.Item("ClientNum")
                    .Comment = reader.Item("Comment")
                    .FakeClient = reader.Item("FakeClient")
                    .HoStoreNumber = reader.Item("HOStoreNumber")
                    .IsActiveClient = reader.Item("ActiveClient")
                    .IsDoorCounter = reader.Item("DoorCounter")
                    .IsEdgePulseEnabled = reader.Item("EdgePulseAllowed")
                    .IsKPILite = reader.Item("KPILite")
                    .IsMacroOked = reader.Item("MacroOked")
                    .IsMentorningClient = reader.Item("MentoringClient")
                    .IsSuperStoreActive = reader.Item("SuperstoreActive")
                    .Jewelsure = reader.Item("Jewelsure")
                    .LayawaysOnPickUp = reader.Item("LaybysOnPickup")
                    .NoOfSites = reader.Item("SilentManagerSites")
                    .Path = reader.Item("Where")
                    .ProactiveCallDate = reader.Item("LastAccountManagerCall")
                    .RecEmail = reader.Item("RECEmail")
                    .Results = reader.Item("Results")
                    .SqlServer = reader.Item("SQLServer")
                    .State = reader.Item("State")
                    .IsSupplierWebSystem = reader.Item("SuppSystem")
                    .SupportEmail = reader.Item("TheirSupportEmail")
                    .UseEdgeSW = reader.Item("TheEdge")
                    .AccountManagerId = If(reader.Item("AccountManager") = "Not Allocated", 0, reader.Item("AccountManager"))
                    .SalesMaximum = If(reader.IsDBNull("SalesMaximum"), 0, reader.Item("SalesMaximum"))
                    .SalesMinimum = If(reader.IsDBNull("SalesMinimum"), 0, reader.Item("SalesMinimum"))
                    .StockMaximum = If(reader.IsDBNull("StockMaximum"), 0, reader.Item("StockMaximum"))
                    .StockMinimum = If(reader.IsDBNull("StockMinimum"), 0, reader.Item("StockMinimum"))
                    .StockMaximumQty = If(reader.IsDBNull("SalesMaximumQty"), 0, reader.Item("SalesMaximumQty"))
                    .StockMinimumQty = If(reader.IsDBNull("SalesMinimumQty"), 0, reader.Item("SalesMinimumQty"))
                End With
            End While
        Catch ex As Exception

            Throw
        End Try
        Return _clientEntity
    End Function

    Public Function GetBuyingGroups() As List(Of BuyingGroupEntity)
        Dim _buyingGroups As List(Of BuyingGroupEntity) = New List(Of BuyingGroupEntity)()
        Try
            Dim reader As SqlDataReader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, StoredProcNames.getBuyingGroups)
            Dim _buyingGroup As BuyingGroupEntity = Nothing
            While reader.Read()

                _buyingGroup = New BuyingGroupEntity()
                _buyingGroup.BuyingGroupID = reader.Item("BuyingGroupId")
                _buyingGroup.BuyingGroupName = reader.Item("BuyingGroupName")

                _buyingGroups.Add(_buyingGroup)
            End While
        Catch ex As Exception

            Throw
        End Try
        Return _buyingGroups
    End Function

    Public Function GetSuperstores() As List(Of SuperstoreEntity)
        Dim _superStores As List(Of SuperstoreEntity) = New List(Of SuperstoreEntity)()
        Try
            Dim reader As SqlDataReader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, StoredProcNames.getSuperstores)
            Dim _superStore As SuperstoreEntity = Nothing
            While reader.Read()

                _superStore = New SuperstoreEntity()

                With _superStore
                    .ID = reader.Item("ID")
                    .GroupNum = reader.Item("GroupNum")
                    .GroupName = reader.Item("GroupName")
                    .DeptsToUse = reader.Item("DeptsToUse")
                    .Region = If(reader.IsDBNull("Region"), "", reader.Item("Region"))
                    .Status = If(reader.IsDBNull("Status"), False, reader.Item("Status"))
                End With

                _superStores.Add(_superStore)
            End While
        Catch ex As Exception

            Throw
        End Try
        Return _superStores
    End Function


    Function GetClientStores() As List(Of ClientStoreDetailsEntity)
        Dim _clientStores As New List(Of ClientStoreDetailsEntity)
        Try
            Dim reader As SqlDataReader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, StoredProcNames.GetClientStores)

            While reader.Read()

            End While

        Catch ex As Exception
            Throw
        End Try
        Return _clientStores
    End Function


    Function GetClients() As List(Of ClientEntity)
        Dim _clients As New List(Of ClientEntity)
        Try
            Dim reader As SqlDataReader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, StoredProcNames.getClients)

            While reader.Read()
                Dim _client = New ClientEntity()
                _client.ID = reader("ID")
                _client.ClientName = reader("ClientName")
                _client.ClientNumber = reader("ClientNum")
                _client.ClientDisplayName = _client.ClientNumber + " ---- " + _client.ClientName
                _clients.Add(_client)
            End While

        Catch ex As Exception

        End Try
        Return _clients
    End Function

    Function UpdateClientStore(ByVal clientId As Int32, ByVal client As ClientEntity) As Boolean

        Try

            Dim sqlParams(23) As SqlParameter
            sqlParams(0) = New SqlParameter("@ClientId", clientId)
            sqlParams(1) = New SqlParameter("@ClientName", client.ClientName)
            sqlParams(2) = New SqlParameter("@ResultsClientNum", client.ClientNumber)
            sqlParams(3) = New SqlParameter("@KPILite", client.IsKPILite)
            sqlParams(4) = New SqlParameter("@Where", client.Path)
            sqlParams(5) = New SqlParameter("@Comment", client.Comment)
            sqlParams(6) = New SqlParameter("@Email", client.EmailAddress)
            sqlParams(7) = New SqlParameter("@ActiveClient", client.IsActiveClient)
            sqlParams(8) = New SqlParameter("@DoorCounter", client.IsDoorCounter)
            sqlParams(9) = New SqlParameter("@Results", client.Results)
            sqlParams(10) = New SqlParameter("@MacroOked", client.IsMacroOked)
            sqlParams(11) = New SqlParameter("@RecEmail", client.RecEmail.ToString)
            sqlParams(12) = New SqlParameter("@SqlServer", client.SqlServer)
            sqlParams(13) = New SqlParameter("@Jewelsure", client.Jewelsure)
            sqlParams(14) = New SqlParameter("@HoStoreNumber", client.HoStoreNumber)
            sqlParams(15) = New SqlParameter("@EdgePulseAllowed", client.IsEdgePulseEnabled)
            sqlParams(16) = New SqlParameter("@State", client.State)
            sqlParams(17) = New SqlParameter("@FakeClient", client.FakeClient)
            sqlParams(18) = New SqlParameter("@SalesMaximum", client.SalesMaximum)
            sqlParams(19) = New SqlParameter("@SalesMinimum", client.SalesMinimum)
            sqlParams(20) = New SqlParameter("@StockMaximum", client.StockMaximum)
            sqlParams(21) = New SqlParameter("@StockMinimum", client.StockMinimum)
            sqlParams(22) = New SqlParameter("@SalesMaximumQty", client.StockMaximumQty)
            sqlParams(23) = New SqlParameter("@SalesMinimumQty", client.StockMinimumQty)


            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "UpdateClient", sqlParams)


        Catch ex As Exception

        End Try

        Return True
    End Function


End Class
