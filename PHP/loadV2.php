<?php
    //Establish conection to elcstone_db
        $db = new mysqli("108.167.189.18", "elcstone_admin", "password", "elcstone_db");

	//Check connection
	if ($db -> connect_error){ //if error attribute true
		die('Connection To DB Failed');
	}
	 //Get the username from the URL
	 $username = ($_GET['username']);

	 //Get the row for that user
	$query = "SELECT * FROM users WHERE username = '$username'";
    $result = $db->query($query); 
	$prow = mysqli_fetch_array($result);
	
	//Query The History database
	$query2 = "SELECT * FROM history2";
	$result2 = $db->query($query2); 
	$hrow = mysqli_fetch_array($result2);
	
	
	//Return the json encoded spacePrefs and portalPref objects and the histroy objects
	echo ($prow['spacePrefs'] ."#". $prow['portalPrefs'] ."#". $hrow['space'] ."#". $hrow['portal'] ."#". $hrow['ninja'] ."#". $hrow['dino'] ."#". $hrow['apple']);  //The number sign seperates the objects
 

	$db ->close(); //close the connection
   
?>