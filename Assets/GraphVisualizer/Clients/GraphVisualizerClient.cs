using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;

// Bridge between runtime and editor code: the graph created in runtime code can call GraphVisualizerClient.Show(...)
// and the EditorWindow will register itself with the client to display any available graph.
public class GraphVisualizerClient
{
    static private GraphVisualizerClient s_Instance;
    private Dictionary<PlayableGraph, string> m_Graphs = new Dictionary<PlayableGraph, string>();

    static public GraphVisualizerClient instance
    {
        get
        {
            if (s_Instance == null)
                s_Instance = new GraphVisualizerClient();
            return s_Instance;
        }
    }
    ~GraphVisualizerClient()
    {
        m_Graphs.Clear();
    }
    static public void Show(PlayableGraph graph, string name)
    {
        if (!instance.m_Graphs.ContainsKey(graph))
        {
            instance.m_Graphs.Add(graph, name);
        }
    }
    static public void Hide(PlayableGraph graph)
    {
        if (instance.m_Graphs.ContainsKey(graph))
        {
            instance.m_Graphs.Remove(graph);
        }
    }

    static public IEnumerable<KeyValuePair<PlayableGraph, string>> GetGraphs()
    {
        return instance.m_Graphs;
    }
}
