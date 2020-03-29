Imports System.Net.IPAddress
Imports System.Windows.Forms.MaskedTextBox
Imports System
Imports System.Drawing
Imports System.Windows.Forms
'Imports ModulesON
'Imports GlobalVariables



Public Class RescueInfoPC
    Private showCheckedNodesButton As Button

    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click

        'Dim m As New GV
        Dim Usuario As String = ""
        Dim IpAddress As String = ""
        Dim adminuser As String = ""
        Dim pass As String = ""
        Dim valorHost As String = ""
        'Dim IP As String = ""
        Dim user As String = ""
        Dim Cpuid As String = ""
        Dim CPUModel As String = ""
        Dim MBoard As String = ""
        Dim Hdisc As Double = 0
        Dim Ram As Double = 0
        Dim Process As String = ""
        Dim ArrayListProcess As New ArrayList
        Dim ArrayListCpu As New ArrayList
        Dim ArrayListMBoard As New ArrayList
        Dim ArraySizeHDisk As New ArrayList
        Dim ArraySpecsHDisk As New ArrayList
        Dim TotalSize As Long = 0
        TreeViewProcess.CheckBoxes = True
        'showCheckedNodesButton = New Button
        Dim strComputer As String = "10.177.1.41"
        Dim strDomain As String = "DCACFCAPITAL"
        Dim strUser As String = "administrador"
        Dim strPassword As String = "magistral"
        Dim MboardDesc As String = ""
        Dim MediaDisk As String = ""
        Dim cRam As Double = 0


        If UsuarioTbox.Text = "" And IpTbox.Text <> "" Then
            'validaciones
            If Len(IpTbox.Text) > 15 Or Len(IpTbox.Text) < 10 Then
                MsgBox("El valor debe ser una IP valida, ejemplo 10.177.1.255")
            Else

                GV.IP = IpTbox.Text
                'limpia la ip
                GV.IP = Replace(GV.IP, ",", ".")
                GV.IP = Replace(GV.IP, " ", "")

                user = GetHost(GV.IP)
                UsuarioTbox.Text = user

                'se asignan valores a las variables
                'user = UsuarioTbox.Text
                'user = UsuarioTbox.Text
                GV.adminuser = AdminTbox.Text
                GV.Password = PassTbox.Text

                If user <> "" Then
                    If GV.adminuser <> "" And GV.Password <> "" Then
                        '' ''Call GetProcessByUser(Usuario, adminuser, pass)
                        Call GetSpecs(GV.IP, user, ArrayListProcess, ArrayListCpu, ArrayListMBoard, ArraySizeHDisk, ArraySpecsHDisk, TotalSize, Ram, adminuser, pass)
                        '' ''Dim GrandfatherNOde As TreeNode = tree.nodes.add("Grandfather")
                        '' ''Dim fatherNode As TreeNode = GrandfatherNOde.Nodes.Add("Father")
                        '' ''Dim sonNode As TreeNode = fatherNode.Nodes.Add("Son")

                        Dim xfatherNode As TreeNode
                        Dim xSonNode As TreeNode

                        '' 'llena  procesos al treeview
                        xfatherNode = TreeViewProcess.Nodes.Add("Procesos:")
                        For Each Proc In ArrayListProcess
                            xSonNode = xfatherNode.Nodes.Add(Proc)
                        Next
                        MsgBox("k")


                        'For Each TreeViewProcess As TreeNode In eNode.Node.Nodes
                        '    xSonNode.Checked = eNode.Node.Checked = False
                        'Next

                        '' 'llena datos procesador al treeview  
                        'xfatherNode = TreeViewSpecs.Nodes.Add("Procesador:")
                        'For Each Spec In ArrayListCpu
                        '    xSonNode = xfatherNode.Nodes.Add(Spec)
                        'Next

                        ' '' 'llena datos Mother al treeview
                        'xfatherNode = TreeViewSpecs.Nodes.Add("Placa Madre:")
                        'For Each Spec In ArrayListMBoard
                        '    xSonNode = xfatherNode.Nodes.Add(Spec)
                        'Next

                        ' '' 'llena total de capacidad de disco
                        'xfatherNode = TreeViewSpecs.Nodes.Add("Total de HDD:")
                        'xSonNode = TreeViewSpecs.Nodes.Add(TotalSize & "Gb")

                        ' '' 'llena datos Disco Duro logicos al treeview
                        'xfatherNode = TreeViewSpecs.Nodes.Add("Discos Duros Logicos:")
                        'For Each Spec In ArraySpecsHDisk
                        '    xSonNode = xfatherNode.Nodes.Add(Spec)
                        'Next

                        '' 'llena total de ram
                        'xfatherNode = TreeViewSpecs.Nodes.Add("Ram:")
                        'xSonNode = xfatherNode.Nodes.Add(Ram & "GB")


                        '' Set the checked state of one of the nodes to
                        '' demonstrate the showCheckedNodesButton button behavior.
                        'For Each xSonNode In TreeViewProcess.Nodes
                        '    'TreeViewProcess.Nodes(0).Nodes(xSonNode).Checked = True
                        '    xSonNode = TreeViewProcess.Nodes(0).Nodes(xSonNode).Checked = True
                        'Next

                        '' Initialize showCheckedNodesButton.
                        'showCheckedNodesButton.Size = New Size(144, 24)
                        'showCheckedNodesButton.Text = "Show Checked Nodes"
                        'AddHandler showCheckedNodesButton.Click, AddressOf showCheckedNodesButton_Click

                        '' Initialize the form.
                        'Me.ClientSize = New Size(292, 273)
                        'Me.Controls.AddRange(New Control() {showCheckedNodesButton, treeView1})

                        'Me.ResumeLayout(False)


                        'Dim nodeChecked As Boolean = False
                        ''Dim Checknode As TreeNode
                        'For Each xSonNode In TreeViewProcess.Nodes
                        '    xSonNode.Checked = nodeChecked
                        '    If xSonNode.Nodes.Count > 0 Then
                        '        ' If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                        '        Me.CheckAllChildNodes(xSonNode, nodeChecked)
                        '    End If
                        'Next xSonNode


                        ' ''' Crea los Check box en los datos en el tree view
                        'Dim eNode As System.Windows.Forms.TreeViewEventArgs

                        'Dim childNodeCK As TreeNode = eNode.Node
                        'Dim NodeChecked As Boolean = eNode.Node.Checked

                        'For Each ChildNode As TreeNode In childNodeCK.Nodes
                        '    If NodeChecked = True Then
                        '        ChildNode.Checked = NodeChecked
                        '    End If
                        'Next

                        'If childNodeCK.Checked = False Then
                        '    If eNode.Node.Parent Is Nothing = False Then
                        '        eNode.Node.Parent.Checked = False
                        '    End If
                        'End If
                        'Dim nodeChecked = True
                        'Call CheckAllChildNodes(xSonNode, nodeChecked)

                    Else
                        MsgBox("El cammpo de administrador no se puede dejar vacio, tampoco la contraseña")
                    End If
                Else
                    MsgBox("El cammpo de usuario no se puede dejar vacio")
                End If
            End If
        ElseIf (UsuarioTbox.Text) <> "" And (IpTbox.Text) = "" Then
            If IsNumeric(UsuarioTbox.Text) = True Then
                MsgBox("El valor no puede ser en mumeros, debe ser un nombre de usuario, ejemplo Hgonzalez")
            ElseIf IsNumeric(UsuarioTbox.Text) = False Then
                valorHost = UsuarioTbox.Text
                Usuario = valorHost
                IpTbox.Text = GetIp(valorHost)

                'se asignan valores a las variables
                user = UsuarioTbox.Text
                GV.adminuser = AdminTbox.Text
                GV.Password = PassTbox.Text

                If user <> "" Then
                    If adminuser <> "" And pass <> "" Then

                    Else
                        MsgBox("El campo de administrador no se puede dejar vacio, tampoco la contraseña")
                    End If
                Else
                    MsgBox("El campo de usuario no se puede dejar vacio")
                End If
            End If
        End If


        'Me.Controls.AddRange(New Control() {button2, TreeViewProcess})

        'Me.ResumeLayout(False)
    End Sub 'NewNew

    '<STAThreadAttribute()> _
    'Shared Sub Main()
    '    Application.Run(New TreeViewProcess)
    'End Sub 'Main

    Public Sub button2_Click_1(sender As Object, e As EventArgs) Handles button2.Click
        ' Disable redrawing of treeView1 to prevent flickering 
        ' while changes are made.
        TreeViewProcess.BeginUpdate()

        ' Collapse all nodes of treeView1.
        TreeViewProcess.CollapseAll()

        ' Add the CheckForCheckedChildren event handler to the BeforeExpand event.
        AddHandler TreeViewProcess.BeforeExpand, AddressOf CheckForCheckedChildren

        ' Expand all nodes of treeView1. Nodes without checked children are 
        ' prevented from expanding by the checkForCheckedChildren event handler.
        TreeViewProcess.ExpandAll()

        ' Remove the checkForCheckedChildren event handler from the BeforeExpand 
        ' event so manual node expansion will work correctly.
        RemoveHandler TreeViewProcess.BeforeExpand, AddressOf CheckForCheckedChildren

        ' Enable redrawing of treeView1.
        TreeViewProcess.EndUpdate()
    End Sub 'showCheckedNodesButton_Click

    ' Prevent expansion of a node that does not have any checked child nodes.
    Public Sub CheckForCheckedChildren(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs)
        If Not HasCheckedChildNodes(e.Node) Then
            e.Cancel = True
        End If
    End Sub 'CheckForCheckedChildren

    ' Returns a value indicating whether the specified 
    ' TreeNode has checked child nodes.
    Public Function HasCheckedChildNodes(ByVal node As TreeNode) As Boolean
        'Dim m As New GlobalVariables
        If node.Nodes.Count = 0 Then
            Return False
        End If
        Dim childNode As TreeNode
        For Each childNode In node.Nodes
            If childNode.Checked Then
                'Return True
                'Call KillProcess(TreeViewProcess, childNode.Checked)
                GV.ProcessName = childNode.Text
                Call KillProcess(TreeViewProcess, HasCheckedChildNodes(childNode))

            End If
            ' Recursively check the children of the current child node.
            If HasCheckedChildNodes(childNode) Then
                'Return True
                Call KillProcess(TreeViewProcess, HasCheckedChildNodes(childNode))
                GV.ProcessName = childNode.Name
            End If
        Next childNode
        Return False
    End Function 'HasCheckedChildNodes


    'Private Sub showCheckedNodesButton_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    ' Disable redrawing of treeView1 to prevent flickering 
    '    ' while changes are made.
    '    TreeViewProcess.BeginUpdate()

    '    ' Collapse all nodes of treeView1.
    '    TreeViewProcess.CollapseAll()

    '    ' Add the CheckForCheckedChildren event handler to the BeforeExpand event.
    '    AddHandler TreeViewProcess.BeforeExpand, AddressOf CheckForCheckedChildren

    '    ' Expand all nodes of treeView1. Nodes without checked children are 
    '    ' prevented from expanding by the checkForCheckedChildren event handler.
    '    TreeViewProcess.ExpandAll()

    '    ' Remove the checkForCheckedChildren event handler from the BeforeExpand 
    '    ' event so manual node expansion will work correctly.
    '    RemoveHandler TreeViewProcess.BeforeExpand, AddressOf CheckForCheckedChildren

    '    ' Enable redrawing of treeView1.
    '    TreeViewProcess.EndUpdate()
    'End Sub 'showCheckedNodesButton_Click

    '' Prevent expansion of a node that does not have any checked child nodes.
    'Private Sub CheckForCheckedChildren(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs)
    '    If Not HasCheckedChildNodes(e.Node) Then
    '        e.Cancel = True
    '    End If
    'End Sub 'CheckForCheckedChildren

    '' Returns a value indicating whether the specified 
    '' TreeNode has checked child nodes.
    'Private Function HasCheckedChildNodes(ByVal node As TreeNode) As Boolean
    '    If node.Nodes.Count = 0 Then
    '        Return False
    '    End If
    '    Dim childNode As TreeNode
    '    For Each childNode In node.Nodes
    '        If childNode.Checked Then
    '            Return True
    '        End If
    '        ' Recursively check the children of the current child node.
    '        If HasCheckedChildNodes(childNode) Then
    '            Return True
    '        End If
    '    Next childNode
    '    Return False
    'End Function 'HasCheckedChildNodes

    'Public Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
    '    Dim ArrayCheckedNode As ArrayList
    '    Call KillProcess(TreeViewProcess, ArrayCheckedNode)
    'End Sub

    'Private Sub button2_Click_1(sender As Object, e As EventArgs) Handles button2.Click

    'End Sub
End Class 'Form1 


    '' Updates all child tree nodes recursively.
    'Private Sub CheckAllChildNodes(treeNode As TreeNode, nodeChecked As Boolean)
    '    Dim node As TreeNode
    '    For Each node In treeNode.Nodes
    '        node.Checked = nodeChecked
    '        If node.Nodes.Count > 0 Then
    '            ' If the current node has child nodes, call the CheckAllChildsNodes method recursively.
    '            Me.CheckAllChildNodes(node, nodeChecked)
    '        End If
    '    Next node
    'End Sub

    '' NOTE   This code can be added to the BeforeCheck event handler instead of the AfterCheck event.
    '' After a tree node's Checked property is changed, all its child nodes are updated to the same value.
    'Private Sub node_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles TreeViewProcess.AfterCheck
    '    ' The code only executes if the user caused the checked state to change.
    '    If e.Action <> TreeViewAction.Unknown Then
    '        If e.Node.Nodes.Count > 0 Then
    '            ' Calls the CheckAllChildNodes method, passing in the current 
    '            ' Checked value of the TreeNode whose checked state changed. 
    '            Me.CheckAllChildNodes(e.Node, e.Node.Checked)
    '        End If
    '    End If
    'End Sub


    ''Sub CheckChildNodes(ByVal Parent As TreeNode, ByVal Checked As Boolean)
    ''    For Each child As TreeNode In Parent.Nodes
    ''        child.Checked = Checked
    ''        If child.Nodes.Count > 0 Then
    ''            CheckChildNodes(child, Checked)
    ''        End If
    ''    Next
    ''End Sub
    '' Updates all child tree nodes recursively.
    'Private Sub CheckAllChildNodes(treeNode As TreeNode, nodeChecked As Boolean)
    '    Dim node As TreeNode
    '    For Each node In treeNode.Nodes
    '        node.Checked = nodeChecked
    '        If node.Nodes.Count > 0 Then
    '            ' If the current node has child nodes, call the CheckAllChildsNodes method recursively.
    '            Me.CheckAllChildNodes(node, nodeChecked)
    '        End If
    '    Next node
    'End Sub

    '' NOTE   This code can be added to the BeforeCheck event handler instead of the AfterCheck event.
    '' After a tree node's Checked property is changed, all its child nodes are updated to the same value.
    'Private Sub node_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles TreeViewProcess.AfterCheck
    '    ' The code only executes if the user caused the checked state to change.
    '    If e.Action <> TreeViewAction.Unknown AndAlso e.Node.Checked Then
    '        If e.Node.Nodes.Count > 0 Then
    '            ' Calls the CheckAllChildNodes method, passing in the current 
    '            ' Checked value of the TreeNode whose checked state changed. 
    '            Me.CheckAllChildNodes(e.Node, e.Node.Checked)
    '        End If
    '    End If
    'End Sub

'End Class
Public Class GV
    'Variable unica para los logs
    Public Shared Inmessage As String = ""
    Public Shared IP As String = ""
    Public Shared ProcessName As Object
    Public Shared adminuser As String = ""
    Public Shared Password As String = ""
End Class

