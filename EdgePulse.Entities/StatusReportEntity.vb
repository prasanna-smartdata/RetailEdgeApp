Imports System.Windows.Media.Animation

Public Class StatusReportEntity
    Inherits BaseEntity
    Implements ICloneable

    Private _groupNum As Int16
    Private _clientNumber As Int16
    Private _storeID As Int16
    Private _storeName As String
    Private _clientMembership As String
    Private _systemID As String
    Private _availableMonthsOfData As Int16
    Private _lastSalesDate As Date
    Private _deptCoding As Int16
    Private _sales12Months As Long
    Private _salesPrevMonth As Long
    Private _currentStock As Long
    Private _salesPercent As Long


    Public Property GroupNum() As Int16
        Get
            Return _groupNum
        End Get
        Set(ByVal Value As Int16)
            _groupNum = Value
        End Set
    End Property

    Public Property ClientNumber() As Int16
        Get
            Return _clientNumber
        End Get
        Set(ByVal Value As Int16)
            _clientNumber = Value
        End Set
    End Property
    Public Property StoreID() As Int16
        Get
            Return _storeID
        End Get
        Set(ByVal Value As Int16)
            _storeID = Value
        End Set
    End Property
    Public Property StoreName() As String
        Get
            Return _storeName
        End Get
        Set(ByVal Value As String)
            _storeName = Value
        End Set
    End Property
    Public Property ClientMembership() As String
        Get
            Return _clientMembership
        End Get
        Set(ByVal Value As String)
            _clientMembership = Value
        End Set
    End Property
    Public Property SystemID() As String
        Get
            Return _systemID
        End Get
        Set(ByVal Value As String)
            _systemID = Value
        End Set
    End Property
    Public Property AvailableMonthsOfData() As Int16
        Get
            Return _availableMonthsOfData
        End Get
        Set(ByVal Value As Int16)
            _availableMonthsOfData = Value
        End Set
    End Property
    Public Property LastSalesDate() As Date
        Get
            Return _lastSalesDate
        End Get
        Set(ByVal Value As Date)
            _lastSalesDate = Value
        End Set
    End Property
    Public Property DeptCoding() As Int16
        Get
            Return _deptCoding
        End Get
        Set(ByVal Value As Int16)
            _deptCoding = Value
        End Set
    End Property
    Public Property Sales12Months() As Long
        Get
            Return _sales12Months
        End Get
        Set(ByVal Value As Long)
            _deptCoding = Value
        End Set
    End Property
    Public Property SalesPrevMonth() As Long
        Get
            Return _salesPrevMonth
        End Get
        Set(ByVal Value As Long)
            _salesPrevMonth = Value
        End Set
    End Property
    Public Property CurrentStock() As Long
        Get
            Return _currentStock
        End Get
        Set(ByVal Value As Long)
            _currentStock = Value
        End Set
    End Property
    Public Property SalesPercent() As Long
        Get
            Return _salesPercent
        End Get
        Set(ByVal Value As Long)
            _salesPercent = Value
        End Set
    End Property

    Public Function Clone() As Object Implements ICloneable.Clone
        Throw New NotImplementedException()
    End Function
End Class
