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
            Dim sqlParams(1) As SqlParameter
            sqlParams(0) = New SqlParameter("@ClientID", SqlDbType.Int)
            sqlParams(0).Value = clientId

            Dim reader As SqlDataReader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, StoredProcNames.getClientDetails, sqlParams)

            While reader.Read()

                _clientEntity = New ClientEntity()
                With _clientEntity
                    .AccountManagerId = reader.Item("AccountManager")
                    .EmailAddress = reader.Item("Email")
                    .BuyingGroupId = reader.Item("BuyingGroupId")
                    .ClientName = reader.Item("ClientName")
                    .ClientNumber = reader.Item("ClientNum")
                    .Comment = reader.Item("Comment")
                    .FakeClient = reader.Item("FakeClient")
                    .HoStoreNumber = reader.Item("HOStoreNumber")
                    .IsActiveClient = reader.Item("ActiveClient")
                    .IsDoorCounter = reader.Item("DoorCounter")
                    .IsEdgePulseEnabled = reader.Item("EdgePulseEnabled")
                    .IsKPILite = reader.Item("KPILite")
                    .IsMacroOked = reader.Item("MacroOked")
                    .IsMentorningClient = reader.Item("MentoringClient")
                    .IsSuperStoreActive = reader.Item("SuperstoreActive")
                    .IsSupplierWebSystem = reader.Item("SupplierSystem")
                    .Jewelsure = reader.Item("Jewelsure")
                    .LayawaysOnPickUp = reader.Item("LaybuysOnPickup")
                    .NoOfSites = reader.Item("SilentManagerSites")
                    .Path = reader.Item("Where")
                    .ProactiveCallDate = reader.Item("LastAccountManagerCall")
                    .RecEmail = reader.Item("RECEmail")
                    .Results = reader.Item("Results")
                    .SqlServer = reader.Item("SQLServer")
                    .State = reader.Item("State")
                    .SupportEmail = reader.Item("TheirSupportEmail")
                    .UseEdgeSW = reader.Item("TheEdge")
                    .SalesMaximum = reader.Item("SalesMaximum")
                    .SalesMinimum = reader.Item("SalesMinimum")
                    .StockMaximum = reader.Item("StockMaximum")
                    .StockMinimum = reader.Item("StockMinimum")
                    .StockMaximumQty = reader.Item("StockMaximumQty")
                    .StockMinimumQty = reader.Item("StockMinimumQty")
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
            Dim reader As SqlDataReader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, StoredProcNames.GetSuperstores)
            Dim _superStore As SuperstoreEntity = Nothing
            While reader.Read()

                _superStore = New SuperstoreEntity()

                With _superStore
                    .ID = reader.Item("ID")
                    .GroupNum = reader.Item("GroupNum")
                    .GroupName = reader.Item("GroupName")
                    .DeptsToUse = reader.Item("DeptsToUse")

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
                _client.id = reader("ID")
                _client.ClientName = reader("ClientName")
                _client.ClientNumber = reader("ClientNum")
                _client.ClientDisplayName = _client.ClientNumber + " ---- " + _client.ClientName
                _clients.Add(_client)
            End While

        Catch ex As Exception

        End Try
        Return _clients
    End Function

    Function UpdateClient(ByVal client As ClientEntity) As Boolean

        Try

            Dim sqlParams(7) As SqlParameter
            sqlParams(0) = New SqlParameter("@ClientId", client.ID, SqlDbType.Int)
            sqlParams(1) = New SqlParameter("@ClientName", client.ClientName, SqlDbType.VarChar, 100)
            sqlParams(2) = New SqlParameter("@AccountManager", client.AccountManagerId, SqlDbType.NVarChar, 50)
            sqlParams(3) = New SqlParameter("@NoOfSites", client.NoOfSites, SqlDbType.Int)
            sqlParams(4) = New SqlParameter("@Comment", client.Comment, SqlDbType.NVarChar)
            sqlParams(5) = New SqlParameter("@Path", client.Path, SqlDbType.NVarChar)
            sqlParams(6) = New SqlParameter("@BuyingGroupId", client.BuyingGroupId, SqlDbType.Int)
            sqlParams(7) = New SqlParameter("@ProactiveCallDate", client.ProactiveCallDate.ToString(), SqlDbType.Date)



            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "UpdateClientInfo", sqlParams)


        Catch ex As Exception

        End Try

        Return True
    End Function


End Class
