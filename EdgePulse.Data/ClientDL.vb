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


            Dim reader As SqlDataReader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, StoredProcNames.getClientDetails)

            While reader.Read()

                _clientEntity = New ClientEntity()
                _clientEntity.AccountManagerId = reader.Item("AccountManager")
                _clientEntity.EmailAddress = reader.Item("Email")
                _clientEntity.BuyingGroupId = reader.Item("BuyingGroupId")
                _clientEntity.ClientName = reader.Item("ClientName")
                _clientEntity.ClientNumber = reader.Item("ClientNum")
                _clientEntity.Comment = reader.Item("Comment")
                _clientEntity.FakeClient = reader.Item("FakeClient")
                _clientEntity.HoStoreNumber = reader.Item("HOStoreNumber")
                _clientEntity.IsActiveClient = reader.Item("ActiveClient")
                _clientEntity.IsDoorCounter = reader.Item("DoorCounter")
                _clientEntity.IsEdgePulseEnabled = reader.Item("EdgePulseEnabled")
                _clientEntity.IsKPILite = reader.Item("KPILite")
                _clientEntity.IsMacroOked = reader.Item("MacroOked")
                _clientEntity.IsMentorningClient = reader.Item("MentoringClient")
                _clientEntity.IsSuperStoreActive = reader.Item("SuperstoreActive")
                _clientEntity.IsSupplierWebSystem = reader.Item("SupplierSystem")
                _clientEntity.Jewelsure = reader.Item("Jewelsure")
                _clientEntity.LayawaysOnPickUp = reader.Item("LaybuysOnPickup")
                _clientEntity.NoOfSites = reader.Item("SilentManagerSites")
                _clientEntity.Path = reader.Item("Where")
                _clientEntity.ProactiveCallDate = reader.Item("LastAccountManagerCall")
                _clientEntity.RecEmail = reader.Item("RECEmail")
                _clientEntity.Results = reader.Item("Results")
                _clientEntity.SqlServer = reader.Item("SQLServer")
                _clientEntity.State = reader.Item("State")
                _clientEntity.SupportEmail = reader.Item("TheirSupportEmail")
                _clientEntity.UseEdgeSW = reader.Item("TheEdge")

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
                _superStore.ID = reader.Item("ID")
                _superStore.GroupNum = reader.Item("GroupNum")
                _superStore.GroupName = reader.Item("GroupName")
                _superStore.DeptsToUse = reader.Item("DeptsToUse")

                _superStores.Add(_superStore)
            End While
        Catch ex As Exception

            Throw
        End Try
        Return _superStores
    End Function


End Class
