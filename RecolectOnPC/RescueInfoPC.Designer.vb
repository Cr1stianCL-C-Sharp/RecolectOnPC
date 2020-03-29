<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RescueInfoPC
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.BtnBuscar = New System.Windows.Forms.Button()
        Me.UsuarioTbox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.IpTbox = New System.Windows.Forms.MaskedTextBox()
        Me.AdminTbox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.PassTbox = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TabBusqueda = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TreeViewSpecs = New System.Windows.Forms.TreeView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.button2 = New System.Windows.Forms.Button()
        Me.TreeViewProcess = New System.Windows.Forms.TreeView()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabBusqueda.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnBuscar
        '
        Me.BtnBuscar.Location = New System.Drawing.Point(185, 41)
        Me.BtnBuscar.Name = "BtnBuscar"
        Me.BtnBuscar.Size = New System.Drawing.Size(74, 28)
        Me.BtnBuscar.TabIndex = 0
        Me.BtnBuscar.Text = "Buscar"
        Me.BtnBuscar.UseVisualStyleBackColor = True
        '
        'UsuarioTbox
        '
        Me.UsuarioTbox.Location = New System.Drawing.Point(19, 44)
        Me.UsuarioTbox.Name = "UsuarioTbox"
        Me.UsuarioTbox.Size = New System.Drawing.Size(160, 22)
        Me.UsuarioTbox.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(265, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 17)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Por Ip:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 17)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Por Usuario:"
        '
        'IpTbox
        '
        Me.IpTbox.Location = New System.Drawing.Point(265, 44)
        Me.IpTbox.Mask = "##.###.###.###"
        Me.IpTbox.Name = "IpTbox"
        Me.IpTbox.Size = New System.Drawing.Size(140, 22)
        Me.IpTbox.TabIndex = 6
        '
        'AdminTbox
        '
        Me.AdminTbox.Location = New System.Drawing.Point(19, 48)
        Me.AdminTbox.Name = "AdminTbox"
        Me.AdminTbox.Size = New System.Drawing.Size(160, 22)
        Me.AdminTbox.TabIndex = 9
        Me.AdminTbox.Text = "administrador"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(156, 17)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Usuario Administrador :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(265, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 17)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Constraseña:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.IpTbox)
        Me.GroupBox1.Controls.Add(Me.BtnBuscar)
        Me.GroupBox1.Controls.Add(Me.UsuarioTbox)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(43, 26)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(485, 83)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Busqueda:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.PassTbox)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.AdminTbox)
        Me.GroupBox2.Location = New System.Drawing.Point(43, 124)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(485, 87)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Credenciales:"
        '
        'PassTbox
        '
        Me.PassTbox.Location = New System.Drawing.Point(265, 49)
        Me.PassTbox.Name = "PassTbox"
        Me.PassTbox.Size = New System.Drawing.Size(140, 22)
        Me.PassTbox.TabIndex = 12
        Me.PassTbox.UseSystemPasswordChar = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(43, 238)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(117, 17)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Especificaciones:"
        '
        'TabBusqueda
        '
        Me.TabBusqueda.Controls.Add(Me.TabPage1)
        Me.TabBusqueda.Controls.Add(Me.TabPage2)
        Me.TabBusqueda.Location = New System.Drawing.Point(12, 12)
        Me.TabBusqueda.Name = "TabBusqueda"
        Me.TabBusqueda.SelectedIndex = 0
        Me.TabBusqueda.Size = New System.Drawing.Size(589, 595)
        Me.TabBusqueda.TabIndex = 7
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TreeViewSpecs)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(581, 566)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Busqueda"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TreeViewSpecs
        '
        Me.TreeViewSpecs.Location = New System.Drawing.Point(6, 267)
        Me.TreeViewSpecs.Name = "TreeViewSpecs"
        Me.TreeViewSpecs.Size = New System.Drawing.Size(569, 293)
        Me.TreeViewSpecs.TabIndex = 14
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.button2)
        Me.TabPage2.Controls.Add(Me.TreeViewProcess)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(581, 566)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Procesos"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'button2
        '
        Me.button2.Location = New System.Drawing.Point(344, 11)
        Me.button2.Name = "button2"
        Me.button2.Size = New System.Drawing.Size(131, 23)
        Me.button2.TabIndex = 1
        Me.button2.Text = "Terminar Proceso"
        Me.button2.UseVisualStyleBackColor = True
        '
        'TreeViewProcess
        '
        Me.TreeViewProcess.Location = New System.Drawing.Point(26, 40)
        Me.TreeViewProcess.Name = "TreeViewProcess"
        Me.TreeViewProcess.Size = New System.Drawing.Size(514, 503)
        Me.TreeViewProcess.TabIndex = 0
        '
        'RescueInfoPC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(626, 651)
        Me.Controls.Add(Me.TabBusqueda)
        Me.Name = "RescueInfoPC"
        Me.Text = "RescueInfoPC"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabBusqueda.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnBuscar As System.Windows.Forms.Button
    Friend WithEvents UsuarioTbox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents IpTbox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents AdminTbox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TabBusqueda As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents PassTbox As System.Windows.Forms.TextBox
    Friend WithEvents TreeViewProcess As System.Windows.Forms.TreeView
    Friend WithEvents TreeViewSpecs As System.Windows.Forms.TreeView
    Friend WithEvents button2 As System.Windows.Forms.Button

End Class
