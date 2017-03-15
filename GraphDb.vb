Module GraphDb

    Public SrvUri As New Uri("http://localhost:7474/db/data")
    Public User As String = "root"
    Public Password As String = "root"

    Public Sub CreateNode(ByVal Label As String, ByVal Prop As String, ByVal Val As String)
        Dim CypherQuery As String = "(n:" + Label + "{ " + Prop + ": '" + Val + "'})"
        Dim clnt As New Neo4jClient.GraphClient(SrvUri, User, Password)
        clnt.Connect()
        Dim query = clnt.Cypher.Create(CypherQuery)
        query.ExecuteWithoutResults()
    End Sub

    Public Sub SetProperty(ByVal Label As String, ByVal Key As String, ByVal KeyVal As String, ByVal Prop As String, ByVal Val As String)
        Dim clnt As New Neo4jClient.GraphClient(SrvUri, User, Password)
        clnt.Connect()
        Dim query = clnt.Cypher.Match("(n:" + Label + " {" + Key + ": '" + KeyVal + "'})").Set("n." + Prop + "='" + Val + "'")
        query.ExecuteWithoutResults()
    End Sub

    Public Sub Relation(ByVal Relationship As String, ByVal Label1 As String, ByVal Label2 As String, ByVal Key1 As String, ByVal KeyVal1 As String, ByVal Key2 As String, ByVal KeyVal2 As String)
        Dim clnt As New Neo4jClient.GraphClient(SrvUri, User, Password)
        clnt.Connect()
        Dim query = clnt.Cypher.Match("((node1:" + Label1 + "))").Match("((node2:" + Label2 + "))").Where("( node1." + Key1 + " = '" + KeyVal1 + "' AND node2." + Key2 + " = '" + KeyVal2 + "' )").Create("((node1)-[r:" + Relationship + "]->(node2))")
        query.ExecuteWithoutResults()
    End Sub

    Public Function GetNodeProperty(ByVal Label As String, ByVal Prop As String, ByVal Key As String, ByVal KeyVal As String) As String
        Dim clnt As New Neo4jClient.GraphClient(SrvUri, User, Password)
        clnt.Connect()
        Dim query = clnt.Cypher.Match("((node:" + Label + "))").Where("( node." + Key + " = '" + KeyVal + "')").Return(Of String)("node." + Prop)
        If query.Results.Count > 0 Then
            Return query.Results.First
        End If
        Return "null"
    End Function

End Module
