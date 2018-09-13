<?php
    //Establish conection to elcstone_db
		$db = new mysqli("127.0.0.1", "admin", "password", "ugames");

	//Check connection
	if ($db -> connect_error){ //if error attribute true
		die('Connection To DB Failed');
	}

	 //Get the username from the form
	 $username = ($_POST['username']);
   
	//Get that users row
	$query = "SELECT * FROM users WHERE username = '$username'";
	$result = $db->query($query);
	
	//if no rows returned the user does not exsist
	if  ($result-> num_rows == 0) { //notice how we use result -> because result is an object
		echo "User not found";
	}
	else{
		//If foud delete the row and retrn result messege
		$query = "DELETE FROM users WHERE username = '$username'";
		$result = $db->query($query);	
		echo "User deleted successfully";
	}
		
	$db ->close(); //close the connection
   
?>