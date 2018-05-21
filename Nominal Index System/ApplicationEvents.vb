

Imports System.Threading 'to create a mutex which will ensure that only one application is running


Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Protected Overrides Function OnInitialize(ByVal commandLineArgs As System.Collections.ObjectModel.ReadOnlyCollection(Of String)) As Boolean
            objMutex = New Mutex(False, "Nominal_Index_System_APPMUTEX") 'creates the mutex
            ' If objMutex.WaitOne(0, False) = False Then
            'objMutex.Close()
            '  objMutex = Nothing
            ' MsgBox("Application is already running!", MsgBoxStyle.Information, AppName)
            '  End
            ' End If

            Me.MinimumSplashScreenDisplayTime = 5000
            Return MyBase.OnInitialize(commandLineArgs)
        End Function

        Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            e.BringToForeground = True
            frmMainInterface.WindowState = FormWindowState.Maximized
            frmMainInterface.BringToFront()
        End Sub

    End Class

End Namespace

