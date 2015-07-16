Imports System
Imports System.Text
Imports System.Security.Cryptography
Imports Org.Mentalis.Security.Cryptography

Public Class RC4
    ' Encrypt and Decrypt Packets
    Public Shared Sub Main(ByRef pBuffer As IList(Of Byte), ByVal key As String, ByVal encryptData As Boolean)
        Dim c As Integer = pBuffer.Count
        Dim bytes As Byte() = New Byte(c) {}
        For i = 0 To c - 1
            bytes(i) = pBuffer.Item(i)
        Next

        Dim algorithm As SymmetricAlgorithm = New ARCFourManaged() ' New RC4CryptoServiceProvider()
        algorithm.Key = hexStringToBytes(key)

        Dim encryptor As ICryptoTransform
        Dim decryptor As ICryptoTransform
        encryptor = algorithm.CreateEncryptor
        decryptor = algorithm.CreateDecryptor

        If encryptData = True Then
            bytes = encryptor.TransformFinalBlock(bytes, 0, c)
        Else
            bytes = decryptor.TransformFinalBlock(bytes, 0, c)
        End If

        algorithm.Clear()
        encryptor.Dispose()
        decryptor.Dispose()

        pBuffer.Clear()
        For i = 0 To c - 1
            pBuffer.Add(bytes(i))
        Next

    End Sub
    ' Convert Hexstring Key to an array of bytes
    Private Shared Function hexStringToBytes(key As String) As Byte()
        ' Convert key from a hexstring to a byte array
        key = key.ToUpper
        If Not (key.Length Mod 2 = 0) Then
            Err.Raise(1)
            Console.ReadKey()
        End If

        Dim bytes As Byte() = New Byte(key.Length / 2 - 1) {}

        For i = 0 To bytes.Length - 1
            Dim byteValue As String = key.Substring(i * 2, 2)
            bytes(i) = Byte.Parse(byteValue, Globalization.NumberStyles.HexNumber)
        Next

        Return bytes
    End Function
End Class
