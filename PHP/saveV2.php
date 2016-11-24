<?php
    //Establish conection to elcstone_db
        $db = new mysqli("108.167.189.18", "elcstone_admin", "password", "elcstone_db");

	//Check connection
	if ($db -> connect_error){ //if error attribute true
		die('Connection To DB Failed');
	}

	 //Get the data from the form for all the json encoded objects	
	//This is where the limitation of get first becoems evident
		//you can only pass so much characters through the url , so we must use post
			//post is safer anyways
	 $username = ($_POST['username']);
     $spacePrefs = ($_POST['spacePrefs']); 
	 $portalPrefs = ($_POST['portalPrefs']); 
	 $spaceHist = ($_POST['spaceHist']); 
	 $portalHist = ($_POST['portalHist']); 
	 $ninjaHist = ($_POST['ninjaHist']); 
	 $dinoHist = ($_POST['dinoHist']); 
	 $appleHist = ($_POST['appleHist']); 

	//Save the prefrences in that users row
	$query = "UPDATE users SET spacePrefs = '$spacePrefs', portalPrefs = '$portalPrefs' WHERE username = '$username'";
    $result = $db->query($query);
	
	//Save the communal history objects in another table 
	$query = "UPDATE history2 SET space = '$spaceHist', portal = '$portalHist', ninja = '$ninjaHist', dino = '$dinoHist', apple = '$appleHist'";
	$result = $db->query($query);
	
	$db ->close(); //close the connection
   
?>