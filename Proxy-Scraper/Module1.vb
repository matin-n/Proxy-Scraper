Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions

Module Module1

    Sub Main()

        Console.WriteLine("Press any key to start...")
        Console.ReadKey()
        Console.WriteLine("Process has started...")

        Dim values As List(Of Long) = New List(Of Long)
        scrapeProxies(values)

        Console.WriteLine("Done Scraping...")
        Console.WriteLine("Press any key to list the IP of the scraped proxies...")
        Console.ReadKey()

        For Each element In values
            Console.WriteLine(element)
        Next

        Console.ReadKey()
    End Sub

    Private Sub scrapeProxies(values As List(Of Long))
        Try
            Parallel.ForEach(File.ReadLines(Directory.GetCurrentDirectory() & "\proxysources.txt"),' New ParallelOptions With {.MaxDegreeOfParallelism = 10},
                             Sub(line)
                                 Dim value As String = New System.Net.WebClient().DownloadString(line)
                                 Dim matches As MatchCollection = Regex.Matches(value, "(\d{1,3}\.){3}\d{1,3}") '':(\d+)"

                                 For Each m As Match In matches
                                     For Each c As Capture In m.Captures
                                         values.Add(IPAddressToLong(IPAddress.Parse(c.Value)))
                                         Console.WriteLine(c.Value & " " & line)
                                     Next
                                 Next

                             End Sub)
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub

    Public Function IPAddressToLong(address As System.Net.IPAddress) As Long
        Dim byteIP As Byte() = address.GetAddressBytes()

        Dim ip As Long = CLng(byteIP(3)) << 24
        ip += CLng(byteIP(2)) << 16
        ip += CLng(byteIP(1)) << 8
        ip += CLng(byteIP(0))
        Return ip
    End Function

End Module