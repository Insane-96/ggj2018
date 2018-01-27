using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}


    //crea un overlavsphere ogni click di un tasto e se dentro l'overlap ci sono enemy attiva l 'interfaccia Inoisedetected
   /* private void MakeNoise()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, noiseRadius);
        foreach (Collider col in cols)
        {
            IListener Ilistener = col.GetComponent<IListener>();
            if (Ilistener == null)
                continue;

            Ilistener.Listen(transform.position);
        }
    }*/

}
