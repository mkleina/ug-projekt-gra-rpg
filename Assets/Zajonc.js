#pragma strict

var zajoncclip:AudioClip;
var isZajonc = true;




function OnTriggerEnter(o:Collider){
	Debug.Log("Na wejscie");

	 if(isZajonc == true){
	 	playKappa();
	 }


}

function playKappa(){
	 GetComponent(AudioSource).PlayOneShot(zajoncclip, 1.0);
	 isZajonc = false;

}