Public Class nodes
    Public Shared Function generate(nodeCount As Integer) As IList(Of nodeElement)
        Dim nodeSet As IList(Of nodeElement) = New List(Of nodeElement)
        Dim rnd As Random = New Random
        For i = 0 To nodeCount - 1
            Dim n As nodeElement = New nodeElement
            n.x = rnd.Next(-10000, 10000)
            n.y = rnd.Next(-10000, 10000)
            nodeSet.Add(n)
        Next
        Return nodeSet
    End Function

    Public Shared Function selectHub(ByRef nodeSet As IList(Of nodeElement)) As nodeElement
        Dim D As nodeElement = New nodeElement
        Dim rnd As Random = New Random

        Dim index As Integer = rnd.Next(0, nodeSet.Count - 1)

        D = nodeSet.Item(index)
        nodeSet.RemoveAt(index)
        nodeCount -= 1

        Return D
    End Function
End Class
