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
    Private _orginalClientStoresCollection As New ObservableCollection(Of ClientStoreEntity)

    Private _superStoresCollection As New ObservableCollection(Of SuperstoreEntity)
    Private _clientManagementBL As New ClientManagementBL()
#End Region


#Region "Properties"

    Public Property ItemsView As CollectionView
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
    Public Property OrignalClientStoresCollection() As ObservableCollection(Of ClientStoreEntity)
        Get
            Return _orginalClientStoresCollection
        End Get
        Set(ByVal value As ObservableCollection(Of ClientStoreEntity))
            _orginalClientStoresCollection = value
            OnPropertyChanged("OrignalClientStoresCollection")

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

    Private _saveCommand As ICommand


    Public ReadOnly Property SaveCommand As DelegateCommand
        Get
            Return If(Me._saveCommand, New DelegateCommand(Of ClientEntity)(AddressOf SaveClientStore))
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
    Private Sub SaveClientStore(obj As ClientEntity)
        Try

        Catch ex As Exception

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

            'Saving the clientstores for future reference
            For Each item As ClientStoreEntity In _clientStoresCollection
                _orginalClientStoresCollection.Add(item.Clone())
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Function Filter(ByVal client As ClientStoreEntity) As Boolean
        Return SearchKey Is Nothing OrElse client.ClientName.IndexOf(SearchKey, StringComparison.OrdinalIgnoreCase) <> -1 OrElse client.ClientNumber.IndexOf(SearchKey, StringComparison.OrdinalIgnoreCase) <> -1
        'OrElse dragon.RomajiName.IndexOf(Search, StringComparison.OrdinalIgnoreCase) <> -1
    End Function



#End Region

#Region "Consctructors"

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
