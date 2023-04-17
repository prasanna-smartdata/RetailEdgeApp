Imports System.Collections.ObjectModel
Imports EdgePulse.Entities
Imports EdgePulse.Infrastructure

Imports Prism.Commands
Imports System.Windows.Input
Imports EdgePulse.Business

Public Class ClientViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _editClickCommand As DelegateCommand = Nothing

    Private _exitCommand As DelegateCommand = Nothing
    Private _openSuperstoreCommand As DelegateCommand = Nothing

    Private _clientsCollection As New ObservableCollection(Of ClientEntity)
    Private _clientManagementBL As New ClientManagementBL()
    Private _client As ClientEntity
    Private _buyingGroups As New ObservableCollection(Of BuyingGroupEntity)

#End Region


#Region "Properties"

    Private _selectedClient As ClientEntity
    Public Property SelectedClient() As ClientEntity
        Get
            Return _selectedClient
        End Get
        Set(ByVal value As ClientEntity)
            _selectedClient = value

        End Set
    End Property

    Private _selectedBuyingGroup As BuyingGroupEntity
    Public Property SelectedBuyingGroup() As BuyingGroupEntity
        Get
            Return _selectedBuyingGroup
        End Get
        Set(ByVal value As BuyingGroupEntity)
            _selectedBuyingGroup = value
            OnPropertyChanged("SelectedBuyingGroup")
        End Set
    End Property

    Public Property BuyingGroups() As ObservableCollection(Of BuyingGroupEntity)
        Get
            Return _buyingGroups
        End Get
        Set(ByVal value As ObservableCollection(Of BuyingGroupEntity))
            _buyingGroups = value
            OnPropertyChanged("BuyingGroups")
        End Set
    End Property
    Public Property Client() As ClientEntity
        Get
            Return _client
        End Get
        Set(ByVal value As ClientEntity)
            _client = value
            OnPropertyChanged("Client")
        End Set
    End Property
    Private _isClientsLoaded As Boolean
    Private _loadClient As DelegateCommand(Of ClientEntity)

    Public Property IsClientsLoaded() As Boolean
        Get
            Return _isClientsLoaded
        End Get
        Set(ByVal value As Boolean)
            _isClientsLoaded = value

        End Set
    End Property

    Public Property ClientsCollection() As ObservableCollection(Of ClientEntity)
        Get
            Return _clientsCollection
        End Get
        Set(ByVal value As ObservableCollection(Of ClientEntity))
            _clientsCollection = value

        End Set
    End Property


    Public ReadOnly Property EditClickCommand As DelegateCommand
        Get
            If _editClickCommand Is Nothing Then
                _editClickCommand = New DelegateCommand(AddressOf UpdateClient)
            End If

            Return _editClickCommand

        End Get
    End Property

    Public ReadOnly Property OpenSuperstorCommand As DelegateCommand
        Get

            If _openSuperstoreCommand Is Nothing Then
                _openSuperstoreCommand = New DelegateCommand(AddressOf OpenSuperstore)
            End If

            Return _openSuperstoreCommand
        End Get
    End Property

    Public ReadOnly Property ExitCommand As ICommand
        Get

            If _exitCommand Is Nothing Then
                _exitCommand = New DelegateCommand(AddressOf Close)
            End If

            Return _exitCommand
        End Get
    End Property

    Public ReadOnly Property LoadClient As DelegateCommand
        Get
            Return If(Me._loadClient, New DelegateCommand(AddressOf LoadClientDetails))
        End Get
    End Property


    Private _logText As String
    Public Property LogText() As String
        Get
            Return _logText
        End Get
        Set(ByVal value As String)
            _logText = value
            OnPropertyChanged("LogText")
        End Set
    End Property


#End Region

#Region "Methods"

    Public Sub LoadClientDetails()
        Try
            Client = _clientManagementBL.GetClientDetails(SelectedClient.ID)
            SelectedBuyingGroup = BuyingGroups.Single(Function(i) i.BuyingGroupID = Client.BuyingGroupId)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub GetClients()

        Try
            Me._isClientsLoaded = True

            _clientsCollection = New ObservableCollection(Of ClientEntity)(_clientManagementBL.GetClients())
            Me._isClientsLoaded = False

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub GetBuyingGroups()
        Try


            _buyingGroups = New ObservableCollection(Of BuyingGroupEntity)(_clientManagementBL.GetBuyingGroups())

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub GetSuperStores()

    End Sub

    Public Sub Close()
        Me.Close()

    End Sub

    Private Sub OpenSuperstore()

        Dim superStoreWindows As New ManageSuperstores()
        superStoreWindows.ShowDialog()
    End Sub
    Public Sub UpdateClient()

        Try

            _clientManagementBL.UpdateClient(SelectedClient.ID, Client)


        Catch ex As Exception

        End Try



    End Sub





#End Region

#Region "Wizards"

    Public Sub New()

        Try
            GetClients()
            GetBuyingGroups()
        Catch ex As Exception

        End Try

    End Sub
#End Region




End Class
