Imports System.Collections.ObjectModel
Imports XAMLPowerToys.Model
'
<Serializable()> Public Class UIControls
    Inherits ObservableCollection(Of UIControl)

#Region " Declarations "

    Private _bolAutoAppendExecute As Boolean = True

#End Region

#Region " Properties "

    Public Property AutoAppendExecute() As Boolean
        Get
            Return _bolAutoAppendExecute
        End Get
        Set(ByVal Value As Boolean)
            _bolAutoAppendExecute = Value
        End Set
    End Property

#End Region

#Region " Constructors "

    Public Sub New()
    End Sub

#End Region

#Region " Methods "

    ''' <summary>
    '''     This method only takes a CheckBox, ComboBox, DatePicker, Image, Label, TextBlock or TextBox and returns a control.
    ''' </summary>
    ''' <param name="enumUIControlType" type="XAMLPowerToys.Model.ControlType">
    '''     <para>
    '''         
    '''     </para>
    ''' </param>
    ''' <param name="enumUIPlatform" type="XAMLPowerToys.UI.UIPlatform">
    '''     <para>
    '''         
    '''     </para>
    ''' </param>
    ''' <returns>
    '''     A XAMLPowerToys.UI.UIControl
    ''' </returns>
    ''' <exception cref="ArgumentOutOfRangeException">Throws ArgumentOutOfRangeException when an invalid ControlType is passed.</exception>
    Public Function GetUIControl(ByVal enumUIControlType As ControlType, ByVal enumUIPlatform As UIPlatform) As UIControl

        Dim enumUIControlRole As UIControlRole

        Select Case enumUIControlType

            Case ControlType.CheckBox

                enumUIControlRole = UIControlRole.CheckBox

            Case ControlType.ComboBox

                enumUIControlRole = UIControlRole.ComboBox

            Case ControlType.Image

                enumUIControlRole = UIControlRole.Image

            Case ControlType.Label

                enumUIControlRole = UIControlRole.Label

            Case ControlType.TextBlock

                enumUIControlRole = UIControlRole.TextBlock

            Case ControlType.TextBox

                enumUIControlRole = UIControlRole.TextBox

            Case ControlType.DatePicker

                enumUIControlRole = UIControlRole.DatePicker

            Case Else
                Throw New ArgumentOutOfRangeException("enumUIControlType", enumUIControlType, "Programmer did not program this value.")
        End Select

        Return GetUIControl(enumUIControlRole, enumUIPlatform)
    End Function

    Public Function GetUIControl(ByVal enumUIControlRole As UIControlRole, ByVal enumUIPlatform As UIPlatform) As UIControl

        For Each ctrl As UIControl In Me

            If ctrl.ControlRole = enumUIControlRole AndAlso ctrl.UIPlatform = enumUIPlatform Then
                Return ctrl
            End If

        Next

        Return Nothing
    End Function

    Public Function GetUIControlsForPlatform(ByVal enumUIPlatform As UIPlatform) As List(Of UIControl)
        Return (From d In Me Where d.UIPlatform = enumUIPlatform Select d Order By d.ControlRole.ToString).ToList
    End Function

#End Region

End Class
