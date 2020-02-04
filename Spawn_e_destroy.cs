using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_e_destroy : MonoBehaviour
{
	public GameObject plataformas_Para_instanciar;

	public float X_Pode_Instanciar;
	public float X_Spawn;
	public float Y_Spawn;

	private Transform transform_plataforma_existente;
	private Transform transform_interno;
	private bool pode_instanciar = true;
	private float vel_plataformas_import;

	void Start(){
		vel_plataformas_import = (plataformas_Para_instanciar.GetComponent<Movimento_plataformas>().velocidade_plataformas * .2f);
	}

    // Update is called once per frame
    void Update()
    {
    	transform_plataforma_existente = transform.GetChild(0);

    	if(transform_plataforma_existente.position.x < X_Pode_Instanciar && pode_instanciar){
    		Instantiate(plataformas_Para_instanciar, new Vector3(X_Spawn - vel_plataformas_import, Y_Spawn, 0), Quaternion.identity, this.transform);
    		pode_instanciar = false;
    	}

    	if(transform_plataforma_existente.position.x < X_Spawn * -1){
    		Destroy(transform_plataforma_existente.gameObject);
    		pode_instanciar = true;
    	}
    }
}
