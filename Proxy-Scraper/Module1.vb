Imports System.IO
Imports System.Text.RegularExpressions

Module Module1

    Sub Main()

        Console.WriteLine("Press any key to start...")
        Console.ReadKey()
        Console.WriteLine("Process has started...")

        Dim values As List(Of String) = New List(Of String)

        Try
            Parallel.ForEach(File.ReadLines(Directory.GetCurrentDirectory() & "\proxysources.txt"),' New ParallelOptions With {.MaxDegreeOfParallelism = 10},
                             Sub(line)
                                 Dim value As String = New System.Net.WebClient().DownloadString(line)
                                 Dim matches As MatchCollection = Regex.Matches(value, "(\d{1,3}\.){3}\d{1,3}") '':(\d+)"

                                 For Each m As Match In matches
                                     For Each c As Capture In m.Captures
                                         values.Add(c.Value)
                                         Console.WriteLine(c.Value & " " & line)
                                     Next
                                 Next

                             End Sub)
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