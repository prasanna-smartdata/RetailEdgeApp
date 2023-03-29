Imports System.Collections.ObjectModel
Imports EdgePulse.Entities
Imports EdgePulse.Infrastructure

Imports Prism.Commands
Imports System.Windows.Input

Public Class AddEditClientViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private ReadOnly _editClickCommand As DelegateCommand = Nothing
    Private ReadOnly _exitClickCommand As DelegateCommand = Nothing

    Private _clientsCollection As New ObservableCollection(Of ClientEntity)

#End Region


#Region "Properties"
    Public ReadOnly Property EditClickCommand As DelegateCommand
        Get
            Return If(Me._editClickCommand, New DelegateCommand(Of ClientEntity)(AddressOf UpdateClient))
        End Get
    End Property

#End Region

#Region "Methods"

    Public Sub GetClients()

    End Sub

    Public Sub GetBuyingGroups()

    End Sub

    Public Sub GetSuperStores()

    End Sub

    Public Sub Close()
        Me.Close()

    End Sub
    Public Sub UpdateClient()

    End Sub

    Private _exitCommand As DelegateCommand

    Public ReadOnly Property ExitCommand As ICommand
        Get

            If _exitCommand Is Nothing Then
                _exitCommand = New DelegateCommand(AddressOf Close)
            End If

            Return _exitCommand
        End Get
    End Property


#End Region

#Region "Wizards"
#End Region


End Class
