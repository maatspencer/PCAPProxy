Imports System
Imports System.Text
Imports System.Security.Cryptography
Imports Org.Mentalis.Security.Cryptography

Public Class RC4
    ' Encrypt and Decrypt Packets
    Public Shared Function Main(ByVal bytes As Byte(), ByVal key As String, ByVal encryptData As Boolean) As Byte()
        Dim buffer As Byte()
        Dim algorithm As SymmetricAlgorithm = New RC4CryptoServiceProvider()
        algorithm.Key = hexStringToBytes(key)

        Dim encryptor As ICryptoTransform
        Dim decryptor As ICryptoTransform
        encryptor = algorithm.CreateEncryptor
        decryptor = algorithm.CreateDecryptor

        If encryptData = True Then
            buffer = encryptor.TransformFinalBlock(bytes, 0, bytes.Length)
        Else
            buffer = decryptor.TransformFinalBlock(bytes, 0, bytes.Length)
        End If

        algorithm.Clear()
        encryptor.Dispose()
        decryptor.Dispose()

        Return buffer
    End Function
    ' Convert Hexstring Key to an array of bytes
    Private Shared Function hexStringToBytes(key As String) As Byte()
        ' Convert key from a hexstring to a byte array
        key = key.ToUpper
        Dim bytes(key.Length / 2 - 1) As Byte

        For i = 0 To bytes.Length - 1
            Dim byteValue As String = key.Substring(i * 2, 2)
            bytes(i) = Byte.Parse(byteValue, Globalization.NumberStyles.HexNumber)
        Next

        Return bytes
    End Function
End Class
