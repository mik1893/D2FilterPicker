Imports System.IO
Imports System.Net
Imports Microsoft.Win32
Imports Newtonsoft.Json

Class MainWindow

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        ' Read loot list and setup the combobox
        Dim rawjson = File.ReadAllText(My.Application.Info.DirectoryPath & "\filters.json")
        Dim lst = JsonConvert.DeserializeObject(Of List(Of LootFilter))(rawjson)
        For Each i In lst
            filterComboBox.Items.Add(i)
        Next
        filterComboBox.DisplayMemberPath = "Name"
        filterComboBox.SelectedValuePath = "Url"

        If File.Exists(My.Settings.LocalFilterPath) Then
            loadCurrentFilter(My.Settings.LocalFilterPath)
        Else
            browseForFile()
        End If


    End Sub
    Private Sub applyButton_Click(sender As Object, e As RoutedEventArgs) Handles applyButton.Click
        Try

            If filterTextBox.Text IsNot Nothing Then
                If currentPathLabel.Content IsNot Nothing Then
                    My.Computer.FileSystem.WriteAllText(currentPathLabel.Content, filterTextBox.Text, False)
                    loadCurrentFilter(currentPathLabel.Content)
                End If
            End If
        Catch ex As Exception
            MsgBox("Can't apply the loot filter:" & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub filterComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles filterComboBox.SelectionChanged
        Try
            Dim address As String = filterComboBox.SelectedValue.ToString()
            Dim client As WebClient = New WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
            filterTextBox.Text = reader.ReadToEnd
        Catch ex As Exception
            MsgBox("Can't load the loot filter:" & vbCrLf & ex.Message)
        End Try
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

        loadCurrentFilter(path)
    End Sub

    Private Sub loadCurrentFilter(path As String)
        currentPathLabel.Content = path
        Dim Fs As StreamReader
        Dim currentFilter As String
        Fs = New StreamReader(path)
        currentFilter = Fs.ReadToEnd()
        currentTextBox.Text = currentFilter
    End Sub

End Class


