Imports EdgePulse.Infrastructure
Imports Prism.Commands
Imports EdgePulse.Entities
Imports EdgePulse.Business
Imports System.Collections.ObjectModel
Imports EdgePulse.Data

Public Class SuperstoreProcessViewModel
    Inherits ViewModelBase

    Public Event BuildSuperstoreCompletedEvent As EventHandler(Of EventArgs)
    Public Event RestartSuperstoreProcessEvent As EventHandler(Of EventArgs)

    Private _showStatusReport As Boolean = True
    Private _statusReportBL As New StatusReportBL
    Private _clientManagementBL As New ClientManagementBL()
    Private _superStoresCollection As New ObservableCollection(Of SuperstoreEntity)
    Private _statusReportRecords As List(Of StatusReportEntity) = Nothing


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

    Private _logText As String = "[BEGIN] Processing.... " & vbCrLf
    Public Property LogText() As String
        Get
            Return _logText
        End Get
        Set(ByVal value As String)
            _logText = value
            OnPropertyChanged("LogText")
        End Set
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

    Private _selectedMonth As Object
    Public Property SelectedMonth() As Object
        Get
            Return _selectedMonth
        End Get
        Set(ByVal value As Object)
            _selectedMonth = value
        End Set
    End Property

    Private _statusReportMonthsCollection As ObservableCollection(Of Object)
    Public Property StatusReportMonthsCollection() As ObservableCollection(Of Object)
        Get
            Return _statusReportMonthsCollection
        End Get
        Set(ByVal value As ObservableCollection(Of Object))
            _statusReportMonthsCollection = value
        End Set
    End Property

    Public Sub GetSuperStores()
        Try
            AppendToLog("Loading Superstores")
            SuperstoresCollection = New ObservableCollection(Of SuperstoreEntity)(_clientManagementBL.GetSuperstores())

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Sub GetMonthsDataForPreviousMonths()
        Try
            StatusReportMonthsCollection = New ObservableCollection(Of Object)(HelperFile.GetMonthsListForStatusReport())
            AppendToLog("Loading the Previous Months Dropdown")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GenerateStatusReport()
        Try
            If SelectedSuperstore IsNot Nothing Then
                'After successfully generating the report change the _isStatusReportGenerated to true.
                'That will hide the generate status report button and show other  buttons
                ShowStatusReport = False
                ShowBuildSuperstore = True
                ShowCancel = True
                Dim lgText As String = ""
                _statusReportRecords = _statusReportBL.GenerateStatusReport(_selectedSuperstore.ID, "012023", lgText)

                AppendToLog(lgText)
            End If

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
                AppendToLog("Cancelling Status Report Creation")

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


    'Call this method to add the log
    Sub AppendToLog(ByVal text)
        _logText += text + Environment.NewLine
        OnPropertyChanged("LogText")

    End Sub

#Region "Consctructors"

    Public Sub New()

        Try
            GetSuperStores()
            GetMonthsDataForPreviousMonths()
        Catch ex As Exception
            Throw
        End Try

    End Sub
#End Region

End Class
