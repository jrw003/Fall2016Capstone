#pragma strict

var newScene : String;

function OnTriggerEnter2D (Col : Collider2D){
    if(Col.CompareTag("Player")){
        Application.LoadLevel(newScene);
    }

}