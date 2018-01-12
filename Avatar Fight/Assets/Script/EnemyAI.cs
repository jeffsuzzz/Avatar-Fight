using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public float walkingSpeed;
	public float max_health;
	public float cur_health;
	public GameObject healthBar;
	public GameObject CBT;

	public GameObject target;
	public float targetDistance;
	//private Transform target;
	private NavMeshAgent navMA;
	private bool attackflag;
	private bool deathflag;     //for sending message
	private GameObject snoweffect;

	private GameObject burneffect;
	private GameObject stuneffect;
	private float burnlastingtime;
	private float burnnum;
	private int burndebuff;
	public bool stunflag;
	private bool flyingflag;
	private bool flyingupflag;

	// Use this for initialization
	void Start () {
		navMA = this.GetComponent<NavMeshAgent>();
		navMA.speed = walkingSpeed;
		navMA.stoppingDistance = 10;

		cur_health = max_health;
		InvokeRepeating("decrease_health", 1, 1);

		attackflag = true;
		deathflag = false;

		transform.GetChild (0).SendMessage("SetCamera", target.transform.GetChild (2).gameObject.GetComponent<Camera>());
		snoweffect = transform.GetChild(2).gameObject;
		snoweffect.SetActive (false);

		burneffect = transform.GetChild(3).gameObject;
		burneffect.SetActive(false);
		stuneffect= transform.GetChild(4).gameObject;
		stuneffect.SetActive(false);

		burnlastingtime = 0f;
		burndebuff = 0;
		burnnum = 4.5f;
		stunflag = false;

		flyingflag = false;
		flyingupflag = true;
	}

	// Update is called once per frame
	void Update () {
		

		Vector3 targetPos = target.transform.position;
		targetPos.y = 0;
		targetDistance = Vector3.Distance(targetPos, transform.position);

		if (cur_health != 0)
		{
			navMA.SetDestination(targetPos);

			burningdamage();
			tornado();
			if(stunflag){
				StartCoroutine(Stun());
			}
			else{
				if (targetDistance <= 12) {
					attack();
				}
			}
		}
		else navMA.Stop();

		if(target.GetComponent<main_script>().health <= 0)
		{
			Destroy(gameObject);
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.name == ("FireboltCollider")) {
			set_healthBar(50);
			burnlastingtime = 5.5f;
			burnnum = 4.5f;
			burndebuff++;
			if (burndebuff > 3) burndebuff = 3;
		}
		else if (other.name == ("Flamethrower(Clone)")) {
			set_healthBar(65);	
			burnlastingtime = 5.5f;
			burnnum = 4.5f;
			burndebuff++;
			if (burndebuff > 3) burndebuff = 3;
		}
		else if (other.name == ("Meteor(Clone)")) {
			set_healthBar(50);
			burnlastingtime = 5.5f;
			burnnum = 4.5f;
			burndebuff++;
			if (burndebuff > 3) burndebuff = 3;
		}
		else if (other.name == ("AirBladeCylinder")) set_healthBar(60);	//AirBlade
		else if (other.name == ("Cyclone(Clone)")) {
			set_healthBar(50);	//Tornado
			flyingflag = true;
			StartCoroutine(DelayFly());
			StartCoroutine(FlayFall());
		}
		else if (other.name == ("Lightning Strike(Clone)")) {
			set_healthBar(60);
			stunflag = true;
		}
		else if (other.name == ("WaveMesh")) {
			set_healthBar(50);
			SlowSpeed ();
		}
		else if (other.name == ("WaterPewpew(Clone)")) {
			set_healthBar(65);	//WaterSplash
			SlowSpeed ();
		}
		else if (other.name == ("rock_a(Clone)")) set_healthBar(100);	//EarthSundering
		else if(other.name == ("EarthSpurt(Clone)")) set_healthBar(50);

	}
	void OnCollisionEnter(Collision collision){      
		if(collision.collider.name == ("Rock16_5_A(Clone)")){
			attackflag = false;
			delayHit ();
		}
	}

	public void setTarget(GameObject targetobj) {
		target = targetobj;
	}

	void attack() {
		if (attackflag) {
			target.SendMessage("beenHit");
			attackflag = false;
			StartCoroutine(delayHit());
		}
	}
	IEnumerator delayHit() {       
		yield return new WaitForSeconds(2);
		attackflag = true;
	}

	void decrease_health()
	{
		//set_healthBar(50);
	}

	void set_healthBar(float health_loss)
	{
		if (cur_health != 0)
		{
			initialCBT(health_loss.ToString());
		}

		cur_health -= health_loss;
		if (cur_health < 0)
		{
			cur_health = 0;
		}
		healthBar.transform.localScale = new Vector3(cur_health / max_health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

		if (cur_health == 0 && !deathflag)
		{
			deathflag = true;
			DestroyObject(gameObject, 3);
			GameObject childHB = transform.GetChild(0).gameObject;
			childHB.SetActive(false);
			target.SendMessage("monsterKill");
		}
	}

	void initialCBT(string text_damage)
	{
		GameObject temp_CBT = Instantiate(CBT)as GameObject;
		temp_CBT.transform.SetParent(transform.FindChild("EnemyCanvas"));
		temp_CBT.GetComponent<RectTransform>().transform.localPosition = CBT.transform.localPosition;
		temp_CBT.GetComponent<RectTransform>().transform.localRotation = CBT.transform.localRotation;
		temp_CBT.GetComponent<RectTransform>().transform.localScale = CBT.transform.localScale;

		temp_CBT.GetComponent<Text>().text = "-" + text_damage;
		temp_CBT.GetComponent<Animator>().SetTrigger("Hit");

		Destroy(temp_CBT, 1.0F);
	}

	public void SpeedUp(int i)
	{
		walkingSpeed += 1*(i-1);
		navMA.speed = walkingSpeed;
	}

	public void IncreaseHP(int i)
	{
		max_health += 20 * (i-1);
	}

	public void SlowSpeed(){
		walkingSpeed -= 5;
		navMA.speed = walkingSpeed;
		snoweffect.SetActive (true);
		StartCoroutine(delayRestoreSpeed());
	}
	IEnumerator delayRestoreSpeed() {       
		yield return new WaitForSeconds(3);
		walkingSpeed +=3;
		navMA.speed = walkingSpeed;
		snoweffect.SetActive (false);
	}
		
	IEnumerator Stun() {  
		navMA.speed = 0;
		stuneffect.SetActive (true);
		yield return new WaitForSeconds(3);
		navMA.speed = walkingSpeed;
		stunflag = false;
		stuneffect.SetActive (false);
	}


	void burningdamage()
	{
		if (burnlastingtime > 0)
		{           
			burneffect.SetActive(true);
			//burnnum decrease from 4.5-3-1.5
			if (burnlastingtime <= burnnum)
			{
				set_healthBar(5 * burndebuff);
				burnnum -= 1.5f;
			}
			burnlastingtime -= Time.deltaTime * 1f;
		}
		else
		{
			burneffect.SetActive(false);
			burndebuff = 0;
		}

	}

	IEnumerator DelayFly()
	{
		yield return new WaitForSeconds(6);
		flyingflag = false;
		navMA.enabled = true;
		flyingupflag = true;
	}
	IEnumerator FlayFall()
	{
		yield return new WaitForSeconds(3);
		flyingupflag = false;
	}

	void tornado()
	{
		if (flyingflag)
		{
			navMA.enabled = false;
			transform.Rotate(0, 300*Time.deltaTime, 0, Space.World);
			Vector3 flyvector = transform.position - target.transform.position;
			flyvector.y = 0;
			flyvector.Normalize();

			if (flyingupflag)
			{
				flyvector += new Vector3(0, 1, 0);
			}
			else
			{
				flyvector += new Vector3(0, -0.8f, 0);
			}
			transform.Translate(flyvector * Time.deltaTime * 10, Space.World);
		}
	}

}
