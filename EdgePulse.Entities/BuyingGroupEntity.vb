Public Class BuyingGroupEntity
    Inherits BaseEntity
    Implements ICloneable
    Public Function Clone() As Object Implements ICloneable.Clone
        Return CType(Me.MemberwiseClone(), ClientEntity)
    End Function
End Class
