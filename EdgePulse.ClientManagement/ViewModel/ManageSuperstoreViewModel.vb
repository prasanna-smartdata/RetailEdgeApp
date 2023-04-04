Imports System.Collections.ObjectModel
Imports EdgePulse.Infrastructure
Imports Prism.Commands
Imports EdgePulse.Entities
Public Class ManageSuperstoreViewModel
    Inherits ViewModelBase


#Region "Fields"

    Private ReadOnly _exitClickCommand As DelegateCommand = Nothing

    Private _clientsCollection As New ObservableCollection(Of ClientEntity)
    Private _superStoresCollection As New ObservableCollection(Of SuperstoreEntity)

#End Region


#Region "Properties"


#End Region

#Region "Methods"

    Public Sub GetClients()

    End Sub

    Public Sub GetBuyingGroups()
        Try

        Catch ex As Exception

        End Try
    End Sub

    Public Sub GetSuperStores()

    End Sub

    Public Sub Close()
        Me.Close()

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
