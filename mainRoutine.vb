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
        Console.WriteLine()
        Console.WriteLine("--------------------------------")
        Console.WriteLine("Node List")
        Console.WriteLine("--------------------------------")
        nodeSet = New List(Of nodeElement)
        nodeSet = nodes.generate(nodeCount)

        ' Select Hub Node randomly from set
        Console.WriteLine()
        Console.WriteLine("--------------------------------")
        Console.WriteLine("Hub Node")
        Console.WriteLine("--------------------------------")
        hub = New nodeElement
        hub = nodes.selectHub(nodeSet)
        Console.WriteLine("Hub: " & hub.x & ", " & hub.y)

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
