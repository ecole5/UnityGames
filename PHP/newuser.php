<?php
    //Establish conection to elcstone_db
		$db = new mysqli("127.0.0.1", "admin", "password", "ugames");

	//Check connection
	if ($db -> connect_error){ //if error attribute true
		die('Connection To DB Failed');
	}

	 //Get the data from the form 
	 $username = ($_POST['username']);
     $spacePrefs = ($_POST['spacePrefs']); 
	 $portalPrefs = ($_POST['portalPrefs']);  
	
	//Query databse for the desired user name 
    $query = "SELECT * FROM users WHERE username = '$username'"; 
    $result = $db->query($query);

	//If zero rows returned user does not exist so make new row the the name and prefrences objects

	if  (mysqli_num_rows($result) == 0) {
		//Tries is set to default to 3 automaticlly
		$query = "INSERT INTO users (username, password, spacePrefs, portalPrefs) VALUES ('$username', '$username', '$spacePrefs', '$portalPrefs')";
		$result = $db->query($query);	
		echo "User created on network, password is username.";
	}
	else { //username already taken
		echo "Username already taken, please try again.";
	
	}

	$db ->close(); //close the connection
   
?>