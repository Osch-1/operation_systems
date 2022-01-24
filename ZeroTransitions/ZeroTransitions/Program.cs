using operationSystemLecser;
class Program 
{
    static void Main( string[] args )
    {
        string[] strings = File.ReadAllLines( "../../../1.txt" );
        var grammaType = strings[ 0 ];

        Dictionary<string, List<Connection>> connections = new Dictionary<string, List<Connection>>();

        for ( int i = 1; i < strings.Length; i++ )
        {
            List<Connection> list = new();

            var item = strings[ i ];
            var from = item.Split( " -> " ).First();
            var elements = item.Split( " -> " )[ 1 ].Split( " | " );
            foreach ( var element in elements )
            {
                Connection connection = null;
                if ( element.Length == 1 )
                {
                    connection = new Connection( from, "F", element );
                }
                else
                {
                    connection = new Connection( from, element[ 1 ].ToString(), element[ 0 ].ToString() );
                }

                bool isContains = false;
                foreach ( var listItem in list )
                {
                    if ( listItem.By == connection.By )
                    {
                        isContains = true;
                        listItem.To += connection.To;
                    }
                }
                if ( !isContains ) list.Add( connection );
            }

            connections.Add( from.Sort(), list );
        }

        connections.Add( "F", new List<Connection> { } );

        var newConnections = new Dictionary<string, List<Connection>>();
        var mergedNodes = new List<string>();
        foreach ( var item in connections )
        {
            var tempMergedNodes = new List<string> { };
            foreach ( var transition in item.Value )
            {
                if ( transition.By == "e" )
                {
                    if ( !mergedNodes.Contains( transition.From + transition.To ) )
                        mergedNodes.Add( transition.From + transition.To );
                }
            }

            var mergedNode = "";
            foreach ( var tempNode in tempMergedNodes )
            {
                foreach ( var tempNodeChar in tempNode )
                {
                    if ( mergedNode.Contains( tempNodeChar ) )
                    {
                        mergedNode += tempNodeChar;
                    }
                }
            }
            if ( mergedNode != "" )
            {
                mergedNodes.Add( mergedNode );
            }
        }

        for ( int i = 0; i < mergedNodes.Count; i++ )
        {
            var idsForDelete = new List<int>();

            for ( int j = 0; j < mergedNodes.Count; j++ )
            {
                if ( mergedNodes[ i ] != mergedNodes[ j ] )
                {
                    foreach ( var item in mergedNodes[ j ] )
                    {
                        if ( mergedNodes[ i ].Contains( item ) )
                        {
                            idsForDelete.Add( j );
                            break;
                        }
                    }
                }
            }
            if ( idsForDelete.Any() )
            {
                foreach ( var id in idsForDelete )
                {
                    mergedNodes[ i ] += mergedNodes[ id ];
                }
                mergedNodes[ i ] = new string( mergedNodes[ i ].Distinct().ToArray() );
                mergedNodes[ i ] = mergedNodes[ i ].Sort();
            }
        }
        mergedNodes = mergedNodes.Distinct().ToList();

        foreach ( var item in mergedNodes )
        {
            var keysToDelete = new List<string>();
            var transitionsToAdd = new List<Connection>();
            foreach ( var connection in connections )
            {
                if ( item.Contains( connection.Key ) )
                {
                    keysToDelete.Add( connection.Key );
                    foreach ( var transition in connection.Value )
                    {
                        if ( mergedNodes.Any( item => IsContain( item, transition.To ) ) && transition.By != "e" )
                        {
                            transitionsToAdd.Add( new Connection( item, mergedNodes.First( item => IsContain( item, transition.To ) ), transition.By ) );
                        }
                        else if ( transition.By != "e" )
                        {
                            transitionsToAdd.Add( new Connection( item, transition.To, transition.By ) );
                        }
                    }
                }
                foreach ( var to in connection.Value )
                {
                    if ( IsContain( item, to.To ) )
                    {
                        to.To = item;
                    }
                }
            }

            keysToDelete = keysToDelete.Distinct().ToList();
            transitionsToAdd = transitionsToAdd.Distinct().ToList();
            foreach ( var deleteConnection in keysToDelete )
            {
                connections.Remove( deleteConnection );
            }
            connections.Add( item, transitionsToAdd );
        }

        int tempCount = 0;
        while ( connections.Count != tempCount )
        {
            tempCount = connections.Count;
            var tempConnections = new Dictionary<string, List<Connection>>();
            foreach ( var collectionItem in connections )
            {
                foreach ( var item in collectionItem.Value )
                {
                    if ( !connections.ContainsKey( item.To.Sort() ) )
                    {
                        tempConnections.Add( item.To, GetConnectionsFromValues( item.To, connections ) );
                    }
                }
            }
            foreach ( var connection in tempConnections )
            {
                connections.Add( connection.Key.Sort(), connection.Value );
            }
        }

        var lines = new List<string> { };
        foreach ( var connection in connections )
        {
            var line = $"{new string( connection.Key.Distinct().ToArray() )} ";
            foreach ( var item in connection.Value )
            {
                line += $"{new string( item.To.Distinct().ToArray() )}({item.By}) ";
            }
            if ( connection.Value.Count != 0 )
                lines.Add( line );
        }

        File.WriteAllLines( "../../../out.txt", lines );
    }

    static List<Connection> GetConnectionsFromValues( string values, Dictionary<string, List<Connection>> connections )
    {
        var result = new List<Connection>();

        foreach ( var item in values )
        {
            if ( connections.ContainsKey( item.ToString() ) )
            {
                foreach ( var element in connections[ item.ToString() ] )
                {
                    if ( !result.Select( item => item.By ).Contains( element.By ) )
                    {
                        result.Add( new Connection( element.From, element.To, element.By ) );
                    }
                    else
                    {
                        if ( !result.Find( item => item.By == element.By ).To.Contains( element.To ) )
                        {
                            result.Find( item => item.By == element.By ).To += element.To;
                        }
                    }
                }
            }
        }

        return result;
    }

    static bool IsContain( string where, string what )
    {
        foreach ( var symbol in what )
        {
            if ( !where.Contains( symbol ) )
            {
                return false;
            }
        }
        return true;
    }
}