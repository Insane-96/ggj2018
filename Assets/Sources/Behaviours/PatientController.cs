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

    //crea un overlavsphere e se dentro l'overlap ci sono enemy attiva il metodo NoiseDetection
    private void MakeNoise(Vector3 center,float noiseRadius, Transform NoisePosition)
    {
        Collider[] cols = Physics.OverlapSphere(center, noiseRadius);
        foreach (Collider col in cols)
        {
            IEnemy enemy = col.GetComponent<IEnemy>();
            if (enemy == null)
                continue;

            enemy.NoiseDetection(NoisePosition);
        }
    }
}
