Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports PcapDotNet.Core

Public Class deviceAdvancedInformation
    Public Shared Sub Main()
        Console.WriteLine("Network Devices: ")

        ' Scan and Print all Network Devices
        For i = 0 To allDevices.Count - 1
            devicePrint(allDevices(i), i)
        Next
    End Sub

    Public Shared Sub devicePrint(device As IPacketDevice, Count As Integer)
        ' Name
        Console.WriteLine((Count + 1) & ". " & device.Name)

        ' Description
        If device.Description <> Nothing Then
            Console.WriteLine(vbTab & "Description: " & device.Description)
        End If

        ' Loopback Address
        Console.WriteLine(vbTab & "Loopback: " & (If(((device.Attributes And DeviceAttributes.Loopback) = DeviceAttributes.Loopback), "yes", "no")))
        ' IP addresses
        For Each address As DeviceAddress In device.Addresses
            Console.WriteLine(vbTab & "Address Family: " + address.Address.Family.ToString)

            If address.Address IsNot Nothing Then
                Console.WriteLine(vbTab & "Address: " & address.Address.ToString)
            End If
            If address.Netmask IsNot Nothing Then
                Console.WriteLine(vbTab & "Netmask: " & address.Netmask.ToString)
            End If
            If address.Broadcast IsNot Nothing Then
                Console.WriteLine(vbTab & "Broadcast Address: " & address.Broadcast.ToString)
            End If
            If address.Destination IsNot Nothing Then
                Console.WriteLine(vbTab & "Destination Address: " & address.Destination.ToString)
            End If
        Next
        Console.WriteLine()

    End Sub

End Class
