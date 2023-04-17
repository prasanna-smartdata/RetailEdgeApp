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

    Private _clientsCollection As New ObservableCollection(Of ClientEntity)
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
    Public Property ClientsCollection() As ObservableCollection(Of ClientEntity)
        Get
            Return _clientsCollection
        End Get
        Set(ByVal value As ObservableCollection(Of ClientEntity))
            _clientsCollection = value
            OnPropertyChanged("ClientsCollection")

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


    Public Sub GetSuperStores()
        Try
            SuperstoresCollection = New ObservableCollection(Of SuperstoreEntity)(_clientManagementBL.GetSuperstores())
        Catch ex As Exception

        End Try

    End Sub

    Public Sub GetClients()

        Try

            _clientsCollection = New ObservableCollection(Of ClientEntity)(_clientManagementBL.GetClients())

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Function Filter(ByVal client As ClientEntity) As Boolean
        Return SearchKey Is Nothing OrElse client.ClientName.IndexOf(SearchKey, StringComparison.OrdinalIgnoreCase) <> -1
        'OrElse dragon.OriginalName.IndexOf(SearchKey, StringComparison.OrdinalIgnoreCase) <> -1 OrElse dragon.RomajiName.IndexOf(Search, StringComparison.OrdinalIgnoreCase) <> -1
    End Function

#End Region

#Region "Wizards"

    Public Sub New()

        Try
            GetSuperStores()
            GetClients()
            ItemsView = CollectionViewSource.GetDefaultView(ClientsCollection)
            Dim searchFilter As Predicate(Of Object) = AddressOf Filter
            ItemsView.Filter = searchFilter
        Catch ex As Exception
            Throw
        End Try

    End Sub
#End Region


End Class
