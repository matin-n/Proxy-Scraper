Imports System.IO
Imports System.Text.RegularExpressions

Module Module1

    Sub Main()

        Console.WriteLine("Press any key to start...")
        Console.ReadKey()
        Console.WriteLine("Process has started...")

        Dim values As List(Of String) = New List(Of String)

        Try
            For Each line In File.ReadLines(Directory.GetCurrentDirectory() & "\proxysources.txt")
                Dim value As String = New System.Net.WebClient().DownloadString(line)
                Dim matches As MatchCollection = Regex.Matches(value, "(\d{1,3}\.){3}\d{1,3}") '':(\d+)"

                For Each m As Match In matches
                    For Each c As Capture In m.Captures
                        values.Add(c.Value)
                    Next
                Next

            Next
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try

        Console.WriteLine("Done Scraping...")
        Console.WriteLine("Press any key to list the IP of the scraped proxies...")
        Console.ReadKey()

        For Each element In values
            Console.WriteLine(element)
        Next

        Console.ReadKey()
    End Sub

End Module