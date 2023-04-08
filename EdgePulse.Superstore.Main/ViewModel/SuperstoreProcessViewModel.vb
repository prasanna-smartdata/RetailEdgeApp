Imports EdgePulse.Infrastructure
Imports Prism.Commands
Public Class SuperstoreProcessViewModel
    Inherits ViewModelBase

    Public Event BuildSuperstoreCompletedEvent As EventHandler(Of EventArgs)
    Public Event RestartSuperstoreProcessEvent As EventHandler(Of EventArgs)

    Private _showStatusReport As Boolean = True
    Public Property ShowStatusReport() As Boolean
        Get
            Return _showStatusReport
        End Get
        Set(ByVal value As Boolean)
            _showStatusReport = value
            OnPropertyChanged("ShowStatusReport")
        End Set
    End Property

    Private _showBuildSuperstore As Boolean = False
    Public Property ShowBuildSuperstore() As Boolean
        Get
            Return _showBuildSuperstore
        End Get
        Set(ByVal value As Boolean)
            _showBuildSuperstore = value
            OnPropertyChanged("ShowBuildSuperstore")

        End Set
    End Property

    Private _showCancel As Boolean = False
    Public Property ShowCancel() As Boolean
        Get
            Return _showCancel
        End Get
        Set(ByVal value As Boolean)
            _showCancel = value
            OnPropertyChanged("ShowCancel")

        End Set
    End Property

    Private _isStatusReportVerified As Boolean = False
    Public Property IsStatusReportVerified() As Boolean
        Get
            Return _isStatusReportVerified
        End Get
        Set(ByVal value As Boolean)
            _isStatusReportVerified = value
            OnPropertyChanged("IsStatusReportVerified")

        End Set
    End Property

    Private _generatedStatusReportClick As DelegateCommand
    Public ReadOnly Property GenerateStaturReportClick() As DelegateCommand
        Get
            If _generatedStatusReportClick Is Nothing Then
                _generatedStatusReportClick = New DelegateCommand(AddressOf GenerateStatusReport)
            End If

            Return _generatedStatusReportClick
        End Get


    End Property


    Private _cancelButtonClick As DelegateCommand
    Public ReadOnly Property CancelButtonClick() As DelegateCommand
        Get
            If _cancelButtonClick Is Nothing Then
                _cancelButtonClick = New DelegateCommand(AddressOf Cancel)
            End If

            Return _cancelButtonClick
        End Get

    End Property



    Private _buildProcessClick As DelegateCommand
    Public ReadOnly Property BuildProcessClick() As DelegateCommand
        Get
            If _buildProcessClick Is Nothing Then
                _buildProcessClick = New DelegateCommand(AddressOf BuildProcess)
            End If

            Return _buildProcessClick
        End Get

    End Property

    Private _showKPIReportButton As Boolean = True
    Public Property ShowKPIReportButton() As Boolean
        Get
            Return _showKPIReportButton
        End Get
        Set(ByVal value As Boolean)
            _showKPIReportButton = value
        End Set
    End Property

    Private _buildSuperstoreCompleted As Boolean = False
    Public Property BuildSuperstoreCompleted() As Boolean
        Get
            Return _buildSuperstoreCompleted
        End Get
        Set(ByVal value As Boolean)
            _buildSuperstoreCompleted = value

            OnPropertyChanged("BuildSuperstoreCompleted")
        End Set
    End Property
    Private Sub GenerateStatusReport()
        Try
            'After successfully generating the report change the _isStatusReportGenerated to true.
            'That will hide the generate status report button and show other  buttons
            ShowStatusReport = False
            ShowBuildSuperstore = True
            ShowCancel = True



        Catch ex As Exception

        End Try
    End Sub


    Sub Cancel()
        Try
            'Reset the functionality based on the stage . like status report or build process or kpireports
            If Not ShowStatusReport Then
                ShowStatusReport = True
                ShowBuildSuperstore = False
                ShowCancel = False

            End If

            If BuildSuperstoreCompleted Then
                RaiseEvent RestartSuperstoreProcessEvent(Me, New EventArgs)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Overridable Sub OnBuildSuperstoreCompleted(e As EventArgs)

        RaiseEvent BuildSuperstoreCompletedEvent(Me, e)
    End Sub

    Sub BuildProcess()
        Try
            BuildSuperstoreCompleted = True
            OnBuildSuperstoreCompleted(New EventArgs())
        Catch ex As Exception

        End Try
    End Sub
End Class
