Imports System.IO

Public Class Status
    Public Shared Property objectId As Int32
    Public Shared Property pos As Location = New Location()
    Public Shared Property data As StatData = New StatData()

    Public Shared Sub parse(b As BinaryReader)
        objectId = b.ReadInt32


    End Sub
End Class
