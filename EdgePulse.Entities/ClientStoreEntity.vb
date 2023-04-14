Public Class ClientStoreEntity
    Inherits BaseEntity
    Implements ICloneable

    Private _id As Int32
    Public Property ID() As Int32
        Get
            Return _id
        End Get
        Set(ByVal value As Int32)
            _id = value
            OnPropertyChanged("ID")
        End Set
    End Property

    Private _clientId As Int32
    Public Property ClientID() As Int32
        Get
            Return _clientId
        End Get
        Set(ByVal value As Int32)
            _clientId = value
            OnPropertyChanged("ClientID")
        End Set
    End Property

    Private _clientName As String
    Public Property ClientName() As String
        Get
            Return _clientName
        End Get
        Set(ByVal value As String)
            _clientName = value
            OnPropertyChanged("ClientName")
        End Set
    End Property

    Private _clientNum As String
    Public Property ClientNumber() As String
        Get
            Return _clientNum
        End Get
        Set(ByVal value As String)
            _clientNum = value
            OnPropertyChanged("ClientNumber")
        End Set
    End Property

    Private _storeID As Int16
    Public Property StoreID() As Int16
        Get
            Return _storeID
        End Get
        Set(ByVal value As Int16)
            _storeID = value
            OnPropertyChanged("StoreID")
        End Set
    End Property

    Private _storeName As String
    Public Property StoreName() As String
        Get
            Return _storeName
        End Get
        Set(ByVal value As String)
            _storeName = value
            OnPropertyChanged("StoreName")
        End Set
    End Property

    Private _isSelected As Boolean
    Public Property IsSelected() As Boolean
        Get
            Return _isSelected
        End Get
        Set(ByVal value As Boolean)
            _isSelected = value
            OnPropertyChanged("IsSelected")
        End Set
    End Property
    Public Function Clone() As Object Implements ICloneable.Clone
        Return CType(Me.MemberwiseClone(), ClientStoreEntity)
    End Function
End Class
