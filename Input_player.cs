using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_player : MonoBehaviour
{
	public LayerMask 	plataforma_contato;
	public LayerMask 	inimigo_contato;
	public GameObject 	plataformas_velocidade;
	public GameObject 	placar;

	private Rigidbody2D 		rb_2d_jogador;
	private BoxCollider2D 		box_collider_jogador;
	private RaycastHit2D 		raycast;
	private	Vector2 			posicao_raycast;

	private float 	x_inicial;
	private float 	y_inicial;

	// Start is called before the first frame update
	void Start()
	{
		rb_2d_jogador 				= GetComponent<Rigidbody2D>();
		box_collider_jogador 		= GetComponent<BoxCollider2D>();

		posicao_raycast = box_collider_jogador.bounds.center;
		x_inicial = box_collider_jogador.bounds.center.x;
		y_inicial = box_collider_jogador.bounds.center.y;
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

	void FixedUpdate(){
		mantendo_passo();
		morreu();
	}

	void rotacao_jogador(){
		rb_2d_jogador.gravityScale = transform.parent.GetComponent<Rigidbody2D>().gravityScale;
		RaycastHit2D raycast = Physics2D.Raycast(posicao_raycast, Vector2.up * Mathf.Sign(rb_2d_jogador.gravityScale), Mathf.Infinity, plataforma_contato);
		float delta_s = Mathf.Sqrt(2 * raycast.distance/ Mathf.Abs(rb_2d_jogador.gravityScale * (9 + .8f)));
		float velocidade_plats = plataformas_velocidade.GetComponent<Movimento_plataformas>().velocidade_plataformas;

		posicao_raycast = new Vector2(box_collider_jogador.bounds.center.x + (velocidade_plats * delta_s), box_collider_jogador.bounds.center.y - (box_collider_jogador.bounds.extents.y * Mathf.Sign(rb_2d_jogador.gravityScale)));

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

	void mantendo_passo(){
		if(box_collider_jogador.bounds.center.x < x_inicial && box_collider_jogador.bounds.center.x > x_inicial - 3){
			rb_2d_jogador.velocity = new Vector2(1, rb_2d_jogador.velocity.y);
		}
	}

	float momento_morte = 0;
	bool morte = false;
	public bool morreu(){
		if(Time.time > momento_morte + 3 && morte){
			box_collider_jogador.enabled = false;
			rb_2d_jogador.AddForce(new Vector2(100, 400 * (Mathf.Sign(rb_2d_jogador.gravityScale)) ));
			morte = false;
		}

		if(box_collider_jogador.bounds.center.x < x_inicial - 3 || box_collider_jogador.bounds.center.y < y_inicial - 3 || box_collider_jogador.bounds.center.y > 6 /*hardcode*/ ||  rb_2d_jogador.IsTouchingLayers(inimigo_contato)){
			if(rb_2d_jogador.IsTouchingLayers(inimigo_contato)){
				morte = true;
				gameObject.GetComponent<Animator>().SetBool("morreu", true);
			}
			return true;
		}else if(morte == false){
			momento_morte = Time.time;
		}
		return false;
	}
}