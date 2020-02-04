using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento_plataformas : MonoBehaviour
{
	private Transform plataformas_transform;

	public float velocidade_plataformas;
	// Start is called before the first frame update
    void Start()
    {
        plataformas_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
     	plataformas_transform.Translate(new Vector3(velocidade_plataformas * Time.deltaTime * -1, 0, 0));
    }
}
