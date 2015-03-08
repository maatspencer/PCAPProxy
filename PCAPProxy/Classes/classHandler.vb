Imports System.IO
Public Class classHandler

    Public Shared Sub parse(b As BinaryReader)
        x = b.ReadSingle
        y = b.ReadSingle
    End Sub
End Class
