Imports System.IO

Public Class Status
    Public Property objectId() As Int32
    Public Property pos() As Location
    Public Property data() As New List(Of StatData)

    Public Shared Function parse(b As BinaryReader) As Status
        Dim status As New Status

        status.objectId = b.ReadInt32
        status.pos = Location.parse(b)

        For i = 0 To b.ReadInt16 - 1
            status.data.Add(StatData.parse(b))
        Next

        Return status
    End Function
End Class
