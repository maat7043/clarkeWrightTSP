Module mainRoutine
    Public nodeCount As Integer
    Public nodeSet As IList(Of nodeElement)
    Public hub As nodeElement
    Public savList As List(Of demandPair)


    Sub Main()
        ' Console Window Setup
        Console.Title = "Sorting Algorithm Comparison"
        Console.ForegroundColor = ConsoleColor.White
        Console.WindowLeft = 0
        Console.WindowTop = 0

        ' Publication/Version Info
        Console.WriteLine("Implementation of the Clarke-Wright Algorithm to solve the TSP")
        Console.WriteLine("Written in VB.Net")
        Console.WriteLine("by Matt Spencer")
        Console.WriteLine("")
        Console.WriteLine("Version 1.0 Updated 2015.4.15")
        Console.WriteLine("")
        Console.Write("Press any key to continue...")
        Console.ReadKey()
        Console.Clear()

        ' Data Input
        Console.Write("How many nodes are in your system: ")
        nodeCount = Console.ReadLine

        ' Generate X random Nodes between +-10000 x and y
        nodeSet = New List(Of nodeElement)
        nodeSet = nodes.generate(nodeCount)

        ' Select Hub Node randomly from set
        
        hub = New nodeElement
        hub = nodes.selectHub(nodeSet)

        ' Print the node set to console
        Console.WriteLine()
        Console.WriteLine("--------------------------------")
        Console.WriteLine("Node List")
        Console.WriteLine("--------------------------------")
        Console.WriteLine("Hub: " & hub.x & ", " & hub.y)
        For i = 0 To nodeCount - 1
            Console.WriteLine("Node(" & i & "): " & nodeSet.Item(i).x & ", " & nodeSet.Item(i).y)
        Next

        ' Generate savings pairs
        Console.WriteLine()
        Console.WriteLine("--------------------------------")
        Console.WriteLine("Savings Pairs:")
        Console.WriteLine("--------------------------------")
        savList = New List(Of demandPair)
        savList = clarkeWright.savingsList

        ' Determine the optimal route
        Console.WriteLine()
        Console.WriteLine("--------------------------------")
        Console.WriteLine("Route Progression:")
        Console.WriteLine("--------------------------------")
        clarkeWright.determinePath()


        Console.ReadKey()

    End Sub

End Module
