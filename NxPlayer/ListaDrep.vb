Imports WMPLib

Public Class ListaDrep

    Public IcanToMove As Boolean = False
    Dim IcanResize As Boolean = False


    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click
        Me.Hide()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If CheckBox1.CheckState = CheckState.Checked Then
            IcanToMove = True
            IcanResize = True
            PictureBox6.Visible = True
        Else
            Me.Left = Form1.Left + Form1.Width + 5
            Me.Top = Form1.Top
            Me.Height = Form1.Height
            IcanToMove = False
            IcanResize = False
            Me.Width = 280
            PictureBox6.Visible = False
        End If



    End Sub


    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click

        Dim NxFileDialog As New OpenFileDialog

        NxFileDialog.Multiselect = True
        'NxFileDialog.Filters.Add("Archivo de audio")
        'NxFileDialog.Filters.Add("Archivo de vídeo")
        'NxFileDialog.Filters.Add("Lista de reproduccion")

        'If NxFileDialog.ShowDlg() = Windows.Forms.DialogResult.OK Then
        If NxFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then

            For Each File As String In NxFileDialog.FileNames

                Dim eInfo As New IO.FileInfo(File)

                If eInfo.Extension.ToLower = ".nxpl" Then

                    LoadPlayList(File)
                Else
                    LoadToPlayList(File)
                End If

            Next

        End If

    End Sub


    Private Sub ListView1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick

        'Try
        If ListView1.Items(0).Index > -1 Then

            Reprnum = ListView1.SelectedItems(0).Index
            If ListView1.SelectedItems(0).Tag = "Audio" Then

                Form1.AxWindowsMediaPlayer1.URL = ListView1.SelectedItems(0).ToolTipText
                Form1.AxWindowsMediaPlayer1.Visible = False
                Ventana_emergente.PictureBox1.BackgroundImage = My.Resources.note_38578_640
                'lCMD = "+" & ListView1.SelectedItems(0).ToolTipText

            ElseIf ListView1.SelectedItems(0).Tag = "Video" Then

                Form1.AxWindowsMediaPlayer1.URL = ListView1.SelectedItems(0).ToolTipText
                Form1.AxWindowsMediaPlayer1.Visible = True
                Form1.AxWindowsMediaPlayer1.Location = Form1.PictureBox2.Location
                Form1.AxWindowsMediaPlayer1.Size = Form1.PictureBox2.Size
                Ventana_emergente.PictureBox1.BackgroundImage = My.Resources.videoT
                'lCMD = "-" & ListView1.SelectedItems(0).ToolTipText
            Else

                Form1.Label3.Text = "<Error! No media file>"
                Ventana_emergente.Label1.Text = "<Error! No media file>"
                Form1.AxWindowsMediaPlayer1.Ctlcontrols.stop()
                'lCMD = "+" & ListView1.SelectedItems(0).ToolTipText

            End If


        End If
        'Catch ex As Exception

        'End Try

    End Sub

    Dim ToDelete As Boolean = False

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

        If ToDelete = False Then
            ListView1.MultiSelect = True
            PictureBox1.Image = My.Resources.MsgEstulyGood
            ToDelete = True
        Else
            For Each elemento As ListViewItem In ListView1.SelectedItems()
                ListView1.SelectedItems(0).Remove()
            Next

            PictureBox1.Image = My.Resources.ListaMenosN
            ToDelete = False
            ListView1.MultiSelect = False
        End If




    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        Try

            If ListView1.Items.Count > 0 Then
                Label1.Text = ListView1.SelectedItems(0).Text
            Else
                Label1.Text = ""
            End If
        Catch ex As Exception

        End Try



    End Sub




    Sub LoadToPlayList(ByVal Efile As String)

        Dim track As String = Efile

        Dim Par As Integer = 0

        Dim tipoM As String

        Dim fileProperty As System.IO.FileInfo

        Dim lviElemento As System.Windows.Forms.ListViewItem


        fileProperty = My.Computer.FileSystem.GetFileInfo(track)

        If fileProperty.Extension = ".mp3" Then
            Par = 0
            tipoM = "Audio"
        ElseIf fileProperty.Extension = ".MP3" Then
            Par = 0
            tipoM = "Audio"
        ElseIf fileProperty.Extension = ".wav" Then
            Par = 0
            tipoM = "Audio"
        ElseIf fileProperty.Extension = ".WAV" Then
            Par = 0
            tipoM = "Audio"
        ElseIf fileProperty.Extension = ".cda" Then
            Par = 0
            tipoM = "Audio"
        ElseIf fileProperty.Extension = ".CDA" Then
            Par = 0
            tipoM = "Audio"
        ElseIf fileProperty.Extension = ".MP4" Then
            Par = 1
            tipoM = "Video"
        ElseIf fileProperty.Extension = ".mp4" Then
            Par = 1
            tipoM = "Video"
        ElseIf fileProperty.Extension = ".3GP" Then
            Par = 1
            tipoM = "Video"
        ElseIf fileProperty.Extension = ".3gp" Then
            Par = 1
            tipoM = "Video"
        ElseIf fileProperty.Extension = ".FLV" Then
            Par = 1
            tipoM = "Video"
        ElseIf fileProperty.Extension = ".WMV" Then
            Par = 1
            tipoM = "Video"
        ElseIf fileProperty.Extension = ".wmv" Then
            Par = 1
            tipoM = "Video"
        ElseIf fileProperty.Extension = ".flv" Then
            Par = 1
            tipoM = "Video"
        ElseIf fileProperty.Extension = ".AVI" Then
            Par = 1
            tipoM = "Video"
        ElseIf fileProperty.Extension = ".avi" Then
            Par = 1
            tipoM = "Video"
        ElseIf fileProperty.Extension = ".WMA" Then
            Par = 0
            tipoM = "Audio"
        ElseIf fileProperty.Extension = ".wma" Then
            Par = 0
            tipoM = "Audio"
        ElseIf fileProperty.Extension = ".vob" Then
            Par = 1
            tipoM = "Video"
        ElseIf fileProperty.Extension = ".VOB" Then
            Par = 1
            tipoM = "Video"
        Else
            Par = 2
            tipoM = "Desconocido"
        End If



        Dim UrlWMP As String
        Try
            UrlWMP = track
            Dim mp3 As New ID3TagLibrary.MP3File(UrlWMP)
            ImageList1.Images.Add(mp3.Tag2.Artwork(1))
            Par = ImageList1.Images.Count - 1
        Catch ex As Exception

        End Try


        fileProperty = My.Computer.FileSystem.GetFileInfo(track)
        lviElemento = New System.Windows.Forms.ListViewItem(fileProperty.Name, Par)
        lviElemento.SubItems.Add(fileProperty.FullName)
        lviElemento.SubItems.Add(fileProperty.Extension)
        lviElemento.ToolTipText = track
        lviElemento.Tag = tipoM
        ListView1.Items.Add(lviElemento)

        lviElemento = Nothing


    End Sub


    '________________________________________________
    Private aaa As Boolean = False
    Private MouseX As Integer
    Private MouseY As Integer
    '_________________________________________________

    Private Sub Panel1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            aaa = True
            MouseX = e.X
            MouseY = e.Y

        End If
    End Sub

    Private Sub Panel1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseMove
        If IcanToMove = True Then
            If aaa = True Then
                Dim tmp As Point = New Point

                tmp.X = Me.Location.X + (e.X - MouseX)
                tmp.Y = Me.Location.Y + (e.Y - MouseY)
                Me.Location = tmp
                tmp = Nothing


            End If
        End If

    End Sub

    Private Sub Panel1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            aaa = False

        End If
    End Sub




    '________________________________________________________________________
    Dim x As Integer, y As Integer, a As Integer = x, b As Integer = y
    Private XY As Point
    '________________________________________________________________________


    Private Sub PictureBox6_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox6.MouseDown
        XY.X = CInt(CLng(x))
        XY.Y = CInt(CLng(y))

    End Sub

    Private Sub PictureBox6_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox6.MouseMove
        If IcanResize = True Then
            If e.Button = Windows.Forms.MouseButtons.Right Or e.Button = Windows.Forms.MouseButtons.Left Then
                'redimensionamos el ancho
                If (Me.Width + (x + e.X)) > 0 Then

                    Me.Width = Me.Width + (x + e.X)
                End If
                'redimensionams el alto
                If (Me.Height + (y + e.Y)) > 0 Then

                    Me.Height = Me.Height + (y + e.Y)
                End If
            End If
        End If


    End Sub


    'Sub GetRepList()

    '    For Each dfile As String In My.Computer.FileSystem.GetFiles("N:\Nexus\AppWork\NxReproductor\AddRepList")

    '        Dim eInfo As New IO.FileInfo(dfile)

    '        If eInfo.Extension.ToLower = ".nxlp" Then

    '            LoadPlayList(dfile)

    '        End If

    '    Next

    'End Sub

    'Sub ClearAll()

    '    For Each dfile As String In My.Computer.FileSystem.GetFiles("N:\Nexus\AppWork\NxReproductor\AddRepList")

    '        My.Computer.FileSystem.DeleteFile(dfile)

    '    Next

    'End Sub


    'Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick

    '    Try
    '        Dim Gett As String = IO.File.ReadAllText("N:\Nexus\AppWork\NxReproductor\RepList")

    '        If Gett = "True" Then
    '            GetRepList()
    '            'IO.File.WriteAllText("N:\Nexus\AppWork\NxReproductor\RepList", "False")
    '            ClearAll()
    '        Else
    '            Exit Sub
    '        End If
    '    Catch ex As Exception

    '    End Try



    'End Sub

    Private Sub ListaDrep_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        ColumnHeader1.Width = ListView1.Width - 4
    End Sub

    Private Sub Form1_Deactivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate
        Panel1.BackColor = Color.DarkGray
        Panel2.BackColor = Color.DarkGray

    End Sub

    Private Sub Form1_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        'Panel1.BackColor = SystmColor
        ' Panel2.BackColor = SystmColor
    End Sub

    ' Dim getUser As String = IO.File.ReadAllText("N:\Nexus\AppWork\NxDisckExplorer\SDroot")
    'Dim This As String = "N:\Users"

    Sub LoadPlayList(ByVal ListPath As String)

        Dim rFile As String = IO.File.ReadAllText(ListPath)

        Dim xCh As Char
        Dim Pila As String = ""
        Dim Counter As Integer = 3

        For Each xCh In rFile
            Counter = Counter + 1

            If xCh = "|" Then
                Counter = 0

                LoadToPlayList(Pila)
                Pila = ""

            Else
                If Counter > 2 Then
                    Pila = Pila & xCh
                End If
            End If

        Next


    End Sub


    Sub savePlaylist()
        ' Dim Route As String = This & "\" & getUser & "\User\Music\Saved play lists"
        Dim Route As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments + "/nxPlaylists"


        Dim toSave As String = ""
        Dim x As Boolean = True

        For Each ListItem As ListViewItem In Me.ListView1.Items

            If x = True Then
                toSave = ListItem.ToolTipText & "|" & vbNewLine
                x = False
            Else
                toSave = toSave & ListItem.ToolTipText & "|" & vbNewLine
            End If

        Next

        Dim dat As String = Now.Date
        Dim tim As String = TimeOfDay

        dat = dat.Replace("\", "")
        dat = dat.Replace("/", "")
        tim = tim.Replace(":", "")

        If IO.Directory.Exists(Route) Then
            IO.File.WriteAllText(Route & "\" & dat & "_" & tim & ".nxpl", toSave)
            MsgBox("Playlist saved (...Documents\nxPlaylists)")
        Else
            IO.Directory.CreateDirectory(Route)
            IO.File.WriteAllText(Route & "\" & dat & "_" & tim & ".nxpl", toSave)
            MsgBox("Playlist saved (...Documents\nxPlaylists)")

        End If




    End Sub

    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        If ListView1.Items.Count > 0 Then
            savePlaylist()
        End If

    End Sub

End Class