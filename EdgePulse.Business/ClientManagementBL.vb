Imports System.Data
Imports System.Data.SqlClient
Imports EdgePulse.Data
Imports EdgePulse.Entities
Imports EdgePulse.Infrastructure

Public Class ClientManagementBL


#Region "Add Edit Client"
    Dim clientDL As New ClientDL()
    Function GetClients() As List(Of ClientEntity)

        Try

            Return clientDL.GetClients()
        Catch ex As Exception
            Throw
        End Try

        Return Nothing
    End Function

    Function GetBuyingGroups() As List(Of BuyingGroupEntity)

        Try
            Return clientDL.GetBuyingGroups()
        Catch ex As Exception
            Throw
        End Try
    End Function

    Function GetSuperstores() As List(Of SuperstoreEntity)

        Try
            Return clientDL.GetSuperstores()
        Catch ex As Exception

        End Try
    End Function
    Function GetClientDetails(ByVal clientId As Int32) As ClientEntity

        Try
            Return clientDL.GetClientDetails(clientId)
        Catch ex As Exception
            Throw
        End Try
        Return Nothing
    End Function
    Function UpdateClient(ByVal clientId As Int32, ByVal Client As ClientEntity) As Boolean


        Try
            Return clientDL.UpdateClient(clientId, Client)

        Catch ex As Exception
            Throw
        End Try


        Return True



    End Function

#End Region


#Region "Add Edit Client-Store-Superstore"

#End Region


End Class
