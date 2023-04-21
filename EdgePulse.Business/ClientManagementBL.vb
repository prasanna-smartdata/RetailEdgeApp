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

    Function GetClientStores() As List(Of ClientStoreEntity)

        Try
            Return clientDL.GetClientStores()
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


    Function UpdateClient(ByVal Client As ClientEntity) As Boolean
        Try
            Return clientDL.UpdateClient(Client)

        Catch ex As Exception
            Throw
        End Try
        Return True

    End Function

    Function GetnewClientStores() As List(Of ClientStoreEntity)

        Try
            Return clientDL.getNewlyAddedstore()
        Catch ex As Exception
            Throw
        End Try
    End Function

#End Region


#Region "Add Edit Client-Store-Superstore"

    Function SaveSuperstoreClientData(ByVal clientStores As List(Of ClientStoreEntity), ByVal superStoreId As Int16) As Boolean
        Try

            For Each item As ClientStoreEntity In clientStores
                If item.IsSelected Then
                    'Add new record
                    clientDL.SaveSuperstoreClient(item.ID, superStoreId)
                Else
                    'Delete the record
                    clientDL.DeleteSuperstoreClient(item.ID, superStoreId)

                End If
            Next



        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Function UpdateorSaveSuperStore(SelectedSuperstore As SuperstoreEntity) As Boolean
        Try
            clientDL.UpdateSuperStore(SelectedSuperstore)


        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Function GetSuperstoreClients(ByVal SuperstoreID As Int16) As List(Of ClientStoreSuperstoreEntity)

        Try
            Return clientDL.GetSuperstoreClients(SuperstoreID)
        Catch ex As Exception
            Throw
        End Try
    End Function
#End Region


End Class
