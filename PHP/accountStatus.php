<?php
    //Establish conection to elcstone_db
        $db = new mysqli("108.167.189.18", "elcstone_admin", "password", "elcstone_db");

	//Check connection
	if ($db -> connect_error){ //if error attribute true
		die('Connection To DB Failed');
	}

	 //Get the data from the URL, the string will be identifer = value in 
	 $username = ($_POST['username']);
   
	//Query the database 
    $query = "SELECT * FROM users WHERE username = '$username'";
	$result = $db->query($query);	
	$row = mysqli_fetch_array($result); //get number of login attempts for user

	
	if ($row['tries'] == 0){ //return string blocked, if tries is 0
		echo "BLOCKED";
	}
	else if ($username == $row['password']){ //return new if the password is the same as username
		echo "NEW";
	}
	else { //normal otherwise
		echo "NORMAL";
	}

	$db ->close(); //close the connection
   
?>