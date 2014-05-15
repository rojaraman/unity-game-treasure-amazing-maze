#pragma strict

var 
sound : AudioClip;

var exitted : boolean = false;


function OnMouseEntered()

{
	audio.PlayOneShot(sound);

}

function OnMouseUp()

{

if(exitted)

{
   Application.Quit();

}

else

 {
   Application.LoadLevel("HP");
 }
}
