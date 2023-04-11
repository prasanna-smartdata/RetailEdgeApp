﻿Imports System.Collections.ObjectModel
Imports EdgePulse.Entities
Imports EdgePulse.Infrastructure

Imports Prism.Commands
Imports System.Windows.Input
Imports EdgePulse.Business

Public Class AddEditClientViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _editClickCommand As DelegateCommand = Nothing

    Private _exitCommand As DelegateCommand = Nothing
    Private _openSuperstoreCommand As DelegateCommand = Nothing

    Private _clientsCollection As New ObservableCollection(Of ClientEntity)
    Private _clientManagementBL As New ClientManagementBL()
    Private _client As ClientEntity


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

    Private _isClientsLoaded As Boolean
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
            Return If(Me._editClickCommand, New DelegateCommand(Of ClientEntity)(AddressOf UpdateClient))
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

#End Region

#Region "Methods"

    Public Sub GetClients()

        Try
            Me._isClientsLoaded = True

            _clientsCollection = New ObservableCollection(Of ClientEntity)(_clientManagementBL.GetClients())
            Me._isClientsLoaded = False

        Catch ex As Exception

        End Try

    End Sub

    Public Sub GetBuyingGroups()

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

            _clientManagementBL.UpdateClient(SelectedClient)

        Catch ex As Exception

        End Try



    End Sub





#End Region

#Region "Wizards"

    Public Sub New()

        Try
            GetClients()
        Catch ex As Exception

        End Try

    End Sub
#End Region




End Class
