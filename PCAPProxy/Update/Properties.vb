Public Class Properties
    Public Shared listenPort As String = "2050"
    Public Shared listenIP As String = ""
    Public Shared remoteIP As String = "54.219.44.205" 'USSW
    Public Shared remotePort As String = "2050"

    Public Shared interfaceNumber As Integer = "10000"
    Public Shared Endianess As Boolean = BitConverter.IsLittleEndian


    Public Shared clientKey As String = "311f80691451c71b09a13a2a6e"
    Public Shared serverKey As String = "72c5583cafb6818995cbd74b80"
    Public Shared versionNumber As String = "21.0.3" ' Not currently used
    Public Shared AGCVersion As String = "1400621020"
    Public Shared packetArray As ArrayList


    Public Shared Sub Load()
        interfaceNumber = My.Settings.InterfaceNum
        listenIP = My.Settings.listenIP
        clientKey = My.Settings.clientKey
        serverKey = My.Settings.serverKey
        versionNumber = My.Settings.versionNumber
        AGCVersion = My.Settings.AGCVersion
        packetArray = My.Settings.packets
    End Sub
End Class
