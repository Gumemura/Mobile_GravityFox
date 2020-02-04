using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculo_aceleracao_gravidade : MonoBehaviour
{
	public float      aceleracao_adaptada;

	private float y;
	private float t_inicial;

	private bool pega_tempo_inicial = true;

	void Start()
	{
		y = transform.GetChild(0).position.y - transform.GetChild(1).position.y;
	}


	void Update()
	{
		transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale = Mathf.Abs(transform.parent.GetComponent<Rigidbody2D>().gravityScale);

		if(pega_tempo_inicial){
			t_inicial = Time.time;
			pega_tempo_inicial = false;
		}
		if(transform.GetChild(0).GetComponent<Rigidbody2D>().IsTouchingLayers(Physics2D.AllLayers)){
			aceleracao_adaptada = (y * 2) / Mathf.Pow(Time.time - t_inicial, 2);
			transform.GetChild(0).Translate(new Vector3(0, y, 0));
			pega_tempo_inicial = true;
		}
	}
}
