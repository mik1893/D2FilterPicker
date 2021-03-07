Imports System.IO
Imports System.Net
Imports Microsoft.Win32
Imports Newtonsoft.Json

Class MainWindow

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        ' Actions on application load
        loadFilterList()

        If File.Exists(My.Settings.LocalFilterPath) Then
            loadCurrentFilter(My.Settings.LocalFilterPath)
            currentPathTextBox.Text = My.Settings.LocalFilterPath
        Else
            browseForFile()
        End If
    End Sub

    Private Sub loadFilterList()
        ' Read loot list and setup the combobox
        filterComboBox.Items.Clear()
        Dim localFilterList As String = My.Application.Info.DirectoryPath & "\" & My.Settings.LocalFilterList
        Dim rawjson = File.ReadAllText(localFilterList)
        Dim lst = JsonConvert.DeserializeObject(Of List(Of LootFilter))(rawjson)
        For Each i In lst
            filterComboBox.Items.Add(i)
        Next
        filterComboBox.DisplayMemberPath = "Name"
        filterComboBox.SelectedValuePath = "Url"
    End Sub
    Private Sub applyButton_Click(sender As Object, e As RoutedEventArgs) Handles applyButton.Click
        Try

            If filterTextBox.Text IsNot Nothing Then
                If currentPathTextBox.Text IsNot Nothing Then
                    My.Computer.FileSystem.WriteAllText(currentPathTextBox.Text, filterTextBox.Text, False)
                    loadCurrentFilter(currentPathTextBox.Text)
                End If
            End If
        Catch ex As Exception
            MsgBox("Can't apply the loot filter:" & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub filterComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles filterComboBox.SelectionChanged
        If (e.AddedItems.Count > 0) Then
            Try
                Dim address As String = filterComboBox.SelectedValue.ToString()
                Dim client As WebClient = New WebClient()
                Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
                filterTextBox.ScrollToHome()
                filterTextBox.Text = reader.ReadToEnd
            Catch ex As Exception
                MsgBox("Can't load the loot filter:" & vbCrLf & ex.Message)
            End Try
        End If
    End Sub

    Private Sub browseButton_Click(sender As Object, e As RoutedEventArgs) Handles browseButton.Click
        browseForFile()
    End Sub

    Private Sub browseForFile()
        Dim OpenFd As New OpenFileDialog()
        With OpenFd
            .FileName = ""
            .Title = "Open Text File"
            .InitialDirectory = "c:\"
            .Filter = "D2 Item Filter|*.filter"
            .ShowDialog()
        End With
        Dim path As String = OpenFd.FileName

        My.Settings.LocalFilterPath = path
        My.Settings.Save()
        currentPathTextBox.Text = path
        loadCurrentFilter(path)
    End Sub

    Private Sub loadCurrentFilter(path As String)
        Try
            Dim Fs As StreamReader
            Dim currentFilter As String
            Fs = New StreamReader(path)
            currentFilter = Fs.ReadToEnd()
            currentTextBox.Text = currentFilter
        Catch ex As Exception
            MsgBox("Can't load the loot filter:" & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub updateButton_Click(sender As Object, e As RoutedEventArgs) Handles updateButton.Click
        Try
            Dim address As String = My.Settings.RemoteFilterList
            Dim client As WebClient = New WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
            Dim localFilterList As String = My.Application.Info.DirectoryPath & "\" & My.Settings.LocalFilterList
            My.Computer.FileSystem.WriteAllText(localFilterList, reader.ReadToEnd, False)
            loadFilterList()
            filterTextBox.Text = ""
        Catch ex As Exception
            MsgBox("Can't load the loot filter:" & vbCrLf & ex.Message)
        End Try
    End Sub

End Class


