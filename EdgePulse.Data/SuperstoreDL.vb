Imports EdgePulse.Entities
Imports EdgePulse.Infrastructure
Imports System.Data
Imports System.Data.SqlClient

Public Class SuperstoreDL
    Inherits RMHClientBaseDL
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

                End With

                _superStores.Add(_superStore)
            End While
        Catch ex As Exception

            Throw
        End Try
        Return _superStores
    End Function
    Public Function GetSuperstoreClientStoresDetails(ByVal superstoreId As Int16) As List(Of Report_SSClientStoresDetailsEntity)
        Dim _ssClientStoreDetailsList As List(Of Report_SSClientStoresDetailsEntity) = New List(Of Report_SSClientStoresDetailsEntity)()
        Try

            Dim sqlParams(1) As SqlParameter
            sqlParams(0) = New SqlParameter("@SuperstoreId", SqlDbType.Int)
            sqlParams(0).Value = superstoreId

            Dim reader As SqlDataReader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, StoredProcNames.GetSuperstoreClientStoresDetails)
            Dim _ssClientStoreDetails As Report_SSClientStoresDetailsEntity = Nothing

            While reader.Read()

                _ssClientStoreDetails = New Report_SSClientStoresDetailsEntity()

                With _ssClientStoreDetails
                    .SuperstoreGroupID = reader.Item("SuperstoreGroupID")
                    .ClientID = reader.Item("ClientID")
                    .ClientNum = reader.Item("ClientNum")
                    .StoreID = reader.Item("StoreID")
                    .StoreName = reader.Item("StoreName")
                    .DateCreated = reader.Item("DateCreated")
                End With
                _ssClientStoreDetailsList.Add(_ssClientStoreDetails)
            End While

        Catch ex As Exception
            Throw
        End Try

        Return _ssClientStoreDetailsList

    End Function
End Class
