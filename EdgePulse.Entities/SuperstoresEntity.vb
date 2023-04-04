Public Class SuperstoreEntity
    Inherits BaseEntity
    Implements ICloneable

    Private _id As Int16
    Private _clientNumber As Int16
    Private _storeNumber As Int16
    Private _superStoreId As Int16

    Public Property ID() As Int16
        Get
            Return _id
        End Get
        Set(ByVal Value As Int16)
            _id = Value
            OnPropertyChanged("ID")
        End Set
    End Property

    Public Property ClientNumber() As Int16
        Get
            Return _clientNumber
        End Get
        Set(ByVal Value As Int16)
            _clientNumber = Value
            OnPropertyChanged("ClientNumber")
        End Set
    End Property

    Public Property StoreNumber() As Int16
        Get
            Return _storeNumber
        End Get
        Set(ByVal Value As Int16)
            _storeNumber = Value
            OnPropertyChanged("StoreNumber")
        End Set
    End Property
    Public Property SuperStoreId() As Int16
        Get
            Return _superStoreId
        End Get
        Set(ByVal Value As Int16)
            _superStoreId = Value
            OnPropertyChanged("SuperStoreId")
        End Set
    End Property
    Public Function Clone() As Object Implements ICloneable.Clone
        Return CType(Me.MemberwiseClone(), SuperstoreEntity)
    End Function
End Class
