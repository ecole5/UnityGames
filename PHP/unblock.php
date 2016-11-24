<?php
    //Establish conection to elcstone_db
        $db = new mysqli("108.167.189.18", "elcstone_admin", "password", "elcstone_db");

	//Check connection
	if ($db -> connect_error){ //if error attribute true
		die('Connection To DB Failed');
	}

	 //Get the data username from the form
	 $username = ($_POST['username']);
   
	//Get that users row
	$query = "SELECT * FROM users WHERE username = '$username'";
	$result = $db->query($query);
	
	//If no rows returned return user not found
	if  ($result-> num_rows == 0) {
		echo "User not found";
	}
	//Otherwise query the db and reset tries to 3 and return sucsses
	else{
		$query = "SELECT * FROM users WHERE username = '$username'";
		$result = $db->query($query); 
		$row = mysqli_fetch_array($result);
		$query = "UPDATE users SET tries = 3, password = '$username' WHERE username = '$username'";
		$db->query($query);
		echo "User unblocked successfully";
	}
		
	$db ->close(); //close the connection
   
?>