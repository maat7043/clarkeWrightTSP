Imports System.Linq
Public Class clarkeWright
    Public Shared Function savingsList() As List(Of demandPair)
        ' Create a list of savings pairs
        Dim savings As List(Of demandPair) = New List(Of demandPair)

        ' Generate a savings value for every node pair
        For x = 0 To nodeCount - 1
            For y = 0 To nodeCount - 1
                If Not y = x Then
                    Dim n As demandPair = New demandPair
                    n.nod1 = x
                    n.nod2 = y
                    n.savings = calculateSavings(x, y)

                    ' i,j is the same as j,i so only add once
                    Dim hasItem As Boolean = (From item In savings
                                                Where item.nod1 = y
                                                Where item.nod2 = x
                                                Select item).Any()
                    If Not hasItem Then
                        savings.Add(n)
                    End If
                End If
            Next
        Next

        ' Sort the list by greatest first
        savings.Sort(Function(x, y) x.savings.CompareTo(y.savings))
        savings.Reverse()

        ' Print the sorted demand pairs
        For i = 0 To savings.Count - 1
            Console.WriteLine("NODE(" & savings.Item(i).nod1 & "," & savings.Item(i).nod2 & "): " & savings.Item(i).savings)
        Next

        Return savings
    End Function

    ' Returns the relative savings obtained by linking this pair of nodes
    Private Shared Function calculateSavings(node1 As Integer, node2 As Integer) As Double
        Dim savings As Double
        Dim a As nodeElement = New nodeElement
        Dim b As nodeElement = New nodeElement
        Dim c As nodeElement = New nodeElement

        a = nodeSet.Item(node1)
        b = nodeSet.Item(node2)
        c = hub

        savings = nodeDistance(a, c) + nodeDistance(b, c) - nodeDistance(a, b)

        Return savings
    End Function

    ' Returns the distance between two nodes
    Private Shared Function nodeDistance(a As nodeElement, b As nodeElement) As Double
        Dim dist As Double
        Dim dx As Integer = b.x - a.x
        Dim dy As Integer = b.y - a.y

        dist = Math.Sqrt(dx ^ 2 + dy ^ 2)

        Return dist
    End Function

    ' Process savings list to determine a route
    Public Shared Sub determinePath()
        Dim currentPath As path = New path

        ' Read through the list of savings pairs
        For i = 0 To savList.Count - 1

            ' form the two nodes into elements of the set
            Dim n1 As nodeElement = New nodeElement
            Dim n2 As nodeElement = New nodeElement
            n1 = nodeSet.Item(savList.Item(i).nod1)
            n2 = nodeSet.Item(savList.Item(i).nod2)

            ' Case 1: both nodes are unused
            If n1.uses = 0 And n2.uses = 0 Then
                ' Create a new link
                Dim l As link = New link
                l.node1 = savList.Item(i).nod1
                l.node2 = savList.Item(i).nod2

                ' Establish a new route
                Dim r As route = New route
                r.links.Add(l)

                ' Add the route to the path
                currentPath.routes.Add(r)

                ' Update the node uses
                nodeSet.Item(savList.Item(i).nod1).uses += 1
                nodeSet.Item(savList.Item(i).nod2).uses += 1

                ' Case 2a: First node used second not is not
            ElseIf n1.uses = 1 And n2.uses = 0 Then
                ' loop through routes in path
                For j = 0 To currentPath.routes.Count - 1
                    ' loop through links in route
                    For k = 0 To currentPath.routes.Item(j).links.Count - 1
                        ' (a,b) ->(a,c) to (b,a) -> (a,c)
                        If currentPath.routes.Item(j).links.Item(k).node1 = savList.Item(i).nod1 Then
                            ' reverse all preceeding links
                            For z = 0 To k
                                Dim t As Integer = currentPath.routes.Item(j).links.Item(z).node1
                                currentPath.routes.Item(j).links.Item(z).node1 = currentPath.routes.Item(j).links.Item(z).node2
                                currentPath.routes.Item(j).links.Item(z).node2 = t
                            Next

                            ' Create a new link
                            Dim l As link = New link
                            l.node1 = savList.Item(i).nod1
                            l.node2 = savList.Item(i).nod2

                            ' add the new link to the current route
                            currentPath.routes.Item(j).links.Add(l)

                            ' Update the node uses
                            nodeSet.Item(savList.Item(i).nod1).uses += 1
                            nodeSet.Item(savList.Item(i).nod2).uses += 1

                            ' (b,a) ->(a,c)
                        ElseIf currentPath.routes.Item(j).links.Item(k).node2 = savList.Item(i).nod1 Then
                            ' Create a new link
                            Dim l As link = New link
                            l.node1 = savList.Item(i).nod1
                            l.node2 = savList.Item(i).nod2

                            ' add the new link to the current route
                            currentPath.routes.Item(j).links.Add(l)

                            ' Update the node uses
                            nodeSet.Item(savList.Item(i).nod1).uses += 1
                            nodeSet.Item(savList.Item(i).nod2).uses += 1
                        End If
                    Next
                Next

                ' Case 2b: second node only is used
            ElseIf n1.uses = 0 And n2.uses = 1 Then
                ' loop through routes in path
                For j = 0 To currentPath.routes.Count - 1
                    ' loop through links in route
                    For k = 0 To currentPath.routes.Item(j).links.Count - 1
                        ' (a,b) -> (c,b) to (a,b) -> (b,c)
                        If currentPath.routes.Item(j).links.Item(k).node2 = savList.Item(i).nod2 Then

                            ' Create a new link
                            Dim l As link = New link
                            l.node1 = savList.Item(i).nod2 ' note the new link is reversed
                            l.node2 = savList.Item(i).nod1

                            ' add the new link to the current route
                            currentPath.routes.Item(j).links.Add(l)

                            ' Update the node uses
                            nodeSet.Item(savList.Item(i).nod1).uses += 1
                            nodeSet.Item(savList.Item(i).nod2).uses += 1

                            ' (b,a) -> (c,b) to (a,b) -> (b,c)
                        ElseIf currentPath.routes.Item(j).links.Item(k).node1 = savList.Item(i).nod2 Then
                            ' reverse all preceeding links
                            For z = 0 To k
                                Dim t As Integer = currentPath.routes.Item(j).links.Item(z).node1
                                currentPath.routes.Item(j).links.Item(z).node1 = currentPath.routes.Item(j).links.Item(z).node2
                                currentPath.routes.Item(j).links.Item(z).node2 = t
                            Next

                            ' Create a new link
                            Dim l As link = New link
                            l.node1 = savList.Item(i).nod2 ' note the new link is reversed
                            l.node2 = savList.Item(i).nod1

                            ' add the new link to the current route
                            currentPath.routes.Item(j).links.Add(l)

                            ' Update the node uses
                            nodeSet.Item(savList.Item(i).nod1).uses += 1
                            nodeSet.Item(savList.Item(i).nod2).uses += 1
                        End If
                    Next
                Next

                ' Case 3: Both nodes have been used
            Else

            End If

            ' Print the current routes
            For j = 0 To currentPath.routes.Count - 1
                Console.Write("hub->")
                For k = 0 To currentPath.routes(j).links.Count - 1
                    Console.Write(currentPath.routes(j).links.Item(k).node1 & "->" & currentPath.routes(j).links.Item(k).node2 & "->")
                Next
                Console.Write("hub    ")
            Next
            Console.WriteLine()
        Next
    End Sub
End Class
