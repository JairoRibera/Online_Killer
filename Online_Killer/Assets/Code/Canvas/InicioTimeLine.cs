using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class InicioTimeLine : MonoBehaviour
{
    public PlayableDirector timeline;

    void Start()
    {
        // Pausamos la Timeline al inicio
        timeline.time = 0;
        timeline.Evaluate();
        timeline.Pause();
    }

    // Este método se llamará desde el botón
    public void IniciarTimeline()
    {
        timeline.Play();
    }
}
