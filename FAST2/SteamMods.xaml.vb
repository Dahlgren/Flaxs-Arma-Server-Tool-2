﻿Imports System.Threading
Imports FAST2.Models

Public Class SteamMods
    Private Shared _instance As SteamMods

    Public Shared ReadOnly Property Instance As SteamMods
        Get
            'If there is no instance or it has been destroyed...
            If _instance Is Nothing Then
                '...create a new one.
                _instance = New SteamMods
            End If

            Return _instance
        End Get
    End Property


    'Manages actions for steam mods tab buttons
    Private Sub IActionButtons_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles IModActionButtons.SelectionChanged
        
        If IAddSteamMod.IsSelected
            Dim importDialog As New ImportSteamMod
            importDialog.Show()
        ElseIf IAddLocalMod.IsSelected
            ModCollection.AddLocalMod()
        End If
        
        Dim thread As New Thread(
            Sub()
                Thread.Sleep(600)
                Dispatcher.Invoke(
                    Sub()
                        sender.SelectedItem = Nothing
                    End Sub
                    )
            End Sub
            )
        thread.Start()
    End Sub

    Private Sub SteamMods_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        UpdateModsView()
       
        
    End Sub

    Public Sub UpdateModsView

        'IModView.Items.Clear()

        If My.Settings.Mods IsNot Nothing
            IModView.DataContext = My.Settings.Mods.SteamMods
            
        End If

        'If My.Settings.Mods IsNot Nothing
        '    For Each steamMod In My.Settings.Mods.SteamMods
        '        IModView.Items.Add(steamMod)
        '    Next
        'End If
    End Sub
End Class
