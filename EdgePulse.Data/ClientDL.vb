Imports System.Data
Imports System.Data.SqlClient
Imports EdgePulse.Entities
Imports EdgePulse.Infrastructure

Public Class ClientDL
    Inherits ClientBaseDL

    Public Function GetClients() As List(Of ClientEntity)
        Dim _clients As List(Of ClientEntity) = New List(Of ClientEntity)()
        Try
            Dim reader As SqlDataReader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, StoredProcNames.getClients)
            Dim _clientEntity As ClientEntity = Nothing
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

                _clients.Add(_clientEntity)
            End While
        Catch ex As Exception

            Throw
        End Try
        Return _clients
    End Function

End Class
