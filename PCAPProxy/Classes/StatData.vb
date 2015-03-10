Imports System.IO

Public Class StatData
    Public Property obf0() As Integer
    Public Property obf1() As Int32
    Public Property obf2() As String

    Public Shared Function parse(b As BinaryReader) As StatData
        Dim data As New StatData
        data.obf0 = b.ReadByte
        If isUTFData(data.obf0) = True Then
            data.obf2 = b.ReadString
        Else
            data.obf1 = b.ReadInt32
        End If

        Return data
    End Function

    Private Shared Function isUTFData(obf0 As Integer) As Boolean
        Select Case obf0
            Case 31 Or 62 Or 82 Or 38 Or 54
                Return True
            Case Else
                Return False
        End Select
    End Function
End Class
