Imports System.IO.FileStream
Imports System.IO.StreamWriter
Imports System.IO
Imports RecolectOnPC.GlobalVariables
Imports System.Net
Imports System.Web
Imports System.Net.NetworkAccess
Imports System.Net.IPHostEntry
Imports System.Math
Imports System.Double
Imports RecolectOnPC.RescueInfoPC


Module ModulesON
    Public Function GetHost(ByVal valorIp As String) As String
        'elimina posibles espacios o comas
        'valorIp = Replace(valorIp, ",", ".")
        'valorIp = Replace(valorIp, " ", "")
        'Validar IP,
        Try
            'valida si el pc esta disponible en red
            If PcAvailable(valorIp) = True Then
                Dim host As System.Net.IPHostEntry
                host = System.Net.Dns.GetHostByAddress(valorIp)
                GetHost = host.HostName
            Else
                MsgBox("El Pc al que intentas llegar no esta disponile en red-IP: " & valorIp)
            End If
        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
            GV.Inmessage = "Error en Funcion GetHost"
            Call LogErrorFileWriter(GV.Inmessage, valorIp)
        End Try

    End Function

    Public Function GetIp(valorHost As String) As String
        Dim strIPAddress As String = ""
        Dim strHostName As String = ""
        'Dim usuario As String = (My.Computer.Name)

        Try
            Dim hostname As IPHostEntry = Dns.GetHostByName(valorHost)
            Dim ip As IPAddress() = hostname.AddressList
            valorHost = ip(0).ToString()
            If valorHost Is Nothing Then
                MsgBox("El Pc al que intentas llegar no esta disponile en red- Usuario: " & valorHost)
            Else
                GetIp = valorHost
            End If

        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
            GV.Inmessage = "Error en Funcion GetIp - " & ex.Message.ToString
            Call LogErrorFileWriter(GV.Inmessage, valorHost)
        End Try
    End Function

    Public Function PcAvailable(ByVal valorIp) As Boolean
        Dim InMessage As String = ""
        'Dim usuario As String = ""
        'usuario = GetHost(valorIp)
        Try
            'valida si el pc esta disponible en red
            'If PcAvailable(valorIp) Then
            ' Consulta si tienen conectividad en red
            If My.Computer.Network.Ping(valorIp) Then
                'MsgBox("Server pinged successfully.")
                PcAvailable = True
            Else
                InMessage = "Pc no encendido para recuparar datos desde la IP:" & valorIp
                Call LogErrorFileWriter(InMessage, valorIp)
                PcAvailable = False
            End If
        Catch ex As Exception
            InMessage = "Error en el metodo - PcAvailable- " & ex.Message.ToString
            Call LogErrorFileWriter(InMessage, valorIp)
        End Try
    End Function

    Public Sub LogErrorFileWriter(ByVal InMessage, ByVal usuario)

        Try
            Dim logpath As String = "\\10.177.1.230\RescueOnPc$\LOGS" & "\" & usuario & "\logRecolectON.txt"
            '''E:\RescueOnPc$\LOGS
            Dim fechaLog As Date = (Date.Now)
            Dim dir As String = "\\10.177.1.230\RescueOnPc$\LOGS" & "\" & usuario
            Dim HereMessage As String = ""
            'crea el directorio
            My.Computer.FileSystem.CreateDirectory(dir)

            If System.IO.File.Exists(logpath) = True Then
                'LogWriter(logpath, True)
                Using writer As StreamWriter = New StreamWriter(logpath, True)
                    writer.Write(fechaLog & " " & InMessage & vbCrLf)
                End Using
                InMessage = ""
            Else
                'do nothing
            End If
            InMessage = ""
        Catch ex As Exception
            'do nothing on error
        End Try
    End Sub

    



    Public Function GetSpecs(ByVal ip As String, ByVal user As String, ByRef ArrayListProcess As ArrayList, ByRef ArrayListCpu As ArrayList _
, ByRef ArrayListMBoard As ArrayList, ByRef ArraySizeHDisk As ArrayList, ByRef ArraySpecsHDisk As ArrayList, ByRef TotalSize As Long, ByRef Ram As Double, ByVal adminuser As String, ByVal pass As String)


        Try
            Dim strComputer As String = user
            Dim strDomain As String = "DCACFCAPITAL"
            'Dim strUser As String = adminuser
            'Dim strPassword As String = pass
            Dim objSWbemLocator As Object
            Dim objSWbemServices As Object
            Dim colSwbemObjectSet As Object
            Dim MboardDesc As String = ""
            Dim MediaDisk As String = ""
            Dim cRam As Double = 0

            '' '' garantiza el acceso mediante log de cuenta de administrador de dominio o local remotamente
            objSWbemLocator = CreateObject("WbemScripting.SWbemLocator")
            objSWbemServices = objSWbemLocator.ConnectServer(strComputer, _
                "root\cimv2", _
                 GV.adminuser, _
                 GV.Password, _
                 "MS_409", _
                 "ntlmdomain:" + strDomain)

            '' ''obtiene los procesos mediante Wmi
            colSwbemObjectSet = _
                objSWbemServices.ExecQuery("Select * From Win32_Process")
            For Each objProcess In colSwbemObjectSet
                'xPrcess = xProcess & CType(objProcess.Name, String)
                ArrayListProcess.Add(CType(objProcess.Name, String))
            Next
            'elimina los procesos repetidos
            Dim r As Integer = 1
            Dim ArrayListProcessOK As ArrayList = Nothing
            Dim TestComp As Integer
            For Each item In ArrayListProcess
                'xPrcess = xProcess & CType(objProcess.Name, String)
                Do

                    TestComp = StrComp(ArrayListProcess(0), ArrayListProcess(r))

                    'If ArrayListProcess(0) <> ArrayListProcess(1) Then
                    If TestComp = 0 Then
                        MsgBox("son iguales")
                        'ArrayListProcessOK.AddRange(ArrayListProcess(0))
                        'ArrayListProcess.Remove(0)
                        'do nothing
                        ArrayListProcess.Remove(0)
                    End If
                    r = r + 1
                Loop Until ArrayListProcess.Count <> 0
            Next


            '' 'obtiene datos de la CPU
            colSwbemObjectSet = _
                objSWbemServices.ExecQuery("Select * From Win32_Processor")
            For Each CPU In colSwbemObjectSet

                ArrayListCpu.Add(CType(CPU.ProcessorId, String))
                ArrayListCpu.Add(CType(CPU.name, String))
                ArrayListCpu.Add(CType(CPU.CurrentClockSpeed, String))
                ArrayListCpu.Add(CType(CPU.DataWidth, String))
                ArrayListCpu.Add(CType(CPU.Description, String))
                ArrayListCpu.Add(CType(CPU.DeviceID, String))
                ArrayListCpu.Add(CType(CPU.L2CacheSize, String))
                ArrayListCpu.Add(CType(CPU.L3CacheSpeed, String))
                ArrayListCpu.Add(CType(CPU.Manufacturer, String))
                ArrayListCpu.Add(CType(CPU.NumberOfCores, String))
                ArrayListCpu.Add(CType(CPU.NumberOfLogicalProcessors, String))
                ArrayListCpu.Add(CType(CPU.Status, String))

            Next

            '' '''obtiene el datos de la motherboard
            colSwbemObjectSet = _
                objSWbemServices.ExecQuery("Select * from " & _
                "Win32_BaseBoard")
            For Each Mother In colSwbemObjectSet

                ArrayListMBoard.Add(CType(Mother.Description, String))
                ArrayListMBoard.Add(CType(Mother.Manufacturer, String))
                ArrayListMBoard.Add(CType(Mother.Name, String))
                ArrayListMBoard.Add(CType(Mother.Product, String))
                ArrayListMBoard.Add(CType(Mother.Status, String))
            Next

            '' ' 'obtiene la cantidad de disco duro
            colSwbemObjectSet = _
                objSWbemServices.ExecQuery("Select * from " & _
                "Win32_DiskDrive")
            For Each objDisk In colSwbemObjectSet
                MediaDisk = objDisk.mediatype()
                If MediaDisk <> "External hard disk media" Then 'consulta por el tipo de disco duro(Que sea interno no usb o externo)
                    Dim Separate As String = "****************************************"
                    ArraySizeHDisk.Add(CType(objDisk.size, String)) ' ''Suma el total de las unidades logicas de los HD
                    ArraySpecsHDisk.Add(CType(objDisk.Name, String))
                    ArraySpecsHDisk.Add(CType(objDisk.size, String))
                    ArraySpecsHDisk.Add(CType(objDisk.Description, String))
                    ArraySpecsHDisk.Add(CType(objDisk.Partitions, String))
                    ArraySpecsHDisk.Add(CType(objDisk.SerialNumber, String))
                    ArraySpecsHDisk.Add(Separate)

                End If
            Next
            For Each Disk In ArraySizeHDisk
                TotalSize = TotalSize + Disk
            Next
            TotalSize = (TotalSize / 1073741824) ' '' Se divide por 1073741824 cantidad de bytes en un GB
            TotalSize = CLng(TotalSize)
            TotalSize = Math.Round((TotalSize), 0)

            'obtiene la cantidad de ram
            colSwbemObjectSet = _
               objSWbemServices.ExecQuery("Select * from " & _
                "Win32_ComputerSystem")
            For Each item In colSwbemObjectSet
                cRam = item.TotalPhysicalMemory
            Next item
            cRam = cRam / 1073741824 '''''1gb en bytes (1073741824)
            'redondea la cantidad de ram
            Ram = Math.Round((cRam), 0)

        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
            GV.Inmessage = "Error en Funcion GetSpecs- Error: " & ex.Message.ToString
            Call LogErrorFileWriter(GV.Inmessage, user)
        End Try


    End Function

    Public Function KillProcess(TreeViewProcess, childNode)

        Dim objSWbemLocator As Object
        Dim objWMIService As Object
        Dim colProcessList As Object
        Dim strComputer As String = GV.IP
        Dim strDomain As String = "DCACFCAPITAL"
        Dim MboardDesc As String = ""
        Dim MediaDisk As String = ""
        Dim cRam As Double = 0
        Dim Posexe As Integer
        Posexe = InStr(2, GV.ProcessName, ".exe")
        GV.ProcessName = Mid(GV.ProcessName, 1, Posexe)
        GV.ProcessName = GV.ProcessName & "exe"

        objSWbemLocator = CreateObject("WbemScripting.SWbemLocator")
        objWMIService = objSWbemLocator.ConnectServer(strComputer, _
            "root\CIMV2", _
            GV.adminuser, _
            GV.Password, _
            "MS_409", _
            "ntlmdomain:" + strDomain)
        colProcessList = objWMIService.ExecQuery _
            ("SELECT * FROM Win32_Process WHERE Name = '" & GV.ProcessName & "'")
        For Each objProcess In colProcessList
            objProcess.Terminate()
        Next

        'ArrayCheckedNode.add(xKill)
        'Next

        ''For Each KillProcess In ArrayCheckedNode
        'objSWbemLocator = CreateObject("WbemScripting.SWbemLocator")
        'objSWbemServices = objSWbemLocator.ConnectServer(strComputer, _
        '    "root\cimv2", _
        '     strUser, _
        '     strPassword, _
        '     "MS_409", _
        '     "ntlmdomain:" + strDomain)

        'colSwbemObjectSet = _
        '    objSWbemServices.ExecQuery("SELECT * FROM Win32_Process WHERE Name = 'notepad.exe'")


        'strComputer = "."
        'objWMIService = GetObject("winmgmts:" _
        ' & "{impersonationLevel=impersonate}!\\" & strComputer & "\root\cimv2")
        'colProcessList = objWMIService.ExecQuery _
        ' ("SELECT * FROM Win32_Process WHERE Name = 'Diagnose.exe'")
        'For Each objProcess In colProcessList
        '    objProcess.Terminate()
        'Next

        'colSwbemObjectSet = _
        'objSWbemServices.ExecQuery("SELECT * FROM Win32_Process WHERE Name = '" & "" & "'")

        'For Each objProcess In colSwbemObjectSet
        '    objProcess.Terminate()
        'Next
        'Next

        'For Each objProcess In colSwbemObjectSet
        '    'xPrcess = xProcess & CType(objProcess.Name, String)
        '    ArrayListProcess.Add(CType(objProcess.Name, String))
        'Next


        'strComputer = "."
        'objWMIService = GetObject("winmgmts:" _
        ' & "{impersonationLevel=impersonate}!\\" & strComputer & "\root\cimv2")
        'colProcessList = objWMIService.ExecQuery _
        ' ("SELECT * FROM Win32_Process WHERE Name = 'Diagnose.exe'")
        'For Each objProcess In colProcessList
        '    objProcess.Terminate()
        'Next


    End Function
    'Public Function removeDuplicate(arlList)
    '    Dim HashSet As Object
    '    Dim h As String
    '    For Each item on arllist
    '        If item = arlList Then
    '        arlList.remove() Then
    '            'arlList.addAll(h)
    '    Next

    'End Function



    'Public Function GetPcInfo(ByVal ip, ByVal usuario, ByRef Cpuid, ByRef CPUModel _
    '                        , ByRef MBoard, ByRef Hdisc, ByRef Ram, ByVal user, ByVal adminuser, ByVal pass)

    '    GetProcessByUser(user, adminuser, pass)
    '    'Obtiene CPU ID y Modelo
    '    'ReadCpuSerial(ip, usuario, Cpuid, CPUModel)
    '    'obtiene ID de la mother
    '    'MBoard = ReadMboard(ip, usuario)
    '    'obtiene capacidad de disco
    '    'Hdisc = ReadHdisc(ip, usuario)
    '    'obtiene cantidad ram
    '    'Ram = ReadRam(ip, usuario)

    'End Function

    'Public Function GetTable(xProcess)
    '    Dim table As New DataTable
    '    table.Columns.Add(xProcess, GetType(String))
    '    Return table
    'End Function
    'Public Function GetProcessByUser(user, adminuser, pass)
    '    'Dim strHostName As String = ""
    '    'Dim usuario As String = (My.Computer.Name)
    '    'Dim Inmessage As String
    '    'Dim objWMIService As Object
    '    'Dim trMsg As String = ""
    '    'Dim strComputer As String = "."
    '    'Dim IPConfigSet As Object
    '    'valorIp = Replace(valorIp, ",", ".")

    '    'Dim objSWbemLocator As Object
    '    'Dim objSWbemServices As Object
    '    'Dim colItems As Object
    '    'Dim cpu_ids As String = ""
    '    Try
    '        'Dim strComputer As String = "FullComputerName"
    '        'Dim strComputer As String = "jbriceno"
    '        Dim strComputer As String = user
    '        Dim strDomain As String = "DCACFCAPITAL"
    '        Dim strUser As String = adminuser
    '        Dim strPassword As String = pass
    '        Dim objSWbemLocator As Object
    '        Dim objSWbemServices As Object
    '        Dim colSwbemObjectSet As Object
    '        Dim xProcess As String = ""

    '        ''obtiene los procesos mediante Wmi
    '        objSWbemLocator = CreateObject("WbemScripting.SWbemLocator")
    '        objSWbemServices = objSWbemLocator.ConnectServer(strComputer, _
    '            "root\cimv2", _
    '             strUser, _
    '             strPassword, _
    '             "MS_409", _
    '             "ntlmdomain:" + strDomain)
    '        colSwbemObjectSet = _
    '            objSWbemServices.ExecQuery("Select * From Win32_Process")
    '        For Each objProcess In colSwbemObjectSet
    '            xProcess = xProcess & objProcess.Name
    '            'MsgBox(xProcess)
    '        Next
    '    Catch ex As Exception
    '        'MsgBox(ex.Message.ToString)
    '        Inmessage = "Error en Funcion GetHost- " & ex.Message.ToString
    '        Call LogErrorFileWriter(Inmessage, user)
    '    End Try

    'End Function

    'Public Function ReadCpuSerial(ByVal ip, ByVal user, ByRef Cpuid, ByRef CPUModel _
    '                               , ByVal adminuser, ByVal pass) As String

    '    Try
    '        Dim strComputer As String = user
    '        Dim strDomain As String = "DCACFCAPITAL"
    '        Dim strUser As String = adminuser
    '        Dim strPassword As String = pass
    '        Dim objSWbemLocator As Object
    '        Dim objSWbemServices As Object
    '        Dim colSwbemObjectSet As Object

    '        Dim cpu_id As String = ""
    '        Dim cpuName As String = ""
    '        Dim MboardDesc As String = ""
    '        Dim arraydisk(9) As Double
    '        Dim i As Integer = 0
    '        Dim DiskIndex As String = ""
    '        Dim MediaDisk As String = ""
    '        Dim TotalSize As Double = 0
    '        Dim ReadHdisc As Double = 0
    '        Dim cRam As Double = 0

    '        ''obtiene los procesos mediante Wmi
    '        objSWbemLocator = CreateObject("WbemScripting.SWbemLocator")
    '        objSWbemServices = objSWbemLocator.ConnectServer(strComputer, _
    '            "root\cimv2", _
    '             strUser, _
    '             strPassword, _
    '             "MS_409", _
    '             "ntlmdomain:" + strDomain)
    '        colSwbemObjectSet = _
    '            objSWbemServices.ExecQuery("Select * From Win32_Processor")
    '        For Each cpu In colSwbemObjectSet
    '            cpu_id = cpu.ProcessorId
    '            cpuName = cpu.name()
    '        Next

    '        'obtiene el modelo de la motherboard
    '        colSwbemObjectSet = _
    '            objSWbemServices.ExecQuery("Select * from " & _
    '            "Win32_ComputerSystem")
    '        For Each caption In colSwbemObjectSet
    '            MboardDesc = caption.Model
    '        Next

    '        'obtiene la cantidad de disco duro
    '        colSwbemObjectSet = _
    '            objSWbemServices.ExecQuery("Select * from " & _
    '            "Win32_DiskDrive")
    '        For Each objDisk In colSwbemObjectSet
    '            DiskIndex = objDisk.index
    '            MediaDisk = objDisk.mediatype()
    '            If MediaDisk <> "External hard disk media" Then
    '                'consulta por el tipo de disco duro
    '                arraydisk(DiskIndex) = objDisk.Size
    '            End If
    '        Next
    '        Do While i <> 9
    '            TotalSize = TotalSize + arraydisk(i)
    '            i = i + 1
    '        Loop
    '        TotalSize = (TotalSize / 1073741824)
    '        ReadHdisc = CDbl(TotalSize)
    '        'redondea la cantidad de disco duro
    '        ReadHdisc = Math.Round((ReadHdisc), 2)

    '        'obtiene la ram
    '        colSwbemObjectSet = _
    '           objSWbemServices.ExecQuery("Select * from " & _
    '            "Win32_ComputerSystem")
    '        For Each item In colSwbemObjectSet
    '            cRam = item.TotalPhysicalMemory
    '        Next item
    '        cRam = cRam / 1073741824 '''''1gb en bytes (1073741824)
    '        'redondea la cantidad de ram
    '        cRam = Math.Round((cRam), 2)

    '    Catch ex As Exception
    '        'MsgBox(ex.Message.ToString)
    '        Inmessage = "Error en Funcion ReadCpuSerial- Error: " & ex.Message.ToString
    '        Call LogErrorFileWriter(Inmessage, user)
    '    End Try


    'Dim wmi As Object
    'Dim processors As Object
    'Dim objSWbemServices As Object
    'Dim objSWbemLocator As Object
    'Dim cpu As Object
    'Dim cpu_id As String = ""
    'Dim cpuName As String = ""
    'Dim computer As String = ""
    ''Dim strComputer As String
    ''Dim colItems As Object
    'Dim LCompare As String = ""

    'Try
    '    LCompare = LCase(My.Computer.Name)
    '    If user = (My.Computer.Name) Or user = LCompare Then
    '        computer = "."
    '        wmi = GetObject("winmgmts:" & _
    '        "{impersonationLevel=impersonate}!\\" & _
    '        computer & "\root\cimv2")
    '        processors = wmi.ExecQuery("Select * from " & _
    '            "Win32_Processor")
    '        'processors = objSWbemServices.ExecQuery("Select * from " & _
    '        '    "Win32_Processor")
    '        For Each cpu In processors
    '            cpu_id = cpu.ProcessorId
    '            cpuName = cpu.name()
    '        Next cpu

    '        Cpuid = cpu_id
    '        CPUModel = cpuName
    '    Else
    '        'user = "10.177.1.41"
    '        objSWbemLocator = CreateObject("WbemScripting.SWbemLocator")
    '        objSWbemServices = objSWbemLocator.ConnectServer _
    '            (ip, "root\cimv2", "fabrikam\administrador", "acfadmin")
    '        objSWbemServices.Security_.ImpersonationLevel = 3
    '        processors = objSWbemServices.ExecQuery("Select * from " & _
    '            "Win32_Processor")
    '        For Each cpu In processors
    '            'cpu_ids = cpu_ids & ", " & cpu.ProcessorId
    '            cpu_id = cpu.ProcessorId
    '            cpuName = cpu.name()
    '        Next cpu
    '        Cpuid = cpu_id
    '        CPUModel = cpuName
    '    End If

    '    'Cpuid = cpu_id
    '    'CPUModel = cpuName

    'Catch ex As Exception
    '    'MsgBox(ex.Message.ToString)
    '    Inmessage = "Error al generar - ReadCpuSerial"
    '    Call LogErrorFileWriter(Inmessage, user)
    'End Try
    'End Function

    'Public Function ReadMboard(ByVal ip, usuario)

    '    Dim MboardDesc As String = ""
    '    Dim strComputer As String = "."
    '    Dim objSWbemServices As Object
    '    Dim colItems As Object
    '    Dim objSWbemLocator As Object
    '    Dim LCompare As String = ""
    '    Dim computer As String = ""
    '    Dim wmi As Object

    '    Try
    '        computer = "."
    '        wmi = GetObject("winmgmts:" & _
    '        "{impersonationLevel=impersonate}!\\" & _
    '        computer & "\root\cimv2")
    '        colItems = wmi.ExecQuery("Select * from " & _
    '            "Win32_ComputerSystem")
    '        'Win32_MotherboardDevice 
    '        'Win32_ComputerSystem

    '        For Each caption In colItems
    '            MboardDesc = caption.Model
    '        Next
    '        ReadMboard = MboardDesc


    '        'objWMIServices = CreateObject("WbemScripting.SWbemLocator")
    '        'objWMIServices = objSWbemLocator.ConnectServer _
    '        '    (ip, "root\cimv2", "fabrikam\administrador", "acfadmin")
    '        'objWMIServices.Security_.ImpersonationLevel = 3

    '        'colItems = objWMIServices.ExecQuery("Select * from " & _
    '        '    "Win32_ComputerSystem")
    '        objSWbemLocator = CreateObject("WbemScripting.SWbemLocator")
    '        objSWbemServices = objSWbemLocator.ConnectServer _
    '            (ip, "root\cimv2", "fabrikam\administrador", "acfadmin")
    '        objSWbemServices.Security_.ImpersonationLevel = 3

    '        colItems = objSWbemServices.ExecQuery("Select * from " & _
    '            "Win32_ComputerSystem")


    '        For Each caption In colItems
    '            'Dim MboardDesc2 As String = ""
    '            Dim MboardDesc3 As String = ""
    '            'Dim hola As String = ""
    '            MboardDesc = caption.Model
    '            'MboardDesc2 = caption.Manufacturer
    '            'MboardDesc3 = caption.name

    '            'hola = caption.Description
    '        Next
    '        ReadMboard = MboardDesc


    '    Catch ex As Exception
    '        'MsgBox(ex.Message.ToString)
    '        Inmessage = "Error al generar - ReadMboard"
    '        Call LogErrorFileWriter(Inmessage, usuario)
    '    End Try
    'End Function

    'Public Function ReadHdisc(ByVal ip, usuario)
    '    Dim objSWbemServices As Object
    '    Dim objSWbemLocator As Object
    '    Dim TotalSize As String = ""
    '    Dim colItems As Object
    '    Dim strComputer As String = "."


    '    Try
    '        '    objSWbemLocator = CreateObject("WbemScripting.SWbemLocator")
    '        '    objSWbemServices = objSWbemLocator.ConnectServer _
    '        '        (ip, "root\cimv2", "fabrikam\administrador", "acfadmin")
    '        '    objSWbemServices.Security_.ImpersonationLevel = 3

    '        '    colItems = objSWbemServices.ExecQuery("Select * from " & _
    '        '        "Win32_LogicalDisk where DeviceID='c:'")
    '        '    For Each objDisk In colItems
    '        '        'DiskA = objDisk.DeviceID.size
    '        '        DiskA = objDisk.DeviceID
    '        '        SizeA = objDisk.Size
    '        '    Next

    '        '    colItems = objSWbemServices.ExecQuery("Select * from " & _
    '        '        "Win32_LogicalDisk where DeviceID='d:'")
    '        '    For Each objDisk In colItems
    '        '        'DiskA = objDisk.DeviceID.size
    '        '        DiskB = objDisk.DeviceID
    '        '        SizeB = objDisk.Size
    '        '    Next

    '        '    'objWMIServiceA = GetObject("winmgmts:" & "{impersonationLevel=impersonate}!\\" & ip & "\root\cimv2:Win32_logicalDisk.DeviceID='c:'").Size
    '        '    'objWMIServiceB = GetObject("winmgmts:" & "{impersonationLevel=impersonate}!\\" & ip & "\root\cimv2:Win32_logicalDisk.DeviceID='e:'").Size

    '        '    If DiskB Is Nothing And SizeB Is Nothing Then
    '        '        MsgBox("no existe el disco d")
    '        '        'Cheque si existe el disco "E" , si no existe solo agrega "C"
    '        '        objWMIServiceA = DirectCast(objWMIServiceA, String)

    '        '        ReadHdisc = DiskA / 1073741824
    '        '    ElseIf objWMIServiceB <> Nothing Then
    '        '        'si existe agrega ambos discos al valor final:
    '        '        objWMIServiceA = DirectCast(objWMIServiceA, String)
    '        '        objWMIServiceB = DirectCast(objWMIServiceB, String)
    '        '        DiskA = CDbl(objWMIServiceA)
    '        '        DiskB = CDbl(objWMIServiceB)

    '        '        ReadHdisc = DiskA / 1073741824 + DiskB / 1073741824
    '        '    End If

    '        'End If

    '        objSWbemLocator = CreateObject("WbemScripting.SWbemLocator")
    '        objSWbemServices = objSWbemLocator.ConnectServer _
    '            (ip, "root\cimv2", "fabrikam\administrador", "acfadmin")
    '        objSWbemServices.Security_.ImpersonationLevel = 3

    '        colItems = objSWbemServices.ExecQuery("Select * from " & _
    '            "Win32_DiskDrive")
    '        For Each objDisk In colItems
    '            'DiskA = objDisk.DeviceID.size
    '            'DiskA = objDisk.DeviceID
    '            TotalSize = objDisk.Size
    '            TotalSize = (TotalSize / 1073741824)
    '        Next
    '        ReadHdisc = CDbl(TotalSize)

    '    Catch ex As Exception
    '        'MsgBox(ex.Message.ToString)
    '        Inmessage = "Error al generar - ReadHdisc"
    '        Call LogErrorFileWriter(Inmessage, usuario)
    '    End Try
    'End Function

    'Public Function ReadRam(ByVal ip, usuario)

    '    'Dim objWMIService As Object
    '    Dim objSWbemLocator As Object
    '    'Dim colItems As Object
    '    Dim computer As String = "."
    '    Dim wmi As Object
    '    Dim Rams As Object
    '    Dim cRam As String = ""
    '    Dim LCompare As String = ""
    '    Dim objSWbemServices As Object
    '    Try

    '        objSWbemLocator = CreateObject("WbemScripting.SWbemLocator")
    '        objSWbemServices = objSWbemLocator.ConnectServer _
    '            (ip, "root\cimv2", "fabrikam\administrador", "acfadmin")
    '        objSWbemServices.Security_.ImpersonationLevel = 3

    '        Rams = objSWbemServices.ExecQuery("Select * from " & _
    '            "Win32_ComputerSystem")

    '        For Each item In Rams
    '            'cpu_ids = cpu_ids & ", " & cpu.ProcessorId
    '            cRam = item.TotalPhysicalMemory
    '        Next item

    '        cRam = cRam / 1073741824 '''''1gb en bytes (1073741824)
    '        ReadRam = cRam


    '    Catch ex As Exception
    '        'MsgBox(ex.Message.ToString)
    '        Inmessage = "Error al generar - ReadRam"
    '        Call LogErrorFileWriter(Inmessage, usuario)
    '    End Try

    'End Function

End Module

'Public Function IsIpAddressValid(Address As String) As Boolean

'    Return System.Net.IPAddress.TryParse(Address, Nothing)

'End Function


'Public Function IsValidIPAddress(ByVal strIPAddress As String) As Boolean

'    Dim varAddress As Object, n As Long, lCount As Long
'    varAddress = Split(strIPAddress, ".", , vbTextCompare)

'    If IsArray(varAddress) Then
'        For n = LBound(varAddress) To UBound(varAddress)
'            lCount = lCount + 1
'            varAddress(n) = CByte(varAddress(n))
'        Next
'        IsValidIPAddress = (lCount = 4)
'    End If

'End Function



'Public Function GetHost(valorIp As String) As String

'valorIp = Replace(valorIp, ",", ".")
'Dim GetIP As Object
'Dim process As New Process()
'process.StartInfo.FileName = "cmd.exe"
'process.StartInfo.Arguments = "NBTSTAT -A " & valorIp
'process.StartInfo.UseShellExecute = False
'process.StartInfo.RedirectStandardOutput = True
'process.Start()

'' Synchronously read the standard output of the spawned process. 
'Dim reader As StreamReader = process.StandardOutput
'Dim output As String = reader.ReadToEnd()
'Console.WriteLine(output)

'process.WaitForExit()
'process.Close()

'Console.WriteLine(Environment.NewLine + Environment.NewLine + "Press any key to exit.")
'Console.ReadLine()

'End Function
'valorIp = Replace(valorIp, ",", ".")
'Dim GetIP As Object
'Dim objShell = CreateObject("wScript.Shell")
'Dim objScriptExec
'Dim strComputer = valorIp

'GetIP = Process.Start("cmd.exe", "/c ""NBTSTAT -A " & valorIp)

'objScriptExec = objShell.Exec("NBTSTAT -A " & strComputer)

'Dim strNBTResults = LCase(objScriptExec.StdOut.ReadAll)
'GetHost = Console.Read()
'lblTest.Text = "Result: " & strNBTResults
'End Function

'        Dim proc = New Process
'        Dim StartInfo = New ProcessStartInfo
'        Dim FileName = "program.exe",
'        Dim Arguments = "command line arguments to your executable",
'        Dim UseShellExecute As Boolean = False
'        Dim RedirectStandardOutput As Boolean = True
'        Dim CreateNoWindow As Boolean = True

'        proc.Start()
'        While (proc.StandardOutput.EndOfStream Is Not True)
'        Dim Line As string = proc.StandardOutput.ReadLine();
'    // do something with line
'}

'            valorIp = Replace(valorIp, ",", ".")

'            Dim GetIP As Object

'            'GetIP = Process.Start("NBTSTAT.exe -A" & valorIp)
'            GetIP = Process.Start("cmd", "/c ""NBTSTAT.exe -A" & valorIp)



'        GetIp = Process.Start("wmic:root\cli&gt;computersystem list brief /format:list")

'wmic.exe /node:"jdhit-dc01" /user:jdhitsolutions\administrator computersystem list brief /format:list
'Enter the password :*********
'Dim GetIP As Object
'Dim ruta As String = "wmic.exe /node" & valorIp & " Get ComputerSystem UserName"
'GetIP = Process.Start("wmic.exe /node" & valorIp & " Get ComputerSystem UserName")

'valorIp = Replace(valorIp, ",", ".")

'Dim User As String
'Dim strComputer As String = valorIp
'Dim strDomain As String = "DCACFCAPITAL"
'Dim strUser As String = "administrador"
'Dim strPassword As String = "magistral"
'Dim objSWbemLocator As Object
'Dim objSWbemServices As Object
'Dim colSwbemObjectSet As Object
'Dim ipadress As String = ""
''Shell(EXEC"Wscript.StdOut.Write 'Please enter your user name:'")
''strUser = Wscript.StdIn.ReadLine
''objPassword = CreateObject("ScriptPW.Password")
''Wscript.StdOut.Write "Please enter your password:"


'objSWbemLocator = CreateObject("WbemScripting.SWbemLocator")
'objSWbemServices = objSWbemLocator.ConnectServer(strComputer, _
'    "root\cimv2", _
'     strUser, _
'     strPassword, _
'     "MS_409", _
'     "ntlmdomain:" + strDomain)
'colSwbemObjectSet = _
'    objSWbemServices.ExecQuery("Select * From Win32_ComputerSystem")
'For Each objProcess In colSwbemObjectSet
'    User = User & objProcess.UserName
'Next

'GetHost = User


'wmic.exe /node:172.28.1.100 ComputerSystem Get UserName

'valorIp = Replace(valorIp, ",", ".")
'Try
'    Dim host As System.Net.IPHostEntry
'    ' host = System.Net.Dns.GetHostByAddress(valorIp)
''host = System.Net.Dns.GetHostEntry(valorIp)
'host = System.Net.Dns.GetHostEntry([system.net.ipaddress]valorIp2).HostName
'    'Obtiene el usuario y el dns
'    GetHost = host.HostName
'    'rescata solo el usuario
'    GetHost = GetHost.Substring(0, GetHost.IndexOf("."))

'    ''End If

'Catch ex As Exception
'    'MsgBox(ex.Message.ToString)
'    'Inmessage = "Error en Funcion GetHost"
'    'Call LogErrorFileWriter(Inmessage, usuario)
'End Try

''obtener valores de comandos de consola Nbtstat
'Dim p As New Process()
''Dim param As String = "NBTSTAT -A " & valorIp
'Dim param As String = "-A " & valorIp

'' this is the name of the process we want to execute 
'p.StartInfo.FileName = "NBTSTAT.exe"
''If Not (workingDir = "") Then
''p.StartInfo.WorkingDirectory = workingDir
''End If
'p.StartInfo.Arguments = param
'' need to set this to false to redirect output
'p.StartInfo.UseShellExecute = False
'p.StartInfo.RedirectStandardOutput = True
'' start the process 
'p.Start()
'' read all the output
'' here we could just read line by line and display it
'' in an output window 
'Dim output As String = p.StandardOutput.ReadToEnd
'' wait for the process to terminate 
'p.WaitForExit()
'Return output



'Dim myIPValue As String
'Private Sub TextBoxIP_KeyPress(KeyAscii As Integer)
'    Dim mySplit() As String, c As Integer, jumpC As Integer, tmpString As String, tmpSplit As String
'    If KeyAscii = 46 Then
'        mySplit = Split(TextBoxIP.Text, Chr(46), -1)
'        For c = 0 To UBound(mySplit)
'            If IsNumeric(mySplit(c)) Then jumpC = jumpC + 4
'            If Len(Trim(mySplit(c))) < 3 Then mySplit(c) = Space(3 - Len(Trim(mySplit(c)))) & Trim(mySplit(c))
'        Next c
'        tmpString = mySplit(0) & "." & mySplit(1) & "." & mySplit(2) & "." & mySplit(3)
'        TextBoxIP.Text = tmpString
'        TextBoxIP.SelectionStart = jumpC
'        KeyAscii = 0
'    End If
'End Sub
'Private Sub TextBoxIP_KeyUp(KeyCode As Integer, Shift As Integer)
'    Dim validSplit() As String, v As Integer, validString As String
'    validSplit = Split(TextBoxIP.Text, Chr(46), -1)
'    For v = 1 To UBound(validSplit)
'        If validSplit(v) = "" Or v = UBound(validSplit) Then
'            If Val(Trim(validSplit(v - 1))) > 255 Or (v = UBound(validSplit) And Val(Trim(validSplit(v))) > 255) Then
'                MsgBox("Invalid value")
'                If Val(validSplit(3)) > 0 Then v = v + 1
'                validSplit(v - 1) = ""
'                validString = validSplit(0) & "." & validSplit(1) & "." & validSplit(2) & "." & validSplit(3)
'                TextBoxIP.Text = validString
'                TextBoxIP.SelectionStart = (v - 1) * 4
'                Exit For
'            End If
'        End If
'    Next v
'    myIPValue = Replace(TextBoxIP.Text, " ", "")
'End Sub