using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_player : MonoBehaviour
{
	public LayerMask 	plataforma_contato;
	public GameObject 	plataformas_velocidade;
	public GameObject 	placar;

	private Rigidbody2D 		rb_2d_jogador;
	private BoxCollider2D 		box_collider_jogador;
	private RaycastHit2D 		raycast;
	private	Vector2 			posicao_raycast;

	// Start is called before the first frame update
	void Start()
	{
		rb_2d_jogador 				= GetComponent<Rigidbody2D>();
		box_collider_jogador 		= GetComponent<BoxCollider2D>();

		posicao_raycast = box_collider_jogador.bounds.center;
	}

	// Update is called once per frame

	private float 	distance_player_plataforma = 0;
	private float 	rotacao;
	private float 	y_contato; 
	private int 	alternante;

	void Update()
	{	
		rotacao_jogador();
	}

	void rotacao_jogador(){
		float velocidade_plats = plataformas_velocidade.GetComponent<Movimento_plataformas>().velocidade_plataformas;

		rb_2d_jogador.gravityScale = transform.parent.GetComponent<Rigidbody2D>().gravityScale;
		float aceleracao_ajustada = rb_2d_jogador.gravityScale * (9 + .8f);

		RaycastHit2D raycast = Physics2D.Raycast(posicao_raycast, Vector2.up * Mathf.Sign(rb_2d_jogador.gravityScale), Mathf.Infinity, plataforma_contato);
		posicao_raycast = new Vector2(velocidade_plats * (raycast.distance/ Mathf.Abs(aceleracao_ajustada)), box_collider_jogador.bounds.center.y - (box_collider_jogador.bounds.extents.y * Mathf.Sign(rb_2d_jogador.gravityScale)));

		if(box_collider_jogador.IsTouchingLayers(plataforma_contato)){
			foreach(Touch touch in Input.touches){
				if (touch.phase == TouchPhase.Began){
					distance_player_plataforma = raycast.distance;
					y_contato = box_collider_jogador.bounds.center.y - (box_collider_jogador.bounds.extents.y * Mathf.Sign(rb_2d_jogador.gravityScale));
					alternante =  -1 * (int)Mathf.Sign(y_contato);
				}
			}
		}

		//Raio metade
		Debug.DrawRay(new Vector2(-10, y_contato + (distance_player_plataforma * alternante/ 2)	), Vector2.right * 10, Color.blue, 1, true);

		//Raio distancia
		Debug.DrawRay(posicao_raycast, Vector2.up * raycast.distance * Mathf.Sign(rb_2d_jogador.gravityScale), Color.red, 1, true);

		if(box_collider_jogador.bounds.center.y > y_contato + (distance_player_plataforma * alternante/ 2)){
			rotacao = 180;
		}else{
			rotacao = 0;
		}

		transform.rotation = new Quaternion(rotacao, 0, 0, 0);
	}
}