Imports System.Globalization

Public Module DateTimeExtensions
    ''' <summary>
    ''' Returns passed datetime with zero padding using current culture separators
    ''' </summary>
    ''' <param name="sender"><seealso cref="DateTime"/></param>
    ''' <returns>month zero padded/day zero padded/year zero padded</returns>
    ''' <remarks>
    ''' order of date parts year, month, day which can be changed to say month, day, year
    ''' </remarks>
    <System.Runtime.CompilerServices.Extension>
    Public Function ZeroPad(ByVal sender As DateTime) As String
        Dim dateSeparator As String = CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator
        Dim timeSeparator As String = CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator

        Return $"{sender.Year:D2}{dateSeparator}{sender.Month:D2}{dateSeparator}{sender.Day:D2} {sender.Hour:D2}{timeSeparator}{sender.Minute:D2}{timeSeparator}{sender.Second:D2}"
    End Function
End Module
