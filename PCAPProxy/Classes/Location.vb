Imports System.IO

Public Class Location
    Public Property x() As Single
    Public Property y() As Single

    Public Shared Function parse(b As BinaryReader) As Location
        Dim pos As New Location
        pos.x = b.ReadSingle
        pos.y = b.ReadSingle

        Return pos
    End Function
End Class
