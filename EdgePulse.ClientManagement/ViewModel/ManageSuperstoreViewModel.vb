Imports System.Collections.ObjectModel
Imports EdgePulse.Infrastructure
Imports Prism.Commands
Imports EdgePulse.Entities
Imports EdgePulse.Business
Imports System.ComponentModel

Public Class ManageSuperstoreViewModel
    Inherits ViewModelBase


#Region "Fields"

    Private ReadOnly _exitClickCommand As DelegateCommand = Nothing

    Private _clientStoresCollection As New ObservableCollection(Of ClientStoreEntity)
    Private _newClientStoresCollection As New ObservableCollection(Of ClientStoreEntity)
    Private _selectedClientStoresCollection As New ObservableCollection(Of ClientStoreSuperstoreEntity)

    Private _superStoresCollection As New ObservableCollection(Of SuperstoreEntity)
    Private _clientManagementBL As New ClientManagementBL()
#End Region


#Region "Properties"

    Public _itemsView As CollectionView
    Public Property ItemsView() As CollectionView
        Get
            Return _itemsView
        End Get
        Set(ByVal value As CollectionView)
            _itemsView = value
            OnPropertyChanged("ItemsView")

        End Set
    End Property
    'Public ReadOnly Property ItemsView As ICollectionView
    '    Get
    '        Return CollectionViewSource.GetDefaultView(ClientsCollection)
    '    End Get

    'End Property
    Public Property ClientStoresCollection() As ObservableCollection(Of ClientStoreEntity)
        Get
            Return _clientStoresCollection
        End Get
        Set(ByVal value As ObservableCollection(Of ClientStoreEntity))
            _clientStoresCollection = value
            OnPropertyChanged("ClientStoresCollection")

        End Set
    End Property

    'Save the orginal collection for tracking the changes
    Public Property SelectedClientStoresCollection() As ObservableCollection(Of ClientStoreSuperstoreEntity)
        Get
            Return _selectedClientStoresCollection
        End Get
        Set(ByVal value As ObservableCollection(Of ClientStoreSuperstoreEntity))
            _selectedClientStoresCollection = value
            OnPropertyChanged("SelectedClientStoresCollection")

        End Set
    End Property

    Public Property SuperstoresCollection() As ObservableCollection(Of SuperstoreEntity)
        Get
            Return _superStoresCollection
        End Get
        Set(ByVal value As ObservableCollection(Of SuperstoreEntity))
            _superStoresCollection = value
            OnPropertyChanged("SuperstoresCollection")

        End Set
    End Property

    Private _selectedSuperstore As SuperstoreEntity
    Public Property SelectedSuperstore() As SuperstoreEntity
        Get
            Return _selectedSuperstore
        End Get
        Set(ByVal value As SuperstoreEntity)
            _selectedSuperstore = value

            OnPropertyChanged("SelectedSuperstore")
            GetSuperstoreClients()
        End Set
    End Property

    Private _searchKey As String
    Public Property SearchKey() As String
        Get
            Return _searchKey
        End Get
        Set(ByVal value As String)
            _searchKey = value
            OnPropertyChanged("SearchKey")
            ItemsView.Refresh()
        End Set
    End Property

    Private _saveCommand As DelegateCommand
    Private _updateCommand As DelegateCommand

    Public ReadOnly Property SaveCommand As DelegateCommand
        Get
            Return If(Me._saveCommand, New DelegateCommand(AddressOf SaveClientStore))
        End Get
    End Property

    Private _resetCommand As DelegateCommand
    Public ReadOnly Property ResetCommand() As DelegateCommand
        Get
            Return If(Me._resetCommand, New DelegateCommand(AddressOf ResetSuperstoreClients))
        End Get
    End Property



    Public ReadOnly Property UpdateSuperStoreCommand As DelegateCommand
        Get
            Return If(Me._updateCommand, New DelegateCommand(AddressOf UpdateSuperStore))
        End Get
    End Property
#End Region

#Region "Methods"

    'Private Async Function GetClients() As Task(Of ObservableCollection(Of ClientEntity))

    '    If ClientsCollection IsNot Nothing Then
    '        Dim resultItems As IEnumerable(Of ClientEntity) = Await LoadMainTableDataAsync()
    '        Me.ClientsCollection = New ObservableCollection(Of ClientEntity)(resultItems)

    '    End If

    'End Function


    'Public Function LoadMainTableDataAsync() As Task(Of ObservableCollection(Of ClientEntity))
    '    Return Task.Run(Function()

    '                        Return New ObservableCollection(Of ClientEntity)(_clientManagementBL.GetClients())
    '                    End Function)
    'End Function

    Private Sub ResetSuperstoreClients()
        Try
            GetSuperstoreClients()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SaveClientStore()
        Try
            Dim finalCollection = New ObservableCollection(Of ClientStoreEntity)(ClientStoresCollection.Where(Function(x) x.IsSelected = True))


            If Not SelectedClientStoresCollection.Count.Equals(0) Then

                Dim finalCollectionTemp = New List(Of ClientStoreEntity)(ClientStoresCollection.Where(Function(x) x.IsSelected = True))

                For Each item As ClientStoreSuperstoreEntity In SelectedClientStoresCollection
                    Dim result = finalCollectionTemp.Exists(Function(obj As ClientStoreEntity)
                                                                Return obj.ID = item.ClientStoreId
                                                            End Function)


                    If Not result Then
                        Dim deleteClientStore As New ClientStoreEntity()

                        With deleteClientStore
                            .ID = item.ClientStoreId
                            .IsSelected = False
                        End With
                        finalCollection.Add(deleteClientStore)
                    End If
                Next

            End If


            _clientManagementBL.SaveSuperstoreClientData(finalCollection.ToList(), SelectedSuperstore.ID)

            MessageBox.Show("Successfully Saved")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub UpdateSuperStore()
        Try

            _clientManagementBL.UpdateorSaveSuperStore(SelectedSuperstore)

            MessageBox.Show("Updated Sucessfully")

        Catch ex As Exception
            Throw
        End Try



    End Sub

    Public Sub GetSuperStores()
        Try
            SuperstoresCollection = New ObservableCollection(Of SuperstoreEntity)(_clientManagementBL.GetSuperstores())
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try

    End Sub

    Public Sub GetClientStores()

        Try

            _clientStoresCollection = New ObservableCollection(Of ClientStoreEntity)(_clientManagementBL.GetClientStores())


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub GetNewClientStores()

        Try

            _newClientStoresCollection = New ObservableCollection(Of ClientStoreEntity)(_clientManagementBL.GetnewClientStores())


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Sub GetSuperstoreClients()

        Try
            If Not SelectedSuperstore Is Nothing Then
                Dim storesCollection As List(Of ClientStoreSuperstoreEntity) = _clientManagementBL.GetSuperstoreClients(SelectedSuperstore.ID)
                _selectedClientStoresCollection = New ObservableCollection(Of ClientStoreSuperstoreEntity)(storesCollection)

                'select the clients which are already selected
                UpdateClientStoreCollection()

            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub
    Private Function Filter(ByVal client As ClientStoreEntity) As Boolean
        Return SearchKey Is Nothing OrElse client.ClientName.IndexOf(SearchKey, StringComparison.OrdinalIgnoreCase) <> -1 OrElse client.ClientNumber.IndexOf(SearchKey, StringComparison.OrdinalIgnoreCase) <> -1
        'OrElse dragon.RomajiName.IndexOf(Search, StringComparison.OrdinalIgnoreCase) <> -1
    End Function



#End Region

#Region "Consctructors"

    Sub UpdateClientStoreCollection()
        Try
            For Each clientStore In ClientStoresCollection
                clientStore.IsSelected = False
            Next

            For Each item As ClientStoreEntity In ClientStoresCollection

                If (SelectedClientStoresCollection.Any(Function(x) x.ClientStoreId = item.ID)) Then
                    item.IsSelected = True
                End If
            Next
            ClientStoresCollection = New ObservableCollection(Of ClientStoreEntity)(ClientStoresCollection.OrderByDescending(Function(x) x.IsSelected))
            ItemsView = CollectionViewSource.GetDefaultView(ClientStoresCollection)
            ' SortClientStores()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Sub SortClientStores()
        Try
            Dim selectedClientStores = New ObservableCollection(Of ClientStoreEntity)(ClientStoresCollection.Where(Function(x) x.IsSelected))

            Dim nonSelectedClientStoresCollection = New ObservableCollection(Of ClientStoreEntity)(ClientStoresCollection.Where(Function(x) x.IsSelected = False))

            selectedClientStores.Union(nonSelectedClientStoresCollection)
            ClientStoresCollection.Clear()
            ClientStoresCollection = selectedClientStores

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub New()

        Try
            GetSuperStores()
            GetClientStores()


            ItemsView = CollectionViewSource.GetDefaultView(ClientStoresCollection)
            Dim searchFilter As Predicate(Of Object) = AddressOf Filter
            ItemsView.Filter = searchFilter
        Catch ex As Exception
            Throw
        End Try

    End Sub
#End Region


End Class
