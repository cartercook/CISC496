using UnityEngine;
using Resources = TypeSafety.Resources;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections.Generic;
using UnityEngine.UI;
using Pathfinding;

[RequireComponent(typeof(Pathfinding.AIPath))]
public class Ghost : RTSEntity {
	public static readonly Color LIGHT_BLUE = new Color(0, 0.6f, 1, 0.32f);
	readonly Color32 YELLOW = new Color32(255, 242, 0, 1);

	public RTSEntity enemy;
	public GhostDestination destination;

	const float FULL_HEALTH = 10;
	readonly Color PURPLE = new Color32(144, 66, 244, 82); // purple = stunned

	float health = FULL_HEALTH;
	bool attacking = false;
	Timer damageTimer = new Timer(0.07f);

	Material material;
	GhostPath pathing;

	Animator animator;
	HealthBar[] healthBars;
	
	AudioClip spawnSFX;
	AudioClip deathSFX;
	AudioClip attackSFX;


	// --------------- PRIVATE FUNCTIONS ---------------

	void Start () {
		//play spawn sound
		spawnSFX = Resources.Audio.sfxghostspawn;
		GetComponent<AudioSource>().PlayOneShot(spawnSFX);

		//Get child components
		Transform child = transform.GetChild(0);
		material = child.GetComponent<MeshRenderer>().materials[1];
		animator = child.GetComponent<Animator>();
		
		//Get worldspace canvas healthbars
		healthBars = GetComponentsInChildren<HealthBar>();

		//Get navigation scripts
		pathing = GetComponent<GhostPath>();
		SetDestination(transform.position);

		attackSFX = Resources.Audio.sfxghostattack;
		
		// create associated GhostDestination object
		destination = GhostDestination.Instantiate(this);
	}

	void Update() {
		damageTimer.UpdateTimer();

		if (!damageTimer.IsRunning()) {
			animator.SetBool("damaged", false);
		}
	}

	void setAttacking(bool value) {
		attacking = value;
		animator.SetBool("attack", value);
	}



	// --------------- PUBLIC FUNTIONS ---------------

	// public setter for navmesh target. Do not use internally! Use AIScript.SetDestination instead
	public void SetDestination(Vector3 destination) {
		if (attacking) {
			setAttacking(false);
			GetComponent<AudioSource>().Stop();

			enemy = null;
		}
		pathing.SetDestination(destination);
	}

	public void SetTarget(RTSEntity target)
	{
        //so that we are unable to switch to attacking distant targets immediately
        setAttacking(false);
		GetComponent<AudioSource>().Stop();

		enemy = target;
		pathing.SetTarget(target.transform);
	}

	public void Attack() {
		setAttacking(true);
		
		// stop moving
		pathing.SetDestination(transform.position);
	}

	public void stun() {
		animator.SetBool("attack", false);
		GetComponent<AudioSource> ().Stop ();

        pathing.canMove = false;
		setHighlight(PURPLE);
	}

	public void unstun() {
		animator.SetBool("attack", attacking);

        pathing.canMove = true;
		setHighlight(false);
	}

	public void hurtEnemy() {
		if(enemy != null) {
			AudioSource audio = GetComponent<AudioSource>();
			audio.clip = attackSFX;
			audio.Play();

			enemy.Damage(10);
		} else {
			// stop attacking, remove enemy reference
			setAttacking(false);
			GetComponent<AudioSource>().Stop ();
			enemy = null;
			
			// Stop moving
			SetDestination(transform.position);
		}
	}



	// --------------- OVERRIDE FUNCTIONS ---------------

	public override void Damage(float damage) {
		health -= damage;

		if (health <= 0) {
			//play sound
			AudioClip deathSFX = Resources.Audio.sfxghostdying;
			GetComponent<AudioSource>().PlayOneShot(deathSFX, 0.1f);

			// begin ghost death animation
			GetComponent<GhostDeath>().enabled = true;

			// destroy this script, not the gameObject
			Destroy(this);

		} else {
			animator.SetBool("damaged", true);
			damageTimer.ResetTimer();

			foreach (HealthBar bar in healthBars) {
				bar.health = health/FULL_HEALTH;
			}
		}
	}
	
	//set coloured outline on shader
	public override void setHighlight(Color colour) {
		material.color = colour;
	}
	public override void setHighlight(bool value) {
		if (value) {
			material.color = LIGHT_BLUE;
		} else {
			material.color = Color.white;
		}
	}
}
