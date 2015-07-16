Imports System.IO

Public Class byteHandler
    Public Shared Sub Endianess(ByRef buffer As IList(Of Byte))
        If Properties.Endianess = True Then
            Array.Reverse(buffer)
        End If
    End Sub

    Public Shared Sub arrayToIList(arr As Byte(), ByRef b As IList(Of Byte))
        For i = 0 To arr.Length - 1
            b.Add(arr(i))
        Next
    End Sub

    Public Shared Function readInt16(b As IList(Of Byte), ByRef o As Integer) As Int16
        Dim bytes As Byte() = New Byte(2) {}
        bytes = b.Skip(o).Take(2).ToArray
        o += 2
        Endianess(bytes)

        Dim var As Int16 = BitConverter.ToInt16(bytes, 0)

        Return var
    End Function
    Public Shared Function readUInt16(b As IList(Of Byte), ByRef o As Integer) As UInt16
        Dim bytes As Byte() = New Byte(2) {}
        bytes = b.Skip(o).Take(2).ToArray
        o += 2
        Endianess(bytes)

        Dim var As UInt16 = BitConverter.ToUInt16(bytes, 0)

        Return var
    End Function

    Public Shared Function readInt32(b As IList(Of Byte), ByRef o As Integer) As Int32
        Dim bytes As Byte() = New Byte(4) {}
        bytes = b.Skip(o).Take(4).ToArray
        o += 4
        Endianess(bytes)

        Dim var As Int32 = BitConverter.ToInt32(bytes, 0)

        Return var
    End Function

    Public Shared Function readUInt32(b As IList(Of Byte), ByRef o As Integer) As UInt32
        Dim bytes As Byte() = New Byte(4) {}
        bytes = b.Skip(o).Take(4).ToArray
        o += 4
        Endianess(bytes)

        Dim var As UInt32 = BitConverter.ToUInt32(bytes, 0)

        Return var
    End Function

    Public Shared Function readSingle(b As IList(Of Byte), ByRef o As Integer) As Single
        Dim bytes As Byte() = New Byte(4) {}
        bytes = b.Skip(o).Take(4).ToArray
        o += 4
        Endianess(bytes)

        Dim var As Single = BitConverter.ToSingle(bytes, 0)

        Return var
    End Function

    Public Shared Function readByte(b As IList(Of Byte), ByRef o As Integer) As Byte
        Dim bytes As Byte() = New Byte(1) {}
        bytes = b.Skip(o).Take(1).ToArray
        o += 1

        Dim var As Byte = bytes(0)

        Return var
    End Function
End Class
