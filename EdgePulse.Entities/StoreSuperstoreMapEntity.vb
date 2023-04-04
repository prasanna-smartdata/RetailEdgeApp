Public Class StoreSuperstoreMapEntity
    Inherits BaseEntity
    Implements ICloneable

    Private _id As Int16
    Private _clientStoreId As Int16
    Private _superStoreGroupId As Int16



    Public Property ID() As Int16
        Get
            Return _id
        End Get
        Set(ByVal Value As Int16)
            _id = Value
            OnPropertyChanged("ID")
        End Set
    End Property

    Public Property ClientStoreId() As Int16
        Get
            Return _clientStoreId
        End Get
        Set(ByVal Value As Int16)
            _clientStoreId = Value
            OnPropertyChanged("ClientStoreId")
        End Set
    End Property

    Public Property SuperStoreGroupId() As Int16
        Get
            Return _superStoreGroupId
        End Get
        Set(ByVal Value As Int16)
            _superStoreGroupId = Value
            OnPropertyChanged("SuperStoreGroupId")
        End Set
    End Property

    Public Function Clone() As Object Implements ICloneable.Clone
        Return CType(Me.MemberwiseClone(), StoreSuperstoreMapEntity)
    End Function
End Class
